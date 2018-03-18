using Harmony;
using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(ClothingSlot), "CheckForChangeLayer")]
    internal class ClothingSlot_CheckForChangeLayer
    {
        public static bool Prefix(ClothingSlot __instance)
        {
            ModClothingComponent clothingComponent = ModUtils.GetComponent<ModClothingComponent>(__instance.m_GearItem);
            if (clothingComponent == null)
            {
                return true;
            }

            int actualDrawLayer = Mathf.Max(40, clothingComponent.DrawLayer);
            ModUtils.ExecuteMethod(__instance, "UpdatePaperDollTextureLayer", actualDrawLayer);

            return false;
        }
    }
}