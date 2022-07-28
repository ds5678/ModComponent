using MelonLoader.TinyJSON;
using ModComponent.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.API;

internal static class TinyJsonExtensions
{
	internal static bool ContainsKey(this ProxyObject dict, string key)
	{
		foreach (var pair in dict)
		{
			if (pair.Key == key)
			{
				return true;
			}
		}
		return false;
	}

	internal static Variant GetVariant(this ProxyObject dict, string className, string fieldName)
	{
		Variant subDict;
		try
		{
			subDict = dict[className];
		}
		catch (KeyNotFoundException ex)
		{
			throw new Exception($"The json doesn't have an entry for '{className}'", ex);
		}
		try
		{
			return subDict[fieldName];
		}
		catch (KeyNotFoundException ex)
		{
			throw new Exception($"The '{className}' entry in the json doesn't have a field for '{fieldName}'", ex);
		}
	}

	internal static T GetEnum<T>(this ProxyObject dict, string className, string fieldName) where T : Enum
	{
		return EnumUtils.ParseEnum<T>(dict.GetVariant(className, fieldName));
	}

	internal static ProxyArray GetProxyArray(this ProxyObject dict, string className, string fieldName)
	{
		return dict.GetVariant(className, fieldName) as ProxyArray
			   ?? throw new Exception($"The field '{fieldName}' in entry '{className}' is not an array");
	}

	private static float[] ConvertToFloatArray(this ProxyArray proxy)
	{
		List<float> result = new List<float>();
		foreach (var item in proxy)
		{
			result.Add(item);
		}
		return result.ToArray();
	}

	internal static float[] GetFloatArray(this ProxyObject dict, string className, string fieldName)
	{
		return dict.GetProxyArray(className, fieldName).ConvertToFloatArray();
	}

	private static int[] ConvertToIntArray(this ProxyArray proxy)
	{
		List<int> result = new List<int>();
		foreach (var item in proxy)
		{
			result.Add(item);
		}
		return result.ToArray();
	}

	internal static int[] GetIntArray(this ProxyObject dict, string className, string fieldName)
	{
		return dict.GetProxyArray(className, fieldName).ConvertToIntArray();
	}

	private static string[] ConvertToStringArray(this ProxyArray proxy)
	{
		List<string> result = new List<string>();
		foreach (var item in proxy)
		{
			result.Add(item);
		}
		return result.ToArray();
	}

	internal static string[] GetStringArray(this ProxyObject dict, string className, string fieldName)
	{
		return dict.GetProxyArray(className, fieldName).ConvertToStringArray();
	}

	private static Vector3 ConvertToVector3(this Variant array)
	{
		return new Vector3((float)array[0], (float)array[1], (float)array[2]);
	}

	internal static Vector3 GetVector3(this ProxyObject dict, string className, string fieldName)
	{
		return dict.GetVariant(className, fieldName).ConvertToVector3();
	}
}
