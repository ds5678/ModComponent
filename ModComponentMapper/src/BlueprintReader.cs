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
                Implementation.Log("Blueprints directory '{0}' does not exist.", blueprintsDirectory);
                return;
            }

            string[] files = Directory.GetFiles(blueprintsDirectory, "*.json");
            foreach (string eachFile in files)
            {
                Implementation.Log("Processing blueprint definition '{0}'.", eachFile);
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