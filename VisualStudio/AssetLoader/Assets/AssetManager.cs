namespace AssetLoader
{
	public static class AssetManager
	{
		public static bool IsKnownAsset(string name) => ModAssetBundleManager.IsKnownAsset(name) || AlternateIconManager.ContainsKey(name) || AlternateAssetManager.AssetExists(name);
		public static UnityEngine.Object GetAsset(string name)
		{
			if (AlternateIconManager.ContainsKey(name)) return AlternateIconManager.GetTexture(name);
			else if (ModAssetBundleManager.IsKnownAsset(name)) return ModAssetBundleManager.LoadAsset(name);
			else if (AlternateAssetManager.AssetExists(name)) return AlternateAssetManager.GetAsset(name);
			else return null;
		}
	}
}
