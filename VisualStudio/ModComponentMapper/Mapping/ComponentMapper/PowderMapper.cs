using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
	internal static class PowderMapper
	{
		internal static void Configure(ModComponent modComponent)
		{
			ModPowderComponent modPowderComponent = modComponent.TryCast<ModPowderComponent>();
			if (modPowderComponent is null) return;

			PowderItem powderItem = ModComponentUtils.ComponentUtils.GetOrCreateComponent<PowderItem>(modComponent);
			powderItem.m_Type = ModComponentUtils.EnumUtils.TranslateEnumValue<GearPowderType, PowderType>(modPowderComponent.PowderType);
			powderItem.m_WeightLimitKG = modPowderComponent.CapacityKG;
			powderItem.m_WeightKG = modPowderComponent.CapacityKG;
		}
	}
}