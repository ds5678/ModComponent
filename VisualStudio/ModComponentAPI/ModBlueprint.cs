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

    public class ModBlueprint : MonoBehaviour
    {
        /// <summary>
        /// The name of each gear needed to craft this item (e.g. GEAR_Line)
        /// </summary>
        public string[] RequiredGear;

        /// <summary>
        /// How many of each item are required? <br/>
        /// This list has to match the RequiredGear list.
        /// </summary>
        public int[] RequiredGearUnits;
        
        /// <summary>
        /// How many liters of kerosene are required?
        /// </summary>
        public float KeroseneLitersRequired;
        
        /// <summary>
        /// How much gunpowder is required? (in kilograms)
        /// </summary>
        public float GunpowderKGRequired;

        
        /// <summary>
        /// Tool required to craft (e.g. GEAR_Knife)
        /// </summary>
        public string RequiredTool;

        /// <summary>
        /// List of optional tools to speed the process of crafting or to use in place of the required tool.
        /// </summary>
        public string[] OptionalTools;

        
        /// <summary>
        /// Where to craft?
        /// </summary>
        public CraftingLocation RequiredCraftingLocation = CraftingLocation.Anywhere;
        
        /// <summary>
        /// Requires a lit fire in the ammo workbench to craft?
        /// </summary>
        public bool RequiresLitFire = false;
        
        /// <summary>
        /// Requires light to craft?
        /// </summary>
        public bool RequiresLight = true;

        
        /// <summary>
        /// The name of the item produced.
        /// </summary>
        public string CraftedResult;
        
        /// <summary>
        /// Number of the item produced.
        /// </summary>
        public int CraftedResultCount;

        
        /// <summary>
        /// Number of in-game minutes required.
        /// </summary>
        public int DurationMinutes;
        
        /// <summary>
        /// Audio to be played.
        /// </summary>
        public string CraftingAudio;


        /// <summary>
        /// The skill associated with crafting this item. (e.g. Gunsmithing)
        /// </summary>
        public SkillType AppliedSkill = SkillType.None;

        /// <summary>
        /// The skill improved on crafting success.
        /// </summary>
        public SkillType ImprovedSkill = SkillType.None;

        public ModBlueprint() { }
        public ModBlueprint(System.IntPtr intPtr) : base(intPtr) { }
    }
}
