using Harmony;
using MelonLoader.TinyJSON;
using ModComponentAPI;
using ModComponentMapper.InformationMenu;
using ModComponentUtils;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ModComponentMapper
{
	internal class ModPlaceHolderComponent : ModComponentAPI.ModComponent
	{
		public ModPlaceHolderComponent(System.IntPtr intPtr) : base(intPtr) { }

		static ModPlaceHolderComponent() => UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentMapper.ModPlaceHolderComponent>();
	}

	internal class ModExistingEntry
	{
		public bool changeWeight = false;
		public float newWeight;
		public Dictionary<string, ProxyObject> behaviourChanges = new Dictionary<string, ProxyObject>();

		/// <summary>
		/// For testing in Unity Explorer
		/// </summary>
		public void ListBehaviourNames()
		{
			foreach (var pair in behaviourChanges)
			{
				Logger.Log(pair.Key);
			}
		}

		public void SetWeightChange(float newWeight)
		{
			this.changeWeight = true;
			this.newWeight = newWeight;
		}
	}

	public static class ModExisting
	{
		public const string EXISTING_JSON_DIRECTORY_NAME = "existing-json";
		private static bool isInitialized = false;

		private static Dictionary<string, ModExistingEntry> modExistingEntries = new Dictionary<string, ModExistingEntry>();

		public static string GetExistingJsonDirectory() => Path.Combine(ModComponentMain.Implementation.GetModsFolderPath(), EXISTING_JSON_DIRECTORY_NAME);

		public static void Initialize()
		{
			ReadDefinitions();
			ChangePrefabs();
			isInitialized = true;
		}

		public static bool IsInitialized() => isInitialized;

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
			if (string.IsNullOrEmpty(fileText)) return;
			ProxyObject dict = JSON.Load(fileText) as ProxyObject;
			if (!ModComponentUtils.JsonUtils.ContainsKey(dict, "GearName"))
			{
				Logger.LogWarning("The JSON file doesn't contain the key: 'GearName'");
				PageManager.SetItemPackNotWorking(filePath);
				return;
			}

			string gearName = dict["GearName"];
			ModExistingEntry modExistingEntry;
			if (modExistingEntries.ContainsKey(gearName)) modExistingEntry = modExistingEntries[gearName];
			else
			{
				modExistingEntry = new ModExistingEntry();
				modExistingEntries.Add(gearName, modExistingEntry);
			}
			foreach (var pair in dict)
			{
				if (pair.Key == "GearName") continue;
				else if (pair.Key == "WeightKG") modExistingEntry.SetWeightChange(pair.Value);
				else
				{
					ProxyObject data = (ProxyObject)pair.Value;
					if (modExistingEntry.behaviourChanges.ContainsKey(pair.Key)) modExistingEntry.behaviourChanges[pair.Key] = data;
					else modExistingEntry.behaviourChanges.Add(pair.Key, data);
				}
			}
		}

		private static void ChangeWeight(GameObject item, float newWeight)
		{
			GearItem gearItem = ModComponentUtils.ComponentUtils.GetComponent<GearItem>(item);
			if (gearItem is null)
			{
				Logger.Log("Could not assign new weight. Item has no GearItem component.");
				return;
			}
			gearItem.m_WeightKG = newWeight;
		}

		private static void ChangeGameObject(GameObject item)
		{

			if (item is null) return;
			string name = NameUtils.NormalizeName(item.name);
			if (!modExistingEntries.ContainsKey(name)) return;
			ModExistingEntry entry = modExistingEntries[name];
			if (entry.changeWeight)
			{
				ChangeWeight(item, entry.newWeight);
			}
			if (entry.behaviourChanges.Count > 0)
			{
				ProxyObject dict = JSON.Load(JSON.Dump(entry.behaviourChanges)) as ProxyObject;
				ComponentJson.InitializeComponents(ref item, dict);
				if (ModComponentUtils.ComponentUtils.GetComponent<ModComponent>(item) is null)
				{
					var placeholder = item.AddComponent<ModPlaceHolderComponent>();
					Mapper.Map(item);
					GameObject.Destroy(placeholder);
				}
				else Mapper.Map(item);
			}
		}

		private static void ChangePrefabs()
		{
			foreach(var pair in modExistingEntries)
			{
				GameObject item = Resources.Load(pair.Key).TryCast<GameObject>();
				if (item is null) continue;

				ModExistingEntry entry = pair.Value;
				if (entry.changeWeight)
				{
					ChangeWeight(item, entry.newWeight);
				}
				if (entry.behaviourChanges.Count > 0)
				{
					ProxyObject dict = JSON.Load(JSON.Dump(entry.behaviourChanges)) as ProxyObject;
					ComponentJson.InitializeComponents(ref item, dict);
					if (ModComponentUtils.ComponentUtils.GetComponent<ModComponent>(item) is null)
					{
						var placeholder = item.AddComponent<ModPlaceHolderComponent>();
						Mapper.Map(item);
						GameObject.Destroy(placeholder);
					}
					else Mapper.Map(item);
				}
			}
		}

		[HarmonyPatch(typeof(GearItem), "Awake")]
		internal static class GearItem_Awake
		{
			private static void Postfix(GearItem __instance)
			{
				FixName(__instance);
				if (!IsInitialized() || ComponentUtils.GetModComponent(__instance) != null) return;
				else ChangeGameObject(ComponentUtils.GetGameObject(__instance));
			}

			private static void FixName(Component component)
			{
				if (component?.name is null) return;
				if (component.name.Contains(" ("))
				{
					component.name = component.name.Substring(0, component.name.IndexOf(" ("));
				}
			}
		}
	}
}
