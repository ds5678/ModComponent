using ModComponent.Utils;
using System;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModGenericComponent : ModBaseComponent
{
	void Awake()
	{
		CopyFieldHandler.UpdateFieldValues(this);
	}

	public ModGenericComponent(IntPtr intPtr) : base(intPtr) { }
}