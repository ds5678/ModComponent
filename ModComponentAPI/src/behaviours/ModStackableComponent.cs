using UnityEngine;

namespace ModComponentAPI
{
    //[DisallowMultipleComponent]
    public class ModStackableComponent : MonoBehaviour
    {
        public string SingleUnitTextID;

        //[Tooltip("Localization key to be used for stacks with multiple items. E.g. '2 arrows'.")]
        public string MultipleUnitTextID;

        //[Tooltip("An optional sprite name (from a UIAtlas) that will be add to the stack.")]
        public string StackSprite;

        public int UnitsPerItem;

        //Between 0 and 1
        public float ChanceFull;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModStackableComponent>(this);
        }

        public ModStackableComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}