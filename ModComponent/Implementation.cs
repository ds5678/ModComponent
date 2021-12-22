using MelonLoader;
using ModComponent.Mapper;

namespace ModComponent
{
	internal class Implementation : MelonMod
	{
		internal static Implementation instance;

		public override void OnApplicationStart()
		{
			instance = this;

			Logger.LogDebug("Debug Compilation");
			Logger.LogNotDebug("Release Compilation");
			
			Settings.instance.AddToModSettings("ModComponent");

			ZipFileLoader.Initialize();

			AutoMapper.LoadPendingAssetBundles();
		}
	}
}
