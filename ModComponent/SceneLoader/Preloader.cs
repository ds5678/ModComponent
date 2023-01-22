﻿using Il2Cpp;
using HarmonyLib;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.SceneLoader;

public static class Preloader
{
	private static bool initialized = false;
	internal static GameObject? gameManagerObjectPrefab;

	private static void Initialize(GameManager gameManager)
	{
		if (initialized || gameManager == null)
		{
			return;
		}
		else
		{
			gameManagerObjectPrefab = new GameObject();
			GameObject.DontDestroyOnLoad(gameManagerObjectPrefab);
			gameManagerObjectPrefab.SetActive(false);
			gameManagerObjectPrefab.name = gameManager.name;

			CopyFieldHandler.CopyFieldsIl2Cpp(gameManagerObjectPrefab.AddComponent<GameManager>(), gameManager);
			initialized = true;
		}
	}

	public static void InstantiateGameManager()
	{
		if (gameManagerObjectPrefab == null)
		{
			Logger.LogError("gameManagerObjectPrefab == null!");
		}
		else
		{
			Logger.Log("instantiate");
			GameObject.Instantiate(gameManagerObjectPrefab).SetActive(true);
		}
	}

	[HarmonyPatch(typeof(GameManager), nameof(GameManager.Awake))]
	internal static class GameManager_Awake
	{
		private static void Postfix(GameManager __instance)
		{
			if (GameManager.m_ActiveScene == "MainMenu")
			{
				Initialize(__instance);
			}
			if (gameManagerObjectPrefab == null)
			{
				Logger.LogError("The GameManager prefab was destroyed!!!!!!!!!!!!");
			}
		}
	}
}
