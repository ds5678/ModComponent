using Harmony;
using ModComponentAPI;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcessAltFire")]//Exists
	internal class PlayerManager_InteractiveObjectsProcessAltFire
	{
		internal static bool Prefix(PlayerManager __instance)
		{
			AlternativeAction alternativeAction = ModComponentUtils.ComponentUtils.GetComponent<AlternativeAction>(__instance.m_InteractiveObjectUnderCrosshair);
			if (alternativeAction == null)
			{
				return true;
			}

			alternativeAction.Execute();
			return false;
		}
	}
}
