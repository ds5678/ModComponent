using Il2Cpp;
using System.Reflection;
using UnityEngine;

namespace ModComponent.Utils;

public static class ModUtils
{
	private static readonly Dictionary<string, uint> eventIds = new();

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
		return array ?? Array.Empty<T>();
	}

	public static void PlayAudio(string audioName)
	{
		if (audioName != null)
		{
			GameAudioManager.PlaySound(audioName, InterfaceManager.GetSoundEmitter());
		}
	}

	internal static Delegate? CreateDelegate(Type delegateType, object target, string methodName)
	{
		MethodInfo methodInfo = target.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		return methodInfo == null
			? null
			: Delegate.CreateDelegate(delegateType, target, methodInfo);
	}

	public static GameObject? GetChild(this GameObject gameObject, string childName)
	{
		return string.IsNullOrEmpty(childName)
			|| gameObject == null
			|| gameObject.transform == null
			? null
			: gameObject.transform.FindChild(childName).GetGameObject();
	}

	public static GameObject? GetParent(this GameObject gameObject)
	{
		return gameObject == null
			|| gameObject.transform == null
			|| gameObject.transform.parent == null
			? null
			: gameObject.transform.parent.gameObject;
	}

	public static GameObject? GetSibling(this GameObject gameObject, string siblingName)
	{
		GameObject? parent = gameObject.GetParent();
		return parent == null ? null : parent.GetChild(siblingName);
	}

	/// <summary>
	/// Recursively finds the first child object with that name
	/// </summary>
	/// <param name="parent"></param>
	/// <param name="childName"></param>
	/// <returns></returns>
	public static GameObject? GetInChildren(GameObject parent, string childName)
	{
		if (string.IsNullOrEmpty(childName))
		{
			return null;
		}

		Transform transform = parent.transform;
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject child = transform.GetChild(i).gameObject;
			if (child.name == childName)
			{
				return child;
			}
			else if (child.transform.childCount > 0)
			{
				GameObject? grandChild = GetInChildren(child, childName);
				if (grandChild != null)
				{
					return grandChild;
				}
			}
		}
		return null;
	}

	internal static T GetItem<T>(string name, string? reference = null) where T : Component
	{
		GameObject? gameObject = AssetBundleUtils.LoadAsset<GameObject>(name);
		if (gameObject == null)
		{
			throw new ArgumentException("Could not load '" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + ".");
		}

		T targetType = ComponentUtils.GetComponentSafe<T>(gameObject);
		if (targetType == null)
		{
			throw new ArgumentException("'" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + " is not a '" + typeof(T).Name + "'.");
		}

		return targetType;
	}

	internal static T[] GetItems<T>(string[] names, string? reference = null) where T : Component
	{
		T[] result = new T[names.Length];

		for (int i = 0; i < names.Length; i++)
		{
			result[i] = GetItem<T>(names[i], reference);
		}

		return result;
	}

	internal static T? GetMatchingItem<T>(string name, string? reference = null) where T : Component
	{
		try
		{
			return GetItem<T>(name, reference);
		}
		catch (ArgumentException e)
		{
			Logger.LogError(e.Message);
			return default;
		}
	}

	internal static T[] GetMatchingItems<T>(string[] names, string? reference = null) where T : Component
	{
		names = NotNull(names);

		List<T> values = new();

		for (int i = 0; i < names.Length; i++)
		{
			T? matchingItem = GetMatchingItem<T>(names[i], reference);
			if (matchingItem != null)
			{
				values.Add(matchingItem);
			}
		}

		return values.ToArray();
	}

	internal static Il2CppAK.Wwise.Event? MakeAudioEvent(string? eventName)
	{
		if (string.IsNullOrEmpty(eventName) || GetAKEventIdFromString(eventName) == 0)
		{
			Il2CppAK.Wwise.Event emptyEvent = new();
			emptyEvent.WwiseObjectReference = ScriptableObject.CreateInstance<WwiseEventReference>();
			emptyEvent.WwiseObjectReference.objectName = "NULL_WWISEEVENT";
			emptyEvent.WwiseObjectReference.id = GetAKEventIdFromString("NULL_WWISEEVENT");
			return emptyEvent;
		}

		Il2CppAK.Wwise.Event newEvent = new();
		newEvent.WwiseObjectReference = ScriptableObject.CreateInstance<WwiseEventReference>();
		newEvent.WwiseObjectReference.objectName = eventName;
		newEvent.WwiseObjectReference.id = GetAKEventIdFromString(eventName);
		return newEvent;
	}

	private static uint GetAKEventIdFromString(string eventName)
	{
		if (eventIds.Count == 0)
		{
			Type type = typeof(Il2CppAK.EVENTS);
			foreach (PropertyInfo prop in type.GetProperties(BindingFlags.Static | BindingFlags.Public))
			{
				string key = prop.Name.ToLowerInvariant();
				uint value = (uint)prop.GetValue(null)!;
				eventIds.Add(key, value);
			}
		}

		eventIds.TryGetValue(eventName.ToLowerInvariant(), out uint id);
		return id;
	}
}