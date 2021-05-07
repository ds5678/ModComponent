using System;

namespace ModComponentAPI
{
	public class ModGenericComponent : ModComponent
	{
		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModGenericComponent>(this);
		}

		public ModGenericComponent(IntPtr intPtr) : base(intPtr) { }
	}
}