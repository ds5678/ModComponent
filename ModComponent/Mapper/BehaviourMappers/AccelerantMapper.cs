extern alias Hinterland;
using Hinterland;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper.BehaviourMappers;

internal static class AccelerantMapper
{
	internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
	public static void Configure(GameObject prefab)
	{
		ModAccelerantBehaviour modAccelerantComponent = ComponentUtils.GetComponentSafe<ModAccelerantBehaviour>(prefab);
		if (modAccelerantComponent == null) return;

		FireStarterItem fireStarterItem = ComponentUtils.GetOrCreateComponent<FireStarterItem>(modAccelerantComponent);

		fireStarterItem.m_IsAccelerant = true;
		fireStarterItem.m_FireStartDurationModifier = modAccelerantComponent.DurationOffset;
		fireStarterItem.m_FireStartSkillModifier = modAccelerantComponent.SuccessModifier;
		fireStarterItem.m_ConsumeOnUse = modAccelerantComponent.DestroyedOnUse;
	}
}
