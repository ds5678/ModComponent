using Il2Cpp;
using HarmonyLib;

using ModComponent.AssetLoader;

namespace ModComponent.Patches;

[HarmonyPatch(typeof(GameAudioManager), nameof(GameAudioManager.Start))]
internal static class GameAudioManager_LoadSoundBanksPath
{
	internal static void Postfix()
	{
		ModSoundBankManager.RegisterPendingSoundBanks();
	}
}