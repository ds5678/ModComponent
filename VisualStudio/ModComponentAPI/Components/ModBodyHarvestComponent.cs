namespace ModComponentAPI
{
	public class ModBodyHarvestComponent : ModComponent
	{
		public bool CanCarry;

		public string HarvestAudio;


		public string GutPrefab;

		public int GutQuantity;

		public float GutWeightKgPerUnit;



		public string HidePrefab;

		public int HideQuantity;

		public float HideWeightKgPerUnit;



		public string MeatPrefab;

		public float MeatAvailableMinKG;

		public float MeatAvailableMaxKG;



		public bool CanQuarter;

		public string QuarterAudio;

		public float QuarterBagMeatCapacityKG;

		public float QuarterBagWasteMultiplier;

		public float QuarterDurationMinutes;

		public string QuarterObjectPrefab;

		public float QuarterPrefabSpawnAngle;

		public float QuarterPrefabSpawnRadius;


		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModBodyHarvestComponent>(this);
		}


		public ModBodyHarvestComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
