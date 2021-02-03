using Harmony;
using ModComponentAPI;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(EquipItemPopup), "AllowedToHideAmmoPopup")]//Exists
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