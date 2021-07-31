using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModComponentUtils
{
	public static class ModUtils
	{
		public static bool AlmostZero(float value)
		{
			return Mathf.Abs(value) < 0.001f;
		}

		public static void CopyFields<T>(T copyTo, T copyFrom)
		{
			Type typeOfT = typeof(T);
			FieldInfo[] fieldInfos = typeOfT.GetFields();
			foreach (FieldInfo fieldInfo in fieldInfos)
			{
				fieldInfo.SetValue(copyTo, fieldInfo.GetValue(copyFrom));
			}
			if (fieldInfos.Length == 0)
			{
				Logger.LogError("There were no fields to copy!");
			}
		}

		public static string DefaultIfEmpty(string value, string defaultValue)
		{
			return string.IsNullOrEmpty(value) ? defaultValue : value;
		}

		public static bool IsNonGameScene()
		{
			return string.IsNullOrEmpty(GameManager.m_ActiveScene) || GameManager.m_ActiveScene == "MainMenu" || GameManager.m_ActiveScene == "Boot" || GameManager.m_ActiveScene == "Empty";
		}

		public static T[] NotNull<T>(T[] array)
		{
			if (array == null) return new T[0];
			else return array;
		}

		public static void PlayAudio(string audioName)
		{
			if (audioName != null) GameAudioManager.PlaySound(audioName, InterfaceManager.GetSoundEmitter());
		}



		internal static Delegate CreateDelegate(Type delegateType, object target, string methodName)
		{
			MethodInfo methodInfo = target.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if (methodInfo == null) return null;
			else return Delegate.CreateDelegate(delegateType, target, methodInfo);
		}

		public static GameObject GetChild(GameObject gameObject, string childName)
		{
			if (string.IsNullOrEmpty(childName)) return null;
			return gameObject?.transform?.FindChild(childName)?.gameObject;
		}

		public static GameObject GetParent(GameObject gameObject)
		{
			return gameObject?.transform?.parent?.gameObject;
		}

		public static GameObject GetSibling(GameObject gameObject, string siblingName)
		{
			return GetChild(GetParent(gameObject), siblingName);
		}

		/// <summary>
		/// Recursively finds the first child object with that name
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public static GameObject GetInChildren(GameObject parent, string childName)
		{
			if (string.IsNullOrEmpty(childName)) return null;
			Transform transform = parent.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject child = transform.GetChild(i).gameObject;
				if (child.name == childName) return child;
				else if (child.transform.childCount > 0)
				{
					GameObject grandChild = GetInChildren(child, childName);
					if (grandChild != null) return grandChild;
				}
			}
			return null;
		}

		internal static T GetItem<T>(string name, string reference = null) where T : UnityEngine.Component
		{
			GameObject gameObject = Resources.Load(name).TryCast<GameObject>();
			if (gameObject == null)
			{
				throw new ArgumentException("Could not load '" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + ".");
			}

			T targetType = ComponentUtils.GetComponent<T>(gameObject);
			if (targetType == null)
			{
				throw new ArgumentException("'" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + " is not a '" + typeof(T).Name + "'.");
			}

			return targetType;
		}

		internal static T[] GetItems<T>(string[] names, string reference = null) where T : UnityEngine.Component
		{
			T[] result = new T[names.Length];

			for (int i = 0; i < names.Length; i++)
			{
				result[i] = GetItem<T>(names[i], reference);
			}

			return result;
		}

		internal static T GetMatchingItem<T>(string name, string reference = null) where T : UnityEngine.Component
		{
			try
			{
				return GetItem<T>(name, reference);
			}
			catch (ArgumentException e)
			{
				Logger.LogError(e.Message);
				return default(T);
			}
		}

		internal static T[] GetMatchingItems<T>(string[] names, string reference = null) where T : UnityEngine.Component
		{
			names = NotNull(names);

			List<T> values = new List<T>();

			for (int i = 0; i < names.Length; i++)
			{
				var matchingItem = GetMatchingItem<T>(names[i], reference);
				if (matchingItem != null)
				{
					values.Add(matchingItem);
				}
			}

			return values.ToArray();
		}
	}
}