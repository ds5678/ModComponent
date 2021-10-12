using MelonLoader;
using ModComponent.Mapper;
using UnityEngine;

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

		public static byte[][] GetItemPackHashes() => Mapper.ZipFileLoader.hashes.ToArray();
	}
}
