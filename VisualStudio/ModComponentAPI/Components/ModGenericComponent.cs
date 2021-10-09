using ModComponentUtils;
using System;

namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModGenericComponent : ModComponent
	{
		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModGenericComponent>(this);
		}

		public ModGenericComponent(IntPtr intPtr) : base(intPtr) { }
	}
}