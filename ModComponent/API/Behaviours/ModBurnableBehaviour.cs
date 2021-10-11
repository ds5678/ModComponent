namespace ModComponent.API.Behaviours
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModBurnableBehaviour : ModFireMakingBaseBehaviour
	{
		/// <summary>
		/// Number of minutes this item adds to the remaining burn time of the fire.
		/// </summary>
		public int BurningMinutes;

		/// <summary>
		/// How long must a fire be burning before this item can be added?
		/// </summary>
		public float BurningMinutesBeforeAllowedToAdd;

		/// <summary>
		/// Temperature increase in °C when added to the fire.
		/// </summary>
		public float TempIncrease;

		public ModBurnableBehaviour(System.IntPtr intPtr) : base(intPtr) { }
	}
}