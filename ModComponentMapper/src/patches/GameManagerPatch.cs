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
                Logger.Log("Mapping failed: " + e);
            }

            //
            //Need to be called after GameManager is initialized
            //

            if (!ModExisting.IsInitialized())//ModExisting only needs to be initialized at the start of the game
            {
                ModExisting.Initialize();
            }
            AlternativeToolManager.ProcessList();
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