using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public class ModBlueprint : MonoBehaviour
    {
        [Header("Requirements")]
        [Tooltip("The name of each gear needed to craft this item (Eg gear_line)")]
        public string[] RequiredGear;
        [Tooltip("How many of each item is required, this list has to match the RequiredGear list")]
        public int[] RequiredGearUnits;

        [Tooltip("Tool required to craft (eg gear_knife)")]
        public string RequiredTool;

        [Tooltip("List of optional tools to speed the process of crafting")]
        public string[] OptionalTools;
        [Tooltip("How many liters of Kerosene needed to craft")]
        public float KeroseneLitersRequired;
        [Tooltip("Requires forge to craft?")]
        public bool RequiresForge;
        [Tooltip("Requires Workbench to craft?")]
        public bool RequiresWorkbench;
        [Tooltip("Requires ligth to craft?")]
        public bool RequiresLight;

     

        [Header("Result")]
        [Tooltip("Item that is the result of this craft")]
        public string CraftedResult;
        [Tooltip("Amount of items you get after you craft")]
        public int CraftedResultCount;

        [Header("Other configurations")]
        [Tooltip("how long do you need to craft, in minutes")]
        public int DurationMinutes;
        [Tooltip("Audio to be played while crafting")]
        public string CraftingAudio;
        [HideInInspector]
        public bool Locked = false;


    }
}
