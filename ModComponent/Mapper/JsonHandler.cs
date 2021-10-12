using System.Collections.Generic;

namespace ModComponent.Mapper
{
	internal static class JsonHandler
	{
		private static Dictionary<string, string> itemJsons = new Dictionary<string, string>();

		public static void RegisterJsonText(string itemName, string text)
		{
			if (string.IsNullOrEmpty(text)) return;

			if (itemJsons.ContainsKey(itemName))
			{
				Logger.Log("Overwriting data for {0}", itemName);
				itemJsons[itemName] = text;
			}
			else itemJsons.Add(itemName, text);
		}

		public static string GetJsonText(string itemName)
		{
			if (itemJsons.ContainsKey(itemName.ToLower()))
			{
				return itemJsons[itemName.ToLower()];
			}
			else
			{
				Logger.Log("Could not find {0} in json dictionary", itemName);
				return null;
			}
		}
	}
}
