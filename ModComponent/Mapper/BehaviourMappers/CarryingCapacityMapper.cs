using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper.BehaviourMappers
{
	internal static class CarryingCapacityMapper
	{
		internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
		public static void Configure(GameObject prefab)
		{
			ModCarryingCapacityBehaviour capacityComponent = ComponentUtils.GetComponentSafe<ModCarryingCapacityBehaviour>(prefab);
			if (capacityComponent == null) return;

			CarryingCapacityBuff capacityBuff = ComponentUtils.GetOrCreateComponent<CarryingCapacityBuff>(capacityComponent);

			capacityBuff.m_IsWorn = ComponentUtils.GetComponentSafe<ModClothingComponent>(capacityComponent) != null
				|| ComponentUtils.GetComponentSafe<ClothingItem>(capacityComponent) != null;

			capacityBuff.m_CarryingCapacityBuffValues = new CarryingCapacityBuff.BuffValues()
			{
				m_MaxCarryCapacityKGBuff = capacityComponent.MaxCarryCapacityKGBuff
			};
		}
	}
}
