using ModComponentMapper.InformationMenu;

namespace ModComponentMapper
{
	public class MapperImplementation
	{
		public static event SceneReady OnSceneReady;

		internal static void OnApplicationStart()
		{
			PageManager.Initialize();

			Logger.LogDebug("Running in Debug Mode");

			ZipFileLoader.Initialize();

			AutoMapper.Initialize();
			ModHealthManager.Initialize();
			AssetBundleManager.LoadPendingAssetBundles();
			GearSpawner.Initialize();
			BlueprintReader.Initialize();
		}

		internal static void SceneReady()
		{
			Logger.Log("Invoking 'SceneReady' for scene '{0}' ...", GameManager.m_ActiveScene);

			System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
			stopwatch.Start();

			OnSceneReady?.Invoke();

			stopwatch.Stop();
			Logger.Log("Completed 'SceneReady' for scene '{0}' in {1} ms", GameManager.m_ActiveScene, stopwatch.ElapsedMilliseconds);
		}
	}
}