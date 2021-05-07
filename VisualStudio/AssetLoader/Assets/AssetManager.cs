namespace AssetLoader
{
	public static class AssetManager
	{
		public static bool IsKnownAsset(string name) => AlternateAssets.AssetExists(name) || ModAssetBundleManager.IsKnownAsset(name);
		public static UnityEngine.Object GetAsset(string name)
		{
			if (AlternateAssets.AssetExists(name)) return AlternateAssets.GetAsset(name);
			else if (ModAssetBundleManager.IsKnownAsset(name)) return ModAssetBundleManager.LoadAsset(name);
			else return null;
		}
	}
}
