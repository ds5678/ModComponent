using Harmony;
using ModComponentAPI;
using ModComponentMapper.SaveData;

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(GearItem), "Deserialize")]
    public class GearItem_Deserialize
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(GearItem __instance)
        {
            ModSaveBehaviour modSaveBehaviour = __instance.GetComponent<ModSaveBehaviour>();
            if (modSaveBehaviour == null)
            {
                return;
            }

            try
            {
                string data = SaveDataManager.GetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType());
                modSaveBehaviour.Deserialize(data);
            }
            catch (System.Exception e)
            {
                Implementation.Log("Deserializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
            }
        }
    }

    [HarmonyPatch(typeof(GearItem), "Serialize")]
    public class GearItem_Serialize
    {
        [HarmonyPriority(Priority.Last)]
        public static void Prefix(GearItem __instance)
        {
            ModSaveBehaviour modSaveBehaviour = __instance.GetComponent<ModSaveBehaviour>();
            if (modSaveBehaviour == null)
            {
                return;
            }

            try
            {
                string data = modSaveBehaviour.Serialize();
                SaveDataManager.SetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType(), data);
            }
            catch (System.Exception e)
            {
                Implementation.Log("Serializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
            }
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "LoadSceneData")]
    public class SaveGameSystemPatch_LoadSceneData
    {
        public static void Postfix()
        {
            SaveDataManager.Clear();
        }

        public static void Prefix(string name, string sceneSaveName)
        {
            string text = SaveGameSlots.LoadDataFromSlot(name, sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX);
            SaveDataManager.Deserialize(text);
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "RestoreGlobalData")]
    public class SaveGameSystemPatch_RestoreGlobalData
    {
        public static void Postfix()
        {
            SaveDataManager.Clear();
        }

        public static void Prefix(string name)
        {
            string text = SaveGameSlots.LoadDataFromSlot(name, "global" + SaveDataManager.DATA_FILENAME_SUFFIX);
            SaveDataManager.Deserialize(text);
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "SaveGlobalData")]
    public class SaveGameSystemPatch_SaveGlobalData
    {
        public static void Postfix(SaveSlotType gameMode, string name)
        {
            string data = SaveDataManager.Serialize();
            SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, "global" + SaveDataManager.DATA_FILENAME_SUFFIX, data);
            SaveDataManager.Clear();
        }

        public static void Prefix()
        {
            SaveDataManager.Clear();
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "SaveSceneData")]
    public class SaveGameSystemPatch_SaveSceneData
    {
        public static void Postfix(SaveSlotType gameMode, string name, string sceneSaveName)
        {
            string data = SaveDataManager.Serialize();
            SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX, data);
            SaveDataManager.Clear();
        }

        public static void Prefix()
        {
            SaveDataManager.Clear();
        }
    }
}