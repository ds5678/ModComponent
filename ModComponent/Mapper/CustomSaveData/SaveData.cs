using MelonLoader.TinyJSON;
using System;
using System.Collections.Generic;

namespace ModComponentMapper.SaveData
{
	internal class SaveData
	{
		private Dictionary<string, string> itemSaveData = new Dictionary<string, string>();

		public void Clear()
		{
			this.itemSaveData.Clear();
		}

		public string GetSaveData(int itemId, Type itemType)
		{
			this.itemSaveData.TryGetValue(GetKey(itemId, itemType), out string data);
			return data;
		}

		public void SetSaveData(int itemId, Type itemType, string data)
		{
			this.itemSaveData[GetKey(itemId, itemType)] = data;
		}

		private static string GetKey(int itemId, Type itemType)
		{
			return itemId + "_" + itemType.Name;
		}

		internal string DumpJson()
		{
			return JSON.Dump(this, EncodeOptions.NoTypeHints);
		}

		internal static SaveData ParseJson(string jsonText)
		{
			var result = new SaveData();
			var dict = MelonLoader.TinyJSON.JSON.Load(jsonText) as MelonLoader.TinyJSON.ProxyObject;
			result.itemSaveData = dict["itemSaveData"].Make<Dictionary<string, string>>();
			return result;
		}
	}
}