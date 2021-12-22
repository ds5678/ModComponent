using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModPowderComponent : ModBaseComponent
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
			CopyFieldHandler.UpdateFieldValues<ModPowderComponent>(this);

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
			switch (modPowderType)
			{
				case ModPowderType.Gunpowder:
					return GearPowderType.Gunpowder;
				case ModPowderType.Salt:
					return EnumUtils.GetMaxValue<GearPowderType>() + 1;
				case ModPowderType.Yeast:
					return EnumUtils.GetMaxValue<GearPowderType>() + 2;
				default:
					return GearPowderType.Gunpowder;
			}
		}

		internal static ModPowderType GetPowderType(GearPowderType gearPowderType)
		{
			GearPowderType maxValue = EnumUtils.GetMaxValue<GearPowderType>();
			if (gearPowderType == maxValue + 1)
				return ModPowderType.Salt;
			else if (gearPowderType == maxValue + 2)
				return ModPowderType.Yeast;
			else
				return ModPowderType.Gunpowder;
		}

		public ModPowderComponent(System.IntPtr intPtr) : base(intPtr) { }

		public enum ModPowderType
		{
			Gunpowder,
			Salt,
			Yeast
		}

		[HideFromIl2Cpp]
		internal override void InitializeComponent(ProxyObject dict, string className = "ModPowderComponent")
		{
			base.InitializeComponent(dict, className);
			this.PowderType = EnumUtils.ParseEnum<ModPowderComponent.ModPowderType>(dict.GetVariant(className,"PowderType"));
			this.CapacityKG = dict.GetVariant(className,"CapacityKG");
			this.ChanceFull = dict.GetVariant(className,"ChanceFull");
		}
	}
}
