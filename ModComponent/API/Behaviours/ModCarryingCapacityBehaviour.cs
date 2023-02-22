using MelonLoader.TinyJSON;
using Il2CppInterop.Runtime.Attributes;
using UnityEngine;

namespace ModComponent.API.Behaviours;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModCarryingCapacityBehaviour : MonoBehaviour
{
	/// <summary>
	/// The maximum buff to the carrying capacity from this item.
	/// </summary>
	public float MaxCarryCapacityKGBuff;

	public ModCarryingCapacityBehaviour(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal void InitializeBehaviour(ProxyObject dict, string className = "ModCarryingCapacityBehaviour")
	{
		this.MaxCarryCapacityKGBuff = dict.GetVariant(className, "MaxCarryCapacityKGBuff");
	}
}
