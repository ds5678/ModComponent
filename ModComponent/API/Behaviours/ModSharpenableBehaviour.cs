using MelonLoader.TinyJSON;
using System;
using Il2CppInterop; using Il2CppInterop.Runtime.Attributes;
using UnityEngine;

namespace ModComponent.API.Behaviours;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModSharpenableBehaviour : MonoBehaviour
{
	/// <summary>
	/// Which tools can be used to sharpen this item, e.g. 'GEAR_SharpeningStone'. <br/>
	/// Leave empty to make this sharpenable without tools.
	/// </summary>
	public string[] Tools = Array.Empty<string>();

	/// <summary>
	/// How many in-game minutes does it take to sharpen this item at minimum skill.
	/// </summary>
	public int MinutesMin;

	/// <summary>
	/// How many in-game minutes does it take to sharpen this item at maximum skill.
	/// </summary>
	public int MinutesMax;

	/// <summary>
	/// How much condition is restored to this item at minimum skill.
	/// </summary>
	public float ConditionMin;

	/// <summary>
	/// How much condition is restored to this item at maximum skill.
	/// </summary>
	public float ConditionMax;

	/// <summary>
	/// The sound to play while sharpening. Leave empty for a sensible default.
	/// </summary>
	public string Audio = "";

	public ModSharpenableBehaviour(IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal void InitializeBehaviour(ProxyObject dict, string className = "ModSharpenableBehaviour")
	{
		this.Audio = dict.GetVariant(className, "Audio");
		this.MinutesMin = dict.GetVariant(className, "MinutesMin");
		this.MinutesMax = dict.GetVariant(className, "MinutesMax");
		this.ConditionMin = dict.GetVariant(className, "ConditionMin");
		this.ConditionMax = dict.GetVariant(className, "ConditionMax");
		this.Tools = dict.GetStringArray(className, "Tools");
	}
}
