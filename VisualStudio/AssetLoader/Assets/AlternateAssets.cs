using System.Collections.Generic;
using UnityEngine;

namespace AssetLoader
{
	public static class AlternateAssets
	{
		private static Dictionary<string, GameObject> alternateAssets = new Dictionary<string, GameObject>();
		public static void AddAlternateAsset(GameObject asset)
		{
			if (asset is null) return;
			else AddAlternateAsset(asset.name, asset);
		}
		public static void AddAlternateAsset(string name, GameObject asset)
		{
			if (string.IsNullOrWhiteSpace(name)) return;
			else if (AssetExists(name)) alternateAssets[name] = asset;
			else alternateAssets.Add(name, asset);
		}
		public static bool AssetExists(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) return false;
			else return alternateAssets.ContainsKey(name) && !(alternateAssets[name] is null);
		}
		public static GameObject GetAsset(string name)
		{
			if (AssetExists(name)) return alternateAssets[name];
			else return null;
		}
	}
}
