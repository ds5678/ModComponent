using UnityEngine;

namespace ModComponentAPI
{
    public class ModStackableComponent : MonoBehaviour
    {
        /// <summary>
        /// Localization key to be used for stacks with a singular item. E.g. '1 arrow'.
        /// </summary>
        public string SingleUnitTextID;

        /// <summary>
        /// Localization key to be used for stacks with multiple items. E.g. '2 arrows'.
        /// </summary>
        public string MultipleUnitTextID;

        /// <summary>
        /// An optional sprite name (from a UIAtlas) that will be add to the stack.
        /// </summary>
        public string StackSprite;

        /// <summary>
        /// The default number of item to make a full stack.
        /// </summary>
        public int UnitsPerItem;

        /// <summary>
        /// Chance of the item having a full stack. Between 0 and 1.
        /// </summary>
        public float ChanceFull;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModStackableComponent>(this);
        }

        public ModStackableComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}