namespace ModComponent.Mapper;

internal static class JsonHandler
{
	private static readonly Dictionary<string, string> itemJsons = new();

	public static void RegisterJsonText(string itemName, string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}

		if (itemJsons.ContainsKey(itemName))
		{
			Logger.Log($"Overwriting data for {itemName}");
			itemJsons[itemName] = text;
		}
		else
		{
			itemJsons.Add(itemName, text);
		}
	}

	public static string GetJsonText(string itemName)
	{
		return itemJsons.TryGetValue(itemName.ToLower(), out string jsonData)
			? jsonData
			: throw new System.Exception($"Could not find json file for {itemName}");
	}
}
