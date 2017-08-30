using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public abstract class ModComponent : MonoBehaviour
    {
        [Header("Description")]
        [Tooltip("Localization key to be used the in-game name of the item.")]
        public string DisplayNameLocalizationId;
        [Tooltip("Localization key to be used the in-game description of the item.")]
        public string DescriptionLocalizatonId;

        [Header("Inventory")]
        [Tooltip("Localization key to be used for the 'Action' (e.g. 'Equip', 'Eat', ...) button in the inventory.")]
        public string InventoryActionLocalizationId;

        [Header("Basic Properties")]
        public float WeightKG;
        public float MaxHP;
        [Tooltip("The number of days it takes for this item to decay from 100% to 0%. Leave at 0 if the item should not decay over time.")]
        public float DaysToDecay;

        [Header("Audio/Inventory")]
        public string PickUpAudio;
        public string StowAudio = "Play_InventoryStow";
        public string PutBackAudio;
        public string WornOutAudio;

        [Header("Inspecting")]
        [Tooltip("Will the item be inspected when picked up? If false, the item will go straight to the inventory.")]
        public bool InspectOnPickup;
        [Tooltip("Distance from the camera during inspect.")]
        public float InspectDistance = 0.4f;
        [Tooltip("Scales the item during inspect")]
        public Vector3 InspectScale = Vector3.one;
        [Tooltip("Each vector component stands for a rotation by the given degrees around the corresponding axis.")]
        public Vector3 InspectAngles;

        public void Awake()
        {
        }
    }
}
