using Harmony;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared
//might have been inlined

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(vp_FPSShooter), "Refresh")]//inlined?
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
