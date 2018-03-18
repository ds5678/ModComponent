using Harmony;
using System.Collections.Generic;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(SkillsManager), "Deserialize")]
    public class SkillManager_Deserialize
    {
        public static bool Prefix(SkillsManager __instance, string text)
        {
            Dictionary<string, string> skillsData = Utils.DeserializeObject<Dictionary<string, string>>(text);

            foreach (var eachEntry in skillsData)
            {
                for (int i = 0; i < __instance.GetNumSkills(); i++)
                {
                    Skill skill = __instance.GetSkillFromIndex(i);
                    if (SerializableSkill.GetSkillSaveKey(skill) == eachEntry.Key)
                    {
                        ModUtils.ExecuteMethod(skill, "Deserialize", eachEntry.Value);
                        break;
                    }
                }
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(SkillsManager), "GetSkill")]
    public class SkillManager_GetSkill
    {
        public static bool Prefix(SkillsManager __instance, ref Skill __result, SkillType skillType)
        {
            List<Skill> m_Skills = ModUtils.GetFieldValue<List<Skill>>(__instance, "m_Skills");
            __result = m_Skills.Find(skill => skill.m_SkillType == skillType);
            return false;
        }
    }

    [HarmonyPatch(typeof(SkillsManager), "Serialize")]
    public class SkillManager_Serialize
    {
        public static bool Prefix(SkillsManager __instance, ref string __result)
        {
            Dictionary<string, string> skillsData = new Dictionary<string, string>();

            for (int i = 0; i < __instance.GetNumSkills(); i++)
            {
                Skill skill = __instance.GetSkillFromIndex(i);
                string data = ModUtils.ExecuteMethod<string>(skill, "Serialize");
                skillsData.Add(SerializableSkill.GetSkillSaveKey(skill), data);
            }

            __result = Utils.SerializeObject(skillsData);

            return false;
        }
    }
}