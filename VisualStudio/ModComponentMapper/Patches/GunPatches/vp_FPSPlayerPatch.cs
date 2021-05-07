using Harmony;
using UnityEngine;

namespace ModComponentMapper.patches
{
	[HarmonyPatch(typeof(vp_FPSPlayer), "Reload")]//positive caller count
	class vp_FPSPlayerReloadPatch
	{
		public static bool Prefix(vp_FPSPlayer __instance)
		{
			if (__instance.Controller.StateEnabled("Reload") || Time.time < __instance.FPSCamera.CurrentShooter.NextAllowedReloadTime)
			{
				GameAudioManager.PlayGUIError();
				return false;
			}

			return true;
		}
	}
}
