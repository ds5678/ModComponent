using UnityEngine;

namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModScentBehaviour : MonoBehaviour
	{
		/// <summary>
		/// What type of smell does this item have? Affects wildlife detection radius and smell strength.
		/// </summary>
		public ScentCategory scentCategory = ScentCategory.RAW_MEAT;

		public ModScentBehaviour(System.IntPtr intPtr) : base(intPtr) { }

		public enum ScentCategory
		{
			RAW_MEAT,
			RAW_FISH,
			COOKED_MEAT,
			COOKED_FISH,
			GUTS,
			QUARTER,
		}
	}
}