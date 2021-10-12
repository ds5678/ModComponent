using MelonLoader;
using ModComponent.Mapper;

namespace ModComponent
{
	internal class Implementation : MelonMod
	{
		public override void OnApplicationStart()
		{
			Logger.LogDebug("Debug Compilation");
			Logger.LogNotDebug("Release Compilation");
			
			Settings.instance.AddToModSettings("ModComponent");

			ZipFileLoader.Initialize();

			AutoMapper.Initialize();
			Mapper.AssetBundleManager.LoadPendingAssetBundles();
		}
	}
}
