using Harmony;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(AfflictionButton), "SetCauseAndEffect")]
    class AfflictionButtonSetCauseAndEffectPatch
    {
        public static void Prefix(ref string causeStr, AfflictionType affType)
        {
            string trackedCause = BuffCauseTracker.getCause(affType);
            if (!string.IsNullOrEmpty(trackedCause))
            {
                causeStr = trackedCause;
            }
        }
    }
}
