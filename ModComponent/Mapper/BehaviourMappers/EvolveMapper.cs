extern alias Hinterland;
using Hinterland;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper.BehaviourMappers;

internal static class EvolveMapper
{
	internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
	internal static void Configure(GameObject prefab)
	{
		ModEvolveBehaviour modEvolveComponent = ComponentUtils.GetComponentSafe<ModEvolveBehaviour>(prefab);
		if (modEvolveComponent == null)
		{
			return;
		}

		EvolveItem evolveItem = ComponentUtils.GetOrCreateComponent<EvolveItem>(modEvolveComponent);
		evolveItem.m_ForceNoAutoEvolve = false;
		evolveItem.m_GearItemToBecome = GetTargetItem(modEvolveComponent.TargetItemName, modEvolveComponent.name);
		evolveItem.m_RequireIndoors = modEvolveComponent.IndoorsOnly;
		evolveItem.m_StartEvolvePercent = 0;
		evolveItem.m_TimeToEvolveGameDays = Mathf.Clamp(modEvolveComponent.EvolveHours / 24f, 0.01f, 1000);
	}

	private static GearItem GetTargetItem(string targetItemName, string reference)
	{
		GameObject? targetItem = Resources.Load(targetItemName)?.Cast<GameObject>();
		if (targetItem != null && ComponentUtils.GetModComponent(targetItem) != null)
		{
			// if this a modded item, map it now (no harm if it was already mapped earlier)
			ItemMapper.Map(targetItem);
		}

		return ModUtils.GetItem<GearItem>(targetItemName, reference);
	}
}