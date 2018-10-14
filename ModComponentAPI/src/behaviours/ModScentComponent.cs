using UnityEngine;

namespace ModComponentAPI
{
    public enum ScentCategory
    {
        RAW_MEAT,
        RAW_FISH,
        COOKED_MEAT,
        COOKED_FISH,
        GUTS,
        QUARTER,
    }

    public class ModScentComponent : MonoBehaviour
    {
        [Tooltip("What type of smell does this item have? Affects wildlife detection radius and smell strength.")]
        public ScentCategory scentCategory = ScentCategory.RAW_MEAT;
    }
}