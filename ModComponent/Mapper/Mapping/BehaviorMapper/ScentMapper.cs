using ModComponentAPI;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper.ComponentMapper
{
	internal static class ScentMapper
	{
		internal static void Configure(ModBaseComponent modComponent) => Configure(ComponentUtils.GetGameObject(modComponent));
		internal static void Configure(GameObject prefab)
		{
			ModScentBehaviour modScentComponent = ComponentUtils.GetComponent<ModScentBehaviour>(prefab);
			if (modScentComponent == null) return;

			Scent scent = ComponentUtils.GetOrCreateComponent<Scent>(modScentComponent);
			scent.m_ScentCategory = EnumUtils.TranslateEnumValue<ScentRangeCategory, ModScentBehaviour.ScentCategory>(modScentComponent.scentCategory);
		}

		internal static float GetScentIntensity(ModBaseComponent modComponent) => GetScentIntensity(ComponentUtils.GetGameObject(modComponent));
		internal static float GetScentIntensity(GameObject prefab)
		{
			Scent scent = ComponentUtils.GetComponent<Scent>(prefab);
			if (scent == null) return 0f;

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