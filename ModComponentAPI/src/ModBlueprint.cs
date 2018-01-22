using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public class ModBlueprint : MonoBehaviour
    {
        [Header("Required Items")]
        [Tooltip("The name of each gear needed to craft this item (e.g. gear_line)")]
        public string[] RequiredGear;
        [Tooltip("How many of each item are required, this list has to match the RequiredGear list")]
        public int[] RequiredGearUnits;
        [Tooltip("How many liters of kerosene are required?")]
        public float KeroseneLitersRequired;

        [Header("Required Tools")]
        [Tooltip("Tool required to craft (e.g. gear_knife)")]
        public string RequiredTool;
        [Tooltip("List of optional tools to speed the process of crafting")]
        public string[] OptionalTools;

        [Header("Required Conditions")]
        [Tooltip("Requires forge to craft?")]
        public bool RequiresForge;
        [Tooltip("Requires Workbench to craft?")]
        public bool RequiresWorkbench;
        [Tooltip("Requires ligth to craft?")]
        public bool RequiresLight;

        [Header("Result")]
        [Tooltip("The name of the item produced.")]
        public string CraftedResult;
        [Tooltip("Number of items produced.")]
        public int CraftedResultCount;

        [Header("Other configurations")]
        [Tooltip("Number of in-game minutes required.")]
        public int DurationMinutes;
        [Tooltip("Audio to be played")]
        public string CraftingAudio;

        [HideInInspector]
        public bool Locked = false;
    }
}
