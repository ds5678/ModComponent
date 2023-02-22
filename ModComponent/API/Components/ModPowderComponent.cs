using Il2Cpp;

using MelonLoader.TinyJSON;
using ModComponent.Utils;
using Il2CppInterop.Runtime.Attributes;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public partial class ModPowderComponent : ModBaseComponent
{
	/// <summary>
	/// The type of powder this container holds. "Gunpowder"
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
			_ => GearPowderType.Gunpowder,
		};
	}

	internal static ModPowderType GetPowderType(GearPowderType gearPowderType)
	{
		return ModPowderType.Gunpowder;
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
