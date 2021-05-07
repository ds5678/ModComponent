using System.Collections.Generic;
using UnityEngine;

namespace Preloader
{
	public static class PreloadingManager
	{
		private static bool alreadyRun = false;
		internal static bool preloadingObjects = false;
		internal static bool loadingMainMenu = false;
		/// <summary>
		/// scene name, path to object in scene, object prefab
		/// </summary>
		internal static Dictionary<string, Dictionary<string, GameObject>> modPreloadedObjects = new Dictionary<string, Dictionary<string, GameObject>>();

		internal static void Initialize()
		{
#if DEBUG
			SceneObjects.AddToList(SceneObjects.defaultLoadList);
#endif
		}

		internal static void OnGUI()
		{
			if (preloadingObjects)
			{
				var style = new UnityEngine.GUIStyle(GUI.skin.label);
				style.alignment = TextAnchor.MiddleCenter;
				style.fontSize = 30;
				UnityEngine.GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), "Preloading Game Assets", style);
			}
			else if (loadingMainMenu)
			{
				var style = new UnityEngine.GUIStyle(GUI.skin.label);
				style.alignment = TextAnchor.MiddleCenter;
				style.fontSize = 30;
				UnityEngine.GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height - 100, 400, 100), "Loading Main Menu", style);
			}
		}
		public static void AddToList(string scene, string path) => SceneObjects.AddToList(scene, path);
		public static void AddToList(Dictionary<string, List<string>> otherList) => SceneObjects.AddToList(otherList);
		internal static void MaybePreloadScenes()
		{
			if (alreadyRun || UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "MainMenu") return;
			alreadyRun = true;
			MelonLoader.MelonCoroutines.Start(ScenePreloader.PreloadScenes());
		}


		public static GameObject GetPrefab(string objectName)
		{
			foreach (var scenePair in modPreloadedObjects)
			{
				foreach (var objectPair in scenePair.Value)
				{
					if (objectPair.Value?.name == objectName)
					{
						return objectPair.Value;
					}
				}
			}
			return null;
		}
		public static GameObject GetPrefab(string sceneName, string path)
		{
			if (!modPreloadedObjects.ContainsKey(sceneName) || !modPreloadedObjects[sceneName].ContainsKey(path))
			{
				Logger.LogError("There is no preloaded object from '{0}' at '{1}'", sceneName, path);
				return null;
			}
			else return modPreloadedObjects[sceneName][path];
		}
		public static GameObject GetInstance(string objectName)
		{
			GameObject gameObject = GetPrefab(objectName);
			if (gameObject) return GameObject.Instantiate(gameObject);
			else return null;
		}
		public static GameObject GetInstance(string sceneName, string path)
		{
			GameObject gameObject = GetPrefab(sceneName, path);
			if (gameObject) return GameObject.Instantiate(gameObject);
			else return null;
		}
	}

}
