using System.Collections.Generic;
using UnityEngine;

namespace AssetLoader
{
	public static class AlternateAssetManager
	{
		private static readonly Dictionary<string, Object> alternateAssets = new Dictionary<string, Object>();
		public static void AddAlternateAsset(Object asset)
		{
			if (asset is null) Logger.LogError("Alternate asset cannot be null");
			else AddAlternateAsset(asset.name, asset);
		}
		public static void AddAlternateAsset(string name, Object asset)
		{
			if (string.IsNullOrWhiteSpace(name)) Logger.LogError("Alternate asset must have a name");
			else if (AssetExists(name)) alternateAssets[name] = asset;
			else alternateAssets.Add(name, asset);
		}
		public static bool AssetExists(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) return false;
			else return alternateAssets.ContainsKey(name) && !(alternateAssets[name] is null);
		}
		public static Object GetAsset(string name)
		{
			if (AssetExists(name)) return alternateAssets[name];
			else return null;
		}
	}
}
