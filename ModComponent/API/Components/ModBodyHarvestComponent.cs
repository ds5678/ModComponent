using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModBodyHarvestComponent : ModBaseComponent
{
	/// <summary>
	/// Can this be carried like a rabbit carcass?
	/// </summary>
	public bool CanCarry;

	/// <summary>
	/// The id for the sound to be played while harvesting.
	/// </summary>
	public string HarvestAudio;


	/// <summary>
	/// The name of the object prefab for the guts.
	/// </summary>
	public string GutPrefab;

	/// <summary>
	/// The number of guts in each harvest.
	/// </summary>
	public int GutQuantity;

	/// <summary>
	/// The weight of the gut before the player harvests it.
	/// </summary>
	public float GutWeightKgPerUnit;



	/// <summary>
	/// The name of the object prefab for the hide.
	/// </summary>
	public string HidePrefab;

	/// <summary>
	/// The number of hides in each harvest.
	/// </summary>
	public int HideQuantity;

	/// <summary>
	/// The weight of the hide before the player harvests it.
	/// </summary>
	public float HideWeightKgPerUnit;



	/// <summary>
	/// The name of the object prefab for the raw meat.
	/// </summary>
	public string MeatPrefab;

	/// <summary>
	/// The minimum amount of meat in each harvest.
	/// </summary>
	public float MeatAvailableMinKG;

	/// <summary>
	/// The maximum amount of meat in each harvest.
	/// </summary>
	public float MeatAvailableMaxKG;


	/// <summary>
	/// Can this be quartered?
	/// </summary>
	public bool CanQuarter;

	/// <summary>
	/// The id for the sound to be played while quartering.
	/// </summary>
	public string QuarterAudio;

	/// <summary>
	/// The maximum meat capacity of a quarter bag.
	/// </summary>
	public float QuarterBagMeatCapacityKG;

	/// <summary>
	/// 
	/// </summary>
	public float QuarterBagWasteMultiplier;

	/// <summary>
	/// How long does it take to quarter?
	/// </summary>
	public float QuarterDurationMinutes;

	/// <summary>
	/// The name of the object prefab for the quarter bag.
	/// </summary>
	public string QuarterObjectPrefab;

	public float QuarterPrefabSpawnAngle;

	public float QuarterPrefabSpawnRadius;


	void Awake()
	{
		CopyFieldHandler.UpdateFieldValues<ModBodyHarvestComponent>(this);
	}


	public ModBodyHarvestComponent(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeComponent(ProxyObject dict, string className = "ModBodyHarvestComponent")
	{
		base.InitializeComponent(dict, className);
		this.CanCarry = dict.GetVariant(className, "CanCarry");
		this.HarvestAudio = dict.GetVariant(className, "HarvestAudio");

		this.GutPrefab = dict.GetVariant(className, "GutPrefab");
		this.GutQuantity = dict.GetVariant(className, "GutQuantity");
		this.GutWeightKgPerUnit = dict.GetVariant(className, "GutWeightKgPerUnit");

		this.HidePrefab = dict.GetVariant(className, "HidePrefab");
		this.HideQuantity = dict.GetVariant(className, "HideQuantity");
		this.HideWeightKgPerUnit = dict.GetVariant(className, "HideWeightKgPerUnit");

		this.MeatPrefab = dict.GetVariant(className, "MeatPrefab");
		this.MeatAvailableMinKG = dict.GetVariant(className, "MeatAvailableMinKG");
		this.MeatAvailableMaxKG = dict.GetVariant(className, "MeatAvailableMaxKG");

		this.CanQuarter = false;
		this.QuarterAudio = "";
		this.QuarterBagMeatCapacityKG = 0f;
		this.QuarterBagWasteMultiplier = 0f;
		this.QuarterDurationMinutes = 1f;
		this.QuarterObjectPrefab = "";
		this.QuarterPrefabSpawnAngle = 0f;
		this.QuarterPrefabSpawnRadius = 1f;

	}
}
