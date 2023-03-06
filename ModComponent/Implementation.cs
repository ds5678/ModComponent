using MelonLoader;
using ModComponent.Mapper;
using ModComponent.Utils;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Il2Cpp;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ModComponent;

internal class Implementation : MelonMod
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	internal static Implementation instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

	public Implementation()
	{
		instance = this;
	}

	public override void OnInitializeMelon()
	{
		Logger.LogDebug("Debug Compilation");
		Logger.LogNotDebug("Release Compilation");

		Settings.instance.AddToModSettings("ModComponent");

		ZipFileLoader.Initialize();

		AssetBundleProcessor.PreloadAssetBundles();

		AssetBundleProcessor.LoadCatalogs();

		AssetBundleProcessor.TestCatalogs();

		AssetBundleProcessor.MapPrefabs();

//		Application.Quit();


	}

	public override void OnApplicationQuit()
	{
		AssetBundleProcessor.CleanupTempFolder();
	}

}
