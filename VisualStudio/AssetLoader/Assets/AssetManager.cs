namespace AssetLoader
{
	public static class AssetManager
	{
		public static bool IsKnownAsset(string name) => AlternateAssetManager.AssetExists(name) || ModAssetBundleManager.IsKnownAsset(name);
		public static UnityEngine.Object GetAsset(string name)
		{
			if (AlternateAssetManager.AssetExists(name)) return AlternateAssetManager.GetAsset(name);
			else if (ModAssetBundleManager.IsKnownAsset(name)) return ModAssetBundleManager.LoadAsset(name);
			else return null;
		}
	}
}
