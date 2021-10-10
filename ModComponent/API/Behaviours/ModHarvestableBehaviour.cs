using UnityEngine;

namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModHarvestableBehaviour : MonoBehaviour
	{
		/// <summary>
		/// The audio to play while harvesting
		/// </summary>
		public string Audio;

		/// <summary>
		/// How many in-game minutes does it take to harvest this item?
		/// </summary>
		public int Minutes;

		/// <summary>
		/// The names of the GearItems harvesting will yield
		/// </summary>
		public string[] YieldNames;

		/// <summary>
		/// The number of the GearItems harvesting will yield
		/// </summary>
		public int[] YieldCounts;

		/// <summary>
		/// The names of the ToolItems that can be used to harvest. Leave empty for harvesting by hand.
		/// </summary>
		public string[] RequiredToolNames;

		public ModHarvestableBehaviour(System.IntPtr intPtr) : base(intPtr) { }
	}
}