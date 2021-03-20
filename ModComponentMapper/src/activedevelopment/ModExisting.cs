using System.IO;
using UnityEngine;
using MelonLoader.TinyJSON;

namespace ModComponentMapper
{
    public static class ModExisting
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
            ProcessFilesFromZips();
        }

        private static void ProcessFiles(string directory)
        {
            if (!Directory.Exists(directory)) return;
            string[] directories = Directory.GetDirectories(directory);
            foreach (string eachDirectory in directories)
            {
                ProcessFiles(eachDirectory);
            }

            string[] files = Directory.GetFiles(directory, "*.json");
            foreach (string eachFile in files)
            {
                ProcessFile(eachFile);
            }
        }
        private static void ProcessFile(string filePath)
        {
            Logger.Log("Processing existing json definition '{0}'.", filePath);
            GameObject item = InitializeExisting(filePath);
            item.AddComponent<ModComponentAPI.ModGenericComponent>();
            Mapper.ConfigureBehaviours(item);
            UnityEngine.Object.DontDestroyOnLoad(item);
        }

        private static void ProcessFilesFromZips()
        {
            foreach (string jsonText in JsonHandler.existingJsons)
            {
                ProcessFileFromText(jsonText);
            }
            JsonHandler.existingJsons.Clear();
        }
        private static void ProcessFileFromText(string text)
        {
            Logger.Log("Processing existing json definition.");
            GameObject item = InitializeExistingFromText(text);
            item.AddComponent<ModComponentAPI.ModGenericComponent>();
            Mapper.ConfigureBehaviours(item);
            UnityEngine.Object.DontDestroyOnLoad(item);
        }

        private static GameObject InitializeExisting(string filepath) //only for existing jsons
        {
            if (string.IsNullOrEmpty(filepath))
            {
                Logger.Log("In InitializeComponents, the filepath was null or empty.");
                return null;
            }
            string data = File.ReadAllText(filepath);
            return InitializeExistingFromText(data);
        }
        private static GameObject InitializeExistingFromText(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            ProxyObject dict = JSON.Load(text) as ProxyObject;
            if (!ModUtils.ContainsKey(dict, "GearName"))
            {
                Logger.LogWarning("The JSON file doesn't contain the key: 'GearName'");
                return null;
            }
            UnityEngine.Object obj = Resources.Load(dict["GearName"]);
            if (obj == null)
            {
                Logger.LogWarning("Resources.Load could not find '{0}'", dict["GearName"]);
                return null;
            }
            GameObject gameObject = obj.Cast<GameObject>();
            if (gameObject == null)
            {
                Logger.LogWarning("The gameobject was null in InitializeExistingFromFile.");
                return null;
            }
            ComponentJson.InitializeComponents(ref gameObject, dict, true);
            ChangeWeight(ref gameObject, dict);
            return gameObject;
        }

        private static void ChangeWeight(ref GameObject item, ProxyObject dict)
        {
            GearItem gearItem = ModUtils.GetComponent<GearItem>(item);
            if (gearItem == null)
            {
                Logger.Log("Could not assign new weight. Item has no GearItem component.");
                return;
            }
            if (ModUtils.ContainsKey(dict, "WeightKG"))
            {
                gearItem.m_WeightKG = dict["WeightKG"];
            }
        }
    }
}
