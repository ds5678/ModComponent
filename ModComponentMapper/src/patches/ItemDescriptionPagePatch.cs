using Harmony;
using ModComponentAPI;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(ItemDescriptionPage), "GetEquipButtonLocalizationId")]//Exists
    class ItemDescriptionPageGetEquipButtonLocalizationIdPatch
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

    [HarmonyPatch(typeof(ItemDescriptionPage), "CanExamine")]//Exists
    class ItemDescriptionPageCanExaminePatch
    {
        public static void Postfix(GearItem gi, ref bool __result)
        {
            // guns can always be examined
            __result |= ModUtils.GetComponent<GunItem>(gi) != null;
        }
    }
}
