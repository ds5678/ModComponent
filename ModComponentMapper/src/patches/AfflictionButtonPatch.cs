using Harmony;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(AfflictionButton), "SetCauseAndEffect")]//Exists
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
