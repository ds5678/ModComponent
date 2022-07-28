using ModComponent.API.Components;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace ModComponent.Utils;

public static class ComponentUtils
{
	[return: NotNullIfNotNull("component")]
	public static T? GetComponentSafe<T>(this Component? component) where T : Component
	{
		return component == null ? default : GetComponentSafe<T>(component.GetGameObject());
	}

	[return: NotNullIfNotNull("gameObject")]
	public static T? GetComponentSafe<T>(this GameObject? gameObject) where T : Component
	{
		return gameObject == null ? default : gameObject.GetComponent<T>();
	}

	[return: NotNullIfNotNull("component")]
	public static T? GetOrCreateComponent<T>(this Component? component) where T : Component
	{
		return component == null ? default : GetOrCreateComponent<T>(component.GetGameObject());
	}

	[return: NotNullIfNotNull("gameObject")]
	public static T? GetOrCreateComponent<T>( this GameObject? gameObject) where T : Component
	{
		if (gameObject == null)
		{
			return default;
		}

		T? result = GetComponentSafe<T>(gameObject);

		if (result == null)
		{
			result = gameObject.AddComponent<T>();
		}

		return result;
	}

	[return: NotNullIfNotNull("component")]
	internal static ModBaseEquippableComponent? GetEquippableModComponent(this Component? component)
	{
		return GetComponentSafe<ModBaseEquippableComponent>(component);
	}

	[return: NotNullIfNotNull("gameObject")]
	internal static ModBaseEquippableComponent? GetEquippableModComponent(this GameObject? gameObject)
	{
		return GetComponentSafe<ModBaseEquippableComponent>(gameObject);
	}

	[return: NotNullIfNotNull("component")]
	internal static ModBaseComponent? GetModComponent(this Component? component)
	{
		return GetComponentSafe<ModBaseComponent>(component);
	}

	[return: NotNullIfNotNull("gameObject")]
	internal static ModBaseComponent? GetModComponent(this GameObject? gameObject)
	{
		return GetComponentSafe<ModBaseComponent>(gameObject);
	}

	[return: NotNullIfNotNull("component")]
	internal static GameObject? GetGameObject(this Component? component)
	{
		try
		{
			return component == null ? default : component.gameObject;
		}
#if !DEBUG
		catch { }
#else
		catch (System.Exception exception)
		{
			Logger.LogError($"Returning null since this could not obtain a Game Object from the component. Stack trace:\n{exception.Message}");
		}
#endif
		return null;
	}
}
