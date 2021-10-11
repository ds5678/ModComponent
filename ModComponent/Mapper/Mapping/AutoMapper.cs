using ModComponent.AssetLoader;
using ModComponentAPI;
using System;
using System.Reflection;
using UnityEngine;

namespace ModComponentMapper
{
	public static class AutoMapper
	{
		internal static void Initialize()
		{
			//AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly; //I think this might only have applied to 1.56
		}

		private static void AutoMapPrefab(string prefabName)
		{
			//Logger.Log("AutoMapPrefab called");
			GameObject prefab = Resources.Load(prefabName)?.Cast<GameObject>();
			if (prefab == null)
			{
				throw new System.NullReferenceException("In AutoMapper.AutoMapPrefab, Resources.Load did not return a GameObject.");
			}
			if (prefab.name.StartsWith("GEAR_")) MapModComponent(prefab);
			//MapBlueprint(prefab, assetBundlePath);
			//if (prefab.name.StartsWith("SKILL_")) MapSkill(prefab);
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


		internal static void MapModComponent(GameObject prefab)
		{
			if (prefab == null) throw new System.ArgumentNullException("Prefab was null in AutoMapper.MapModComponent");

			ComponentJson.InitializeComponents(ref prefab);
			ModBaseComponent modComponent = ModComponent.Utils.ComponentUtils.GetModComponent(prefab);

			if (modComponent == null) throw new System.NullReferenceException("In AutoMapper.MapModComponent, the mod component from the prefab was null.");

			Logger.Log("Mapping {0}", prefab.name);
			ItemMapper.Map(prefab);
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

			Assembly modComponentAPI = typeof(ModBaseComponent).Assembly;
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