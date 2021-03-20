using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
    internal class LiquidMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            ModLiquidComponent modLiquidItemComponent = modComponent.TryCast<ModLiquidComponent>();
            if (modLiquidItemComponent == null)
            {
                return;
            }

            LiquidItem liquidItem = ModUtils.GetOrCreateComponent<LiquidItem>(modComponent);
            liquidItem.m_LiquidCapacityLiters = modLiquidItemComponent.LiquidCapacityLiters;
            liquidItem.m_LiquidType = ModUtils.TranslateEnumValue<GearLiquidTypeEnum, LiquidType>(modLiquidItemComponent.LiquidType);
            liquidItem.m_RandomizeQuantity = modLiquidItemComponent.RandomizeQuantity;
            liquidItem.m_LiquidLiters = modLiquidItemComponent.LiquidLiters;
            liquidItem.m_DrinkingAudio = "Play_DrinkWater";
            liquidItem.m_TimeToDrinkSeconds = 4;
            liquidItem.m_LiquidQuality = LiquidQuality.Potable;
        }
    }
}
