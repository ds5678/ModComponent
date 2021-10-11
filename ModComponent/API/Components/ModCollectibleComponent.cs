namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModCollectibleComponent : ModBaseComponent
	{
		public enum Alignment
		{
			Automatic,
			Left,
			Center,
			Right,
			Justified
		}

		/// <summary>
		/// The localization id for the hud message displayed after this item is picked up.
		/// </summary>
		public string HudMessageLocalizationId = "";

		/// <summary>
		/// The localization id for the narrative content of the item.
		/// </summary>
		public string NarrativeTextLocalizationId = "";

		/// <summary>
		/// The alignment of the narrative text. Options are "Automatic", "Left", "Center", "Right", and "Justified"
		/// </summary>
		public Alignment TextAlignment = Alignment.Automatic;

		public ModCollectibleComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
