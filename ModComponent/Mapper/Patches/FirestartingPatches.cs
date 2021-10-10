using HarmonyLib;
using ModComponentAPI;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(FireManager), "PlayerStartFire")]//Exists
	internal static class FireManager_PlayerStartFire
	{
		internal static void Postfix(FireStarterItem starter, bool __result)
		{
			if (!__result) return;

			ModFireStarterComponent modFireStarterComponent = ModComponent.Utils.ComponentUtils.GetComponent<ModFireStarterComponent>(starter);
			if (modFireStarterComponent == null || !modFireStarterComponent.RuinedAfterUse) return;

			GearItem gearItem = starter.GetComponent<GearItem>();
			if (gearItem != null) gearItem.BreakOnUse();
		}
	}
}