using System;
using UnityEngine;

namespace ModComponentAPI
{
    public enum SkillType
    {
        None = -1,
        Firestarting,
        CarcassHarvesting,
        IceFishing,
        Cooking,
        Rifle,
        Archery,
        ClothingRepair,
        ToolRepair,
        Revolver,
        Gunsmithing
    }

    public class ModMillableComponent : MonoBehaviour
    {
        public bool CanRestoreFromWornOut = false;
        public int RecoveryDurationMinutes = 1;
        public string[] RestoreRequiredGear;
        public int[] RestoreRequiredGearUnits;
        public int RepairDurationMinutes = 1;
        public string[] RepairRequiredGear;
        public int[] RepairRequiredGearUnits;
        public SkillType skill;

        public ModMillableComponent(IntPtr intPtr) : base(intPtr) { }
    }
}
