using System;

namespace ModComponentMapper.SaveData
{
	internal static class SaveDataManager
	{
		internal static string DATA_FILENAME_SUFFIX = "/ModComponent/CustomSaveData";

		private static SaveData saveData = new SaveData();

		internal static void Clear()
		{
			saveData.Clear();
		}

		public static void Deserialize(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				saveData = new SaveData();
			}
			else
			{
				try
				{
					SaveProxy saveProxy = SaveProxy.ParseJson(data);
					saveData = SaveData.ParseJson(saveProxy.data);
				}
				catch
				{
					Logger.LogWarning("Save Data was in an invalid format");
					saveData = new SaveData();
				}
			}
		}

		public static string Serialize()
		{
			SaveProxy saveProxy = new SaveProxy(saveData.DumpJson());

			return saveProxy.DumpJson();
		}

		public static string GetSaveData(int itemId, Type itemType)
		{
			return saveData.GetSaveData(itemId, itemType);
		}

		public static void SetSaveData(int itemId, Type itemType, string data)
		{
			saveData.SetSaveData(itemId, itemType, data);
		}
	}
}