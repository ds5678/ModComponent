using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public abstract class ModFireStartingComponent : MonoBehaviour
    {
        [Tooltip("Is the item destroyed immediately after use?")]
        public bool DestroyedOnUse;

        [Range(-100, 100)]
        [Tooltip("Does this item affect the chance for success? Represents percentage points. Positive values increase the chance, negative values reduce it.")]
        public float SuccessModifier;
    }
}