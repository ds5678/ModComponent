using ModComponent.API.Components;
using ModComponent.AssetLoader;
using UnityEngine;

namespace ModComponent.Mapper
{
	internal static class AutoMapper
	{
		private static void AutoMapPrefab(string prefabName)
		{
			GameObject prefab = Resources.Load(prefabName)?.Cast<GameObject>();
			if (prefab == null)
			{
				throw new System.NullReferenceException("In AutoMapper.AutoMapPrefab, Resources.Load did not return a GameObject.");
			}
			if (prefab.name.StartsWith("GEAR_")) 
				MapModComponent(prefab);
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
			if (prefab == null) 
				throw new System.ArgumentNullException("Prefab was null in AutoMapper.MapModComponent");

			ComponentJson.InitializeComponents(ref prefab);
			ModBaseComponent modComponent = ModComponent.Utils.ComponentUtils.GetModComponent(prefab);

			if (modComponent == null) 
				throw new System.NullReferenceException("In AutoMapper.MapModComponent, the mod component from the prefab was null.");

			Logger.Log($"Mapping {prefab.name}");
			ItemMapper.Map(prefab);
		}
	}
}