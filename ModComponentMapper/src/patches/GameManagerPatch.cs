using Harmony;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal class GameManager_Awake
    {
        public static void Postfix()
        {
            try
            {
                Mapper.MapBlueprints();
                Mapper.MapSkills();
            }
            catch (System.Exception e)
            {
                LogUtils.Log("Mapping failed: " + e);
            }
        }
    }

    [HarmonyPatch(typeof(GameManager), "SetAudioModeForLoadedScene")]
    internal class GameManager_SetAudioModeForLoadedScene
    {
        internal static void Prefix()
        {
            Implementation.SceneReady();
        }
    }
}