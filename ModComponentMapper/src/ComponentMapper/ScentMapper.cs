using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
    internal class ScentMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            ModScentComponent modScentComponent = ModUtils.GetComponent<ModScentComponent>(modComponent);
            if (modScentComponent == null)
            {
                return;
            }

            Scent scent = ModUtils.GetOrCreateComponent<Scent>(modScentComponent);
            scent.m_ScentCategory = ModUtils.TranslateEnumValue<ScentRangeCategory, ScentCategory>(modScentComponent.scentCategory);
        }

        internal static float GetScentIntensity(ModComponent modComponent)
        {
            Scent scent = ModUtils.GetComponent<Scent>(modComponent);
            if (scent == null)
            {
                return 0f;
            }

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