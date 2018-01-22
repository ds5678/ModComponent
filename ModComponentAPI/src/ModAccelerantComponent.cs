using UnityEngine;

namespace ModComponentAPI
{
    public class ModAccelerantComponent : ModFireStartingComponent
    {
        [Header("Accelerant")]
        [Tooltip("In-game seconds offset for fire starting duration from this accelerant. NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.")]
        public float DurationOffset;

    }
}