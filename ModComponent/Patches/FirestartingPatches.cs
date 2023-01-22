using Il2Cpp;
using HarmonyLib;
using ModComponent.API.Behaviours;

namespace ModComponent.Patches;
//[HarmonyLib.HarmonyPatch(typeof(GameAudioManager), "PlayMusic", new Type[] { typeof(string), typeof(GameObject) })]

//Fire fire, FireStarterItem starter, FuelSourceItem tinder, FuelSourceItem fuel, FireStarterItem accelerant, Action<bool> onDoneStartingFire

// Yeah I don't know and I don't care for now
// btw Zombie was here

/*
[HarmonyPatch(typeof(FireManager), "PlayerStartFire")]
public class FireManager_PlayerStartFire
{
	public static void Postfix(FireStarterItem starter, bool __result)
	{
		if (!__result)
		{
			return;
		}

		ModFireStarterBehaviour modFireStarterComponent = ModComponent.Utils.ComponentUtils.GetComponentSafe<ModFireStarterBehaviour>(starter);
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
*/