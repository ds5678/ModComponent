using ModComponentAPI;
using System;
using System.IO;
using System.Reflection;

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

            string[] images = Directory.GetFiles(directory, "*.png");
            foreach (string imagePath in images)
            {
                string imageName = AssetLoader.ModAssetBundleManager.GetAssetName(imagePath);
                string gearName = NameUtils.AddGearPrefix( NameUtils.RemoveCraftingIconPrefix(imageName) );
                CraftingIconManager.RegisterIcon(gearName, imagePath);
            }
        }

        private static string GetBlueprintsDirectory()
        {
            string modDirectory = Implementation.GetModsFolderPath();
            return Path.Combine(modDirectory, BLUEPRINT_DIRECTORY_NAME);
        }

        private static void ProcessFile(string path)
        {
            string text = File.ReadAllText(path);

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