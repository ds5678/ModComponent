using UnityEngine;

namespace ModComponentAPI
{
	/// <summary>
	/// The base class for all modded components involved in making fires
	/// </summary>
	[MelonLoader.RegisterTypeInIl2Cpp]
	public abstract class ModFireMakingComponent : MonoBehaviour
	{
		/// <summary>
		/// Does this item affect the chance for success? Represents percentage points.<br/>
		/// Positive values increase the chance, negative values reduce it.
		/// </summary>
		public float SuccessModifier;

		/// <summary>
		/// In-game seconds offset for fire starting duration from this accelerant.<br/>
		/// NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.
		/// </summary>
		public float DurationOffset;

		public ModFireMakingComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}