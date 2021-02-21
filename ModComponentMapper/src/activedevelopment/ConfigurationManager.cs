using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MelonLoader.TinyJSON;

namespace ModComponentMapper
{
    internal class ConfigurationManager
    {
        private const string CONFIGURATION_DIRECTORY_NAME = "configuration";
        private const string CONFIGURATION_FILE_NAME = "ModComponentConfiguration.json";

        internal static ConfigurationManager configurations = new ConfigurationManager();

        public bool debugMode = true;
        public bool alwaysSpawnItems = false;
        public float pilgramSpawnProbabilityMultiplier = 1f;
        public float voyagerSpawnProbabilityMultiplier = 0.6f;
        public float stalkerSpawnProbabilityMultiplier = 0.4f;
        public float interloperSpawnProbabilityMultiplier = 0.2f;
        public float storySpawnProbabilityMultiplier = 1f;
        public float challengeSpawnProbabilityMultiplier = 1f;

        public static string GetConfigurationFolder()
        {
            return Path.Combine(Implementation.GetModsFolderPath(), CONFIGURATION_DIRECTORY_NAME); ;
        }
        
        internal static void Initialize()
        {
            string configFolder = GetConfigurationFolder();
            string jsonPath = Path.Combine(configFolder, CONFIGURATION_FILE_NAME);
            if (!Directory.Exists(configFolder))
            {
                Logger.Log("'configuration' folder does not exist. Creating...");
                Directory.CreateDirectory(configFolder);
            }
            LoadConfigurations(jsonPath);
            SaveConfigurations(jsonPath);
        }

        private static void LoadConfigurations(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Logger.Log("Configuration file does not exist. Using default values...");
            }
            else
            {
                string text = File.ReadAllText(filepath);
                ProxyObject dict = JSON.Load(text) as ProxyObject;

                if (ComponentJson.ContainsKey(dict, "debugMode"))
                {
                    configurations.debugMode = dict["debugMode"];
                }
                if (ComponentJson.ContainsKey(dict, "alwaysSpawnItems"))
                {
                    configurations.alwaysSpawnItems = dict["alwaysSpawnItems"];
                }
                if (ComponentJson.ContainsKey(dict, "pilgramSpawnProbabilityMultiplier"))
                {
                    configurations.pilgramSpawnProbabilityMultiplier = dict["pilgramSpawnProbabilityMultiplier"];
                }
                if (ComponentJson.ContainsKey(dict, "voyagerSpawnProbabilityMultiplier"))
                {
                    configurations.voyagerSpawnProbabilityMultiplier = dict["voyagerSpawnProbabilityMultiplier"];
                }
                if (ComponentJson.ContainsKey(dict, "stalkerSpawnProbabilityMultiplier"))
                {
                    configurations.stalkerSpawnProbabilityMultiplier = dict["stalkerSpawnProbabilityMultiplier"];
                }
                if (ComponentJson.ContainsKey(dict, "interloperSpawnProbabilityMultiplier"))
                {
                    configurations.interloperSpawnProbabilityMultiplier = dict["interloperSpawnProbabilityMultiplier"];
                }
                if (ComponentJson.ContainsKey(dict, "storySpawnProbabilityMultiplier"))
                {
                    configurations.storySpawnProbabilityMultiplier = dict["storySpawnProbabilityMultiplier"];
                }
                if (ComponentJson.ContainsKey(dict, "challengeSpawnProbabilityMultiplier"))
                {
                    configurations.challengeSpawnProbabilityMultiplier = dict["challengeSpawnProbabilityMultiplier"];
                }
            }
        }

        private static void SaveConfigurations(string filepath)
        {
            string text = JSON.Dump(configurations,EncodeOptions.PrettyPrint);
            File.WriteAllText(filepath,text);
        }
    }
}
