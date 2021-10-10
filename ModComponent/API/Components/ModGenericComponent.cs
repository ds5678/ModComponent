using ModComponent.Utils;
using System;

namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModGenericComponent : ModBaseComponent
	{
		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModGenericComponent>(this);
		}

		public ModGenericComponent(IntPtr intPtr) : base(intPtr) { }
	}
}