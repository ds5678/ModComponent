using ModComponent.API.Components;
using UnityEngine;

namespace ModComponent.Utils
{
	public static class ComponentUtils
	{
		public static T GetComponent<T>(Component component) where T : Component
		{
			return component?.GetGameObject()?.GetComponent<T>();
		}

		public static T GetComponent<T>(GameObject gameObject) where T : Component
		{
			return gameObject?.GetComponent<T>();
		}

		public static T GetOrCreateComponent<T>(this Component component) where T : Component
		{
			return component?.GetGameObject()?.GetOrCreateComponent<T>();
		}

		public static T GetOrCreateComponent<T>(this GameObject gameObject) where T : Component
		{
			if (gameObject == null) return default;

			T result = GetComponent<T>(gameObject);

			if (result == null) result = gameObject.AddComponent<T>();

			return result;
		}

		internal static ModBaseEquippableComponent GetEquippableModComponent(this Component component)
		{
			return GetComponent<ModBaseEquippableComponent>(component);
		}

		internal static ModBaseEquippableComponent GetEquippableModComponent(this GameObject gameObject)
		{
			return GetComponent<ModBaseEquippableComponent>(gameObject);
		}

		internal static ModBaseComponent GetModComponent(this Component component)
		{
			return GetComponent<ModBaseComponent>(component);
		}

		internal static ModBaseComponent GetModComponent(this GameObject gameObject)
		{
			return GetComponent<ModBaseComponent>(gameObject);
		}

		internal static GameObject GetGameObject(this Component component)
		{
			try
			{
				return component?.gameObject;
			}
#if !DEBUG
			catch { }
#else
			catch (System.Exception exception)
			{
				Logger.LogError("Returning null since this could not obtain a Game Object from the component. Stack trace:\n{0}", exception.Message);
			}
#endif
			return null;
		}
	}
}
