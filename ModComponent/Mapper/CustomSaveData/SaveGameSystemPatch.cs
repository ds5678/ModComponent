using HarmonyLib;
using ModComponentAPI;
using ModComponentMapper.SaveData;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(GearItem), "Deserialize")]//Exists
	internal static class GearItem_Deserialize
	{
		[HarmonyPriority(Priority.Last)]
		private static void Postfix(GearItem __instance)
		{
			UnhollowerBaseLib.Il2CppArrayBase<UnityEngine.Component> components = __instance.GetComponents<UnityEngine.Component>();
			foreach (UnityEngine.Component component in components)
			{
				ModSaveBehaviour modSaveBehaviour = component.TryCast<ModSaveBehaviour>();
				if (modSaveBehaviour == null)
				{
					continue;
				}
				try
				{
					string data = SaveDataManager.GetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType());
					modSaveBehaviour.Deserialize(data);
				}
				catch (System.Exception e)
				{
					Logger.Log("Deserializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
				}
			}
		}
	}

	[HarmonyPatch(typeof(GearItem), "Serialize")]//Exists
	internal static class GearItem_Serialize
	{
		[HarmonyPriority(Priority.Last)]
		private static void Prefix(GearItem __instance)
		{
			UnhollowerBaseLib.Il2CppArrayBase<UnityEngine.Component> components = __instance.GetComponents<UnityEngine.Component>();
			foreach (UnityEngine.Component component in components)
			{
				ModSaveBehaviour modSaveBehaviour = component.TryCast<ModSaveBehaviour>();
				if (modSaveBehaviour == null)
				{
					continue;
				}
				try
				{
					string data = modSaveBehaviour.Serialize();
					SaveDataManager.SetSaveData(__instance.m_InstanceID, modSaveBehaviour.GetType(), data);
				}
				catch (System.Exception e)
				{
					Logger.Log("Serializing custom save data for item {0} failed: {1}.", __instance.name, e.Message);
				}
			}
		}
	}

	[HarmonyPatch(typeof(SaveGameSystem), "LoadSceneData")]//Exists
	internal static class SaveGameSystemPatch_LoadSceneData
	{
		private static void Postfix()
		{
			SaveDataManager.Clear();
		}

		private static void Prefix(string name, string sceneSaveName)
		{
			string filename = sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX;
			string text = SaveGameSlots.LoadDataFromSlot(name, filename);
			SaveDataManager.Deserialize(text);
		}
	}

	[HarmonyPatch(typeof(SaveGameSystem), "RestoreGlobalData")]//Exists
	internal static class SaveGameSystemPatch_RestoreGlobalData
	{
		private static void Postfix()
		{
			//Logger.LogWarning("Clearing save data for RestoreGlobalData");
			SaveDataManager.Clear();
		}

		private static void Prefix(string name)
		{
			//return;
			//Logger.LogWarning("Loading data from slot '{0}' for scene '{1}'", name, "global");
			string filename = "global" + SaveDataManager.DATA_FILENAME_SUFFIX;
			//Logger.Log("Filename: '{0}'", filename);
			string text = SaveGameSlots.LoadDataFromSlot(name, filename);
			//if (text == null) Logger.LogError("Found no data in the slot!");
			//else Logger.Log(text);
			SaveDataManager.Deserialize(text);
		}
	}

	[HarmonyPatch(typeof(SaveGameSystem), "SaveGlobalData")]//Exists
	internal static class SaveGameSystemPatch_SaveGlobalData
	{
		private static void Postfix(SaveSlotType gameMode, string name)
		{
			//Logger.LogWarning("Saving data to slot '{0}' for scene '{1}' in '{2}' mode", name, "global", gameMode);
			string filename = "global" + SaveDataManager.DATA_FILENAME_SUFFIX;
			//Logger.Log("Filename: '{0}'", filename);
			string data = SaveDataManager.Serialize();
			//if (data != null) Logger.LogWarning(data);
			//else Logger.LogError("Data to save was null!");
			bool globalSuccess = SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, filename, data);
			//Logger.Log(globalSuccess.ToString());
			SaveDataManager.Clear();
		}

		private static void Prefix()
		{
			//Logger.LogWarning("Clearing save data for SaveGlobalData");
			SaveDataManager.Clear();
		}
	}

	[HarmonyPatch(typeof(SaveGameSystem), "SaveSceneData")]//Exists
	internal static class SaveGameSystemPatch_SaveSceneData
	{
		private static void Postfix(SaveSlotType gameMode, string name, string sceneSaveName)
		{
			//Logger.LogWarning("Saving data to slot '{0}' for scene '{1}' in '{2}' mode", name, sceneSaveName, gameMode);
			string filename = sceneSaveName + SaveDataManager.DATA_FILENAME_SUFFIX;
			//Logger.Log("Filename: '{0}'", filename);
			string data = SaveDataManager.Serialize();
			//if (data != null) Logger.LogWarning(data);
			//else Logger.LogError("Data to save was null!");
			bool sceneSuccess = SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, filename, data);
			//Logger.Log(sceneSuccess.ToString());
			SaveDataManager.Clear();
		}

		private static void Prefix(string name, string sceneSaveName)
		{
			//Logger.LogWarning("Clearing save data for SaveSceneData");
			SaveDataManager.Clear();
		}
	}
}