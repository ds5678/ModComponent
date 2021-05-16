using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using USceneManager = UnityEngine.SceneManagement.SceneManager;

namespace Preloader
{
	internal static class AlternativePreloader
	{
		internal static void PreloadScenes()
		{
			PreloadingManager.preloadingObjects = true;
			Logger.LogBlue("Preloading player object");
			PreloadPlayerObject();
			Logger.LogBlue("Preloading other objects");
			foreach (var pair in SceneObjects.loadList)
			{
				PreloadScene(pair.Key, pair.Value);
			}
			Logger.LogBlue("Done in IEnumerator");
			PreloadingManager.preloadingObjects = false;
		}

		internal static void PreloadScene(string sceneName, List<string> objNames)
		{
			Logger.Log($"Loading scene \"{sceneName}\"");

			var async = USceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
			async.allowSceneActivation = false;

			while (!async.isDone)
			{
				//Logger.Log(async.progress.ToString());
			}
			Scene scene = USceneManager.GetSceneByName(sceneName);
			GameObject[] rootObjects = scene.GetRootGameObjects();
			foreach (var go in rootObjects)
			{
				go.SetActive(false);
			}

			Logger.Log($"Fetching objects");

			foreach (string objName in objNames)
			{
				//Logger.Log($"Fetching object \"{objName}\"");
				Logger.LogBlue($"Fetching object \"{objName}\"");
				// Split object name into root and child names based on '/'
				string rootName;
				string childName = null;

				int slashIndex = objName.IndexOf('/');
				if (slashIndex == -1)
				{
					rootName = objName;
				}
				else if (slashIndex == 0 || slashIndex == objName.Length - 1)
				{
					Logger.LogWarning($"Invalid preload object name given: \"{objName}\"");
					continue;
				}
				else
				{
					rootName = objName.Substring(0, slashIndex);
					childName = objName.Substring(slashIndex + 1);
				}

				// Get root object
				GameObject obj = rootObjects.FirstOrDefault(o => o.name == rootName);
				if (obj == null)
				{
					Logger.LogWarning($"Could not find root object \"{rootName}\" in scene \"{sceneName}\"");
					continue;
				}

				// Get child object
				if (childName != null)
				{
					Transform t = obj.transform.Find(childName);
					if (t == null)
					{
						Logger.LogWarning($"Could not find child object \"{objName}\" in scene \"{sceneName}\"");
						continue;
					}

					obj = t.gameObject;
				}

				if (!PreloadingManager.modPreloadedObjects.TryGetValue
				(
					sceneName,
					out Dictionary<string, GameObject> modScenePreloadedObjects
				))
				{
					modScenePreloadedObjects = new Dictionary<string, GameObject>();
					PreloadingManager.modPreloadedObjects[sceneName] = modScenePreloadedObjects;
				}

				// Create inactive duplicate of requested object
				obj = Object.Instantiate(obj);
				obj.name = ModComponentUtils.NameUtils.NormalizeName(obj.name);
				Object.DontDestroyOnLoad(obj);
				obj.SetActive(false);

				//Check for static batch
				var renderer = obj.GetComponentInChildren<Renderer>();
				if (renderer.isPartOfStaticBatch)
				{
					Logger.LogWarning("'{0}' is part of a static batch", obj.name);
				}
				// Set object to be passed to mod
				modScenePreloadedObjects[objName] = obj;
			}

			var unloadAsync = USceneManager.UnloadSceneAsync(scene);
			while (!unloadAsync.isDone)
			{
				
			}
		}
		internal static void PreloadPlayerObject()
		{
			Logger.Log("Loading CampOffice scene for player object");
			string objName = "Root/Design/Scripting/SCRIPT_GameManager";

			var async = USceneManager.LoadSceneAsync("CampOffice", LoadSceneMode.Single);
			async.allowSceneActivation = false;
			//USceneManager.LoadScene("CampOffice", LoadSceneMode.Single);

			while (!async.isDone)
			{
				//Logger.Log(async.progress.ToString());
			}
			Scene scene = USceneManager.GetSceneByName("CampOffice");
			GameObject[] rootObjects = scene.GetRootGameObjects();
			foreach (var go in rootObjects)
			{
				go.SetActive(false);
			}

			Logger.LogBlue($"Fetching object \"{objName}\"");
			// Split object name into root and child names based on '/'
			string rootName = "Root";
			string childName = "Design/Scripting/SCRIPT_GameManager";

			// Get root object
			GameObject obj = rootObjects.FirstOrDefault(o => o.name == rootName);
			if (obj == null)
			{
				Logger.LogWarning($"Could not find root object \"{rootName}\"");
			}

			// Get child object
			if (childName != null)
			{
				Transform t = obj?.transform.Find(childName);
				if (t == null)
				{
					Logger.LogWarning($"Could not find child object \"{objName}\"");
				}
				else
				{
					obj = t.gameObject;
				}
			}
			else Logger.LogWarning("null childname");

			if (!PreloadingManager.modPreloadedObjects.TryGetValue
			(
				"CampOffice",
				out Dictionary<string, GameObject> modScenePreloadedObjects
			))
			{
				modScenePreloadedObjects = new Dictionary<string, GameObject>();
				PreloadingManager.modPreloadedObjects["CampOffice"] = modScenePreloadedObjects;
			}

			GameManager gameManager = obj?.GetComponent<GameManager>();
			if (gameManager?.m_PlayerObjectPrefab != null)
			{
				GameObject prefab = Object.Instantiate(gameManager.m_PlayerObjectPrefab);
				prefab.name = ModComponentUtils.NameUtils.NormalizeName(prefab.name);
				Object.DontDestroyOnLoad(prefab);
				prefab.SetActive(false);
				modScenePreloadedObjects[prefab.name] = prefab;
			}
			else
			{
				Logger.LogError("No GameManager component on '{0}'!", obj is null ? "null" : obj.name );
			}

			var unloadAsync = USceneManager.UnloadSceneAsync("CampOffice");
			while (!unloadAsync.isDone)
			{
				
			}
		}
	}
}
