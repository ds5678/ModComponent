using Harmony;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(vp_MuzzleFlash), "Shoot")]//positive caller count
    class vp_MuzzleFlashShootPatch
    {
        public static void Prefix(vp_MuzzleFlash __instance, ref Transform tr)
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
            if (animation != null)
            {
                tr = GameManager.GetVpFPSCamera().CurrentShooter.MuzzleFlash.transform.parent;
            }
        }
    }
}
