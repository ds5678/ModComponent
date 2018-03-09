using Harmony;
using ModComponentAPI;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(PlayAkSound), "OnEnable")]
    internal class PlayAkSoundPatch
    {
        private static void Postfix(PlayAkSound __instance)
        {
            GameAudioManager.Play3DSound(__instance.soundName, __instance.gameObject);
        }
    }
}