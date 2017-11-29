using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public class ModHarvestableComponent : MonoBehaviour
    {
        [Tooltip("The audio to play while harvesting")]
        public string Audio;

        [Tooltip("How man in-game minutes does it take to harvest this item?")]
        [Range(1, 120)]
        public int Minutes;

        [Tooltip("The name of the GearItems havesting will yield")]
        public string[] YieldNames;

        [Tooltip("The number of the GearItems havesting will yield")]
        public int[] YieldCounts;
    }
}