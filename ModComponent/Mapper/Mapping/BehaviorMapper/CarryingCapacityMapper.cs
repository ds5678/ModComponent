using ModComponentAPI;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class CarryingCapacityMapper
	{
		internal static void Configure(ModComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
		public static void Configure(GameObject prefab)
		{
			ModCarryingCapacityComponent capacityComponent = ComponentUtils.GetComponent<ModCarryingCapacityComponent>(prefab);
			if (capacityComponent == null) return;

			CarryingCapacityBuff capacityBuff = ComponentUtils.GetOrCreateComponent<CarryingCapacityBuff>(capacityComponent);

			capacityBuff.m_IsWorn = ComponentUtils.GetComponent<ModClothingComponent>(capacityComponent) != null
				|| ComponentUtils.GetComponent<ClothingItem>(capacityComponent) != null;

			capacityBuff.m_CarryingCapacityBuffValues = new CarryingCapacityBuff.BuffValues()
			{
				m_MaxCarryCapacityKGBuff = capacityComponent.MaxCarryCapacityKGBuff
			};
		}
	}
}
