namespace ModComponentAPI
{
	public enum PowderType
	{
		Gunpowder
	}

	public class ModPowderComponent : ModComponent
	{
		/// <summary>
		/// The type of powder this container holds. "Gunpowder" is the only option right now.
		/// </summary>
		public PowderType PowderType = PowderType.Gunpowder;

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
				if (!ModComponentMapper.RandomUtils.RollChance(ChanceFull))
				{
					powderItem.m_WeightKG = powderItem.m_WeightLimitKG * ModComponentMapper.RandomUtils.Range(0.125f, 1f);
				}
			}
		}

		public ModPowderComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
