using HarmonyLib;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(GameManager), "Awake")]
	internal static class GameManager_Awake
	{
		private static void Postfix()
		{
			Logger.Log("The Long Dark version: '{0}'", GameManager.GetVersionString());

			try { BlueprintMapper.MapBlueprints(); }
			catch (System.Exception e)
			{
				Logger.LogError("Blueprint Mapping failed: " + e);
			}


			//
			//Need to be called after GameManager is initialized
			//

			AlternativeToolManager.ProcessList();
		}
	}

	[HarmonyPatch(typeof(GameManager), "SetAudioModeForLoadedScene")]//Exists
	internal static class GameManager_SetAudioModeForLoadedScene
	{
		private static void Prefix()
		{
			MapperCore.SceneReady();
		}
	}
}