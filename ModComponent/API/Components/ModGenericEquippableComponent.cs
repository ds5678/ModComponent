using ModComponent.Utils;

namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModGenericEquippableComponent : EquippableModComponent
	{
		public ModGenericEquippableComponent(System.IntPtr intPtr) : base(intPtr) { }

		protected override void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModGenericEquippableComponent>(this);
			base.Awake();
		}
	}
}
