using Il2CppInterop.Runtime.Attributes;
using MelonLoader.TinyJSON;
using UnityEngine;

namespace ModComponent.API.Behaviours;

/// <summary>
/// The base class for all modded components involved in making fires
/// </summary>
[MelonLoader.RegisterTypeInIl2Cpp]
public abstract class ModFireMakingBaseBehaviour : MonoBehaviour
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

	public ModFireMakingBaseBehaviour(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal virtual void InitializeBehaviour(ProxyObject dict, string className)
	{
		this.SuccessModifier = dict.GetVariant(className, "SuccessModifier");
		this.DurationOffset = dict.GetVariant(className, "DurationOffset");
	}
}