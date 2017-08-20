using Harmony;

using ModComponentAPI;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(ItemDescriptionPage), "GetEquipButtonLocalizationId")]
    class ItemDescriptionPagePatch
    {
        public static void Postfix(GearItem gi, ref string __result)
        {
            if (__result != string.Empty)
            {
                return;
            }

            ModComponent modComponent = ModUtils.GetModComponent(gi);
            if (modComponent != null)
            {
                __result = modComponent.InventoryActionLocalizationId;
            }
        }
    }
}
