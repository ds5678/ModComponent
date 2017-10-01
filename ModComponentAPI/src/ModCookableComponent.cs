using UnityEngine;

namespace ModComponentAPI
{
    public class ModCookableComponent : ModComponent
    {
        [Header("Cookable/Cooking")]
        [Tooltip("Can this be cooked/heated. If not enabled, the other settings in this section will be ignored.")]
        public bool Cooking;
        [Tooltip("How many in-game minutes does it take to cook/heat this item?")]
        [Range(1, 60)]
        public int CookingMinutes = 1;
        [Range(0, 5)]
        [Tooltip("How many liters of water are required for cooking this item? Only potable water applies.")]
        public float CookingWaterRequired;
        [Range(1, 100)]
        [Tooltip("How many units of this item are required for cooking?")]
        public int CookingUnitsRequired = 1;
        [Tooltip("Sound to use when cooking/heating the item. Leave empty for a sensible default.")]
        public string CookingAudio;
        [Tooltip("Convert the item into this item when cooking completes. Leave empty to only heat the item without converting it.")]
        public GameObject CookingResult;
    }
}
