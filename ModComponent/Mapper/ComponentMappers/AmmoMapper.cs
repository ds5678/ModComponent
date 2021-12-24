using ModComponent.API.Components;

namespace ModComponent.Mapper.ComponentMappers
{
	internal static class AmmoMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModAmmoComponent modAmmoCompnent = modComponent.TryCast<ModAmmoComponent>();
			if (modAmmoCompnent == null) return;

			AmmoItem ammoItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<AmmoItem>(modComponent);
            ammoItem.m_AmmoForGunType = ModComponent.Utils.EnumUtils.TranslateEnumValue<GunType, ModAmmoComponent.GunType>(modAmmoCompnent.AmmoForGunType);
        }
	}
}