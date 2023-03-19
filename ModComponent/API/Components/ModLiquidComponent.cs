using Il2CppInterop.Runtime.Attributes;
using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public partial class ModLiquidComponent : ModBaseComponent
{
	/// <summary>
	/// The type of liquid this item contains.
	/// </summary>
	public LiquidKind LiquidType = LiquidKind.Water;

	/// <summary>
	/// The capacity of this container in liters
	/// </summary>
	public float LiquidCapacityLiters;

	/// <summary>
	/// If true, this container will have a random initial quantity.
	/// </summary>
	public bool RandomizeQuantity = false;

	/// <summary>
	/// If initial quantity not randomized, it will have this amount initially.
	/// </summary>
	public float LiquidLiters = 0f;

	void Awake()
	{
		CopyFieldHandler.UpdateFieldValues(this);
	}

	public ModLiquidComponent(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeComponent(ProxyObject dict, string className = "ModLiquidComponent")
	{
		base.InitializeComponent(dict, className);
		this.LiquidType = dict.GetEnum<LiquidKind>(className, "LiquidType");
		this.LiquidCapacityLiters = dict.GetVariant(className, "LiquidCapacityLiters");
		this.RandomizeQuantity = dict.GetVariant(className, "RandomizedQuantity");
		this.LiquidLiters = Mathf.Clamp(dict.GetVariant(className, "LiquidLiters"), 0f, this.LiquidCapacityLiters); //overridden if Randomized
	}
}
