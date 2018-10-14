using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public class ModStackableComponent : MonoBehaviour
    {
        [Tooltip("Localization key to be used for stacks with multiple items. E.g. '2 arrows'.")]
        public string MultipleUnitText;

        [Tooltip("An optional sprite name (from a UIAtlas) that will be add to the stack.")]
        public string StackSprite;
    }
}