namespace ModComponentAPI
{
	public class ModExplosiveComponent : EquippableModComponent
	{
		public float killRange = 5;

		public float explosionDelay;

		public string explosionAudio;

		protected override void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModExplosiveComponent>(this);
			base.Awake();
		}

		public ModExplosiveComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
