using MelonLoader.Utils;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine;
using ModComponent.Mapper;


namespace ModComponent.Utils
{
	internal class AssetBundleProcessor
	{
		internal readonly static string tempFolderName = "_ModComponentTemp";
		internal readonly static string tempFolderPath = Path.Combine(MelonEnvironment.ModsDirectory, tempFolderName);

		internal readonly static List<string> bundleFilePaths = new();
		internal readonly static List<string> catalogFilePaths = new();

		internal Dictionary<string, string> catalogBundleList = new();

		internal readonly static Dictionary<string, string> catalogTestList = new();

		internal readonly static List<string> bundleNames = new();
		internal readonly static List<string> assetList = new();


		internal static void InitTempFolder()
		{
			if (!Directory.Exists(tempFolderPath))
			{
				MelonLoader.MelonLogger.Warning("Creating temp folder ("+ tempFolderName + ")");
				Directory.CreateDirectory(tempFolderPath);
			}
		}

		internal static void CleanupTempFolder()
		{
			foreach (string bundleFilePath in bundleFilePaths)
			{
				if (bundleFilePath != null && File.Exists(bundleFilePath))
				{
					File.Delete(bundleFilePath);
				}
			}
			foreach (string catalogFilePath in catalogFilePaths)
			{
				if (catalogFilePath != null && File.Exists(catalogFilePath))
				{
					File.Delete(catalogFilePath);
				}
			}

			if (Directory.Exists(tempFolderPath))
			{
				Directory.Delete(tempFolderPath, true);
			}
		}

		internal static void PreloadAssetBundles()
		{
			InitTempFolder();

			foreach (string bundleFilePath in bundleFilePaths)
			{
				if (bundleFilePath != null && File.Exists(bundleFilePath))
				{
					string bundleFileName = Path.GetFileName(bundleFilePath);
					MelonLoader.MelonLogger.Warning("PreloadAssetBundles | " + bundleFileName);

					AssetBundle ab = AssetBundle.LoadFromFile(bundleFilePath);
					foreach (string assetName in ab.GetAllAssetNames())
					{
						assetList.Add(assetName);
					}

					bundleNames.Add(ab.name);
					ab.Unload(true);
				}
			}
		}

		internal static void WriteAssettBundleToDisk(string filename, byte[] data)
		{
			InitTempFolder();

			string bundleFilePath = Path.Combine(tempFolderPath, filename);
			if (File.Exists(bundleFilePath))
			{
				File.Delete(bundleFilePath);
			}
			FileStream fs = File.Create(bundleFilePath);
			fs.Write(data);
			fs.Close();
			MelonLoader.MelonLogger.Warning("Bundle Written | " + filename);
			bundleFilePaths.Add(bundleFilePath);
		}

		internal static void WriteCatalogToDisk(string filename, string data)
		{
			InitTempFolder();

			string catalogName = Path.GetFileNameWithoutExtension(filename);

			string catalogFilePath = Path.Combine(tempFolderPath, filename);
			if (File.Exists(catalogFilePath))
			{
				File.Delete(catalogFilePath);
			}

			string firstAsset = null;
			ModContentCatalog contentCatalog = System.Text.Json.JsonSerializer.Deserialize<ModContentCatalog>(data);
			for (int i = 0; i < contentCatalog.m_InternalIds.Length; i++)
			{
				string line = contentCatalog.m_InternalIds[i];
				string assetExtension = Path.GetExtension(line);
				if (assetExtension == ".bundle" || assetExtension == ".unity3d")
				{
					contentCatalog.m_InternalIds[i] = Path.Combine(tempFolderPath, Path.GetFileName(line));
				}
				else if (firstAsset == null)
				{
					firstAsset = Path.GetFileName(line);
				}
			}
			contentCatalog.m_LocatorId = catalogName;
			MelonLoader.MelonLogger.Warning("Catalog m_InternalIds Patched | " + catalogName);

			data = System.Text.Json.JsonSerializer.Serialize<ModContentCatalog>(contentCatalog);

			File.WriteAllText(catalogFilePath, data);
			MelonLoader.MelonLogger.Warning("Catalog Written | " + catalogName);
			catalogFilePaths.Add(catalogFilePath);
			catalogTestList.Add(catalogName, firstAsset);

		}

		internal static void LoadCatalogs()
		{
			foreach (string catalogFilePath in catalogFilePaths)
			{
				string catalogName = Path.GetFileNameWithoutExtension(catalogFilePath);
				IResourceLocator catalogLocator = Addressables.LoadContentCatalogAsync(catalogFilePath).WaitForCompletion();
				string loadState = (catalogLocator != null && catalogLocator.Keys != null) ? "PASSED" : "FAILED";
				MelonLoader.MelonLogger.Warning("Catalog Loading (" + catalogName + ") " + loadState);
			}
		}

		internal static void TestCatalogs()
		{
			foreach (KeyValuePair<string, string> test in catalogTestList)
			{
				
				string catalogName = Path.GetFileNameWithoutExtension(test.Key);
				string testAssetName = Path.GetFileNameWithoutExtension(test.Value);
				string assetExtension = Path.GetExtension(test.Value);

				if (assetExtension == ".png" || assetExtension == ".jpg")
				{
					Texture2D? testObject = Addressables.LoadAssetAsync<Texture2D>(testAssetName).WaitForCompletion();
					string testPassed = (testObject != null && testObject.name != null) ? "PASSED (" + testObject.name + ")" : "FAILED (" + testAssetName + ")";
					MelonLoader.MelonLogger.Warning("Catalog Test (" + catalogName + ") " + testPassed);
				}
				if (assetExtension == ".prefab")
				{
					GameObject? testObject = Addressables.LoadAssetAsync<GameObject>(testAssetName).WaitForCompletion();
					string testPassed = (testObject != null && testObject.name != null) ? "PASSED (" + testObject.name + ")" : "FAILED (" + testAssetName + ")";
					MelonLoader.MelonLogger.Warning("Catalog Test (" + catalogName + ") " + testPassed);
				}


			}
		}

		internal static void MapPrefabs()
		{
			foreach (string assetName in assetList)
			{
				if (assetName.ToLower().EndsWith(@".prefab"))
				{
					AutoMapper.AutoMapPrefab(Path.GetFileNameWithoutExtension(assetName));
				}
			}
		}

	}

}
