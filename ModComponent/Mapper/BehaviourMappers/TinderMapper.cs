extern alias Hinterland;
using Hinterland;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper.BehaviourMappers
{
	internal static class TinderMapper
	{
		internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
		public static void Configure(GameObject prefab)
		{
			ModTinderBehaviour modBurnableComponent = ComponentUtils.GetComponentSafe<ModTinderBehaviour>(prefab);
			if (modBurnableComponent == null) return;

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