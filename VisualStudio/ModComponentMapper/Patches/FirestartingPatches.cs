using Harmony;
using ModComponentAPI;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(FireManager), "PlayerStartFire")]//Exists
    internal class FireManager_PlayerStartFire
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
                gearItem.BreakOnUse();
            }
        }
    }
}