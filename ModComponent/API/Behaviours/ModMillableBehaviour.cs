using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;
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

		[HideFromIl2Cpp]
		internal void InitializeBehaviour(ProxyObject dict, string className = "ModMillableBehaviour")
		{
			this.RepairDurationMinutes = dict.GetVariant(className,"RepairDurationMinutes");
			this.RepairRequiredGear = JsonUtils.MakeStringArray(dict.GetVariant(className,"RepairRequiredGear") as ProxyArray);
			this.RepairRequiredGearUnits = JsonUtils.MakeIntArray(dict.GetVariant(className,"RepairRequiredGearUnits") as ProxyArray);
			this.CanRestoreFromWornOut = dict.GetVariant(className,"CanRestoreFromWornOut");
			this.RecoveryDurationMinutes = dict.GetVariant(className,"RecoveryDurationMinutes");
			this.RestoreRequiredGear = JsonUtils.MakeStringArray(dict.GetVariant(className,"RestoreRequiredGear") as ProxyArray);
			this.RestoreRequiredGearUnits = JsonUtils.MakeIntArray(dict.GetVariant(className,"RestoreRequiredGearUnits") as ProxyArray);
			this.Skill = EnumUtils.ParseEnum<ModComponent.API.ModSkillType>(dict.GetVariant(className,"Skill"));
		}
	}
}
