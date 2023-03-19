using Il2Cpp;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper.BehaviourMappers;

internal static class HarvestableMapper
{
	internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
	internal static void Configure(GameObject prefab)
	{
		ModHarvestableBehaviour modHarvestableComponent = ComponentUtils.GetComponentSafe<ModHarvestableBehaviour>(prefab);
		if (modHarvestableComponent == null)
		{
			return;
		}

		Harvest harvest = ComponentUtils.GetOrCreateComponent<Harvest>(modHarvestableComponent);
		harvest.m_Audio = modHarvestableComponent.Audio;
		harvest.m_DurationMinutes = modHarvestableComponent.Minutes;

		if (modHarvestableComponent.YieldNames.Length != modHarvestableComponent.YieldCounts.Length)
		{
			throw new ArgumentException("YieldNames and YieldCounts do not have the same length on gear item '" + modHarvestableComponent.name + "'.");
		}

		harvest.m_YieldGear = ModUtils.GetItems<GearItem>(modHarvestableComponent.YieldNames, modHarvestableComponent.name);
		harvest.m_YieldGearUnits = modHarvestableComponent.YieldCounts;

		harvest.m_AppliedSkillType = SkillType.None;
		harvest.m_RequiredTools = ModUtils.GetItems<ToolsItem>(modHarvestableComponent.RequiredToolNames, modHarvestableComponent.name);
		harvest.m_GunpowderYield = 0f;
	}
}