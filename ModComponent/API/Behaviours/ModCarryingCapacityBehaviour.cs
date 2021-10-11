using UnityEngine;

namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModCarryingCapacityBehaviour : MonoBehaviour
	{
		/// <summary>
		/// The maximum buff to the carrying capacity from this item.
		/// </summary>
		public float MaxCarryCapacityKGBuff;

		public ModCarryingCapacityBehaviour(System.IntPtr intPtr) : base(intPtr) { }
	}
}
