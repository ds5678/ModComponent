using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class BurnableMapper
	{
		internal static void Configure(ModComponent modComponent) => Configure(modComponent.gameObject);
		public static void Configure(GameObject prefab)
		{
			ModBurnableComponent modBurnableComponent = ModComponentUtils.ComponentUtils.GetComponent<ModBurnableComponent>(prefab);
			if (modBurnableComponent is null) return;

			FuelSourceItem fuelSourceItem = ModComponentUtils.ComponentUtils.GetOrCreateComponent<FuelSourceItem>(modBurnableComponent);
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
