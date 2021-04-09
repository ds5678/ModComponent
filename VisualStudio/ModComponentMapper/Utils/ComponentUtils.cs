using ModComponentAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModComponentMapper
{
    public static class ComponentUtils
    {
        public static T GetComponent<T>(Component component) where T : Component
        {
            return GetComponent<T>(component ? component.gameObject : null);
        }

        public static T GetComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject == null) return default(T);
            else return gameObject.GetComponent<T>();
        }

        public static T GetOrCreateComponent<T>(Component component) where T : Component
        {
            return GetOrCreateComponent<T>(component ? component.gameObject : null);
        }

        public static T GetOrCreateComponent<T>(GameObject gameObject) where T : Component
        {
            T result = GetComponent<T>(gameObject);

            if (result == null) result = gameObject.AddComponent<T>();

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
            return GetModComponent(component ? component.gameObject : null);
        }

        internal static ModComponent GetModComponent(GameObject gameObject)
        {
            return GetComponent<ModComponent>(gameObject);
        }
    }
}
