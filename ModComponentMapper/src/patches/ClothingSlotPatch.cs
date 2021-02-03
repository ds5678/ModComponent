using Harmony;
using ModComponentAPI;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(ClothingSlot), "CheckForChangeLayer")]//Exists
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