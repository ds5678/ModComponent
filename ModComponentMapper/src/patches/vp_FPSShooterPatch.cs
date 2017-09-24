using Harmony;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(vp_FPSShooter), "Refresh")]
    class vp_FPSShooterRefreshPatch
    {
        public static void Postfix(vp_FPSShooter __instance)
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            ModAnimationStateMachine animation = ModUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
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
}
