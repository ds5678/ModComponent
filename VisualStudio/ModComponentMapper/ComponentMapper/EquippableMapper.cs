using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
    internal static class EquippableMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            EquippableModComponent equippableModComponent = modComponent.TryCast<EquippableModComponent>();
            if (equippableModComponent == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(equippableModComponent.InventoryActionLocalizationId)
                && !string.IsNullOrEmpty(equippableModComponent.ImplementationType)
                && !string.IsNullOrEmpty(equippableModComponent.EquippedModelPrefabName))
            {
                equippableModComponent.InventoryActionLocalizationId = "GAMEPLAY_Equip";
            }
        }
    }
}
