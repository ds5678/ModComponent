using System;
using System.Collections.Generic;

namespace ModComponentMapper
{
	internal static class AssetBundleManager
	{
		private static List<string> pendingAssetBundles = new List<string>();
		private static Dictionary<string, string> pendingAssetBundlePaths = new Dictionary<string, string>();

		internal static void Add(string relativePath)
		{
			if (pendingAssetBundles.Contains(relativePath))
			{
				Logger.LogWarning("Asset Bundle Manager already has '{0}' on the list of pending asset bundles.", relativePath);
			}
			else pendingAssetBundles.Add(relativePath);
		}
		internal static void Add(string relativePath, string fullPath)
		{
			Add(relativePath);
			if (pendingAssetBundlePaths.ContainsKey(relativePath))
			{
				Logger.LogWarning("Asset Bundle Manager already has '{0}' in the dictionary of pending asset bundle paths.", relativePath);
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
					if (pendingAssetBundlePaths.ContainsKey(relativePath)) PageManager.SetItemPackNotWorking(pendingAssetBundlePaths[relativePath]);
					Logger.LogError("Could not map the assets in the bundle at '{0}'. {1}", relativePath, e.Message);
				}
			}
			pendingAssetBundles.Clear();
			pendingAssetBundlePaths.Clear();
		}
	}
}
