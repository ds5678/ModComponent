using AssetLoader;
using ModComponentAPI;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
	public static class AutoMapper
	{
		private const string AUTO_MAPPER_DIRECTORY_NAME = "auto-mapped";

		public static string GetAutoMapperDirectory() => Path.Combine(ModComponentMain.Implementation.GetModsFolderPath(), AUTO_MAPPER_DIRECTORY_NAME);

		internal static void Initialize()
		{
			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
#if DEBUG
            JsonHandler.RegisterDirectory(AutoMapper.GetAutoMapperDirectory());
            string modDirectory = ModComponentMain.Implementation.GetModsFolderPath();
            string autoMapperDirectory = GetAutoMapperDirectory();
            if (Directory.Exists(autoMapperDirectory))
            {
                Logger.Log("Loading files from '{0}' ...", autoMapperDirectory);
                AutoMapDirectory(autoMapperDirectory, modDirectory);
            }
            else
            {
                Logger.Log("Directory '{0}' does not exist. Creating ...", autoMapperDirectory);
                Directory.CreateDirectory(autoMapperDirectory);
                return;
            }
#endif
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
				string relativePath = FileUtils.GetRelativePath(eachFile, modDirectory);

				if (relativePath.ToLower().EndsWith(".json")) continue;

				Logger.Log("Found '{0}'", eachFile);
				if (relativePath.ToLower().EndsWith(".unity3d"))
				{
					RegisterAssetBundle(relativePath);
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

				Logger.Log("Ignoring '{0}' - Don't know how to handle this file type.", eachFile);
			}
		}

		private static void AutoMapPrefab(string prefabName)
		{
			//Logger.Log("AutoMapPrefab called");
			GameObject prefab = Resources.Load(prefabName)?.Cast<GameObject>();
			if (prefab is null)
			{
				throw new System.NullReferenceException("In AutoMapper.AutoMapPrefab, Resources.Load did not return a GameObject.");
			}
			if (prefab.name.StartsWith("GEAR_")) MapModComponent(prefab);
			//MapBlueprint(prefab, assetBundlePath);
			if (prefab.name.StartsWith("SKILL_")) MapSkill(prefab);
		}



		private static void RegisterAssetBundle(string relativePath)
		{
			ModAssetBundleManager.RegisterAssetBundle(relativePath);
			AssetBundleManager.Add(relativePath);
		}
		internal static void LoadAssetBundle(string relativePath)
		{
			LoadAssetBundle(ModAssetBundleManager.GetAssetBundle(relativePath));
		}
		internal static void LoadAssetBundle(AssetBundle assetBundle)
		{
			string[] assetNames = assetBundle.GetAllAssetNames();
			foreach (string eachAssetName in assetNames)
			{
				//Logger.Log(eachAssetName);
				if (eachAssetName.EndsWith(".prefab"))
				{
					AutoMapPrefab(eachAssetName);
				}
			}
		}

		private static void LoadDll(string relativePath)
		{
			Logger.LogDebug("Loading '{0}' ...", relativePath);

			string modDirectory = ModComponentMain.Implementation.GetModsFolderPath();
			string absolutePath = Path.Combine(modDirectory, relativePath);
			Assembly.LoadFrom(absolutePath);
		}

		private static void LoadSoundBank(string relativePath)
		{
			ModSoundBankManager.RegisterSoundBank(relativePath);
		}

		private static void MapSkill(GameObject prefab)
		{
			SkillJson.InitializeModSkill(ref prefab);
			ModSkill modSkill = ComponentUtils.GetComponent<ModSkill>(prefab);
			if (modSkill != null)
			{
				SkillsMapper.RegisterSkill(modSkill);
			}
		}

		private static void MapBlueprint(GameObject prefab, string sourcePath)
		{
			ModBlueprint modBlueprint = ComponentUtils.GetComponent<ModBlueprint>(prefab);
			if (modBlueprint != null)
			{
				BlueprintMapper.RegisterBlueprint(modBlueprint, sourcePath);
			}
		}


		internal static void MapModComponent(GameObject prefab)
		{
			if (prefab is null) throw new System.ArgumentNullException("Prefab was null in AutoMapper.MapModComponent");

			ComponentJson.InitializeComponents(ref prefab);
			ModComponent modComponent = ComponentUtils.GetModComponent(prefab);

			if (modComponent is null) throw new System.NullReferenceException("In AutoMapper.MapModComponent, the mod component from the prefab was null.");

			Mapper.Map(prefab);
		}

		private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
		{
			AssemblyName requestedAssemblyName = new AssemblyName(args.Name);

			Assembly modComponentMapper = Assembly.GetExecutingAssembly();
			if (IsCompatible(requestedAssemblyName, modComponentMapper.GetName()))
			{
				Logger.Log("Redirecting load attempt for " + requestedAssemblyName + " to " + modComponentMapper.GetName());
				return Assembly.GetExecutingAssembly();
			}

			Assembly modComponentAPI = typeof(ModComponent).Assembly;
			if (IsCompatible(requestedAssemblyName, modComponentAPI.GetName()))
			{
				Logger.Log("Redirecting load attempt for " + requestedAssemblyName + " to " + modComponentAPI.GetName());
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