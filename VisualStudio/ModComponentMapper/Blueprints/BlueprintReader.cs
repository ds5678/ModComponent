using ModComponentAPI;
using ModComponentMapper.InformationMenu;
using System;
using System.Collections.Generic;
using System.IO;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
	public static class BlueprintReader
	{
		public const string BLUEPRINT_DIRECTORY_NAME = "blueprints";

		public static void Initialize()
		{
			ReadDefinitions();
		}

		internal static void ReadDefinitions()
		{
#if DEBUG
			string blueprintsDirectory = GetBlueprintsDirectory();
			if (!Directory.Exists(blueprintsDirectory))
			{
				Logger.Log("Auxiliary Blueprints directory '{0}' does not exist. Creating...", blueprintsDirectory);
				Directory.CreateDirectory(blueprintsDirectory);
			}
			ProcessFiles(blueprintsDirectory);
#endif
			ProcessFiles(JsonHandler.blueprintJsons);
		}

		private static void ProcessFiles(string directory)
		{
			if (!Directory.Exists(directory))
			{
				return;
			}
			string[] directories = Directory.GetDirectories(directory);
			foreach (string eachDirectory in directories)
			{
				ProcessFiles(eachDirectory);
			}

			string[] files = Directory.GetFiles(directory, "*.json");
			foreach (string eachFile in files)
			{
				Logger.Log("Processing blueprint definition '{0}'.", eachFile);
				ProcessFile(eachFile);
			}
		}
		private static void ProcessFiles(Dictionary<string, string> blueprintJsons)
		{
			//Logger.Log(MelonLoader.TinyJSON.JSON.Dump(blueprintJsons,MelonLoader.TinyJSON.EncodeOptions.PrettyPrint));
			foreach (var pair in blueprintJsons)
			{
				Logger.Log("Processing blueprint definition '{0}'.", pair.Key);
				ProcessText(pair.Key, pair.Value);
			}
			blueprintJsons.Clear();
		}

		private static string GetBlueprintsDirectory()
		{
			string modDirectory = ModComponentMain.Implementation.GetModsFolderPath();
			return Path.Combine(modDirectory, BLUEPRINT_DIRECTORY_NAME);
		}

		private static void ProcessFile(string path)
		{
			string text = File.ReadAllText(path);

			ProcessText(path, text);
		}
		private static void ProcessText(string path, string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				Logger.LogError("Skipping because blueprint text contains no information");
				PageManager.SetItemPackNotWorking(path);
				return;
			}
			//Logger.Log(path);
			//Logger.Log("\n"+text);
			try
			{
				ModBlueprint blueprint = MelonLoader.TinyJSON.JSON.Load(text).Make<ModBlueprint>();
				if (!(blueprint is null)) BlueprintMapper.RegisterBlueprint(blueprint, path);
				else
				{
					Logger.LogError("Skipping because blueprint is null");
					PageManager.SetItemPackNotWorking(path);
				}
			}
			catch (Exception e)
			{
				Logger.LogError("Could not read blueprint from '{0}'. {1}", path, e.Message);
				PageManager.SetItemPackNotWorking(path);
			}
		}
	}
}