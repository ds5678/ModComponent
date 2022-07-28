extern alias Hinterland;
using HarmonyLib;
using Hinterland;
using ModComponent.API.Behaviours;

namespace ModComponent.Patches;

[HarmonyPatch(typeof(FireManager), "PlayerStartFire")]//Exists
internal static class FireManager_PlayerStartFire
{
	internal static void Postfix(FireStarterItem starter, bool __result)
	{
		if (!__result) return;

		ModFireStarterBehaviour modFireStarterComponent = ModComponent.Utils.ComponentUtils.GetComponentSafe<ModFireStarterBehaviour>(starter);
		if (modFireStarterComponent == null || !modFireStarterComponent.RuinedAfterUse) return;

		GearItem gearItem = starter.GetComponent<GearItem>();
		if (gearItem != null) gearItem.BreakOnUse();
	}
}