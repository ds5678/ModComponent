using Harmony;
using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(EquipItemPopup), "AllowedToHideAmmoPopup")]
    internal class EquipItemPopup_AllowedToHideAmmoPopup
    {
        internal static void Postfix(ref bool __result)
        {
            if (__result)
            {
                return;
            }

            __result = ModUtils.GetModComponent(GameManager.GetPlayerManagerComponent().m_ItemInHands) != null;
        }
    }
}