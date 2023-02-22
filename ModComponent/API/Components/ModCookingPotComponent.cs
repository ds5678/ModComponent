using MelonLoader.TinyJSON;
using ModComponent.Utils;
using System;
using Il2CppInterop.Runtime.Attributes;
using UnityEngine;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModCookingPotComponent : ModBaseComponent
{
	/// <summary>
	/// Can the item cook liquids?
	/// </summary>
	public bool CanCookLiquid;

	/// <summary>
	/// Can the item cook grub? <br/>
	/// Cookable canned food counts as grub.
	/// </summary>
	public bool CanCookGrub;

	/// <summary>
	/// Can the item cook meat?
	/// </summary>
	public bool CanCookMeat;

	/// <summary>
	/// The total water capacity of the item.
	/// </summary>
	public float Capacity;

	/// <summary>
	/// Template item to be used in the mapping process.
	/// </summary>
	public string Template = "";

	public Mesh? SnowMesh;
	public Mesh? WaterMesh;

	void Awake()
	{
		CopyFieldHandler.UpdateFieldValues(this);
	}

	public ModCookingPotComponent(IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeComponent(ProxyObject dict, string className = "ModCookingPotComponent")
	{
		base.InitializeComponent(dict, className);
		this.CanCookLiquid = dict.GetVariant(className, "CanCookLiquid");
		this.CanCookGrub = dict.GetVariant(className, "CanCookGrub");
		this.CanCookMeat = dict.GetVariant(className, "CanCookMeat");
		this.Capacity = dict.GetVariant(className, "Capacity");
		this.Template = dict.GetVariant(className, "Template");
		this.SnowMesh = null;// GetChild(this.gameObject, dict.GetVariant(className,"SnowMesh")).GetComponent<MeshFilter>().mesh;
		this.WaterMesh = null; // GetChild(this.gameObject, dict.GetVariant(className,"WaterMesh")).GetComponent<MeshFilter>().mesh;
	}
}