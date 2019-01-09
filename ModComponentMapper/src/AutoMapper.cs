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

        internal static void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;

            string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string autoMapperDirectory = Path.Combine(modDirectory, AUTO_MAPPER_DIRECTORY_NAME);

            if (!Directory.Exists(autoMapperDirectory))
            {
                Implementation.Log("Directory '{0}' does not exist. Skipping ...", autoMapperDirectory);
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
            GameObject prefab = Resources.Load(prefabName) as GameObject;
            MapModComponent(prefab);
            MapBlueprint(prefab, assetBundlePath);
            MapSkill(prefab);
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
                    AutoMapPrefab(eachAssetName, relativePath);
                }
            }
        }

        private static void LoadDll(string relativePath)
        {
            Implementation.Log("Loading '{0}' ...", relativePath);

            string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string absolutePath = Path.Combine(modDirectory, relativePath);
            Assembly.LoadFrom(absolutePath);
        }

        private static void LoadSoundBank(string relativePath)
        {
            ModSoundBankManager.RegisterSoundBank(relativePath);
        }

        private static void MapSkill(GameObject prefab)
        {
            ModSkill modSkill = ModUtils.GetComponent<ModSkill>(prefab);
            if (modSkill != null)
            {
                Mapper.RegisterSkill(modSkill);
            }
        }

        private static void MapBlueprint(GameObject prefab, string sourcePath)
        {
            ModBlueprint modBlueprint = ModUtils.GetComponent<ModBlueprint>(prefab);
            if (modBlueprint != null)
            {
                Mapper.RegisterBlueprint(modBlueprint, sourcePath);
            }
        }

        private static void MapModComponent(GameObject prefab)
        {
            ModComponent modComponent = ModUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                return;
            }

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