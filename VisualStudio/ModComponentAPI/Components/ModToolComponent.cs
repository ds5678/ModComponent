using System;
using System.Reflection;
using UnhollowerBaseLib.Attributes;

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
        /// <summary>
        /// The type of the tool item. This determines for which actions it can be used.
        /// E.g. 'Knife' for cutting, 'Hammer' for pounding, etc.
        /// </summary>
        public ToolType ToolType = ToolType.None;
        
        /// <summary>
        /// How many condition points per use does this tool item lose?
        /// Certains actions have their own time driven degrade value, e.g. DegradePerHourCrafting, which applies only for those actions.
        /// </summary>
        public float DegradeOnUse = 1;


        /// <summary>
        /// Can this item be used for crafting, repairing or both?
        /// </summary>
        public Usage Usage = Usage.All;
        
        /// <summary>
        /// Bonus to the relevant skill when using this item. E.g. the sewing kit gives a bonus of +20.
        /// </summary>
        public int SkillBonus = 0;
        
        /// <summary>
        /// Multiplier for crafting and repair times. Represents percent. 0% means 'finishes instantly'; 100% means 'same time as without tool'.
        /// </summary>
        public float CraftingTimeMultiplier = 1;
        
        /// <summary>
        /// How many condition points does the tool degrade while being used for crafting?
        /// </summary>
        public float DegradePerHourCrafting;

        
        /// <summary>
        /// Can this tool be used to break down items? If not enabled, the other settings in this section will be ignored.
        /// </summary>
        public bool BreakDown;
        
        /// <summary>
        /// Multiplier for the time required to break down an item.
        /// Represents percent. 0% means 'finishes instantly'; 100% means 'same time as without tool'.
        /// </summary>
        public float BreakDownTimeMultiplier = 1;

        
        /// <summary>
        /// Can this tool item be used to open locked containers? If not enabled, the other settings in this section will be ignored.
        /// </summary>
        public bool ForceLocks;

        /// <summary>
        /// Sound to play while forcing a lock. Leave empty for a sensible default.
        /// </summary>
        public string ForceLockAudio;

        
        /// <summary>
        /// Can this tool item be used to clear ice fishing holes? If not enabled, the other settings in this section will be ignored.
        /// </summary>
        public bool IceFishingHole;
        
        /// <summary>
        /// How many condition points does the tool lose when completely clearing an ice fishing hole?
        /// </summary>
        public float IceFishingHoleDegradeOnUse;
        
        /// <summary>
        /// How many in-game minutes does it take to completely clear an ice fishing hole?
        /// </summary>
        public int IceFishingHoleMinutes;

        /// <summary>
        /// Sound to play while clearing an ice fishing hole. Leave empty for a sensible default.
        /// </summary>
        public string IceFishingHoleAudio;

        
        /// <summary>
        /// Can this tool item be used to harvest carcasses? If not enabled, the other settings in this section will be ignored.
        /// </summary>
        public bool CarcassHarvesting;
        
        /// <summary>
        /// How many in-game minutes does it take to harvest one kg of unfrozen meat?
        /// </summary>
        public int MinutesPerKgMeat;
        
        /// <summary>
        /// How many in-game minutes does it take to harvest one kg of frozen meat?
        /// </summary>
        public int MinutesPerKgFrozenMeat;
        
        /// <summary>
        /// How many in-game minutes does it take to harvest one hide?
        /// </summary>
        public int MinutesPerHide;
        
        /// <summary>
        /// How many in-game minutes does it take to harvest one gut?
        /// </summary>
        public int MinutesPerGut;
        
        /// <summary>
        /// How many condition points does the tool degrade while being used for harvesting carcasses?
        /// </summary>
        public float DegradePerHourHarvesting;

        
        /// <summary>
        /// Can this tool item be used during a struggle with wildlife? If not enabled, the other settings in this section will be ignored.
        /// </summary>
        public bool StruggleBonus;
        
        /// <summary>
        /// Multiplier for the damage dealt.
        /// </summary>
        public float DamageMultiplier = 1f;
        
        /// <summary>
        /// Multiplier for the chance the animal will flee (breaking the struggle before the 'struggle bar' is filled).
        /// </summary>
        public float FleeChanceMultiplier = 1f;
        
        /// <summary>
        /// Multiplier for the amount of the 'struggle bar' that is filled with each hit.
        /// </summary>
        public float TapMultiplier = 1f;
        
        /// <summary>
        /// Can this tool cause a puncture wound? If enabled, this will cause the animal to bleed out.
        /// </summary>
        public bool CanPuncture;
        
        /// <summary>
        /// Multiplier for the time it takes the animal to bleed out after receiving a puncture wound.
        /// </summary>
        public float BleedoutMultiplier = 1f;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModToolComponent>(this);
            OnAwake();
        }

        public ModToolComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}