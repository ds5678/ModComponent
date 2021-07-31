#if DEBUG
using Harmony;
using UnityEngine;
#endif

namespace ModComponentMapper
{
#if DEBUG
	[HarmonyPatch(typeof(vp_MuzzleFlash), "Shoot")]//positive caller count
	internal static class vp_MuzzleFlashShootPatch
	{
		public static void Prefix(vp_MuzzleFlash __instance, ref Transform tr)
		{
			PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
			ModAnimationStateMachine animation = ModComponentUtils.ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
			if (animation != null)
			{
				tr = GameManager.GetVpFPSCamera().CurrentShooter.MuzzleFlash.transform.parent;
			}
		}
	}
#endif
}
