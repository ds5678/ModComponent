using UnityEngine;

namespace ModComponent.API.Behaviours
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModMillableBehaviour : MonoBehaviour
	{
		/// <summary>
		/// Can this item be restored from a ruined condition?
		/// </summary>
		public bool CanRestoreFromWornOut = false;

		/// <summary>
		/// The number of minutes it takes to restore this item from a ruined condition.
		/// </summary>
		public int RecoveryDurationMinutes = 1;

		/// <summary>
		/// The gear required to restore this item from a ruined condition.
		/// </summary>
		public string[] RestoreRequiredGear;

		/// <summary>
		/// The units of the gear required to restore this item from a ruined condition.
		/// </summary>
		public int[] RestoreRequiredGearUnits;

		/// <summary>
		/// The number of minutes it takes to repair this item.
		/// </summary>
		public int RepairDurationMinutes = 1;

		/// <summary>
		/// The gear required to repair this item.
		/// </summary>
		public string[] RepairRequiredGear;

		/// <summary>
		/// The units of the gear required to repair this item.
		/// </summary>
		public int[] RepairRequiredGearUnits;

		/// <summary>
		/// The skill associated with repairing this item.
		/// </summary>
		public ModSkillType Skill = ModSkillType.None;

		public ModMillableBehaviour(System.IntPtr intPtr) : base(intPtr) { }
	}
}
