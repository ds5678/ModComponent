using Harmony;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(vp_FPSPlayer), "Reload")]//Exists
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
