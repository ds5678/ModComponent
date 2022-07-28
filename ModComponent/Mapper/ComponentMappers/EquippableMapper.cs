using ModComponent.API.Components;

namespace ModComponent.Mapper.ComponentMappers;

internal static class EquippableMapper
{
	internal static void Configure(ModBaseComponent modComponent)
	{
		ModBaseEquippableComponent equippableModComponent = modComponent.TryCast<ModBaseEquippableComponent>();
		if (equippableModComponent == null) return;

		if (string.IsNullOrEmpty(equippableModComponent.InventoryActionLocalizationId)
			&& !string.IsNullOrEmpty(equippableModComponent.ImplementationType)
			&& !string.IsNullOrEmpty(equippableModComponent.EquippedModelPrefabName))
		{
			equippableModComponent.InventoryActionLocalizationId = "GAMEPLAY_Equip";
		}
	}
}
