using ModComponent.API;
using System;

namespace ModComponent.Mapper
{
	public static class BlueprintReader
	{
		internal static void ReadDefinitions()
		{
			foreach (var pair in JsonHandler.blueprintJsons)
			{
				Logger.Log("Processing blueprint definition '{0}'.", pair.Key);
				ProcessText(pair.Key, pair.Value);
			}
			JsonHandler.blueprintJsons.Clear();
		}

		private static void ProcessText(string path, string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				PackManager.SetItemPackNotWorking(path, "Skipping because blueprint text contains no information");
				return;
			}

			try
			{
				ModBlueprint blueprint = MelonLoader.TinyJSON.JSON.Load(text).Make<ModBlueprint>();
				if (!(blueprint == null)) BlueprintMapper.RegisterBlueprint(blueprint, path);
				else
				{
					PackManager.SetItemPackNotWorking(path, "Skipping because blueprint == null");
				}
			}
			catch (Exception e)
			{
				PackManager.SetItemPackNotWorking(path, $"Could not read blueprint from '{path}'. {e.Message}");
			}
		}
	}
}