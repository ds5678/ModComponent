namespace ModComponent.Mapper
{
	public delegate void SceneReady();

	public class MapperCore
	{
		internal static void InitializeAndMapAssets()
		{
			Logger.LogDebug("Running in Debug Mode");

			ZipFileLoader.Initialize();

			AutoMapper.Initialize();
			AssetBundleManager.LoadPendingAssetBundles();
			BlueprintReader.ReadDefinitions();
		}
	}
}