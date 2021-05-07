using UnityEngine;

namespace ModComponentAPI
{
	public class ModBurnableComponent : MonoBehaviour
	{
		/// <summary>
		/// Number of minutes this item adds to the remaining burn time of the fire.
		/// </summary>
		public int BurningMinutes;

		/// <summary>
		/// How long must a fire be burning before this item can be added?
		/// </summary>
		public float BurningMinutesBeforeAllowedToAdd;

		/// <summary>
		/// Does this item affect the chance for successfully starting a fire?<br/>
		/// Represents percentage points. Positive values increase the chance, negative values reduce it.
		/// </summary>
		public float SuccessModifier;

		/// <summary>
		/// Temperature increase in °C when added to the fire.
		/// </summary>
		public float TempIncrease;

		public ModBurnableComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}