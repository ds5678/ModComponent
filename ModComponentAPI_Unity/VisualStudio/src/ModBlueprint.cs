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

    [DisallowMultipleComponent]
    public class ModBlueprint : MonoBehaviour
    {
        [Header("Required Items")]
        [Tooltip("The name of each gear needed to craft this item (e.g. GEAR_Line)")]
        public string[] RequiredGear;
        [Tooltip("How many of each item are required, this list has to match the RequiredGear list")]
        public int[] RequiredGearUnits;
        [Tooltip("How many liters of kerosene are required?")]
        public float KeroseneLitersRequired;
        [Tooltip("How much gunpowder is required? (in kilograms)")]
        public float GunpowderKGRequired;

        [Header("Required Tools")]
        [Tooltip("Tool required to craft (e.g. GEAR_Knife)")]
        public string RequiredTool;
        [Tooltip("List of optional tools to speed the process of crafting or to use in place of the required tool.")]
        public string[] OptionalTools;

        [Header("Required Conditions")]
        [Tooltip("Where to craft?")]
        public CraftingLocation RequiredCraftingLocation = CraftingLocation.Anywhere;
        [Tooltip("Requires a lit fire in the ammo workbench to craft?")]
        public bool RequiresLitFire = false;
        [Tooltip("Requires ligth to craft?")]
        public bool RequiresLight = true;

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


        [Tooltip("The skill associated with crafting this item. (e.g. Gunsmithing)")]
        public SkillType AppliedSkill = SkillType.None;
        [Tooltip("The skill improved on crafting success.")]
        public SkillType ImprovedSkill = SkillType.None;
    }
}
