using HarmonyLib;
using ModComponent.AssetLoader;
using System.Reflection;
using UnityEngine;

namespace ModComponent.Patches;

static class ResourcePatches
{
	#region Resources.Load
	// Hinterland loads assets by calling Resources.Load which ignores external AssetBundles
	// so we need to patch Resources.Load to redirect specific calls to load from the AssetBundle instead
	// Most of the gear items are in the resources
	[HarmonyPatch]
	internal static class Resources_Load1
	{
		static MethodBase? TargetMethod()
		{
			MethodInfo? targetMethod = typeof(Resources)
				.GetMethods()
				.FirstOrDefault(
					m => m.Name == nameof(Resources.Load)
					&& m.ReturnType == typeof(UnityEngine.Object)
					&& !m.IsGenericMethod
					&& m.GetParameters().Length == 1
					);
			if (targetMethod is null)
			{
				MelonLoader.MelonLogger.Error("Resources.Load 1 not found for patch.");
				return null;
			}

			return targetMethod;
		}
		internal static void Prefix(string path, ref bool __runOriginal)
		{
			if (ModAssetBundleManager.IsKnownAsset(path))
			{
				__runOriginal = false;
			}
		}
		internal static void Postfix(string path, ref UnityEngine.Object __result, ref bool __runOriginal)
		{
			if (ModAssetBundleManager.IsKnownAsset(path))
			{
				MelonLoader.MelonLogger.Warning("Resources.Load 1 | " + path + " | IsKnown | null:" + (__result == null));
				__result = ModAssetBundleManager.LoadAsset(path);
				__runOriginal = false;
			}
		}
	}
	[HarmonyPatch]
	internal static class Resources_Load2
	{
		static MethodBase? TargetMethod()
		{
			MethodInfo? targetMethod = typeof(Resources)
				.GetMethods()
				.FirstOrDefault(
					m => m.Name == nameof(Resources.Load)
					&& m.ReturnType == typeof(UnityEngine.Object)
					&& !m.IsGenericMethod
					&& m.GetParameters().Length == 2
					);
			if (targetMethod is null)
			{
				MelonLoader.MelonLogger.Error("Resources.Load 2 not found for patch.");
				return null;
			}

			return targetMethod;
		}
		internal static void Prefix(string path, ref bool __runOriginal)
		{
			if (ModAssetBundleManager.IsKnownAsset(path))
			{
				__runOriginal = false;
			}
		}
		internal static void Postfix(string path, ref UnityEngine.Object __result, ref bool __runOriginal)
		{
			if (ModAssetBundleManager.IsKnownAsset(path))
			{
				MelonLoader.MelonLogger.Warning("Resources.Load 2 | " + path + " | IsKnown | null:" + (__result == null));
				__result = ModAssetBundleManager.LoadAsset(path);
				__runOriginal = false;
			}
		}
	}
	#endregion






}
