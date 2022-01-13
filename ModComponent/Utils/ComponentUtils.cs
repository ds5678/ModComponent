using ModComponent.API.Components;
using UnityEngine;

namespace ModComponent.Utils
{
	public static class ComponentUtils
	{
		public static T GetComponentSafe<T>(this Component component) where T : Component
		{
			if (component == null)
				return default;
			else
				return GetComponentSafe<T>(component.GetGameObject());
		}

		public static T GetComponentSafe<T>(this GameObject gameObject) where T : Component
		{
			if (gameObject == null)
				return default;
			else
				return gameObject.GetComponent<T>();
		}

		public static T GetOrCreateComponent<T>(this Component component) where T : Component
		{
			if (component == null)
				return default;
			else
				return GetOrCreateComponent<T>(component.GetGameObject());
		}

		public static T GetOrCreateComponent<T>(this GameObject gameObject) where T : Component
		{
			if (gameObject == null)
				return default;

			T result = GetComponentSafe<T>(gameObject);

			if (result == null)
				result = gameObject.AddComponent<T>();

			return result;
		}

		internal static ModBaseEquippableComponent GetEquippableModComponent(this Component component)
		{
			return GetComponentSafe<ModBaseEquippableComponent>(component);
		}

		internal static ModBaseEquippableComponent GetEquippableModComponent(this GameObject gameObject)
		{
			return GetComponentSafe<ModBaseEquippableComponent>(gameObject);
		}

		internal static ModBaseComponent GetModComponent(this Component component)
		{
			return GetComponentSafe<ModBaseComponent>(component);
		}

		internal static ModBaseComponent GetModComponent(this GameObject gameObject)
		{
			return GetComponentSafe<ModBaseComponent>(gameObject);
		}

		internal static GameObject GetGameObject(this Component component)
		{
			try
			{
				if (component == null)
					return default;
				else
					return component.gameObject;
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
}
