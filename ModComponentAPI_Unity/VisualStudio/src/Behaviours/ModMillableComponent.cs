using UnityEngine;

namespace ModComponentAPI
{
    public class ModMillableComponent : MonoBehaviour
    {
        [Tooltip("Can this item be restored from a ruined condition?")]
        public bool CanRestoreFromWornOut = false;

        [Tooltip("The number of minutes it takes to restore this item from a ruined condition.")]
        [Range(1, 360)]
        public int RecoveryDurationMinutes = 1;

        [Tooltip("The gear required to restore this item from a ruined condition.")]
        public string[] RestoreRequiredGear;

        [Tooltip("The units of the gear required to restore this item from a ruined condition.")]
        public int[] RestoreRequiredGearUnits;

        [Tooltip("The number of minutes it takes to repair this item.")]
        [Range(1, 360)]
        public int RepairDurationMinutes = 1;

        [Tooltip("The gear required to repair this item.")]
        public string[] RepairRequiredGear;

        [Tooltip("The units of the gear required to repair this item.")]
        public int[] RepairRequiredGearUnits;

        [Tooltip("The skill associated with repairing this item.")]
        public SkillType skill = SkillType.None;
    }
}
