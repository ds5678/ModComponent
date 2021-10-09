using ModComponentUtils;

namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModBodyHarvestComponent : ModComponent
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
	}
}
