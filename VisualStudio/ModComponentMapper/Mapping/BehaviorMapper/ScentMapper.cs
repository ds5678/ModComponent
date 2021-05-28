using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class ScentMapper
	{
		internal static void Configure(ModComponent modComponent) => Configure(modComponent.gameObject);
		internal static void Configure(GameObject prefab)
		{
			ModScentComponent modScentComponent = ModComponentUtils.ComponentUtils.GetComponent<ModScentComponent>(prefab);
			if (modScentComponent is null) return;

			Scent scent = ModComponentUtils.ComponentUtils.GetOrCreateComponent<Scent>(modScentComponent);
			scent.m_ScentCategory = ModComponentUtils.EnumUtils.TranslateEnumValue<ScentRangeCategory, ScentCategory>(modScentComponent.scentCategory);
		}

		internal static float GetScentIntensity(ModComponent modComponent) => GetScentIntensity(modComponent.gameObject);
		internal static float GetScentIntensity(GameObject prefab)
		{
			Scent scent = ModComponentUtils.ComponentUtils.GetComponent<Scent>(prefab);
			if (scent is null) return 0f;

			switch (scent.m_ScentCategory)
			{
				case ScentRangeCategory.COOKED_MEAT:
					return 5f;
				case ScentRangeCategory.COOKED_FISH:
					return 5f;
				case ScentRangeCategory.GUTS:
					return 20f;
				case ScentRangeCategory.QUARTER:
					return 50f;
				case ScentRangeCategory.RAW_MEAT:
					return 15f;
				case ScentRangeCategory.RAW_FISH:
					return 15f;
				default:
					return 0f;
			}
		}
	}
}