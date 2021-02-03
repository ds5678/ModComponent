using Harmony;
using System.Collections.Generic;

//did a first pass through; has a type issue
//ONE needs to be declared for MelonLoader
/*
namespace ModComponentMapper
{
    //need to modify since I modified the serialization patch
    //[HarmonyPatch(typeof(SkillsManager), "Deserialize")]//Exists
    //public class SkillManager_Deserialize
    //{
    //    public static bool Prefix(SkillsManager __instance, string text)
    //    {
    //        Dictionary<string, string> skillsData = Utils.DeserializeObject<Dictionary<string, string>>(text);

    //        foreach (var eachEntry in skillsData)
    //        {
    //            for (int i = 0; i < __instance.GetNumSkills(); i++)
    //            {
    //                Skill skill = __instance.GetSkillFromIndex(i);
    //                if (SerializableSkill.GetSkillSaveKey(skill) == eachEntry.Key)
    //                {
    //                    ModUtils.ExecuteMethod(skill, "Deserialize", eachEntry.Value);
    //                    break;
    //                }
    //            }
    //        }

    //        return false;
    //    }
    //}

    //new
    [HarmonyPatch(typeof(SkillsManager), "Deserialize")]//Exists
    public class SkillManager_Deserialize
    {
        public static bool Prefix(SkillsManager __instance, string text)
        {
            //SkillsDataObject skdata = Utils.DeserializeObject<SkillsDataObject>(text);
            Dictionary<string, string> skillsData = MelonLoader.TinyJSON.JSON.Load(text).Make<Dictionary<string, string>>();

            foreach (var eachEntry in skillsData)
            {
                for (int i = 0; i < __instance.GetNumSkills(); i++)
                {
                    Skill skill = __instance.GetSkillFromIndex(i);
                    
                    if (SerializableSkill.GetSkillSaveKey(skill) == eachEntry.Key)
                    {
                        __instance.m_Skills[i] = MelonLoader.TinyJSON.JSON.Load(eachEntry.Value).Make<Skill>();
                        //ModUtils.ExecuteMethod(skill, "Deserialize", eachEntry.Value);
                        break;
                    }
                }
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(SkillsManager), "GetSkill")]//Exists
    public class SkillManager_GetSkill
    {
        public static bool Prefix(SkillsManager __instance, ref Skill __result, SkillType skillType)
        {
            List<Skill> m_Skills = ModUtils.GetFieldValue<List<Skill>>(__instance, "m_Skills");
            __result = m_Skills.Find(skill => skill.m_SkillType == skillType);
            return false;
        }
    }

    //[HarmonyPatch(typeof(SkillsManager), "Serialize")]//Exists
    //public class SkillManager_Serialize
    //{
    //    public static bool Prefix(SkillsManager __instance, ref string __result)
    //    {
    //        Dictionary<string, string> skillsData = new Dictionary<string, string>();

    //        for (int i = 0; i < __instance.GetNumSkills(); i++)
    //        {
    //            Skill skill = __instance.GetSkillFromIndex(i);
    //            string data = ModUtils.ExecuteMethod<string>(skill, "Serialize");
    //            skillsData.Add(SerializableSkill.GetSkillSaveKey(skill), data);
    //        }

    //        __result = Utils.SerializeObject(skillsData); //<============================

    //        return false;
    //    }
    //}

    [HarmonyPatch(typeof(SkillsManager), "Serialize")]//Exists
    public class SkillManager_Serialize
    {
        public static bool Prefix(SkillsManager __instance, ref string __result)
        {
            Dictionary<string, string> skillsData = new Dictionary<string, string>();

            for (int i = 0; i < __instance.GetNumSkills(); i++)
            {
                Skill skill = __instance.GetSkillFromIndex(i);
                string data = MelonLoader.TinyJSON.JSON.Dump(skill);
                //string data = ModUtils.ExecuteMethod<string>(skill, "Serialize");
                skillsData.Add(SerializableSkill.GetSkillSaveKey(skill), data);
            }

            //__result = Utils.SerializeObject(skdata);
            __result = MelonLoader.TinyJSON.JSON.Dump(skillsData);

            return false;
        }
    }

    //I created this class to deal with the Serialization issue above; 
    //declared for MelonLoader, but no intptr constructor
    
    public class SkillsDataObject : Il2CppSystem.Object
    {
        public Dictionary<string, string> skillsData;

        public SkillsDataObject()
        {
            this.skillsData = new Dictionary<string, string>();
        }

        public SkillsDataObject(System.IntPtr intPtr) : base(intPtr) { }
    }
    
}

*/