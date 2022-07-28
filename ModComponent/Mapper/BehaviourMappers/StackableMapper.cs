extern alias Hinterland;
using Hinterland;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;

namespace ModComponent.Mapper.BehaviourMappers;

internal static class StackableMapper
{
	public static void Configure(ModBaseComponent modComponent)
	{
		ModStackableBehaviour modStackableComponent = ModComponent.Utils.ComponentUtils.GetComponentSafe<ModStackableBehaviour>(modComponent);
		if (modStackableComponent == null) return;

		StackableItem stackableItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<StackableItem>(modStackableComponent);

		stackableItem.m_LocalizedMultipleUnitText = new LocalizedString { m_LocalizationID = modStackableComponent.MultipleUnitTextID };

		if (string.IsNullOrWhiteSpace(modStackableComponent.SingleUnitTextID))
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
