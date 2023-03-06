using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.AddressableAssets;
using ModComponent.AssetLoader;
using System.Reflection;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
	//[HarmonyPatch]
	//internal static class Resources_Load1
	//{
	//	static MethodBase? TargetMethod()
	//	{
	//		MethodInfo? targetMethod = typeof(Resources)
	//			.GetMethods()
	//			.FirstOrDefault(
	//				m => m.Name == nameof(Resources.Load)
	//				&& m.ReturnType == typeof(UnityEngine.Object)
	//				&& !m.IsGenericMethod
	//				&& m.GetParameters().Length == 1
	//				);
	//		if (targetMethod is null)
	//		{
	//			MelonLoader.MelonLogger.Error("Resources.Load 1 not found for patch.");
	//			return null;
	//		}

	//		return targetMethod;
	//	}
	//	internal static void Prefix(string path, ref bool __runOriginal)
	//	{
	//		//PatchDebugMsg("Resources.Load1_PREFIX");
	//	}
	//	internal static void Postfix(string path, ref UnityEngine.Object __result, ref bool __runOriginal)
	//	{
	//		if (ModAssetBundleManager.IsKnownAsset(path) && __result == null)
	//		{
	//			//PatchDebugMsg("Resources.Load1_POSTFIX | " + path + " | IsKnown | null:" + (__result == null));
	//			__result = ModAssetBundleManager.LoadAsset(path);
	//			__runOriginal = false;
	//		}
	//	}
	//}
	//[HarmonyPatch]
	//internal static class Resources_Load2
	//{
	//	static MethodBase? TargetMethod()
	//	{
	//		MethodInfo? targetMethod = typeof(Resources)
	//			.GetMethods()
	//			.FirstOrDefault(
	//				m => m.Name == nameof(Resources.Load)
	//				&& m.ReturnType == typeof(UnityEngine.Object)
	//				&& !m.IsGenericMethod
	//				&& m.GetParameters().Length == 2
	//				);
	//		if (targetMethod is null)
	//		{
	//			MelonLoader.MelonLogger.Error("Resources.Load 2 not found for patch.");
	//			return null;
	//		}

	//		return targetMethod;
	//	}
	//	internal static void Prefix(string path, ref bool __runOriginal)
	//	{
	//		//PatchDebugMsg("Resources.Load2_PREFIX");
	//	}
	//	internal static void Postfix(string path, ref UnityEngine.Object __result, ref bool __runOriginal)
	//	{
	//		if (ModAssetBundleManager.IsKnownAsset(path) && __result == null)
	//		{
	//			//PatchDebugMsg("Resources.Load2_POSTFIX | " + path + " | IsKnown | null:" + (__result == null));
	//			__result = ModAssetBundleManager.LoadAsset(path);
	//			__runOriginal = false;
	//		}
	//	}
	//}
	#endregion

	#region GearItem.LoadGearItemPrefab
	//[HarmonyPatch(typeof(GearItem), nameof(GearItem.LoadGearItemPrefab), new Type[] { typeof(string) })]
	//internal static class LoadGearItemPrefab
	//{
	//	internal static void Prefix(string name, ref bool __runOriginal)
	//	{
	//		//PatchDebugMsg("LoadGearItemPrefab_PREFIX | " + name);
	//		if (ModAssetBundleManager.IsKnownAsset(name))
	//		{
	//			__runOriginal = false;
	//		}
	//	}
	//	internal static void Postfix(string name, ref bool __runOriginal, ref GearItem __result)
	//	{
	//		if (ModAssetBundleManager.IsKnownAsset(name) && __result == null)
	//		{
	//			//PatchDebugMsg("LoadGearItemPrefab_POSTFIX | " + name + " | KNOWN | " + (__result != null));
	//			__result = ModAssetBundleManager.LoadAsset(name).Cast<GameObject>().GetComponent<GearItem>();
	//			__runOriginal = false;
	//		}
	//	}
	//}
	#endregion

	#region TextureCache.GetAddressableTexture
	//[HarmonyPatch(typeof(TextureCache), nameof(TextureCache.GetAddressableTexture), new Type[] { typeof(string) })]
	//internal static class GetAddressableTexture1
	//{
	//	internal static void Prefix(string name, ref bool __runOriginal)
	//	{
	//		//			PatchDebugMsg("GetAddressableTexture1_PREFIX | " + name);
	//		if (ModAssetBundleManager.IsKnownAsset(name))
	//		{
	//			__runOriginal = false;
	//		}
	//	}
	//	internal static void Postfix(string name, ref bool __runOriginal, ref Texture2D __result)
	//	{
	//		//PatchDebugMsg("GetAddressableTexture1_POSTFIX | " + name + " | " + (__result != null));
	//		if (ModAssetBundleManager.IsKnownAsset(name) && __result == null)
	//		{
	//			__result = ModAssetBundleManager.LoadAsset(name).Cast<Texture2D>();
	//			TextureCache.CacheTexture(__result, name);
	//			__runOriginal = false;
	//		}
	//	}
	//}
	//[HarmonyPatch(typeof(TextureCache), nameof(TextureCache.GetAddressableTexture), new Type[] { typeof(AssetReferenceTexture2D) })]
	//internal static class GetAddressableTexture2
	//{
	//	internal static void Prefix(AssetReferenceTexture2D assetReference, ref bool __runOriginal)
	//	{
	//		//PatchDebugMsg("GetAddressableTexture2_PREFIX | " + assetReference.m_AssetGUID);
	//		if (ModAssetBundleManager.IsKnownAsset(assetReference.m_AssetGUID))
	//		{
	//			__runOriginal = false;
	//		}
	//	}
	//	internal static void Postfix(AssetReferenceTexture2D assetReference, ref bool __runOriginal, ref Texture2D __result)
	//	{
	//		//PatchDebugMsg("GetAddressableTexture2_POSTFIX | " + assetReference.m_AssetGUID + " | " + (__result != null));
	//		if (ModAssetBundleManager.IsKnownAsset(assetReference.m_AssetGUID) && __result == null)
	//		{
	//			__result = ModAssetBundleManager.LoadAsset(assetReference.m_AssetGUID).Cast<Texture2D>();
	//			TextureCache.CacheTexture(__result, assetReference.m_AssetGUID);
	//			__runOriginal = false;
	//		}
	//	}
	//}
	#endregion

	#region GearItemCoverflow.SetGearItem
	//[HarmonyPatch(typeof(GearItemCoverflow), nameof(GearItemCoverflow.SetGearItem), new Type[] { typeof(GearItem), typeof(string), typeof(bool) })]
	//internal static class SetGearItem
	//{
	//	internal static void Prefix(GearItem gi, ref bool __runOriginal)
	//	{
	//		//PatchDebugMsg("SetGearItem_PREFIX | "+ gi.name + " |" + gi.GearItemData.CoverFlowBlendTexture.AssetGUID + "|" + gi.GearItemData.CoverFlowDamageTexture.AssetGUID + "|" + gi.GearItemData.CoverFlowMainTexture.AssetGUID + "|" + gi.GearItemData.CoverFlowOpenedTexture.AssetGUID);
	//		if (ModAssetBundleManager.IsKnownAsset(gi.name))
	//		{
	//			__runOriginal = false;
	//		}
	//	}
	//	internal static void Postfix(GearItemCoverflow __instance, GearItem gi, ref bool __runOriginal)
	//	{
	//		//PatchDebugMsg("SetGearItem_POSTFIX | " + gi.name);

	//		if (ModAssetBundleManager.IsKnownAsset(gi.name))
	//		{
	//			__instance.m_Texture.enabled = true;
	//			__instance.m_TextureWithDamage.enabled = false;
	//			__instance.m_Texture.mainTexture = Il2Cpp.Utils.GetGearCoverflowTexture(gi);
	//			__runOriginal = false;
	//		}

	//	}
	//}
	#endregion

}
