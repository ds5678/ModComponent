using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public class ModStackableComponent : MonoBehaviour
    {
        [Tooltip("Localization key to be used for stacks with a singular item. E.g. '1 arrow'.")]
        public string SingleUnitTextID;

        [Tooltip("Localization key to be used for stacks with multiple items. E.g. '2 arrows'.")]
        public string MultipleUnitText;

        [Tooltip("An optional sprite name (from a UIAtlas) that will be add to the stack.")]
        public string StackSprite;

        [Tooltip("The default number of item to make a full stack.")]
        public int UnitsPerItem = 1;

        [Range(0f, 100f)]
        [Tooltip("Percent chance of the item having a full stack.")]
        public float ChanceFull = 100f;
    }
}