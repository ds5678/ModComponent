using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
    internal class BurnableMapper
    {
        public static void Configure(ModComponent modComponent)
        {
            ModBurnableComponent modBurnableComponent = ModUtils.GetComponent<ModBurnableComponent>(modComponent);
            if (modBurnableComponent == null)
            {
                return;
            }

            FuelSourceItem fuelSourceItem = ModUtils.GetOrCreateComponent<FuelSourceItem>(modComponent);
            fuelSourceItem.m_BurnDurationHours = modBurnableComponent.BurningMinutes / 60f;
            fuelSourceItem.m_FireAgeMinutesBeforeAdding = modBurnableComponent.BurningMinutesBeforeAllowedToAdd;
            fuelSourceItem.m_FireStartSkillModifier = modBurnableComponent.SuccessModifier;
            fuelSourceItem.m_HeatIncrease = modBurnableComponent.TempIncrease;
            fuelSourceItem.m_HeatInnerRadius = 2.5f;
            fuelSourceItem.m_HeatOuterRadius = 6f;
            fuelSourceItem.m_FireStartDurationModifier = 0;
            fuelSourceItem.m_IsWet = false;
            fuelSourceItem.m_IsTinder = false;
            fuelSourceItem.m_IsBurntInFireTracked = false;
        }
    }
}
