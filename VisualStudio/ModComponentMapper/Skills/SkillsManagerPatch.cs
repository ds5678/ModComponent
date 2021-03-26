using Harmony;
using System.Collections.Generic;

namespace ModComponentMapper
{
    /*[HarmonyPatch(typeof(SkillsManager), "Deserialize")]//Exists
    public class SkillManager_Deserialize
    {
        public static void Postfix(SkillsManager __instance,ref string text)
        {
            //SkillsDataObject skdata = Utils.DeserializeObject<SkillsDataObject>(text);
            Dictionary<string, string> skillsData = MelonLoader.TinyJSON.JSON.Load(text).Make<Dictionary<string, string>>();

            foreach (var eachEntry in skillsData)
            {
                for (int i = 0; i < __instance.GetNumSkills(); i++)
                {
                    SerializableSkill skill = __instance.GetSkillFromIndex(i)?.TryCast<SerializableSkill>();
                    
                    if (SerializableSkill.GetSkillSaveKey(skill) == eachEntry.Key)
                    {
                        //__instance.m_Skills[i] = MelonLoader.TinyJSON.JSON.Load(eachEntry.Value).Make<Skill>();
                        skill.Deserialize(eachEntry.Value);
                        break;
                    }
                }
            }
            //Logger.Log("Deserializing skills");
            //Dictionary<string, string> dict = JSON.Load(text)?.Make<Dictionary<string, string>>();
            //dict?.Add("Guitar", "All the points");
            //string newText = JSON.Dump(dict);
            //Logger.Log(newText);
            //text = newText;
            //return false;
            //return true;
        }
    }*/

    /*[HarmonyPatch(typeof(SkillsManager), "GetSkill")]//Exists
    public class SkillManager_GetSkill
    {
        public static bool Prefix(SkillsManager __instance, ref Skill __result, SkillType skillType)
        {
            //List<Skill> m_Skills = ModUtils.GetFieldValue<List<Skill>>(__instance, "m_Skills");
            //__result = m_Skills.Find(skill => skill.m_SkillType == skillType);
            //return false;
            Logger.Log("Getting '{0}' skill", skillType.ToString());
            return true;
        }
    }*/

    /*[HarmonyPatch(typeof(SkillsManager), "Serialize")]//Exists
    public class SkillManager_Serialize
    {
        public static void Postfix(SkillsManager __instance, ref string __result)
        {
            Dictionary<string, string> skillsData = JSON.Load(__result)?.Make<Dictionary<string, string>>();
            if (skillsData == null) return;

            for (int i = 0; i < __instance.GetNumSkills(); i++)
            {
                SerializableSkill skill = __instance.GetSkillFromIndex(i)?.TryCast<SerializableSkill>();
                if (skill) 
                {
                    string skillID = SerializableSkill.GetSkillSaveKey(skill);
                    if (skillsData.ContainsKey(skillID))
                    {
                        Logger.LogWarning("In skills manager serialization, the data for '{0}' is being overwritten.",skillID);
                        skillsData[skillID] = skill.Serialize();
                    }
                    else skillsData.Add(skillID, skill.Serialize()); 
                }
            }

            __result = MelonLoader.TinyJSON.JSON.Dump(skillsData);

            //return false;
            //Logger.Log("Serializing skills");
            //Dictionary<string, string> dict = JSON.Load(__result)?.Make<Dictionary<string, string>>();
            //dict?.Add("Guitar", "All the points");
            //string newText = JSON.Dump(dict);
            //Logger.Log(newText);
            //return true;
        }

    }*/

    public class SkillsDataObject
    {
        public Dictionary<string, string> skillsData;

        public SkillsDataObject()
        {
            this.skillsData = new Dictionary<string, string>();
        }
    }

}
