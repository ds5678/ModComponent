using Harmony;
using System.Collections.Generic;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    internal class BuffCauseTracker
    {
        private static Dictionary<AfflictionType, string> causes = new Dictionary<AfflictionType, string>();

        public static void setCause(AfflictionType buff, string cause)
        {
            if (causes.ContainsKey(buff))
            {
                causes[buff] = cause;
            }
            else
            {
                causes.Add(buff, cause);
            }
        }

        public static string getCause(AfflictionType buff)
        {
            string result;
            causes.TryGetValue(buff, out result);
            return result;
        }
    }

    [HarmonyPatch(typeof(FatigueBuff), "Apply")]//Exists
    internal class FagtigueBuffApplyPatch
    {
        public static void Postfix(FatigueBuff __instance)
        {
            GearItem gearItem = ModUtils.GetComponent<GearItem>(__instance);
            if (gearItem != null)
            {
                BuffCauseTracker.setCause(AfflictionType.ReducedFatigue, gearItem.m_LocalizedDisplayName.Text());
            }
        }
    }
}
