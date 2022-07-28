extern alias Hinterland;
using Hinterland;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Utils;
using System;
using UnityEngine;

namespace ModComponent.Mapper.BehaviourMappers;

internal static class RepairableMapper
{
	internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
	internal static void Configure(GameObject prefab)
	{
		ModRepairableBehaviour modRepairableComponent = ComponentUtils.GetComponentSafe<ModRepairableBehaviour>(prefab);
		if (modRepairableComponent == null) return;

		Repairable repairable = ComponentUtils.GetOrCreateComponent<Repairable>(modRepairableComponent);
		repairable.m_RepairAudio = modRepairableComponent.Audio;
		repairable.m_DurationMinutes = modRepairableComponent.Minutes;
		repairable.m_ConditionIncrease = modRepairableComponent.Condition;

		if (modRepairableComponent.MaterialNames.Length != modRepairableComponent.MaterialCounts.Length)
		{
			throw new ArgumentException("MaterialNames and MaterialCounts do not have the same length on gear item '" + modRepairableComponent.name + "'.");
		}

		repairable.m_RequiredGear = ModUtils.GetItems<GearItem>(modRepairableComponent.MaterialNames, modRepairableComponent.name);
		repairable.m_RequiredGearUnits = modRepairableComponent.MaterialCounts;

		repairable.m_RepairToolChoices = ModUtils.GetItems<ToolsItem>(modRepairableComponent.RequiredTools, modRepairableComponent.name);
		repairable.m_RequiresToolToRepair = repairable.m_RepairToolChoices.Length > 0;
	}
}