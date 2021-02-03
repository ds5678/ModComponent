using Harmony;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(vp_MuzzleFlash), "Shoot")]//Exists
    class vp_MuzzleFlashShootPatch
    {
        public static void Prefix(vp_MuzzleFlash __instance, ref Transform tr)
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            ModAnimationStateMachine animation = ModUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
            if (animation != null)
            {
                tr = GameManager.GetVpFPSCamera().CurrentShooter.MuzzleFlash.transform.parent;
            }
        }
    }
}
