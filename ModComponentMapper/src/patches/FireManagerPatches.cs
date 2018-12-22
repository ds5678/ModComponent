using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Harmony;
using ModComponentAPI;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(FireManager), "PlayerStartFire")]
    class FireManager_PlayerStartFire
    {
        internal static void Postfix(FireStarterItem starter, bool __result)
        {
            if (!__result)
            {
                return;
            }

            ModFireStarterComponent modFireStarterComponent = ModUtils.GetComponent<ModFireStarterComponent>(starter);
            if (modFireStarterComponent == null || !modFireStarterComponent.RuinedAfterUse)
            {
                return;
            }

            GearItem gearItem = starter.GetComponent<GearItem>();
            if (gearItem != null)
            {
                gearItem.Degrade(gearItem.m_MaxHP);
            }
        }
    }
}
