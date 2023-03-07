using ModComponent.API.Components;
using ModComponent.AssetLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ModComponent.Mapper;

internal static class AutoMapper
{
	/// <summary>
	/// Relative Paths
	/// </summary>
	private static readonly List<string> pendingAssetBundles = new();
	/// <summary>
	/// Relative Paths : Zip Paths
	/// </summary>
	private static readonly Dictionary<string, string> pendingAssetBundleZipFileMap = new();

	internal static void AutoMapPrefab(string bundlePath, string prefabName)
	{
		string bundleName = Path.GetFileNameWithoutExtension(bundlePath);

		UnityEngine.Object? loadedObject = Addressables.LoadAssetAsync<GameObject>(prefabName).WaitForCompletion();
		if (loadedObject == null)
		{
			throw new Exception($"({bundleName}) {prefabName} could not be loaded with Resources.Load");
		}

		GameObject? prefab = loadedObject.TryCast<GameObject>();
		if (prefab == null)
		{
			throw new NullReferenceException($"In AutoMapper.AutoMapPrefab, ({bundleName}) {prefabName} loaded object was not a GameObject.");
		}

		if (prefab.name.StartsWith("GEAR_"))
		{
			MapModComponent(prefab);
		}
	}

	private static void LoadAssetBundle(string relativePath)
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
				AutoMapPrefab(assetBundle.name, eachAssetName);
			}
		}
	}


	internal static void MapModComponent(GameObject prefab)
	{
		if (prefab == null)
		{
			throw new ArgumentNullException("Prefab was null in AutoMapper.MapModComponent");
		}

		ComponentJson.InitializeComponents(prefab);
		ModBaseComponent modComponent = Utils.ComponentUtils.GetModComponent(prefab);

		if (modComponent == null)
		{
			throw new NullReferenceException("In AutoMapper.MapModComponent, the mod component from the prefab was null.");
		}

		Logger.Log($"Mapping {prefab.name}");
		ItemMapper.Map(prefab);
	}

	/// <summary>
	/// Add an asset bundle to the list of pending bundles
	/// </summary>
	/// <param name="relativePath">The relative path to the asset bundle from within the mods folder</param>
	internal static void AddAssetBundle(string relativePath)
	{
		if (pendingAssetBundles.Contains(relativePath))
		{
			Logger.LogWarning($"AutoMapper already has '{relativePath}' on the list of pending asset bundles.");
		}
		else
		{
			pendingAssetBundles.Add(relativePath);
		}
	}

	/// <summary>
	/// Add an asset bundle to the list of pending bundles
	/// </summary>
	/// <param name="relativePath">The relative path to the asset bundle from within the mods folder</param>
	/// <param name="zipFilePath">The full path to the zip file containing the asset bundle</param>
	internal static void AddAssetBundle(string relativePath, string zipFilePath)
	{
		AddAssetBundle(relativePath);
		if (pendingAssetBundleZipFileMap.ContainsKey(relativePath))
		{
			Logger.LogWarning($"AutoMapper already has '{relativePath}' in the dictionary of pending asset bundle paths.");
		}
		else
		{
			pendingAssetBundleZipFileMap.Add(relativePath, zipFilePath);
		}
	}

	/// <summary>
	/// Load all the asset bundles and map their assets
	/// </summary>
	internal static void LoadPendingAssetBundles()
	{
		Logger.Log("Loading the pending asset bundles");
		foreach (string relativePath in pendingAssetBundles)
		{
			try
			{
				LoadAssetBundle(relativePath);
			}
			catch (Exception e)
			{
				string errorMessage = $"Could not map the assets in the bundle at '{relativePath}'. {e}";
				if (pendingAssetBundleZipFileMap.ContainsKey(relativePath))
				{
					PackManager.SetItemPackNotWorking(pendingAssetBundleZipFileMap[relativePath], errorMessage);
				}
				else
				{
					Logger.LogError(errorMessage);
				}
			}
		}
		pendingAssetBundles.Clear();
		pendingAssetBundleZipFileMap.Clear();
	}
}