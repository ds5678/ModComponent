using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public abstract class ModComponent : MonoBehaviour
    {
        [Header("Description")]
        [Tooltip("Localization key to be used for the in-game name of the item.")]
        public string DisplayNameLocalizationId;
        [Tooltip("Localization key to be used for the in-game description of the item.")]
        public string DescriptionLocalizatonId;

        [Header("Inventory")]
        [Tooltip("Localization key to be used for the 'Action' (e.g. 'Equip', 'Eat', ...) button in the inventory. The text is purely cosmetic and will not influcence the action the button triggers. Leave empty for a sensible default.")]
        public string InventoryActionLocalizationId;

        [Header("Basic Properties")]
        public float WeightKG;
        public float MaxHP;
        [Tooltip("The number of days it takes for this item to decay - without use - from 100% to 0%. Leave at 0 if the item should not decay over time.")]
        public float DaysToDecay;

        [Header("Audio/Inventory")]
        [Tooltip("Sound to play when the item is picked up.")]
        public string PickUpAudio;
        [Tooltip("Sound to play when the item is holstered.")]
        public string StowAudio = "Play_InventoryStow";
        [Tooltip("Sound to play when the item is dropped.")]
        public string PutBackAudio;
        [Tooltip("Sound to play when the item wore out during an action.")]
        public string WornOutAudio;

        [Header("Inspecting")]
        [Tooltip("Will the item be inspected when picked up? If false, the item will go straight to the inventory.")]
        public bool InspectOnPickup;
        [Tooltip("Distance from the camera during inspect.")]
        public float InspectDistance = 0.4f;
        [Tooltip("Scales the item during inspect.")]
        public Vector3 InspectScale = Vector3.one;
        [Tooltip("Each vector component stands for a rotation by the given degrees around the corresponding axis.")]
        public Vector3 InspectAngles = Vector3.zero;
        [Tooltip("Moves the item during inspect.")]
        public Vector3 InspectOffset = Vector3.zero;

        // this is required for patching with Harmony - do not remove!
        public void Awake()
        {
        }
    }
}
