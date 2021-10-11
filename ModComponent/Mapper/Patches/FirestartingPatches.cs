using HarmonyLib;
using ModComponentAPI;

namespace ModComponent.Mapper.Patches
{
	[HarmonyPatch(typeof(FireManager), "PlayerStartFire")]//Exists
	internal static class FireManager_PlayerStartFire
	{
		internal static void Postfix(FireStarterItem starter, bool __result)
		{
			if (!__result) return;

			ModFireStarterBehaviour modFireStarterComponent = ModComponent.Utils.ComponentUtils.GetComponent<ModFireStarterBehaviour>(starter);
			if (modFireStarterComponent == null || !modFireStarterComponent.RuinedAfterUse) return;

			GearItem gearItem = starter.GetComponent<GearItem>();
			if (gearItem != null) gearItem.BreakOnUse();
		}
	}
}