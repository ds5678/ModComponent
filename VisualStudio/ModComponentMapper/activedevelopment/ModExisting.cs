using MelonLoader.TinyJSON;
using ModComponentAPI;
using System;
using System.IO;
using UnityEngine;

namespace ModComponentMapper
{
	internal class ModPlaceHolderComponent : ModComponentAPI.ModComponent
	{
		public ModPlaceHolderComponent(System.IntPtr intPtr) : base(intPtr) { }
	}

	public static class ModExisting
	{
		public const string EXISTING_JSON_DIRECTORY_NAME = "existing-json";
		private static bool isInitialized = false;

		private static float newWeight = 0f;
		private static bool needToChangeWeight = false;

		public static string GetExistingJsonDirectory()
		{
			return Path.Combine(ModComponentMain.Implementation.GetModsFolderPath(), EXISTING_JSON_DIRECTORY_NAME);
		}

		public static void Initialize()
		{
			ReadDefinitions();
			isInitialized = true;
		}

		public static bool IsInitialized()
		{
			return isInitialized;
		}

		private static void ReadDefinitions()
		{
#if DEBUG
            string existingJsonDirectory = GetExistingJsonDirectory();
            if (!Directory.Exists(existingJsonDirectory))
            {
                Logger.Log("Auxiliary Existing Json directory '{0}' does not exist. Creating...", existingJsonDirectory);
                Directory.CreateDirectory(existingJsonDirectory);
            }
            ProcessFiles(existingJsonDirectory);
#endif
			ProcessFilesFromZips();
		}

		private static void ProcessFiles(string directory)
		{
			if (!Directory.Exists(directory)) return;
			string[] directories = Directory.GetDirectories(directory);
			foreach (string eachDirectory in directories)
			{
				ProcessFiles(eachDirectory);
			}

			string[] files = Directory.GetFiles(directory, "*.json");
			foreach (string eachFile in files)
			{
				Logger.Log("Processing existing json definition. '{0}'", eachFile);
				ProcessFile(eachFile);
			}
		}
		private static void ProcessFile(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				Logger.LogWarning("In InitializeComponents, the filepath was null or empty.");
			}
			string data = File.ReadAllText(filePath);
			ProcessFile(filePath, data);
		}
		private static void ProcessFile(string filePath, string fileText)
		{
			GameObject item = InitializeExisting(filePath, fileText);
			Mapper.Map(item);
			ChangeWeight(item);
			//UnityEngine.Object.DontDestroyOnLoad(item);
		}

		private static void ProcessFilesFromZips()
		{
			foreach (var pair in JsonHandler.existingJsons)
			{
				try
				{
					Logger.Log("Processing existing json definition '{0}'.", pair.Key);
					ProcessFile(pair.Key, pair.Value);
				}
				catch (Exception e)
				{
					PageManager.SetItemPackNotWorking(pair.Key);
					Logger.LogError("Existing-json could not be loaded correctly at '{0}'. {1}", pair.Key, e.Message);
				}
			}
			JsonHandler.existingJsons.Clear();
		}

		private static GameObject InitializeExisting(string filePath, string text)
		{
			try
			{
				if (string.IsNullOrEmpty(text)) return null;
				ProxyObject dict = JSON.Load(text) as ProxyObject;
				if (!JsonUtils.ContainsKey(dict, "GearName"))
				{
					Logger.LogWarning("The JSON file doesn't contain the key: 'GearName'");
					PageManager.SetItemPackNotWorking(filePath);
					return null;
				}
				UnityEngine.Object obj = Resources.Load(dict["GearName"]);
				if (obj is null)
				{
					Logger.LogWarning("Resources.Load could not find '{0}'", dict["GearName"]);
					PageManager.SetItemPackNotWorking(filePath);
					return null;
				}
				GameObject gameObject = obj.Cast<GameObject>();
				if (gameObject is null)
				{
					Logger.LogWarning("The gameobject was null in InitializeExistingFromFile.");
					PageManager.SetItemPackNotWorking(filePath);
					return null;
				}
				GameObject newObject = gameObject;
				//GameObject newObject = GameObject.Instantiate(gameObject);
				//newObject.name = gameObject.name;
				//newObject.SetActive(false);
				//GameObject.DontDestroyOnLoad(newObject);
				//AssetLoader.AlternateAssets.AddAlternateAsset(newObject);
				ComponentJson.InitializeComponents(ref newObject, dict);
				if (ComponentUtils.GetComponent<ModComponent>(newObject) is null) newObject.AddComponent<ModPlaceHolderComponent>();
				SetWeightChange(dict);
				return newObject;
			}
			catch (Exception e)
			{
				PageManager.SetItemPackNotWorking(filePath);
				Logger.LogError("Could not load existing json at '{0}'. {1}", filePath, e.Message);
				return null;
			}
		}

		private static void SetWeightChange(ProxyObject dict)
		{
			if (JsonUtils.ContainsKey(dict, "WeightKG"))
			{
				newWeight = dict["WeightKG"];
				needToChangeWeight = true;
			}
		}
		private static void ChangeWeight(GameObject item)
		{
			if (!needToChangeWeight) return;
			GearItem gearItem = ComponentUtils.GetComponent<GearItem>(item);
			if (gearItem == null)
			{
				Logger.Log("Could not assign new weight. Item has no GearItem component.");
				return;
			}
			gearItem.m_WeightKG = newWeight;
			needToChangeWeight = false;
		}
	}
}
