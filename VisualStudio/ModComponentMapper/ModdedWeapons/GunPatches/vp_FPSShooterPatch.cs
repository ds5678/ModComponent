#if DEBUG
using Harmony;
using UnityEngine;
#endif

namespace ModComponentMapper
{
#if DEBUG
	[HarmonyPatch(typeof(vp_FPSShooter), "Refresh")]//zero caller count
	class vp_FPSShooterRefreshPatch
	{
		public static void Postfix(vp_FPSShooter __instance)
		{
			PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
			ModAnimationStateMachine animation = ModComponentUtils.ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
			if (animation == null)
			{
				return;
			}

			if (__instance.MuzzleFlash != null)
			{
				__instance.MuzzleFlash.transform.localPosition = Vector3.zero;
			}
		}
	}
#endif
}
