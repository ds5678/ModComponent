using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
    internal class StackableMapper
    {
        public static void Configure(ModComponent modComponent)
        {
            ModStackableComponent modStackableComponent = ModUtils.GetComponent<ModStackableComponent>(modComponent);
            if (modStackableComponent == null)
            {
                return;
            }

            StackableItem stackableItem = ModUtils.GetOrCreateComponent<StackableItem>(modComponent);

            stackableItem.m_LocalizedMultipleUnitText = new LocalizedString { m_LocalizationID = modStackableComponent.MultipleUnitTextID };

            if (string.IsNullOrEmpty(modStackableComponent.SingleUnitTextID))
            {
                stackableItem.m_LocalizedSingleUnitText = new LocalizedString { m_LocalizationID = modComponent.DisplayNameLocalizationId };
            }
            else
            {
                stackableItem.m_LocalizedSingleUnitText = new LocalizedString { m_LocalizationID = modStackableComponent.SingleUnitTextID };
            }

            stackableItem.m_StackSpriteName = modStackableComponent.StackSprite;

            stackableItem.m_ShareStackWithGear = new StackableItem[0];
            stackableItem.m_Units = modStackableComponent.UnitsPerItem;
            stackableItem.m_UnitsPerItem = modStackableComponent.UnitsPerItem;
        }
    }
}
