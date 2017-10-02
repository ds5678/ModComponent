using AssetLoader;
using ModComponentAPI;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ModComponentMapper
{
    public class AutoMapper
    {
        private const string AUTO_MAPPER_DIRECTORY_NAME = "auto-mapper";

        public static void OnLoad()
        {
            string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string autoMapperDirectory = Path.Combine(modDirectory, AUTO_MAPPER_DIRECTORY_NAME);
            Log("Loading files from '{0}' ...", autoMapperDirectory);

            string[] files = Directory.GetFiles(autoMapperDirectory);
            foreach (string eachFile in files)
            {
                string relativePath = GetRelativePath(eachFile, modDirectory);

                if (relativePath.ToLower().EndsWith(".unity3d"))
                {
                    LoadAssetBundle(relativePath);
                    continue;
                }

                if (relativePath.ToLower().EndsWith(".bnk"))
                {
                    LoadSoundBank(relativePath);
                    continue;
                }

                Log("Ignoring '{0}' - Don't know how to handle this file type.", eachFile);
            }
        }

        private static void AutoMap(string prefabName)
        {
            GameObject prefab = (GameObject)Resources.Load(prefabName);

            AutoMapperComponent autoMapperComponent = ModUtils.GetComponent<AutoMapperComponent>(prefab);
            if (autoMapperComponent == null)
            {
                Log("Ignoring prefab '{0}', because it does not contain an AutoMapperComponent", prefabName);
                return;
            }

            Log(DumpData.Utils.FormatGameObject(prefab.name, prefab));

            Log("{0}: autoMapperComponent.Entries = {1}", prefabName, autoMapperComponent.Entries);

            //MappedItem mappedItem = Mapper.Map(prefab);

            //Log("ConsoleName for {0} = {1}", autoMapperComponent.name, autoMapperComponent.ConsoleName);
            //mappedItem.RegisterInConsole(ModUtils.DefaultIfEmpty(autoMapperComponent.ConsoleName, GetDefaultConsoleName(prefab.name)));

            //Log("Value for {0} = {1}", autoMapperComponent.name, autoMapperComponent.value);
            //if (autoMapperComponent.value != null)
            //{
            //    Log("Value.name for {0} = {1}", autoMapperComponent.name, autoMapperComponent.value.name);
            //}

            //Log("Values for {0} = {1}", autoMapperComponent.name, autoMapperComponent.values);
            //if (autoMapperComponent.values != null)
            //{
            //    Log("Values count for {0} = {1}", autoMapperComponent.name, autoMapperComponent.values.Length);
            //}


            //Log("LootTableWeights for {0} = {1}", autoMapperComponent.name, autoMapperComponent.LootTableWeights);
            //if (autoMapperComponent.LootTableWeights != null)
            //{
            //    foreach (LootTableWeight eachLootTableWeight in autoMapperComponent.LootTableWeights)
            //    {
            //        mappedItem.AddToLootTable(eachLootTableWeight.LootTable, eachLootTableWeight.Chance);
            //    }
            //}

            //Log("Spawn locations for {0} = {1}", autoMapperComponent.name, autoMapperComponent.SpawnLocations);
            //if (autoMapperComponent.SpawnLocations != null)
            //{
            //    //foreach (SpawnLocation eachSpawnLocation in autoMapperComponent.SpawnLocations)
            //    //{
            //    //    mappedItem.SpawnAt(eachSpawnLocation.Scene, eachSpawnLocation.Position, Quaternion.Euler(eachSpawnLocation.Rotation), eachSpawnLocation.Probability / 100f);
            //    //}
            //}
        }

        private static string GetDefaultConsoleName(string gameObjectName)
        {
            return gameObjectName.Replace("GEAR_", "");
        }

        private static string GetRelativePath(string file, string directory)
        {
            if (file.StartsWith(directory))
            {
                return file.Substring(directory.Length + 1);
            }

            throw new ArgumentException("Could not determine relative path of '" + file + "' to '" + directory + "'.");
        }

        private static void LoadAssetBundle(string relativePath)
        {
            AssetBundle assetBundle = ModAssetBundleManager.RegisterAssetBundle(relativePath);

            string[] assetNames = assetBundle.GetAllAssetNames();
            foreach (string eachAssetName in assetNames)
            {
                if (eachAssetName.EndsWith(".prefab"))
                {
                    AutoMap(eachAssetName);
                }
            }
        }

        private static void LoadSoundBank(string relativePath)
        {
            ModSoundBankManager.RegisterSoundBank(relativePath);
        }

        private static void Log(string message, params object[] parameters)
        {
            LogUtils.Log("AutoMapper", message, parameters);
        }
    }
}
