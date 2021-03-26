using Harmony;
using ModComponentAPI;
using ModComponentMapper.SaveData;

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(GearItem), "Deserialize")]//Exists
    public class GearItem_Deserialize
    {
        [HarmonyPriority(Priority.Last)]
        public static void Postfix(GearItem __instance)
        {
            //UnhollowerBaseLib.Il2CppReferenceArray<UnityEngine.Component> components = __instance.GetComponents(UnhollowerRuntimeLib.Il2CppType.Of<UnityEngine.Component>());
            UnhollowerBaseLib.Il2CppArrayBase<UnityEngine.Component> components = __instance.GetComponents<UnityEngine.Component>();
            //UnhollowerBaseLib.Il2CppArrayBase<ModSaveBehaviour> modSaveComponents = __instance.GetComponents<ModSaveBehaviour>();
            foreach (UnityEngine.Component component in components)
            //foreach (ModSaveBehaviour modSaveBehaviour in modSaveComponents)
            {
                ModSaveBehaviour modSaveBehaviour = component.TryCast<ModSaveBehaviour>();
                if (modSaveBehaviour == null)
                {
                    continue;
                }
                //Logger.Log("Found ModSaveBaviour");
                try
                {
                    string data = SaveDataManager.GetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType());
                    //if (data == null) MelonLoader.MelonLogger.Log("Null save data for {0} of type {1}", modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
                    //if (!string.IsNullOrEmpty(data)) MelonLoader.MelonLogger.Log(data);
                    modSaveBehaviour.Deserialize(data);
                    //MelonLoader.MelonLogger.Log("Deserialized custom save data for {0} of type {1}", modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
                }
                catch (System.Exception e)
                {
                    Logger.Log("Deserializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
                }
            }
            /*ModSaveBehaviour modSaveBehaviour = __instance.GetComponent<ModSaveBehaviour>();
            if (modSaveBehaviour == null)
            {
                return;
            }

            try
            {
                string data = SaveDataManager.GetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType());
                if (data == null) MelonLoader.MelonLogger.Log("Null save data for {0} of type {1}", modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
                if (!string.IsNullOrEmpty(data)) MelonLoader.MelonLogger.Log(data);
                modSaveBehaviour.Deserialize(data);
                MelonLoader.MelonLogger.Log("Deserialized custom save data for {0} of type {1}",modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
            }
            catch (System.Exception e)
            {
                Logger.Log("Deserializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
            }*/
        }
    }

    [HarmonyPatch(typeof(GearItem), "Serialize")]//Exists
    public class GearItem_Serialize
    {
        [HarmonyPriority(Priority.Last)]
        public static void Prefix(GearItem __instance)
        {
            UnhollowerBaseLib.Il2CppArrayBase<UnityEngine.Component> components = __instance.GetComponents<UnityEngine.Component>();
            foreach (UnityEngine.Component component in components)
            {
                ModSaveBehaviour modSaveBehaviour = component.TryCast<ModSaveBehaviour>();
                if (modSaveBehaviour == null)
                {
                    continue;
                }
                //Logger.Log("Found ModSaveBaviour");
                try
                {
                    string data = modSaveBehaviour.Serialize();
                    //if (data == null) MelonLoader.MelonLogger.Log("Null save data for {0} of type {1}", modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
                    //if (!string.IsNullOrEmpty(data)) MelonLoader.MelonLogger.Log(data);
                    SaveDataManager.SetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType(), data);
                    //MelonLoader.MelonLogger.Log("Serialized custom save data for {0} of type {1}", modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
                }
                catch (System.Exception e)
                {
                    Logger.Log("Serializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
                }
            }

            /*ModSaveBehaviour modSaveBehaviour = __instance.GetComponent<ModSaveBehaviour>();
            if (modSaveBehaviour == null)
            {
                return;
            }

            try
            {
                string data = modSaveBehaviour.Serialize();
                if (data == null) MelonLoader.MelonLogger.Log("Null save data for {0} of type {1}", modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
                if (!string.IsNullOrEmpty(data)) MelonLoader.MelonLogger.Log(data);
                SaveDataManager.SetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType(), data);
                MelonLoader.MelonLogger.Log("Serialized custom save data for {0} of type {1}", modSaveBehaviour.name, modSaveBehaviour.GetType().ToString());
            }
            catch (System.Exception e)
            {
                Logger.Log("Serializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
            }*/
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "LoadSceneData")]//Exists
    public class SaveGameSystemPatch_LoadSceneData
    {
        public static void Postfix()
        {
            //MelonLoader.MelonLogger.LogWarning("Clearing save data for LoadSceneData");
            SaveDataManager.Clear();
        }

        public static void Prefix(string name, string sceneSaveName)
        {
            //return;
            //MelonLoader.MelonLogger.LogWarning("Loading data from slot '{0}' for scene '{1}'", name, sceneSaveName);
            string filename = sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX;
            //MelonLoader.MelonLogger.Log("Filename: '{0}'",filename);
            //string text = SaveGameSlots.LoadDataFromSlot(name, sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX);
            string text = SaveGameSlots.LoadDataFromSlot(name, filename);
            //if (text == null) MelonLoader.MelonLogger.LogError("Found no data in the slot!");
            //else MelonLoader.MelonLogger.Log(text);
            SaveDataManager.Deserialize(text);
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "RestoreGlobalData")]//Exists
    public class SaveGameSystemPatch_RestoreGlobalData
    {
        public static void Postfix()
        {
            //MelonLoader.MelonLogger.LogWarning("Clearing save data for RestoreGlobalData");
            SaveDataManager.Clear();
        }

        public static void Prefix(string name)
        {
            //return;
            //MelonLoader.MelonLogger.LogWarning("Loading data from slot '{0}' for scene '{1}'", name, "global");
            string filename = "global" + SaveDataManager.DATA_FILENAME_SUFFIX;
            //MelonLoader.MelonLogger.Log("Filename: '{0}'", filename);
            string text = SaveGameSlots.LoadDataFromSlot(name, filename);
            //if (text == null) MelonLoader.MelonLogger.LogError("Found no data in the slot!");
            //else MelonLoader.MelonLogger.Log(text);
            SaveDataManager.Deserialize(text);
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "SaveGlobalData")]//Exists
    public class SaveGameSystemPatch_SaveGlobalData
    {
        public static void Postfix(SaveSlotType gameMode, string name)
        {
            //MelonLoader.MelonLogger.LogWarning("Saving data to slot '{0}' for scene '{1}' in '{2}' mode", name, "global", gameMode);
            string filename = "global" + SaveDataManager.DATA_FILENAME_SUFFIX;
            //MelonLoader.MelonLogger.Log("Filename: '{0}'", filename);
            string data = SaveDataManager.Serialize();
            //if (data != null) MelonLoader.MelonLogger.LogWarning(data);
            //else MelonLoader.MelonLogger.LogError("Data to save was null!");
            bool globalSuccess = SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, filename, data);
            //Logger.Log(globalSuccess.ToString());
            SaveDataManager.Clear();
        }

        public static void Prefix()
        {
            //MelonLoader.MelonLogger.LogWarning("Clearing save data for SaveGlobalData");
            SaveDataManager.Clear();
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "SaveSceneData")]//Exists
    public class SaveGameSystemPatch_SaveSceneData
    {
        public static void Postfix(SaveSlotType gameMode, string name, string sceneSaveName)
        {
            //MelonLoader.MelonLogger.LogWarning("Saving data to slot '{0}' for scene '{1}' in '{2}' mode", name, sceneSaveName, gameMode);
            string filename = sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX;
            //MelonLoader.MelonLogger.Log("Filename: '{0}'", filename);
            string data = SaveDataManager.Serialize();
            //if (data != null) MelonLoader.MelonLogger.LogWarning(data);
            //else MelonLoader.MelonLogger.LogError("Data to save was null!");
            //SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX, data);
            bool sceneSuccess = SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, filename, data);
            //Logger.Log(sceneSuccess.ToString());
            SaveDataManager.Clear();
            //SaveGameSlots.WriteSlotToDisk(name);
            //SaveGameSystem.SaveGlobalData(gameMode, name);
        }

        public static void Prefix(string name, string sceneSaveName)
        {
            //MelonLoader.MelonLogger.LogWarning("Clearing save data for SaveSceneData");
            //SaveGameSlots.DeleteFileFromSlot(name, sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX);
            SaveDataManager.Clear();
        }
    }
}