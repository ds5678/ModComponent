using HarmonyLib;
using Il2Cpp;

namespace ModComponent.Patches;

internal class LiquidItemPatch
{
	//make water containers able to have randomized initial quantities
	[HarmonyPatch(typeof(LiquidItem), nameof(LiquidItem.Awake))]
	internal static class LiquidItem_Awake
	{
		private static void Postfix(LiquidItem __instance)
		{
			if (!Settings.instance.randomPlasticWaterBottles && (__instance.name == "GEAR_Water500ml" || __instance.name == "GEAR_Water1000ml"))
			{
				return;
			}

			if (__instance.m_RandomizeQuantity && __instance.m_LiquidType == GearLiquidTypeEnum.Water)
			{
				__instance.m_LiquidLiters = ModComponent.Utils.RandomUtils.Range(__instance.m_LiquidCapacityLiters / 8f, __instance.m_LiquidCapacityLiters);
			}
		}
	}
}
