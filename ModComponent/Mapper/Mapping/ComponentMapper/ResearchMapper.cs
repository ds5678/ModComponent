using ModComponent.API;
using ModComponent.API.Components;

namespace ModComponent.Mapper.ComponentMapper
{
	internal static class ResearchMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModResearchComponent modResearch = modComponent.TryCast<ModResearchComponent>();
			if (modResearch == null) return;

			ResearchItem researchItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<ResearchItem>(modResearch);
			researchItem.m_ReadAudio = modResearch.ReadAudio;
			researchItem.m_SkillPoints = modResearch.SkillPoints;
			researchItem.m_NoBenefitAtSkillLevel = modResearch.NoBenefitAtSkillLevel;
			researchItem.m_SkillType = ModComponent.Utils.EnumUtils.TranslateEnumValue<SkillType, ModComponent.API.SkillType>(modResearch.SkillType);
			researchItem.m_TimeRequirementHours = modResearch.TimeRequirementHours;
		}
	}
}
