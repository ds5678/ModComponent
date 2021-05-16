using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AssetLoader
{
	public static class ModAssetBundleManager
	{
		private const string ASSET_NAME_LOCALIZATION = "localization";
		private const string ASSET_NAME_PREFIX_GEAR = "gear_";
		private const string ASSET_NAME_SUFFIX = "atlas";
		private const string ASSET_PATH_SUFFIX_PREFAB = ".prefab";

		private static readonly string[] RESOURCE_FOLDER = { "assets/", "logimages/", "clothingpaperdoll/female/", "clothingpaperdoll/male/", "inventorygridicons/", "craftingicons/" };

		private static Dictionary<string, AssetBundle> knownAssetBundles = new Dictionary<string, AssetBundle>();
		private static Dictionary<string, string> knownAssetMappedNames = new Dictionary<string, string>();
		private static Dictionary<string, AssetBundle> knownAssetNames = new Dictionary<string, AssetBundle>();

		public static AssetBundle GetAssetBundle(string relativePath)
		{
			knownAssetBundles.TryGetValue(relativePath, out AssetBundle result);
			return result;
		}

		public static bool IsKnownAsset(string name)
		{
			if (name is null) return false;
			else return getFullAssetName(name) != null;
		}

		public static Object LoadAsset(string name)
		{
			string fullAssetName = getFullAssetName(name);

			if (knownAssetNames.TryGetValue(fullAssetName, out AssetBundle assetBundle))
			{
				UnityEngine.Object result = ModComponentUtils.AssetBundleUtils.LoadAsset(assetBundle, fullAssetName);
				return result;
			}

			throw new System.Exception("Unknown asset " + name + ". Did you forget to register an AssetBundle?");
		}

		/// <summary>
		/// Registers an asset bundle with AssetLoader to insert bundled assets into the game
		/// </summary>
		/// <param name="relativePath">The relative path within the Mods folder to the asset bundle file</param>
		public static void RegisterAssetBundle(string relativePath)
		{
			if (string.IsNullOrEmpty(relativePath)) throw new System.ArgumentException("The relative path while registering an asset bundle was null or empty");
			if (knownAssetBundles.ContainsKey(relativePath))
			{
				Logger.Log("AssetBundle '{0}' has already been registered.", relativePath);
				return;
			}

			string modDirectory = ModComponentMain.Implementation.GetModsFolderPath();
			string fullPath = Path.Combine(modDirectory, relativePath);

			if (File.Exists(fullPath)) LoadAssetBundle(relativePath, fullPath);
			else throw new FileNotFoundException("AssetBundle '" + relativePath + "' could not be found at '" + fullPath + "'.");
		}

		/// <summary>
		/// Registers an asset bundle with AssetLoader to insert bundled assets into the game
		/// </summary>
		/// <param name="relativePath">The identifier of this asset bundle. Must be unique</param>
		/// <param name="assetBundle">The AssetBundle instance to be registered</param>
		public static void RegisterAssetBundle(string relativePath, AssetBundle assetBundle)
		{
			if (string.IsNullOrEmpty(relativePath)) throw new System.ArgumentException("The relative path while registering an asset bundle was null or empty");
			else if (knownAssetBundles.ContainsKey(relativePath))
			{
				Logger.Log("AssetBundle '{0}' has already been registered.", relativePath);
			}
			else if (assetBundle == null) throw new System.ArgumentNullException("Asset bundle '" + relativePath + "' was null");
			else LoadAssetBundle(relativePath, assetBundle);
		}

		public static string getAssetMappedName(string assetPath, string assetName)
		{
			if (assetName.StartsWith(ASSET_NAME_PREFIX_GEAR) && assetPath.EndsWith(ASSET_PATH_SUFFIX_PREFAB))
			{
				return assetName;
			}

			string result = assetPath;

			result = StripResourceFolder(result);

			int index = result.LastIndexOf(assetName);
			if (index != -1) result = result.Substring(0, index + assetName.Length);

			return result;
		}

		/// <summary>
		/// Takes an asset or file path and returns the name of the file without any file extensions
		/// </summary>
		/// <param name="assetPath">The path to the file. It can be relative or absolute</param>
		/// <returns></returns>
		public static string GetAssetName(string assetPath) => GetAssetName(assetPath, true);

		/// <summary>
		/// Takes an asset or file path and returns the name of the file
		/// </summary>
		/// <param name="assetPath">The path to the file. It can be relative or absolute</param>
		/// <param name="removeFileExtension">Should this remove the file extension from the result?</param>
		/// <returns></returns>
		public static string GetAssetName(string assetPath, bool removeFileExtension)
		{
			string result = assetPath;
			int index = System.Math.Max(assetPath.LastIndexOf('/'), assetPath.LastIndexOf('\\'));

			if (index != -1) result = result.Substring(index + 1);

			if (removeFileExtension)
			{
				index = result.LastIndexOf('.');

				if (index != -1) result = result.Substring(0, index);
			}

			return result;
		}

		/// <summary>
		/// Retrieves the asset's full name from the dictionary
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static string getFullAssetName(string name)
		{
			string lowerCaseName = name.ToLowerInvariant();
			if (knownAssetNames.ContainsKey(lowerCaseName)) return lowerCaseName;
			else if (knownAssetMappedNames.ContainsKey(lowerCaseName)) return knownAssetMappedNames[lowerCaseName];
			else return null;
		}

		/// <summary>
		/// Gets a file's asset bundle data and returns an AssetBundle instance containing that data
		/// </summary>
		/// <param name="fullPath">The entire system file path to the Asset Bundle</param>
		/// <returns>A loaded instance of the asset bundle from the file</returns>
		public static AssetBundle GetAssetBundleFromFile(string fullPath)
		{
			AssetBundle assetBundle = AssetBundle.LoadFromFile(fullPath);
			if (assetBundle) return assetBundle;
			else throw new System.Exception("Could not load AssetBundle from '" + fullPath + "'. The asset bundle might have been made with an incorrect version of Unity (should be 2019.4.3).");
		}

		/// <summary>
		/// Loads an asset bundle into the AssetLoader bank where it can be obtain with Resources.Load or AssetBundle.LoadAsset
		/// </summary>
		/// <param name="relativePath">The relative path to the asset bundle from within the mods folder</param>
		/// <param name="fullPath">The absolute to the asset bundle on the machine executing</param>
		private static void LoadAssetBundle(string relativePath, string fullPath)
		{
			LoadAssetBundle(relativePath, GetAssetBundleFromFile(fullPath));
		}

		/// <summary>
		/// Loads an AssetBundle instance into the AssetLoader bank where it can be obtain with Resources.Load or AssetBundle.LoadAsset
		/// </summary>
		/// <param name="relativePath">The relative path to the asset bundle, or the id to identify it</param>
		/// <param name="assetBundle">The AssetBundle instance to be loaded</param>
		private static void LoadAssetBundle(string relativePath, AssetBundle assetBundle)
		{
			knownAssetBundles.Add(relativePath, assetBundle);

			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Registered AssetBundle '");
			stringBuilder.Append(relativePath);
			stringBuilder.Append("' with the following assets\n");

			foreach (string eachAssetName in assetBundle.GetAllAssetNames())
			{
				string assetName = GetAssetName(eachAssetName);

				if (assetName == ASSET_NAME_LOCALIZATION)
				{
					Object asset = assetBundle.LoadAsset(eachAssetName);
					LocalizationManager.AddToWaitlist(asset, eachAssetName, relativePath);
					continue;
				}

				if (assetName.EndsWith(ASSET_NAME_SUFFIX))
				{
					Object asset = assetBundle.LoadAsset(eachAssetName);
					AtlasManager.LoadUiAtlas(asset);
					continue;
				}

				if (knownAssetNames.ContainsKey(eachAssetName))
				{
					Logger.Log("Duplicate asset name '{0}'.", eachAssetName);
					continue;
				}

				knownAssetNames.Add(eachAssetName, assetBundle);

				string mappedName = getAssetMappedName(eachAssetName, assetName);
				knownAssetMappedNames.Add(mappedName, eachAssetName);

				stringBuilder.Append("  ");
				stringBuilder.Append(mappedName);
				stringBuilder.Append(" => ");
				stringBuilder.Append(eachAssetName);
				stringBuilder.Append("\n");
			}

			Logger.Log(stringBuilder.ToString().Trim());
		}

		/// <summary>
		/// Removes the resource prefixes from an asset path
		/// </summary>
		/// <param name="assetPath">The path to the asset within the asset bundle</param>
		/// <returns>A string without any of the resource folder prefixes</returns>
		private static string StripResourceFolder(string assetPath)
		{
			string result = assetPath;

			while (true)
			{
				string resourceFolder = RESOURCE_FOLDER.Where(eachResourceFolder => result.StartsWith(eachResourceFolder)).FirstOrDefault();
				if (resourceFolder == null)
				{
					break;
				}

				result = result.Substring(resourceFolder.Length);
			}

			return result;
		}

		/// <summary>
		/// Returns an array of string variables without any leading or trailing whitespace
		/// </summary>
		/// <param name="values">An array of string variables.</param>
		/// <returns>A new array containing the trimmed values.</returns>
		public static string[] Trim(string[] values)
		{
			string[] result = new string[values.Length];

			for (int i = 0; i < values.Length; i++)
			{
				result[i] = values[i].Trim();
			}

			return result;
		}
	}
}