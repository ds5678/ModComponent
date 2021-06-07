using ModComponentAPI;
using ModComponentUtils;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class TinderMapper
	{
		internal static void Configure(ModComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
		public static void Configure(GameObject prefab)
		{
			ModTinderComponent modBurnableComponent = ComponentUtils.GetComponent<ModTinderComponent>(prefab);
			if (modBurnableComponent is null) return;

			FuelSourceItem fuelSourceItem = ComponentUtils.GetOrCreateComponent<FuelSourceItem>(modBurnableComponent);
			fuelSourceItem.m_BurnDurationHours = 0.02f;
			fuelSourceItem.m_FireAgeMinutesBeforeAdding = 0;
			fuelSourceItem.m_FireStartSkillModifier = modBurnableComponent.SuccessModifier;
			fuelSourceItem.m_HeatIncrease = 5;
			fuelSourceItem.m_HeatInnerRadius = 2.5f;
			fuelSourceItem.m_HeatOuterRadius = 6f;
			fuelSourceItem.m_FireStartDurationModifier = modBurnableComponent.DurationOffset;
			fuelSourceItem.m_IsWet = false;
			fuelSourceItem.m_IsTinder = true;
			fuelSourceItem.m_IsBurntInFireTracked = false;
		}
	}
}