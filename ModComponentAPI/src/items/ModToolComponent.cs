using UnityEngine;

namespace ModComponentAPI
{
    public enum ToolType
    {
        None,
        HackSaw,
        Hatchet,
        Hammer,
        Knife,
    }

    public enum Usage
    {
        All,
        CraftOnly,
        RepairOnly,
    }

    public class ModToolComponent : EquippableModComponent
    {
        //[Header("Tool/Misc")]
        //[Tooltip("The type of the tool item. This determines for which actions it can be used. E.g. 'Knife' for cutting, 'Hammer' for pounding, etc.")]
        public ToolType ToolType = ToolType.None;
        //[Tooltip("How many condition points per use does this tool item lose? Certains actions have their own time driven degrade value, e.g. DegradePerHourCrafting, which applies only for those actions.")]
        //[Range(0f, 100f)]
        public float DegradeOnUse = 1;

        //[Header("Tool/Crafting & Repairing")]
        //[Tooltip("Can this item be used for crafting, repairing or both?")]
        public Usage Usage = Usage.All;
        //[Tooltip("Bonus to the relevant skill when using this item. E.g. the sewing kit gives a bonus of +20.")]
        //[Range(0, 100)]
        public int SkillBonus = 0;
        //[Tooltip("Multiplier for crafting and repair times. Represents percent. 0% means 'finishes instantly'; 100% means 'same time as without tool'.")]
        //[Range(0f, 1f)]
        public float CraftingTimeMultiplier = 1;
        //[Tooltip("How many condition points does the tool degrade while being used for crafting?")]
        //[Range(0f, 100f)]
        public float DegradePerHourCrafting;

        //[Header("Tool/Break Down")]
        //[Tooltip("Can this tool be used to break down items? If not enabled, the other settings in this section will be ignored.")]
        public bool BreakDown;
        //[Tooltip("Multiplier for the time required to break down an item. Represents percent. 0% means 'finishes instantly'; 100% means 'same time as without tool'.")]
        //[Range(0f, 1f)]
        public float BreakDownTimeMultiplier = 1;

        //[Header("Tool/Locks")]
        //[Tooltip("Can this tool item be used to open locked containers? If not enabled, the other settings in this section will be ignored.")]
        public bool ForceLocks;
        //[Tooltip("Sound to play while forcing a lock. Leave empty for a sensible default.")]
        public string ForceLockAudio;

        //[Header("Tool/Ice Fishing Hole")]
        //[Tooltip("Can this tool item be used to clear ice fishing holes? If not enabled, the other settings in this section will be ignored.")]
        public bool IceFishingHole;
        //[Tooltip("How many condition points does the tool lose when completely clearing an ice fishing hole?")]
        //[Range(0f, 100f)]
        public float IceFishingHoleDegradeOnUse;
        //[Tooltip("How many in-game minutes does it take to completely clear an ice fishing hole?")]
        //[Range(0, 120)]
        public int IceFishingHoleMinutes;
        //[Tooltip("Sound to play while clearing an ice fishing hole. Leave empty for a sensible default.")]
        public string IceFishingHoleAudio;

        //[Header("Tool/Carcass Harvesting")]
        //[Tooltip("Can this tool item be used to harvest carcasses? If not enabled, the other settings in this section will be ignored.")]
        public bool CarcassHarvesting;
        //[Tooltip("How many in-game minutes does it take to harvest one kg of unfrozen meat?")]
        //[Range(0, 60)]
        public int MinutesPerKgMeat;
        //[Tooltip("How many in-game minutes does it take to harvest one kg of frozen meat?")]
        //[Range(0, 60)]
        public int MinutesPerKgFrozenMeat;
        //[Tooltip("How many in-game minutes does it take to harvest one hide?")]
        //[Range(0, 60)]
        public int MinutesPerHide;
        //[Tooltip("How many in-game minutes does it take to harvest one gut?")]
        //[Range(0, 60)]
        public int MinutesPerGut;
        //[Tooltip("How many condition points does the tool degrade while being used for harvesting carcasses?")]
        //[Range(0f, 100f)]
        public float DegradePerHourHarvesting;

        //[Header("Tool/Struggle Bonus")]
        //[Tooltip("Can this tool item be used during a struggle with wildlife? If not enabled, the other settings in this section will be ignored.")]
        public bool StruggleBonus;
        //[Range(0.1f, 4f)]
        //[Tooltip("Multiplier for the damage dealt.")]
        public float DamageMultiplier = 1f;
        //[Range(0.1f, 4f)]
        //[Tooltip("Multiplier for the chance the animal will flee (breaking the struggle before the 'struggle bar' is filled).")]
        public float FleeChanceMultiplier = 1f;
        //[Range(0.1f, 4f)]
        //[Tooltip("Multiplier for the amount of the 'struggle bar' that is filled with each hit.")]
        public float TapMultiplier = 1f;
        //[Tooltip("Can this tool cause a puncture wound? If enabled, this will cause the animal to bleed out.")]
        public bool CanPuncture;
        //[Range(0.1f, 4f)]
        //[Tooltip("Multiplier for the time it takes the animal to bleed out after receiving a puncture wound.")]
        public float BleedoutMultiplier = 1f;

        public ModToolComponent(System.IntPtr intPtr) : base(intPtr) { }

        
    }
}