using Harmony;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(GameManager), "Awake")]//Exists
    internal class GameManager_Awake
    {
        public static void Postfix()
        {
            try
            {
                Mapper.MapBlueprints();
                //Mapper.MapSkills();
            }
            catch (System.Exception e)
            {
                Implementation.Log("Mapping failed: " + e);
            }
        }
    }

    [HarmonyPatch(typeof(GameManager), "SetAudioModeForLoadedScene")]//Exists
    internal class GameManager_SetAudioModeForLoadedScene
    {
        internal static void Prefix()
        {
            Implementation.SceneReady();
        }
    }
}