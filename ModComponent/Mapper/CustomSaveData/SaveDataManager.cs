using MelonLoader.TinyJSON;
using System;

//did a first pass through; has a conversion issue which I think I fixed
//does not need to be declared

namespace ModComponentMapper.SaveData
{
	public struct SaveProxy
	{
		public string data;
	}

	internal static class SaveDataManager
	{
		internal static string DATA_FILENAME_SUFFIX = "/ModComponent/CustomSaveData";

		private static SaveData saveData = new SaveData();

		public static void Clear()
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
					SaveProxy saveProxy = JSON.Load(data).Make<SaveProxy>();
					saveData = JSON.Load(saveProxy.data).Make<SaveData>();
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
			SaveProxy saveProxy = new SaveProxy
			{
				data = JSON.Dump(saveData)
			};

			return JSON.Dump(saveProxy);

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