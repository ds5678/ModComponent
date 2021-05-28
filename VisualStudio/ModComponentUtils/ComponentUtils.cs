using ModComponentAPI;
using UnityEngine;

namespace ModComponentUtils
{
	public static class ComponentUtils
	{
		public static T GetComponent<T>(Component component) where T : Component
		{
			if (component is null) return default;
			else return GetComponent<T>(GetGameObject(component));
		}

		public static T GetComponent<T>(GameObject gameObject) where T : Component
		{
			if (gameObject is null) return default;
			else return gameObject.GetComponent<T>();
		}

		public static T GetOrCreateComponent<T>(Component component) where T : Component
		{
			if (component is null) return default;
			else return GetOrCreateComponent<T>(GetGameObject(component));
		}

		public static T GetOrCreateComponent<T>(GameObject gameObject) where T : Component
		{
			if (gameObject is null) return default;

			T result = GetComponent<T>(gameObject);

			if (result is null) result = gameObject.AddComponent<T>();

			return result;
		}

		internal static EquippableModComponent GetEquippableModComponent(Component component)
		{
			return GetComponent<EquippableModComponent>(component);
		}

		internal static EquippableModComponent GetEquippableModComponent(GameObject gameObject)
		{
			return GetComponent<EquippableModComponent>(gameObject);
		}

		internal static ModComponent GetModComponent(Component component)
		{
			return GetComponent<ModComponent>(component);
		}

		internal static ModComponent GetModComponent(GameObject gameObject)
		{
			return GetComponent<ModComponent>(gameObject);
		}

		internal static GameObject GetGameObject(Component component)
		{
			try
			{
				if (component is null) return null;
				else return component.gameObject;
			}
			catch (System.Exception exception)
			{
				Logger.LogError("Returning null since this could not obtain a Game Object from the component. Stack trace:\n{0}", exception.Message);
				return null;
			}
		}
	}
}
