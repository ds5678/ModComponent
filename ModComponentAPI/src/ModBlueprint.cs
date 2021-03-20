using UnityEngine;

namespace ModComponentAPI
{
    public enum CraftingLocation
    {
        Anywhere,
        Workbench,
        Forge,
        AmmoWorkbench
    }

    //[DisallowMultipleComponent]
    public class ModBlueprint : MonoBehaviour
    {
        //[Header("Required Items")]
        //[Tooltip("The name of each gear needed to craft this item (e.g. gear_line)")]
        public string[] RequiredGear;
        //[Tooltip("How many of each item are required, this list has to match the RequiredGear list")]
        public int[] RequiredGearUnits;
        //[Tooltip("How many liters of kerosene are required?")]
        public float KeroseneLitersRequired;
        //[Tooltip("How much gunpowder is required?")]
        public float GunpowderKGRequired;

        //[Header("Required Tools")]
        //[Tooltip("Tool required to craft (e.g. gear_knife)")]
        public string RequiredTool;
        //[Tooltip("List of optional tools to speed the process of crafting")]
        public string[] OptionalTools;

        //[Header("Required Conditions")]
        //[Tooltip("Where to craft?")]
        public CraftingLocation RequiredCraftingLocation;
        //[Tooltip("Requires a lit fire in the ammo workbench to craft?")]
        public bool RequiresLitFire;
        //[Tooltip("Requires ligth to craft?")]
        public bool RequiresLight;

        //[Header("Result")]
        //[Tooltip("The name of the item produced.")]
        public string CraftedResult;
        //[Tooltip("Number of items produced.")]
        public int CraftedResultCount;

        //[Header("Other configurations")]
        //[Tooltip("Number of in-game minutes required.")]
        public int DurationMinutes;
        //[Tooltip("Audio to be played")]
        public string CraftingAudio;

        public SkillType AppliedSkill;
        public SkillType ImprovedSkill;

        public ModBlueprint() { }
        public ModBlueprint(System.IntPtr intPtr) : base(intPtr) { }
    }
}
