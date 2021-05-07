using MelonLoader.TinyJSON;
using System.Collections.Generic;
using UnityEngine;

namespace AssetLoader
{
	static class LocalizationManager
	{
		internal static List<string> localizationWaitlistBundles = new List<string>(0);
		internal static List<string> localizationWaitlistAssets = new List<string>(0);
		internal static Dictionary<string, Dictionary<string, string>> localizationDictionary = new Dictionary<string, Dictionary<string, string>>();
		public const bool USE_ENGLISH_AS_DEFAULT = true;

		public static bool Exists(string key)
		{
			if (string.IsNullOrEmpty(key)) return false;
			else return localizationDictionary.ContainsKey(key);
		}

		public static bool Exists(string key, string lang)
		{
			if (string.IsNullOrEmpty(key)) return false;
			else return localizationDictionary.ContainsKey(key) && localizationDictionary[key].ContainsKey(lang);
		}

		public static string Get(string key)
		{
			string language = "English";
			if (Localization.IsInitialized()) language = Localization.Language;
			return GetForLang(key, language);
		}

		public static string GetForLang(string key, string lang)
		{
			if (Exists(key, lang)) return localizationDictionary[key][lang];
			else return key;
		}

		public static string GetText(TextAsset textAsset)
		{
			ByteReader byteReader = new ByteReader(textAsset);
			string contents = "";
			if (byteReader.canRead) contents = byteReader.ReadLine();
			while (byteReader.canRead)
			{
				contents = contents + '\n' + byteReader.ReadLine();
			}
			return contents;
		}

		internal static void MaybeLoadPendingAssets()
		{
			if (localizationWaitlistAssets.Count > 0 && Localization.IsInitialized())
			{
				LoadPendingAssets();
			}
		}

		internal static void LoadPendingAssets()
		{
			Logger.Log("Loading Waitlisted Localization Assets");
			for (int i = 0; i < LocalizationManager.localizationWaitlistAssets.Count; i++)
			{
				string bundleName = LocalizationManager.localizationWaitlistBundles[i];
				string assetName = LocalizationManager.localizationWaitlistAssets[i];
				LocalizationManager.LoadLocalization(bundleName, assetName);
			}
			LocalizationManager.localizationWaitlistAssets = new List<string>(0);
			LocalizationManager.localizationWaitlistBundles = new List<string>(0);
			//string text = MelonLoader.TinyJSON.JSON.Dump(localizationDictionary, EncodeOptions.PrettyPrint);
			//Logger.Log(text);
		}

		public static void LoadLocalization(string bundleName, string assetName)
		{
			AssetBundle assetBundle = ModAssetBundleManager.GetAssetBundle(bundleName);
			Object asset = assetBundle.LoadAsset(assetName);
			if (assetName.ToLower().EndsWith("json"))
			{
				LocalizationManager.LoadJSONLocalization(asset);
			}
			else if (assetName.ToLower().EndsWith("csv"))
			{
				LocalizationManager.LoadCSVLocalization(asset);
			}
			else
			{
				Logger.LogWarning("Found localization '{0}' that could not be loaded.", assetName);
			}
		}

		public static void LoadLocalization(string localizationID, Dictionary<string, string> translationDictionary, bool useEnglishAsDefault = false)
		{
			string[] knownLanguages = Localization.GetLanguages()?.ToArray();

			if (!Exists(localizationID)) localizationDictionary.Add(localizationID, new Dictionary<string, string>());

			string[] translations = new string[knownLanguages.Length];
			for (int i = 0; i < knownLanguages.Length; i++)
			{
				string language = knownLanguages[i];

				if (translationDictionary.ContainsKey(language))
				{
					if (!localizationDictionary[localizationID].ContainsKey(language))
					{
						localizationDictionary[localizationID].Add(language, translationDictionary[language]);
					}
					else
					{
						localizationDictionary[localizationID][language] = translationDictionary[language];
					}
				}
				else if (useEnglishAsDefault && translationDictionary.ContainsKey("English"))
				{
					if (!localizationDictionary[localizationID].ContainsKey(language))
					{
						localizationDictionary[localizationID].Add(language, translationDictionary["English"]);
					}
					else
					{
						localizationDictionary[localizationID][language] = translationDictionary["English"];
					}
				}
			}
		}

		public static void LoadCSVLocalization(Object asset)
		{
			TextAsset textAsset = asset.Cast<TextAsset>();
			if (textAsset == null)
			{
				Logger.LogWarning("Asset called '{0}' is not a TextAsset as expected.", asset.name);
				return;
			}

			//Implementation.Log("Processing asset '{0}' as csv localization.", asset.name);

			ByteReader byteReader = new ByteReader(textAsset);
			string[] languages = ModAssetBundleManager.Trim(byteReader.ReadCSV().ToArray());

			while (true)
			{
				string[] values = byteReader.ReadCSV()?.ToArray();
				if (values == null || languages == null || values.Length == 0 || languages.Length == 0)
				{
					break;
				}

				string locID = values[0];
				Dictionary<string, string> locDict = new Dictionary<string, string>();

				int maxIndex = System.Math.Min(values.Length, languages.Length);
				for (int j = 1; j < maxIndex; j++)
				{
					if (!string.IsNullOrEmpty(values[j]) && !string.IsNullOrEmpty(languages[j]))
					{
						locDict.Add(languages[j], values[j]);
					}
				}

				LoadLocalization(locID, locDict, USE_ENGLISH_AS_DEFAULT);
			}
		}

		public static void LoadJSONLocalization(Object asset)
		{
			TextAsset textAsset = asset.Cast<TextAsset>();
			if (textAsset == null)
			{
				Logger.LogWarning("Asset called '{0}' is not a TextAsset as expected.", asset.name);
				return;
			}
			//Implementation.Log("Processing asset '{0}' as json localization.", asset.name);
			string contents = GetText(textAsset);
			ProxyObject dict = JSON.Load(contents) as ProxyObject;
			foreach (var pair in dict)
			{
				string locID = pair.Key;
				Dictionary<string, string> locDict = pair.Value.Make<Dictionary<string, string>>();
				LoadLocalization(locID, locDict, USE_ENGLISH_AS_DEFAULT);
			}
		}


	}
}
