using AssetLoader;
using ModComponentAPI;
using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;
using MelonLoader;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    public class AutoMapper
    {
        private const string AUTO_MAPPER_DIRECTORY_NAME = "auto-mapped";

        public static string GetAutoMapperDirectory()
        {
            return Path.Combine(Implementation.GetModsFolderPath(), AUTO_MAPPER_DIRECTORY_NAME);
        }

        internal static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;

            string modDirectory = Implementation.GetModsFolderPath();
            string autoMapperDirectory = GetAutoMapperDirectory();

            if (!Directory.Exists(autoMapperDirectory))
            {
                Implementation.Log("Directory '{0}' does not exist. Creating ...", autoMapperDirectory);
                Directory.CreateDirectory(autoMapperDirectory);
                return;
            }

            Implementation.Log("Loading files from '{0}' ...", autoMapperDirectory);

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

                if (relativePath.ToLower().EndsWith(".json"))
                {
                    continue;
                }

                Implementation.Log("Found '{0}'", eachFile);
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

                Implementation.Log("Ignoring '{0}' - Don't know how to handle this file type.", eachFile);
            }
        }

        private static void AutoMapPrefab(string prefabName, string assetBundlePath)
        {
            //Implementation.Log("AutoMapPrefab called");
            UnityEngine.Object obj = Resources.Load(prefabName);
            if(obj == null)
            {
                Implementation.Log("In AutoMapPrefab, Resources.Load returned null.");
            }
            GameObject prefab = obj.Cast<GameObject>();
            MapModComponent(prefab);
            MapBlueprint(prefab, assetBundlePath);
            //MapSkill(prefab);
        }

        private static string GetDefaultConsoleName(string gameObjectName)
        {
            return gameObjectName.Replace("GEAR_", "");
        }

        public static string GetRelativePath(string file, string directory)
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
                Implementation.Log(eachAssetName);
                if (eachAssetName.EndsWith(".prefab"))
                {
                    AutoMapPrefab(eachAssetName, relativePath);
                }
                if (eachAssetName.EndsWith(".png"))
                {
                    //Implementation.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    string assetName = AssetLoader.ModAssetBundleManager.GetAssetName(eachAssetName);
                    string mappedName = AssetLoader.ModAssetBundleManager.getAssetMappedName(eachAssetName, assetName);
                    Texture2D texture = assetBundle.LoadAsset(mappedName).Cast<Texture2D>();
                    Utils.CacheTexture(mappedName, texture);
                }
            }
        }

        private static void LoadDll(string relativePath)
        {
            Implementation.Log("Loading '{0}' ...", relativePath);

            string modDirectory = Implementation.GetModsFolderPath();
            string absolutePath = Path.Combine(modDirectory, relativePath);
            Assembly.LoadFrom(absolutePath);
        }

        private static void LoadSoundBank(string relativePath)
        {
            ModSoundBankManager.RegisterSoundBank(relativePath);
        }

        /*private static void MapSkill(GameObject prefab)
        {
            ModSkill modSkill = ModUtils.GetComponent<ModSkill>(prefab);
            if (modSkill != null)
            {
                Mapper.RegisterSkill(modSkill);
            }
        }*/

        private static void MapBlueprint(GameObject prefab, string sourcePath)
        {
            ModBlueprint modBlueprint = ModUtils.GetComponent<ModBlueprint>(prefab);
            if (modBlueprint != null)
            {
                Mapper.RegisterBlueprint(modBlueprint, sourcePath);
            }
        }

        
        internal static void MapModComponent(GameObject prefab)
        {
            //Implementation.Log("MapModComponent Called");
            if(prefab == null)
            {
                Implementation.Log("Prefab is null.");
            }
            ComponentJson.InitializeComponents(ref prefab);
            ModComponent modComponent = ModUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                Implementation.Log("In MapModComponent, the mod component from the prefab was null.");
                return;
            }
            //Implementation.Log("mapping from mapmodcomponent");
            Mapper.Map(prefab);
        }

        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            AssemblyName requestedAssemblyName = new AssemblyName(args.Name);

            Assembly modComponentMapper = Assembly.GetExecutingAssembly();
            if (IsCompatible(requestedAssemblyName, modComponentMapper.GetName()))
            {
                Implementation.Log("Redirecting load attempt for " + requestedAssemblyName + " to " + modComponentMapper.GetName());
                return Assembly.GetExecutingAssembly();
            }

            Assembly modComponentAPI = typeof(ModComponent).Assembly;
            if (IsCompatible(requestedAssemblyName, modComponentAPI.GetName()))
            {
                Implementation.Log("Redirecting load attempt for " + requestedAssemblyName + " to " + modComponentAPI.GetName());
                return modComponentAPI;
            }

            return null;
        }

        private static bool IsCompatible(AssemblyName requested, AssemblyName available)
        {
            if (requested.Name != available.Name || requested.Version.Major != available.Version.Major)
            {
                return false;
            }

            return requested.Version.CompareTo(available.Version) <= 0;
        }
    }
}