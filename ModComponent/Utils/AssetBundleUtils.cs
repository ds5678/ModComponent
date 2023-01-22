using Il2Cpp;
using Il2CppSystem;
using UnityEngine;
using Il2CppInterop;
using Il2CppInterop.Runtime.Injection;
using Il2CppInterop.Runtime;

namespace ModComponent.Utils;

/// <summary>
/// Alternative asset loading methods to avoid triggering the AssetLoader patches
/// </summary>
public static class AssetBundleUtils
{
	/// <summary>
	/// Loads an asset without triggering the AssetLoader patches
	/// </summary>
	public static UnityEngine.Object LoadAsset(AssetBundle assetBundle, string name)
	{
		return LoadAsset(assetBundle, name, Il2CppType.Of<UnityEngine.Object>());
	}
	/// <summary>
	/// Loads an asset without triggering the AssetLoader patches
	/// </summary>
	public static T? LoadAsset<T>(AssetBundle assetBundle, string name) where T : UnityEngine.Object
	{
		return LoadAsset(assetBundle, name, Il2CppType.Of<T>())?.TryCast<T>();
	}
	/// <summary>
	/// Loads an asset without triggering the AssetLoader patches
	/// </summary>
	public static UnityEngine.Object LoadAsset(AssetBundle assetBundle, string name, Il2CppSystem.Type type)
	{
		if (assetBundle == null)
		{
			throw new System.NullReferenceException("The asset bundle cannot be null.");
		}
		if (name == null)
		{
			throw new System.NullReferenceException("The input asset name cannot be null.");
		}
		if (name.Length == 0)
		{
			throw new System.ArgumentException("The input asset name cannot be empty.");
		}
		if (type == null)
		{
			throw new System.NullReferenceException("The input type cannot be null.");
		}
		return assetBundle.LoadAsset_Internal(name, type);
	}
}
