using ModComponentAPI;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class BurnableMapper
	{
		internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
		public static void Configure(GameObject prefab)
		{
			ModBurnableComponent modBurnableComponent = ComponentUtils.GetComponent<ModBurnableComponent>(prefab);
			if (modBurnableComponent == null) return;

			FuelSourceItem fuelSourceItem = ComponentUtils.GetOrCreateComponent<FuelSourceItem>(modBurnableComponent);
			fuelSourceItem.m_BurnDurationHours = modBurnableComponent.BurningMinutes / 60f;
			fuelSourceItem.m_FireAgeMinutesBeforeAdding = modBurnableComponent.BurningMinutesBeforeAllowedToAdd;
			fuelSourceItem.m_FireStartSkillModifier = modBurnableComponent.SuccessModifier;
			fuelSourceItem.m_HeatIncrease = modBurnableComponent.TempIncrease;
			fuelSourceItem.m_HeatInnerRadius = 2.5f;
			fuelSourceItem.m_HeatOuterRadius = 6f;
			fuelSourceItem.m_FireStartDurationModifier = modBurnableComponent.DurationOffset;
			fuelSourceItem.m_IsWet = false;
			fuelSourceItem.m_IsTinder = false;
			fuelSourceItem.m_IsBurntInFireTracked = false;
		}
	}
}
