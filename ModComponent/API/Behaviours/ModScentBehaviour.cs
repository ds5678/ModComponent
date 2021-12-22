using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponent.API.Behaviours
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

		[HideFromIl2Cpp]
		internal void InitializeBehaviour(ProxyObject dict, string className = "ModScentBehaviour")
		{
			this.scentCategory = dict.GetEnum<ModScentBehaviour.ScentCategory>(className,"ScentCategory");
		}
	}
}