using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
	internal static class ResearchMapper
	{
		internal static void Configure(ModComponent modComponent)
		{
			ModResearchComponent modResearch = modComponent.TryCast<ModResearchComponent>();
			if (modResearch is null) return;

			ResearchItem researchItem = ModComponentUtils.ComponentUtils.GetOrCreateComponent<ResearchItem>(modResearch);
			researchItem.m_ReadAudio = modResearch.ReadAudio;
			researchItem.m_SkillPoints = modResearch.SkillPoints;
			researchItem.m_NoBenefitAtSkillLevel = modResearch.NoBenefitAtSkillLevel;
			researchItem.m_SkillType = ModComponentUtils.EnumUtils.TranslateEnumValue<SkillType, ModComponentAPI.SkillType>(modResearch.SkillType);
			researchItem.m_TimeRequirementHours = modResearch.TimeRequirementHours;
		}
	}
}
