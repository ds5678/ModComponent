namespace ModComponentMapper
{
    public static class SkillUtils
    {
        public static Skill GetSerializableSkillByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            SkillsManager skillsManager = GameManager.GetSkillsManager();
            if (skillsManager == null) return null;

            for (int i = 0; i < skillsManager.GetNumSkills(); i++)
            {
                Skill skill = skillsManager.GetSkillFromIndex(i);
                SerializableSkill serializableSkill = skill?.TryCast<SerializableSkill>();
                if (name == serializableSkill?.name) return skill;
            }

            return null;
        }

        public static Skill GetSkillByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            SkillsManager skillsManager = GameManager.GetSkillsManager();
            if (skillsManager == null) return null;

            for (int i = 0; i < skillsManager.GetNumSkills(); i++)
            {
                Skill skill = skillsManager.GetSkillFromIndex(i);
                if (name == skill.name) return skill;
            }

            return null;
        }
    }
}
