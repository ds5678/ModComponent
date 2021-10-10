namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModPurificationComponent : ModBaseComponent
	{
		public float LitersPurify = 1f;
		public float ProgressBarDurationSeconds = 5f;
		public string ProgressBarLocalizationID = "GAMEPLAY_PurifyingWater";
		public string PurifyAudio = "Play_WaterPurification";

		public ModPurificationComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
