using UnityEngine;

namespace ModComponentAPI
{
    public class ModBurnableComponent : MonoBehaviour
    {
        [Range(0, 180)]
        [Tooltip("Number of minutes this item adds to the remaining burn time of the fire.")]
        public int BurningMinutes;

        [Range(0, 60)]
        [Tooltip("How long must a fire be burning before this item can be added?")]
        public float BurningMinutesBeforeAllowedToAdd;

        [Range(-100, 100)]
        [Tooltip("Does this item affect the chance for successfully starting a fire? Represents percentage points. Positive values increase the chance, negative values reduce it.")]
        public float SuccessModifier;

        [Range(0, 20)]
        [Tooltip("Temperature increase in °C when added to the fire.")]
        public float TempIncrease;
    }
}