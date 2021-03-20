using Harmony;
using ModComponentAPI;

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]//not inlined
    internal class PlayerManager_PutOnClothingItem
    {
        private static void Prefix(PlayerManager __instance,GearItem gi,ClothingLayer layerToPutOn)
        {
            //MelonLoader.MelonLogger.Log("Clothing Layer {0}", layerToPutOn);
            if (gi?.m_ClothingItem == null || layerToPutOn == ClothingLayer.NumLayers) return;
            ClothingRegion region = gi.m_ClothingItem.m_Region;
            GearItem itemInSlot = __instance.GetClothingInSlot(region, layerToPutOn);
            if (itemInSlot) __instance.TakeOffClothingItem(itemInSlot);
        }
        private static void Postfix(GearItem gi)
        {
            ModClothingComponent modClothingComponent = ModUtils.GetComponent<ModClothingComponent>(gi);
            modClothingComponent?.OnPutOn?.Invoke();
            Implementation.UpdateWolfIntimidationBuff();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]//Not inlined
    internal class PlayerManager_TakeOffClothingItem
    {
        internal static void Postfix(GearItem gi)
        {
            ModClothingComponent modClothingComponent = ModUtils.GetComponent<ModClothingComponent>(gi);
            modClothingComponent?.OnTakeOff?.Invoke();
            Implementation.UpdateWolfIntimidationBuff();
        }
    }

    [HarmonyPatch(typeof(ClothingSlot), "CheckForChangeLayer")]
    internal class ClothingSlot_CheckForChangeLayer
    {
        private static bool Prefix(ClothingSlot __instance)
        {
            ModClothingComponent clothingComponent = ModUtils.GetComponent<ModClothingComponent>(__instance.m_GearItem);
            if (clothingComponent == null)
            {
                if (__instance.m_GearItem != null)
                {
                    int defaultDrawLayer = DefaultDrawLayers.GetDefaultDrawLayer(__instance.m_ClothingRegion, __instance.m_ClothingLayer);
                    __instance.UpdatePaperDollTextureLayer(defaultDrawLayer);
                }
                return true;
            }

            int actualDrawLayer = clothingComponent.DrawLayer;
            __instance.UpdatePaperDollTextureLayer(actualDrawLayer);
            //Logger.Log("Set the draw layer for '{0}' to {1}", __instance.m_GearItem.name, actualDrawLayer);
            return false;
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UpdateBuffDurations")]
    internal static class PlayerManager_UpdateBuffDurations
    {
        internal static void Postfix(PlayerManager __instance)
        {
            __instance.RemoveWolfIntimidationBuff();
        }
    }
}
