extern alias Hinterland;
using HarmonyLib;
using Hinterland;
using ModComponent.AssetLoader;

namespace ModComponent.Patches;

[HarmonyPatch(typeof(GameAudioManager), "Start")]
internal static class GameAudioManager_LoadSoundBanksPath
{
	internal static void Postfix()
	{
		ModSoundBankManager.RegisterPendingSoundBanks();
	}
}