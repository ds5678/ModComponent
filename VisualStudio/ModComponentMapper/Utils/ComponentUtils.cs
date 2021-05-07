using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper
{
	public static class ComponentUtils
	{
		public static T GetComponent<T>(Component component) where T : Component
		{
			if (component is null) return default;
			else return GetComponent<T>(component.gameObject);
		}

		public static T GetComponent<T>(GameObject gameObject) where T : Component
		{
			if (gameObject is null) return default;
			else return gameObject.GetComponent<T>();
		}

		public static T GetOrCreateComponent<T>(Component component) where T : Component
		{
			return GetOrCreateComponent<T>(component ? component.gameObject : null);
		}

		public static T GetOrCreateComponent<T>(GameObject gameObject) where T : Component
		{
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
	}
}
