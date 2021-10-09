using ModComponentUtils;

namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModRifleComponent : EquippableModComponent
	{
		/// <summary>
		/// The maximum number of round that can be loaded at any given time.
		/// </summary>
		public int ClipSize;

		/// <summary>
		/// Raw damage per shot in HP. This is modified by the target's damage modifier.<br/>
		/// The Rifle skill will increase the damage by 20% at the highest level!
		/// </summary>
		public int DamagePerShot;

		/// <summary>
		/// Range in meters. Beyond this distance, hits will not inflict damage. <br/>
		/// The Rifle skill will increase the range by 40% at the highest level!
		/// </summary>
		public int Range;


		/// <summary>
		/// Time in seconds before the gun can shoot again.
		/// </summary>
		public float FiringDelay = 2.8f;

		/// <summary>
		/// Time in seconds before the gun can be used after loading it.
		/// </summary>
		public float ReloadDelay = 2f;

		/// <summary>
		/// Time in seconds before the gun can shoot after it went into 'Aim' mode.
		/// </summary>
		public float AimDelay = 0f;

		/// <summary>
		/// Time in seconds between firing the gun and the muzzle flash appearing.
		/// </summary>
		public float MuzzleFlashDelay = 0.1f;

		/// <summary>
		/// Time in seconds between firing the gun and the muzzle smoke appearing.
		/// </summary>
		public float MuzzleSmokeDelay = 0.15f;


		/// <summary>
		/// The minimum amount of sway experience with zero fatigue. <br/>
		/// (unknown unit, Hunting Rifle: 0)
		/// </summary>
		public float MinSway = 0;

		/// <summary>
		/// The minimum amount of sway experience with maximum fatigue. <br/>
		/// (unknown unit, Hunting Rifle: 0.75)
		/// </summary>
		public float MaxSway = 0.75f;

		/// <summary>
		/// The amount sway increases for every second in 'Aim' mode. <br/>
		/// (unknown unit, Hunting Rifle: 0.1)
		/// </summary>
		public float SwayIncrement = 0.1f;

		protected override void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModRifleComponent>(this);
			base.Awake();
		}

		public ModRifleComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
