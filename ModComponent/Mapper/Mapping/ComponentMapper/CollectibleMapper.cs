using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
	internal static class CollectibleMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModCollectibleComponent modCollectible = modComponent.TryCast<ModCollectibleComponent>();
			if (modCollectible == null) return;

			NarrativeCollectibleItem narrativeCollectible = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<NarrativeCollectibleItem>(modCollectible);
			narrativeCollectible.m_HudMessageOnPickup = new LocalizedString() { m_LocalizationID = modCollectible.HudMessageLocalizationId };
			narrativeCollectible.m_JournalEntryNumber = 0;
			narrativeCollectible.m_NarrativeTextLocID = modCollectible.NarrativeTextLocalizationId;
			narrativeCollectible.m_ShowDuringInspect = true;
			narrativeCollectible.m_TextAlignment = ModComponent.Utils.EnumUtils.TranslateEnumValue<NGUIText.Alignment, ModCollectibleComponent.Alignment>(modCollectible.TextAlignment);
			narrativeCollectible.m_Type = NarrativeCollectibleItem.CollectibleType.Note;
		}
	}
}
