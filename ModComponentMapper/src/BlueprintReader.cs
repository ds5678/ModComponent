using ModComponentAPI;
using System;
using System.IO;
using System.Reflection;

namespace ModComponentMapper
{
    public class BlueprintReader
    {
        public static void Initialize()
        {
            ReadDefinitions();
        }

        internal static void ReadDefinitions()
        {
            string blueprintsDirectory = GetBlueprintsDirectory();
            if (!Directory.Exists(blueprintsDirectory))
            {
                LogUtils.Log("Blueprints directory '" + blueprintsDirectory + "' does not exist.");
                return;
            }

            string[] files = Directory.GetFiles(blueprintsDirectory, "*.json");
            foreach (string eachFile in files)
            {
                LogUtils.Log("Processing blueprint definition '" + eachFile + "'.");
                ProcessFile(eachFile);
            }
        }

        private static string GetBlueprintsDirectory()
        {
            string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(modDirectory, "blueprints");
        }

        private static void ProcessFile(string path)
        {
            string text = File.ReadAllText(path);

            try
            {
                ModBlueprint blueprint = FastJson.Deserialize<ModBlueprint>(text);
                Mapper.RegisterBlueprint(blueprint, path);
            }
            catch (Exception e)
            {
                throw new System.ArgumentException("Could not read blueprint from '" + path + "'.", e);
            }
        }
    }
}