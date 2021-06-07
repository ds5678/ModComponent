using Harmony;
using ModComponentUtils;
using UnityEngine;

namespace SceneLoader
{
	public static class Preloader
	{
		private static bool initialized = false;
		internal static GameObject gameManagerObjectPrefab;

		private static void Initialize(GameManager gameManager)
		{
			if (initialized || gameManager is null) return;
			else
			{
				gameManagerObjectPrefab = new GameObject();
				Object.DontDestroyOnLoad(gameManagerObjectPrefab);
				gameManagerObjectPrefab.SetActive(false);
				gameManagerObjectPrefab.name = gameManager.name;

				CopyFieldHandler.CopyFieldsIl2Cpp(gameManagerObjectPrefab.AddComponent<GameManager>(), gameManager);
				initialized = true;
			}
		}

		public static void InstantiateGameManager()
		{
			if (gameManagerObjectPrefab is null)
			{
				MelonLoader.MelonLogger.LogError("gameManagerObjectPrefab is null!");
			}
			else
			{
				MelonLoader.MelonLogger.Log("instantiate");
				Object.Instantiate(gameManagerObjectPrefab).SetActive(true);
			}
		}

		[HarmonyPatch(typeof(GameManager), "Awake")]
		internal static class GameManager_Awake
		{
			private static void Postfix(GameManager __instance)
			{
				if (GameManager.m_ActiveScene == "MainMenu")
				{
					Initialize(__instance);
				}
				if (gameManagerObjectPrefab is null)
				{
					MelonLoader.MelonLogger.LogError("The GameManager prefab was destroyed!!!!!!!!!!!!");
				}
			}
		}
	}
}
