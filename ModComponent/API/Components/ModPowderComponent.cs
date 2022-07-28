extern alias Hinterland;
using Hinterland;
using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public partial class ModPowderComponent : ModBaseComponent
{
	/// <summary>
	/// The type of powder this container holds. "Gunpowder", "Salt", or "Yeast"
	/// </summary>
	public ModPowderType PowderType = ModPowderType.Gunpowder;

	/// <summary>
	/// The maximum weight this container can hold.
	/// </summary>
	public float CapacityKG;

	/// <summary>
	/// The percent probability that this container will be found full.
	/// </summary>
	public float ChanceFull = 100f;

	void Awake()
	{
		CopyFieldHandler.UpdateFieldValues(this);

		PowderItem powderItem = this.GetComponent<PowderItem>();
		GearItem gearItem = this.GetComponent<GearItem>();
		if (powderItem && gearItem && !gearItem.m_BeenInspected && ChanceFull != 100f)
		{
			if (!RandomUtils.RollChance(ChanceFull))
			{
				powderItem.m_WeightKG = powderItem.m_WeightLimitKG * RandomUtils.Range(0.125f, 1f);
			}
		}
	}

	internal static GearPowderType GetPowderType(ModPowderType modPowderType)
	{
		return modPowderType switch
		{
			ModPowderType.Gunpowder => GearPowderType.Gunpowder,
			ModPowderType.Salt => EnumUtils.GetMaxValue<GearPowderType>() + 1,
			ModPowderType.Yeast => EnumUtils.GetMaxValue<GearPowderType>() + 2,
			_ => GearPowderType.Gunpowder,
		};
	}

	internal static ModPowderType GetPowderType(GearPowderType gearPowderType)
	{
		GearPowderType maxValue = EnumUtils.GetMaxValue<GearPowderType>();
		return gearPowderType == maxValue + 1
			? ModPowderType.Salt
			: gearPowderType == maxValue + 2 
				? ModPowderType.Yeast 
				: ModPowderType.Gunpowder;
	}

	public ModPowderComponent(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeComponent(ProxyObject dict, string className = "ModPowderComponent")
	{
		base.InitializeComponent(dict, className);
		this.PowderType = dict.GetEnum<ModPowderType>(className, "PowderType");
		this.CapacityKG = dict.GetVariant(className, "CapacityKG");
		this.ChanceFull = dict.GetVariant(className, "ChanceFull");
	}
}
