
//did a first pass through; HAS AN ISSUE!!!!!!!!!!!!!!!!
//TWO need to be declared
/*
namespace ModComponentMapper
{
    public class SerializableSkill : Skill //needs to be declared
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
            if(skill == null)
            {
                Implementation.Log("GetSkillSaveKey returned null");
                return null;
            }
            if (skill.m_SkillType <= SkillType.ToolRepair)
            {
                return "m_" + skill.name + "Serialized";
            }

            return skill.name;
        }

        public void Deserialize(string text)
        {
            //SkillData skillData = Utils.DeserializeObject<SkillData>(text);
            SkillData skillData = MelonLoader.TinyJSON.JSON.Load(text).Make<SkillData>();
            this.SetPoints(skillData.Points, SkillsManager.PointAssignmentMode.AssignInAnyMode);
        }

        public string Serialize()
        {
            SkillData skillData = new SkillData();
            skillData.Points = this.GetPoints();

            //return Utils.SerializeObject(skillData); //<======================================================
            return MelonLoader.TinyJSON.JSON.Dump(skillData);
        }
    }

    //NEEDS TO BE DECLARED !!!!!!!!!!!!!!!!!!!!!!!!!
    public class SkillData : Il2CppSystem.Object //added inheritance to fix SkillData serialization issue
    {
        public int Points;
    }
}
*/