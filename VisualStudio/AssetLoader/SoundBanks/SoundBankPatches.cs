using Harmony;

namespace AssetLoader
{
	[HarmonyPatch(typeof(GameAudioManager), "Start")]
	internal class GameAudioManager_LoadSoundBanksPath
	{
		internal static void Postfix()
		{
			ModSoundBankManager.RegisterPendingSoundBanks();
		}
	}
}