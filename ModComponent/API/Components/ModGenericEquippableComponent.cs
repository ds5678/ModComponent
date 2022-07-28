using ModComponent.Utils;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModGenericEquippableComponent : ModBaseEquippableComponent
{
	public ModGenericEquippableComponent(System.IntPtr intPtr) : base(intPtr) { }

	protected override void Awake()
	{
		CopyFieldHandler.UpdateFieldValues(this);
		base.Awake();
	}
}
