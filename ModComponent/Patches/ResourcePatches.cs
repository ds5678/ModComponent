using HarmonyLib;
using Il2Cpp;
using ModComponent.AssetLoader;
using System.Reflection;
using UnityEngine;

namespace ModComponent.Patches;

static class ResourcePatches
{
	// Hinterland loads assets by calling Resources.Load which ignores external AssetBundles
	// so we need to patch Resources.Load to redirect specific calls to load from the AssetBundle instead
	// Most of the gear items are in the resources
	[HarmonyPatch]
	internal static class Resources_Load
	{
		static MethodBase? TargetMethod()
		{
			MethodInfo[] methods = typeof(Resources).GetMethods();
			foreach (MethodInfo m in methods)
			{
				if (m.Name == nameof(Resources.Load) && m.ReturnType == typeof(UnityEngine.Object) && !m.IsGenericMethod && m.GetParameters().Length == 1)
				{
					return m;
				}
			}
			Logger.LogError("Resources.Load not found for patch.");
			return null;
		}
		internal static bool Prefix(ref string path, ref UnityEngine.Object __result)
		{
			if (!ModAssetBundleManager.IsKnownAsset(path))
			{
				return true;
			}

			__result = ModAssetBundleManager.LoadAsset(path);
			if (__result == null)
			{
				Logger.LogWarning("Resources.Load failed to load the external asset");
			}

			return false;
		}
	}

	//Hinterland stores many of its assets in asset bundles
	//This allows us to enable external asset loading in key locations
	//For example, paperdoll textures are loaded from asset bundles
	[HarmonyPatch(typeof(AssetBundle), nameof(AssetBundle.LoadAsset), new System.Type[] { typeof(string), typeof(Il2CppSystem.Type) })]
	internal static class AssetBundle_LoadAsset
	{
		private static bool Prefix(ref string name, ref UnityEngine.Object __result)
		{
			if (!ModAssetBundleManager.IsKnownAsset(name))
			{
				return true;
			}

			__result = ModAssetBundleManager.LoadAsset(name);
			if (__result == null)
			{
				Logger.LogWarning($"AssetBundle.LoadAsset failed to load the external asset '{name}'");
			}

			return false;
		}
	}

	[HarmonyPatch(typeof(Il2Cpp.GearItem), nameof(Il2Cpp.GearItem.LoadGearItemPrefab), new Type[] { typeof(string) })]
	internal static class GearItem_LoadGearItemPrefab
	{
		private static void Prefix(string name)
		{
			bool isKnown = ModAssetBundleManager.IsKnownAsset(name);
			//			MelonLoader.MelonLogger.Warning("GearItem_LoadGearItemPrefab_PRE | " + name + " | " + isKnown);
		}
		private static void Postfix(string name, ref Il2Cpp.GearItem __result)
		{
			bool isKnown = ModAssetBundleManager.IsKnownAsset(name);
			//			MelonLoader.MelonLogger.Warning("GearItem_LoadGearItemPrefab_POST | " + name + " | " + isKnown);
			if (isKnown == true && __result == null)
			{
				__result = ModAssetBundleManager.LoadAsset(name).Cast<GameObject>().GetComponent<GearItem>();
//				MelonLoader.MelonLogger.Warning("GearItem_LoadGearItemPrefab_POST LoadAsset | " + __result.name);
			}
		}
	}

}
