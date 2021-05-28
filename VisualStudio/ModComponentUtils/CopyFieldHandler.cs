using UnityEngine;
using static ModComponentUtils.NameUtils;

namespace ModComponentUtils
{
	internal static class CopyFieldHandler
	{
		public static void UpdateFieldValues<T>(T componentToUpdate) where T : UnityEngine.Component
		{
			string gearName = NormalizeName(componentToUpdate.name);
			GameObject prefab = Resources.Load(gearName)?.TryCast<GameObject>();
			if (prefab is null)
			{
				Logger.Log("While copying fields for '{0}', the prefab was null.");
			}
			else
			{
				T prefabComponent = prefab.GetComponent<T>();
				if (prefabComponent != null)
				{
					CopyFieldsMono<T>(componentToUpdate, prefabComponent);
				}
			}
		}

		public static void CopyFieldsMono<T>(T copyTo, T copyFrom)
		{
			System.Type typeOfT = typeof(T);
			System.Reflection.FieldInfo[] fieldInfos = typeOfT.GetFields();
			foreach (System.Reflection.FieldInfo fieldInfo in fieldInfos)
			{
				if (fieldInfo.IsInitOnly || fieldInfo.IsLiteral) continue;
				fieldInfo.SetValue(copyTo, fieldInfo.GetValue(copyFrom));
			}
			if (fieldInfos.Length == 0)
			{
				MelonLoader.MelonLogger.LogError("There were no fields to copy!");
			}
		}

		public static void CopyFieldsIl2Cpp<T>(T copyTo, T copyFrom) where T : Il2CppSystem.Object
		{
			Il2CppSystem.Type typeOfT = UnhollowerRuntimeLib.Il2CppType.Of<T>();
			Il2CppSystem.Reflection.FieldInfo[] fieldInfos = typeOfT.GetFields();
			foreach (Il2CppSystem.Reflection.FieldInfo fieldInfo in fieldInfos)
			{
				if (fieldInfo.IsInitOnly || fieldInfo.IsLiteral) continue;
				fieldInfo.SetValue(copyTo, fieldInfo.GetValue(copyFrom));
			}
			if (fieldInfos.Length == 0)
			{
				MelonLoader.MelonLogger.LogError("There were no fields to copy!");
			}
		}
	}
}
