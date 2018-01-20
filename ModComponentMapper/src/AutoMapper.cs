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
        private const string AUTO_MAPPER_DIRECTORY_NAME = "auto-mapped";

        public static void OnLoad()
        {
            string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string autoMapperDirectory = Path.Combine(modDirectory, AUTO_MAPPER_DIRECTORY_NAME);

            if (!Directory.Exists(autoMapperDirectory))
            {
                Log("Directory '{0}' does not exist. Skipping ...", autoMapperDirectory);
                return;
            }

            Log("Loading files from '{0}' ...", autoMapperDirectory);

            AutoMapDirectory(autoMapperDirectory, modDirectory);
        }

        private static void AutoMapDirectory(string directory, string modDirectory)
        {
            string[] directories = Directory.GetDirectories(directory);
            foreach (string eachDirectory in directories)
            {
                AutoMapDirectory(eachDirectory, modDirectory);
            }

            string[] files = Directory.GetFiles(directory);
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

                if (relativePath.ToLower().EndsWith(".dll"))
                {
                    LoadDll(eachFile);
                    continue;
                }

                Log("Ignoring '{0}' - Don't know how to handle this file type.", eachFile);
            }
        }

        private static void MapModComponent(GameObject prefab)
        {
            ModComponent modComponent = ModUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                Log("Ignoring prefab '{0}', because it does not contain a ModComponent", prefab.name);
                return;
            }

            MappedItem mappedItem = Mapper.Map(prefab);

            mappedItem.RegisterInConsole(ModUtils.DefaultIfEmpty(modComponent.ConsoleName, GetDefaultConsoleName(prefab.name)));

            foreach (ModLootTableEntryComponent eachLootTableEntry in modComponent.GetComponents<ModLootTableEntryComponent>())
            {
                mappedItem.AddToLootTable(eachLootTableEntry.LootTable, eachLootTableEntry.Weight);
                UnityEngine.Object.Destroy(eachLootTableEntry);
            }

            foreach (ModSpawnLocationComponent eachSpawnLocation in modComponent.GetComponents<ModSpawnLocationComponent>())
            {
                mappedItem.SpawnAt(eachSpawnLocation.Scene, eachSpawnLocation.Position, Quaternion.Euler(eachSpawnLocation.Rotation), eachSpawnLocation.SpawnChance);
                UnityEngine.Object.Destroy(eachSpawnLocation);
            }
        }

        private static void MapBluePrint(GameObject prefab)
        {
            ModBlueprint modBlueprint = ModUtils.GetComponent<ModBlueprint>(prefab);
            if(modBlueprint == null)
            {
                Log("Ignoring prefab '{0}', because it does not contain a BLUEPRINT", prefab.name);
                return;
            }
            // since whent he mod is laoded the blueprint object is not created yet we have to add all the blueprints to a list and load them once the game has started and the first set of blueprints loaded.
            Mapper.AddBluePrint(modBlueprint);

        }

        private static void AutoMapPrefab(string prefabName)
        {
            GameObject prefab = (GameObject)Resources.Load(prefabName);
            MapModComponent(prefab);
            MapBluePrint(prefab);
            
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
            ModAssetBundleManager.RegisterAssetBundle(relativePath);
            AssetBundle assetBundle = ModAssetBundleManager.GetAssetBundle(relativePath);

            string[] assetNames = assetBundle.GetAllAssetNames();
            foreach (string eachAssetName in assetNames)
            {
                if (eachAssetName.EndsWith(".prefab"))
                {
                    AutoMapPrefab(eachAssetName);
                }
            }
        }

        private static void LoadDll(string relativePath)
        {
            Log("Loading '{0}' ...", relativePath);

            string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string absolutePath = Path.Combine(modDirectory, relativePath);
            Assembly.LoadFrom(absolutePath);
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