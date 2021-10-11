using ModComponent.API;

namespace ModComponent.Mapper.ComponentMapper
{
	internal static class CharcoalMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModCharcoalComponent modCharcoal = modComponent.TryCast<ModCharcoalComponent>();
			if (modCharcoal == null) return;

			CharcoalItem charcoalItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<CharcoalItem>(modCharcoal);
			charcoalItem.m_SurveyGameMinutes = modCharcoal.SurveyGameMinutes;
			charcoalItem.m_SurveyLoopAudio = modCharcoal.SurveyLoopAudio;
			charcoalItem.m_SurveyRealSeconds = modCharcoal.SurveyRealSeconds;
			charcoalItem.m_SurveySkillExtendedHours = modCharcoal.SurveySkillExtendedHours;
		}
	}
}
