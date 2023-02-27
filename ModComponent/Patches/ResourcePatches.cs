using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.AddressableAssets;
using ModComponent.AssetLoader;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement;
using UnityEngine.ResourceManagement.ResourceLocations;
using static Il2Cpp.Panel_Debug;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.AddressableAssets.Initialization;
using static Unity.VisualScripting.Member;
using UnityEngine.Playables;

namespace ModComponent.Patches;

static class ResourcePatches
{
	#region debug methods
	private static void PatchDebugMsg(string msg)
	{
		if (msg != null)
		{
#if DEBUG

			MelonLoader.MelonLogger.Msg(ConsoleColor.Yellow, msg);
#endif
		}
	}
	#endregion

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
			//PatchDebugMsg("Resources.Load1_PREFIX");
		}
		internal static void Postfix(string path, ref UnityEngine.Object __result, ref bool __runOriginal)
		{
			if (ModAssetBundleManager.IsKnownAsset(path))
			{
				//PatchDebugMsg("Resources.Load1_POSTFIX | " + path + " | IsKnown | null:" + (__result == null));
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
			//PatchDebugMsg("Resources.Load2_PREFIX");
		}
		internal static void Postfix(string path, ref UnityEngine.Object __result, ref bool __runOriginal)
		{
			if (ModAssetBundleManager.IsKnownAsset(path))
			{
				//PatchDebugMsg("Resources.Load2_POSTFIX | " + path + " | IsKnown | null:" + (__result == null));
				__result = ModAssetBundleManager.LoadAsset(path);
				__runOriginal = false;
			}
		}
	}
	#endregion

	#region AssetBundle.LoadAssetAsync
	[HarmonyPatch(typeof(AssetBundle), nameof(AssetBundle.LoadAssetAsync), new Type[] { typeof(string) })]
	internal static class AssetBundle_LoadAssetAsync1
	{
		internal static void Prefix(string name, ref bool __runOriginal)
		{
			//PatchDebugMsg("AssetBundle.LoadAssetAsync1_PREFIX");
		}
		internal static void Postfix(string name, ref AssetBundleRequest __result, ref bool __runOriginal)
		{
			//PatchDebugMsg("AssetBundle.LoadAssetAsync1_POSTFIX | " + name + " | null:" + (__result == null));
		}
	}
	[HarmonyPatch(typeof(AssetBundle), nameof(AssetBundle.LoadAssetAsync), new Type[] { typeof(string), typeof(Il2CppSystem.Type) })]
	internal static class AssetBundle_LoadAssetAsync2
	{
		internal static void Prefix(string name, Il2CppSystem.Type type, ref bool __runOriginal)
		{
			//PatchDebugMsg("AssetBundle.LoadAssetAsync2_PREFIX");
		}
		internal static void Postfix(string name, Il2CppSystem.Type type, ref AssetBundleRequest __result, ref bool __runOriginal)
		{
			//PatchDebugMsg("AssetBundle.LoadAssetAsync2_POSTFIX | " + name + " | " + type.ToString() + " |null:" + (__result == null));
		}
	}
	#endregion

	#region GearItemCoverflow.SetGearItem
#warning Not sure what this method does, but let's skip it for now (suppress error) :) - STBlade
	[HarmonyPatch(typeof(GearItemCoverflow), nameof(GearItemCoverflow.SetGearItem), new Type[] { typeof(GearItem), typeof(string), typeof(bool) })]
	internal static class LoadAllAssetsAsync
	{
		internal static void Prefix(GearItem gi, string gearPrefabName, ref bool __runOriginal)
		{
			//			PatchDebugMsg("GearItemCoverflow.SetGearItem_PREFIX | " + gearPrefabName);
			if (ModAssetBundleManager.IsKnownAsset(gearPrefabName))
			{
				__runOriginal = false;
			}
		}
		internal static void Postfix(GearItem gi, string gearPrefabName, ref bool __runOriginal)
		{
			//			PatchDebugMsg("GearItemCoverflow.SetGearItem_POSTFIX | " + gearPrefabName);
			if (ModAssetBundleManager.IsKnownAsset(gearPrefabName))
			{
				__runOriginal = false;
			}
		}
	}
	#endregion

	#region TextureCache.GetAddressableTexture
	[HarmonyPatch(typeof(TextureCache), nameof(TextureCache.GetAddressableTexture), new Type[] { typeof(string) })]
	internal static class GetAddressableTexture
	{
		internal static void Prefix(string name, ref bool __runOriginal)
		{
			//PatchDebugMsg("GetAddressableTexture_PREFIX | " + name);
			if (ModAssetBundleManager.IsKnownAsset(name))
			{
				__runOriginal = false;
			}
		}
		internal static void Postfix(string name, ref bool __runOriginal, ref Texture2D __result)
		{
			//PatchDebugMsg("GetAddressableTexture_POSTFIX | " + name + " | " + (__result != null));
			if (ModAssetBundleManager.IsKnownAsset(name) && __result == null)
			{
				__result = ModAssetBundleManager.LoadAsset(name).Cast<Texture2D>();
				TextureCache.CacheTexture(__result, name);
				__runOriginal = false;
			}
		}
	}
	#endregion

}
