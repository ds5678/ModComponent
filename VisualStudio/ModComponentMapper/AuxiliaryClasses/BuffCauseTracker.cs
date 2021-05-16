using Harmony;
using System.Collections.Generic;

namespace ModComponentMapper
{
	internal static class BuffCauseTracker
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
			GearItem gearItem = ModComponentUtils.ComponentUtils.GetComponent<GearItem>(__instance);
			if (gearItem is null) return;
			else BuffCauseTracker.setCause(AfflictionType.ReducedFatigue, gearItem.m_LocalizedDisplayName.Text());
		}
	}

	[HarmonyPatch(typeof(AfflictionButton), "SetCauseAndEffect")]//positive caller count
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
