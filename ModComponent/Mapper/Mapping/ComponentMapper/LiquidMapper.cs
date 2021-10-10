using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
	internal static class LiquidMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModLiquidComponent modLiquidComponent = modComponent.TryCast<ModLiquidComponent>();
			if (modLiquidComponent == null) return;

			LiquidItem liquidItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<LiquidItem>(modComponent);
			liquidItem.m_LiquidCapacityLiters = modLiquidComponent.LiquidCapacityLiters;
			liquidItem.m_LiquidType = ModComponent.Utils.EnumUtils.TranslateEnumValue<GearLiquidTypeEnum, ModLiquidComponent.LiquidKind>(modLiquidComponent.LiquidType);
			liquidItem.m_RandomizeQuantity = modLiquidComponent.RandomizeQuantity;
			liquidItem.m_LiquidLiters = modLiquidComponent.LiquidLiters;
			liquidItem.m_DrinkingAudio = "Play_DrinkWater";
			liquidItem.m_TimeToDrinkSeconds = 4;
			liquidItem.m_LiquidQuality = LiquidQuality.Potable;
		}
	}
}
