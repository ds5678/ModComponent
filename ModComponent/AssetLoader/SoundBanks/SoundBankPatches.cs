using HarmonyLib;

namespace ModComponent.AssetLoader
{
	[HarmonyPatch(typeof(GameAudioManager), "Start")]
	internal static class GameAudioManager_LoadSoundBanksPath
	{
		internal static void Postfix()
		{
			ModSoundBankManager.RegisterPendingSoundBanks();
		}
	}
}