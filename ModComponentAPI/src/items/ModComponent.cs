using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponentAPI
{
    public enum InventoryCategory
    {
        Auto,
        Clothing,
        FirstAid,
        Firestarting,
        Food,
        Material,
        Tool
    }

    public abstract class ModComponent : MonoBehaviour
    {
        /// <summary>
        /// How this item will be called in the DeveloperConsole. Leave empty for a sensible default.
        /// </summary>
        public string ConsoleName;
        
        /// <summary>
        /// Localization key to be used for the in-game name of the item.
        /// </summary>
        public string DisplayNameLocalizationId;
        
        /// <summary>
        /// Localization key to be used for the in-game description of the item.
        /// </summary>
        public string DescriptionLocalizatonId;

        
        /// <summary>
        /// The inventory category to be used for this item. Leave at 'Auto' for a sensible default.
        /// </summary>
        public InventoryCategory InventoryCategory = InventoryCategory.Auto;
        
        /// <summary>
        /// Localization key to be used for the 'Action' (e.g. 'Equip', 'Eat', ...) button in the inventory.
        /// The text is purely cosmetic and will not influcence the action the button triggers. Leave empty for a sensible default.
        /// </summary>
        public string InventoryActionLocalizationId;
        
        /// <summary>
        /// Sound to play when the item is picked up.
        /// </summary>
        public string PickUpAudio;
        
        /// <summary>
        /// Sound to play when the item is holstered.
        /// </summary>
        public string StowAudio = "Play_InventoryStow";
        
        /// <summary>
        /// Sound to play when the item is dropped.
        /// </summary>
        public string PutBackAudio;
        
        /// <summary>
        /// Sound to play when the item wore out during an action.
        /// </summary>
        public string WornOutAudio;


        /// <summary>
        /// The weight of the item in kilograms.
        /// </summary>
        public float WeightKG;

        /// <summary>
        /// The maximum hit points of the item.
        /// </summary>
        public float MaxHP;
        
        /// <summary>
        /// The number of days it takes for this item to decay - without use - from 100% to 0%. Leave at 0 if the item should not decay over time.
        /// </summary>
        public float DaysToDecay;
        
        /// <summary>
        /// The initial condition of the item when found or crafted.
        /// </summary>
        public InitialCondition InitialCondition;


        /// <summary>
        /// Will the item be inspected when picked up? If not enabled, the item will go straight to the inventory.
        /// </summary>
        public bool InspectOnPickup;
        
        /// <summary>
        /// Distance from the camera during inspect.
        /// </summary>
        public float InspectDistance = 0.4f;
        
        /// <summary>
        /// Scales the item during inspect.
        /// </summary>
        public Vector3 InspectScale = Vector3.one;
        
        /// <summary>
        /// Each vector component stands for a rotation by the given degrees around the corresponding axis.
        /// </summary>
        public Vector3 InspectAngles = Vector3.zero;
        
        /// <summary>
        /// Offset from the center during inspect.
        /// </summary>
        public Vector3 InspectOffset = Vector3.zero;
        

        /// <summary>
        /// Model to show during inspect. NOTE: You must either set BOTH models or NO models.
        /// </summary>
        public GameObject InspectModel;
        
        /// <summary>
        /// Model to show when not inspecting the item. NOTE: You must either set BOTH models or NO models.
        /// </summary>
        public GameObject NormalModel;

        [HideFromIl2Cpp]
        public string GetEffectiveConsoleName()
        {
            if (string.IsNullOrEmpty(this.ConsoleName))
            {
                return this.name.Replace("GEAR_", ""); ;
            }

            return this.ConsoleName;
        }

        public ModComponent(IntPtr intPtr) : base(intPtr) { }
    }
}
