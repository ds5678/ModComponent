using UnityEngine;

namespace ModComponentAPI
{
	public class ModCarryingCapacityComponent : MonoBehaviour
	{
		/// <summary>
		/// The maximum buff to the carrying capacity from this item.
		/// </summary>
		public float MaxCarryCapacityKGBuff;

		public ModCarryingCapacityComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
