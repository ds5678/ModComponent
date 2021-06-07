namespace ModComponentAPI
{
	public class ModFireStarterComponent : ModFireMakingComponent
	{
		/// <summary>
		/// How many in-game seconds this item will take to ignite tinder.
		/// </summary>
		public float SecondsToIgniteTinder;

		/// <summary>
		/// How many in-game seconds this item will take to ignite a torch.
		/// </summary>
		public float SecondsToIgniteTorch;

		/// <summary>
		/// How many times can this item be used?
		/// </summary>
		public float NumberOfUses;

		/// <summary>
		/// Does the item require sunlight to work?
		/// </summary>
		public bool RequiresSunLight;

		/// <summary>
		/// What sound to play during usage. Not used for accelerants.
		/// </summary>
		public string OnUseSoundEvent;

		/// <summary>
		/// Set the condition to 0% after the fire starting finished (either successful or not).
		/// </summary>
		public bool RuinedAfterUse;

		/// <summary>
		/// Is the item destroyed immediately after use?
		/// </summary>
		public bool DestroyedOnUse;

		public ModFireStarterComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
