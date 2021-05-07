using MelonLoader.TinyJSON;
using System.IO;
using UnityEngine;

namespace ModComponentMapper
{
	public static class JsonUtils
	{
		public static bool ContainsKey(MelonLoader.TinyJSON.ProxyObject dict, string key)
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

		public static ProxyObject GetDict(string path)
		{
			string text = File.ReadAllText(path);
			return JSON.Load(text) as ProxyObject;
		}

		public static float[] MakeFloatArray(ProxyArray proxy)
		{
			int count = 0;
			foreach (var item in proxy)
			{
				count++;
			}
			float[] result = new float[count];
			int i = 0;
			foreach (var item in proxy)
			{
				result[i] = item;
				i++;
			}
			return result;
		}

		public static int[] MakeIntArray(ProxyArray proxy)
		{
			int count = 0;
			foreach (var item in proxy)
			{
				count++;
			}
			int[] result = new int[count];
			int i = 0;
			foreach (var item in proxy)
			{
				result[i] = item;
				i++;
			}
			return result;
		}

		public static string[] MakeStringArray(ProxyArray proxy)
		{
			int count = 0;
			foreach (var item in proxy)
			{
				count++;
			}
			string[] result = new string[count];
			int i = 0;
			foreach (var item in proxy)
			{
				result[i] = item;
				i++;
			}
			return result;
		}

		public static Vector3 MakeVector(Variant array)
		{
			return new Vector3((float)array[0], (float)array[1], (float)array[2]);
		}

		internal static void TrySetBool(ref bool destination, ProxyObject dict, string className, string keyName)
		{
			if (!JsonUtils.ContainsKey(dict, className)) return;
			if (!JsonUtils.ContainsKey(dict[className] as ProxyObject, keyName)) return;
			destination = dict[className][keyName];
		}

		internal static void TrySetEnum<T>(ref T destination, ProxyObject dict, string className, string keyName) where T : System.Enum
		{
			if (!JsonUtils.ContainsKey(dict, className)) return;
			if (!JsonUtils.ContainsKey(dict[className] as ProxyObject, keyName)) return;
			destination = EnumUtils.ParseEnum<T>(dict[className][keyName]);
		}

		internal static void TrySetInt(ref int destination, ProxyObject dict, string className, string keyName)
		{
			if (!JsonUtils.ContainsKey(dict, className)) return;
			if (!JsonUtils.ContainsKey(dict[className] as ProxyObject, keyName)) return;
			destination = dict[className][keyName];
		}

		internal static void TrySetFloat(ref float destination, ProxyObject dict, string className, string keyName)
		{
			if (!JsonUtils.ContainsKey(dict, className)) return;
			if (!JsonUtils.ContainsKey(dict[className] as ProxyObject, keyName)) return;
			destination = dict[className][keyName];
		}

		internal static void TrySetString(ref string destination, ProxyObject dict, string className, string keyName)
		{
			if (!JsonUtils.ContainsKey(dict, className)) return;
			if (!JsonUtils.ContainsKey(dict[className] as ProxyObject, keyName)) return;
			destination = dict[className][keyName];
		}

		internal static void TrySetIntArray(ref int[] destination, ProxyObject dict, string className, string keyName)
		{
			if (!JsonUtils.ContainsKey(dict, className)) return;
			if (!JsonUtils.ContainsKey(dict[className] as ProxyObject, keyName)) return;
			destination = MakeIntArray(dict[className][keyName] as ProxyArray);
		}

		internal static void TrySetStringArray(ref string[] destination, ProxyObject dict, string className, string keyName)
		{
			if (!JsonUtils.ContainsKey(dict, className)) return;
			if (!JsonUtils.ContainsKey(dict[className] as ProxyObject, keyName)) return;
			destination = MakeStringArray(dict[className][keyName] as ProxyArray);
		}
	}
}
