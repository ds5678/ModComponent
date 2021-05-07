using Harmony;

namespace Preloader
{
	[HarmonyPriority(Priority.Last)]
	[HarmonyPatch(typeof(GameManager), "Awake")]
	internal class GameManager_Awake
	{
		private static bool Prefix()
		{
			if (PreloadingManager.preloadingObjects) return false;
			else return true;
		}
		private static void Postfix()
		{
			PreloadingManager.MaybePreloadScenes();
		}
	}

	// From SkipIntroRedux. The main menu doesn't load without this.
	[HarmonyPatch(typeof(Panel_MainMenu), "Enable")]
	internal class SkipIntroReduxSkipIntro
	{
		private static void Prefix(Panel_MainMenu __instance)
		{
			MoviePlayer.m_HasIntroPlayedForMainMenu = true;
		}
	}
}
