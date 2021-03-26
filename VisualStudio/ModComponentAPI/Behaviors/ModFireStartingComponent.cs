using UnityEngine;

namespace ModComponentAPI
{
    public abstract class ModFireStartingComponent : MonoBehaviour
    {
        /// <summary>
        /// Is the item destroyed immediately after use?
        /// </summary>
        public bool DestroyedOnUse;

        /// <summary>
        /// Does this item affect the chance for success? Represents percentage points.<br/>
        /// Positive values increase the chance, negative values reduce it.
        /// </summary>
        public float SuccessModifier;

        public ModFireStartingComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}