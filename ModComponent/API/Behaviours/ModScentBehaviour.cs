using Il2CppInterop.Runtime.Attributes;
using MelonLoader.TinyJSON;
using UnityEngine;

namespace ModComponent.API.Behaviours;

[MelonLoader.RegisterTypeInIl2Cpp]
public partial class ModScentBehaviour : MonoBehaviour
{
	/// <summary>
	/// What type of smell does this item have? Affects wildlife detection radius and smell strength.
	/// </summary>
	public ScentCategory scentCategory = ScentCategory.RAW_MEAT;

	public ModScentBehaviour(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal void InitializeBehaviour(ProxyObject dict, string className = "ModScentBehaviour")
	{
		this.scentCategory = dict.GetEnum<ScentCategory>(className, "ScentCategory");
	}
}