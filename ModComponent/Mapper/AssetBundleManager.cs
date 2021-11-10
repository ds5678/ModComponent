using System;
using System.Collections.Generic;

namespace ModComponent.Mapper
{
	internal static class AssetBundleManager
	{
		private static List<string> pendingAssetBundles = new List<string>();
		private static Dictionary<string, string> pendingAssetBundlePaths = new Dictionary<string, string>();

		internal static void Add(string relativePath)
		{
			if (pendingAssetBundles.Contains(relativePath))
			{
				Logger.LogWarning($"Asset Bundle Manager already has '{relativePath}' on the list of pending asset bundles.");
			}
			else pendingAssetBundles.Add(relativePath);
		}
		internal static void Add(string relativePath, string fullPath)
		{
			Add(relativePath);
			if (pendingAssetBundlePaths.ContainsKey(relativePath))
			{
				Logger.LogWarning($"Asset Bundle Manager already has '{relativePath}' in the dictionary of pending asset bundle paths.");
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
					string errorMessage = string.Format("Could not map the assets in the bundle at '{0}'. {1}", relativePath, e);
					if (pendingAssetBundlePaths.ContainsKey(relativePath))
					{
						PackManager.SetItemPackNotWorking(pendingAssetBundlePaths[relativePath], errorMessage);
					}
					Logger.LogError(errorMessage);
				}
			}
			pendingAssetBundles.Clear();
			pendingAssetBundlePaths.Clear();
		}
	}
}
