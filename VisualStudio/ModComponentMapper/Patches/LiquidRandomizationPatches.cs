using Harmony;

namespace ModComponentMapper.Patches
{
	internal class LiquidItemPatch
	{
		//make water containers able to have randomized initial quantities
		[HarmonyPatch(typeof(LiquidItem), "Awake")]
		internal class LiquidItem_Awake
		{
			private static void Postfix(LiquidItem __instance)
			{
				if (!ModComponentMain.Settings.options.randomPlasticWaterBottles && (__instance.name == "GEAR_Water500ml" || __instance.name == "GEAR_Water1000ml")) return;
				if (__instance.m_RandomizeQuantity && __instance.m_LiquidType == GearLiquidTypeEnum.Water)
				{
					__instance.m_LiquidLiters = ModComponentUtils.RandomUtils.Range(__instance.m_LiquidCapacityLiters / 8f, __instance.m_LiquidCapacityLiters);
				}
			}
		}
	}
}
