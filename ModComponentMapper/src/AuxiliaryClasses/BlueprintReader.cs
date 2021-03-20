using ModComponentAPI;
using System;
using System.Collections.Generic;
using System.IO;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    public class BlueprintReader
    {
        public const string BLUEPRINT_DIRECTORY_NAME = "blueprints";

        public static void Initialize()
        {
            ReadDefinitions();
        }

        internal static void ReadDefinitions()
        {
            string blueprintsDirectory = GetBlueprintsDirectory();
            if (!Directory.Exists(blueprintsDirectory))
            {
                Logger.Log("Blueprints directory '{0}' does not exist. Creating...", blueprintsDirectory);
                Directory.CreateDirectory(blueprintsDirectory);
                return;
            }

            ProcessFiles(blueprintsDirectory);
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
        private static void ProcessFiles(List<string> blueprintJsons)
        {
            foreach(string jsonText in blueprintJsons)
            {
                ProcessText(jsonText);
            }
            blueprintJsons.Clear();
        }

        private static string GetBlueprintsDirectory()
        {
            string modDirectory = Implementation.GetModsFolderPath();
            return Path.Combine(modDirectory, BLUEPRINT_DIRECTORY_NAME);
        }

        private static void ProcessFile(string path)
        {
            string text = File.ReadAllText(path);

            ProcessText(text , path);
        }
        private static void ProcessText(string text , string path = null)
        {
            try
            {
                ModBlueprint blueprint = MelonLoader.TinyJSON.JSON.Load(text).Make<ModBlueprint>();
                Mapper.RegisterBlueprint(blueprint, path);
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Could not read blueprint from '" + path + "'.", e);
            }
        }
    }
}