using ModComponentAPI;
using ModComponentUtils;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponentMapper
{
	internal static class SkillsMapper
	{
		private static List<ModSkill> skills = new List<ModSkill>();
		internal static void MapSkill(ModSkill modSkill)
		{
			SerializableSkill skill = new GameObject().AddComponent<SerializableSkill>();

			skill.name = modSkill.name;
			skill.m_LocalizedDisplayName = NameUtils.CreateLocalizedString(modSkill.DisplayName);
			skill.m_SkillType = (SkillType)GameManager.GetSkillsManager().GetNumSkills();
			skill.m_SkillIcon = modSkill.Icon;
			skill.m_SkillIconBackground = modSkill.Image;
			skill.m_SkillImage = modSkill.Image;
			skill.m_TierPoints = new int[] { 0, modSkill.PointsLevel2, modSkill.PointsLevel3, modSkill.PointsLevel4, modSkill.PointsLevel5 };
			skill.m_TierLocalizedBenefits = NameUtils.CreateLocalizedStrings(modSkill.EffectsLevel1, modSkill.EffectsLevel2, modSkill.EffectsLevel3, modSkill.EffectsLevel4, modSkill.EffectsLevel5);
			skill.m_TierLocalizedDescriptions = NameUtils.CreateLocalizedStrings(modSkill.DescriptionLevel1, modSkill.DescriptionLevel2, modSkill.DescriptionLevel3, modSkill.DescriptionLevel4, modSkill.DescriptionLevel5);

			GameManager.GetSkillsManager().InstantiateSkillPrefab(skill.gameObject);
		}

		internal static void MapSkills()
		{
			SkillsManager skillsManager = GameManager.GetSkillsManager();
			if (skillsManager is null) return;

			foreach (ModSkill eachModSkill in skills)
			{
				MapSkill(eachModSkill);
			}
		}

		internal static void RegisterSkill(ModSkill modSkill) => skills.Add(modSkill);
	}
}
