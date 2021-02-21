using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModComponentMapper
{
    public class ModExisting
    {
        public const string EXISTING_JSON_DIRECTORY_NAME = "existing-json";
        private static bool isInitialized = false;

        public static string GetExistingJsonDirectory()
        {
            return Path.Combine(Implementation.GetModsFolderPath(), EXISTING_JSON_DIRECTORY_NAME);
        }

        public static void Initialize()
        {
            ReadDefinitions();
            isInitialized = true;
        }

        public static bool IsInitialized()
        {
            return isInitialized;
        }

        private static void ReadDefinitions()
        {
            string existingJsonDirectory = GetExistingJsonDirectory();
            if (!Directory.Exists(existingJsonDirectory))
            {
                Logger.Log("Existing Json directory '{0}' does not exist. Creating...", existingJsonDirectory);
                Directory.CreateDirectory(existingJsonDirectory);
            }
            else
            {
                ProcessFiles(existingJsonDirectory);
            }
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
                Logger.Log("Processing existing json definition '{0}'.", eachFile);
                GameObject item = ComponentJson.InitializeComponents(eachFile);
                item.AddComponent<ModComponentAPI.ModGenericComponent>();
                Mapper.ConfigureBehaviours(item);
                UnityEngine.Object.DontDestroyOnLoad(item);
                Logger.Log("Reconfigured '{0}'", item.name);
            }
        }
    }
}
