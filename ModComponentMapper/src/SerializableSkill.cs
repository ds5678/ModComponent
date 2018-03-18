namespace ModComponentMapper
{
    public class SerializableSkill : Skill
    {
        public static LocalizedString[] CreateLocalizedStrings(string prefix)
        {
            LocalizedString[] result = new LocalizedString[5];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new LocalizedString() { m_LocalizationID = prefix + (i + 1) };
            }

            return result;
        }

        public static string GetSkillSaveKey(Skill skill)
        {
            if (skill.m_SkillType <= SkillType.ToolRepair)
            {
                return "m_" + skill.name + "Serialized";
            }

            return skill.name;
        }

        public void Deserialize(string text)
        {
            SkillData skillData = Utils.DeserializeObject<SkillData>(text);
            this.SetPoints(skillData.Points, SkillsManager.PointAssignmentMode.AssignInAnyMode);
        }

        public string Serialize()
        {
            SkillData skillData = new SkillData();
            skillData.Points = this.GetPoints();

            return Utils.SerializeObject(skillData);
        }
    }

    public class SkillData
    {
        public int Points;
    }
}