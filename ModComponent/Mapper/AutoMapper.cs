using ModComponent.API.Components;
using ModComponent.AssetLoader;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.Mapper
{
	internal static class AutoMapper
	{
		private static List<string> pendingAssetBundles = new List<string>();
		private static Dictionary<string, string> pendingAssetBundlePaths = new Dictionary<string, string>();

		private static void AutoMapPrefab(string prefabName)
		{
			GameObject prefab = Resources.Load(prefabName)?.TryCast<GameObject>();
			if (prefab == null)
			{
				throw new System.NullReferenceException("In AutoMapper.AutoMapPrefab, Resources.Load did not return a GameObject.");
			}
			if (prefab.name.StartsWith("GEAR_")) 
				MapModComponent(prefab);
		}

		private static void LoadAssetBundle(string relativePath)
		{
			LoadAssetBundle(ModAssetBundleManager.GetAssetBundle(relativePath));
		}
		private static void LoadAssetBundle(AssetBundle assetBundle)
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
			if (prefab == null) 
				throw new System.ArgumentNullException("Prefab was null in AutoMapper.MapModComponent");

			ComponentJson.InitializeComponents(ref prefab);
			ModBaseComponent modComponent = ModComponent.Utils.ComponentUtils.GetModComponent(prefab);

			if (modComponent == null) 
				throw new System.NullReferenceException("In AutoMapper.MapModComponent, the mod component from the prefab was null.");

			Logger.Log($"Mapping {prefab.name}");
			ItemMapper.Map(prefab);
		}

		internal static void AddAssetBundle(string relativePath)
		{
			if (pendingAssetBundles.Contains(relativePath))
			{
				Logger.LogWarning($"AutoMapper already has '{relativePath}' on the list of pending asset bundles.");
			}
			else pendingAssetBundles.Add(relativePath);
		}
		internal static void AddAssetBundle(string relativePath, string fullPath)
		{
			AddAssetBundle(relativePath);
			if (pendingAssetBundlePaths.ContainsKey(relativePath))
			{
				Logger.LogWarning($"AutoMapper already has '{relativePath}' in the dictionary of pending asset bundle paths.");
			}
			else pendingAssetBundlePaths.Add(relativePath, fullPath);
		}

		internal static void LoadPendingAssetBundles()
		{
			Logger.Log("Loading the pending asset bundles");
			foreach (string relativePath in pendingAssetBundles)
			{
				try
				{
					AutoMapper.LoadAssetBundle(relativePath);
				}
				catch (Exception e)
				{
					string errorMessage = $"Could not map the assets in the bundle at '{relativePath}'. {e.ToString()}";
					if (pendingAssetBundlePaths.ContainsKey(relativePath))
					{
						PackManager.SetItemPackNotWorking(pendingAssetBundlePaths[relativePath], errorMessage);
					}
                    else
                    {
						Logger.LogError(errorMessage);
					}
				}
			}
			pendingAssetBundles.Clear();
			pendingAssetBundlePaths.Clear();
		}
	}
}