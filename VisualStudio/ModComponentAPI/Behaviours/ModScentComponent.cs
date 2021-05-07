using UnityEngine;

namespace ModComponentAPI
{
	public enum ScentCategory
	{
		RAW_MEAT,
		RAW_FISH,
		COOKED_MEAT,
		COOKED_FISH,
		GUTS,
		QUARTER,
	}

	public class ModScentComponent : MonoBehaviour
	{
		/// <summary>
		/// What type of smell does this item have? Affects wildlife detection radius and smell strength.
		/// </summary>
		public ScentCategory scentCategory = ScentCategory.RAW_MEAT;

		public ModScentComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}