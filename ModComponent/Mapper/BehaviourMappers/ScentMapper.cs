using Il2Cpp;

using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper.BehaviourMappers;

internal static class ScentMapper
{
	internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
	internal static void Configure(GameObject prefab)
	{
		ModScentBehaviour modScentComponent = ComponentUtils.GetComponentSafe<ModScentBehaviour>(prefab);
		if (modScentComponent == null)
		{
			return;
		}

		Scent scent = ComponentUtils.GetOrCreateComponent<Scent>(modScentComponent);
		scent.m_ScentCategory = EnumUtils.TranslateEnumValue<ScentRangeCategory, ModScentBehaviour.ScentCategory>(modScentComponent.scentCategory);
	}

	internal static float GetScentIntensity(ModBaseComponent modComponent) => GetScentIntensity(ComponentUtils.GetGameObject(modComponent));
	internal static float GetScentIntensity(GameObject prefab)
	{
		Scent scent = ComponentUtils.GetComponentSafe<Scent>(prefab);
		if (scent == null)
		{
			return 0f;
		}

		return scent.m_ScentCategory switch
		{
			ScentRangeCategory.COOKED_MEAT => 5f,
			ScentRangeCategory.COOKED_FISH => 5f,
			ScentRangeCategory.GUTS => 20f,
			ScentRangeCategory.QUARTER => 50f,
			ScentRangeCategory.RAW_MEAT => 15f,
			ScentRangeCategory.RAW_FISH => 15f,
			_ => 0f,
		};
	}
}