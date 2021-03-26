using System;
using System.Reflection;
using UnityEngine;

namespace ModComponentAPI
{
    internal static class CopyFieldHandler
    {
        public static void UpdateFieldValues<T>(T componentToUpdate) where T : UnityEngine.Component
        {
            string gearName = componentToUpdate.name.Replace("(Clone)", "");
            //Implementation.Log(gearName);
            GameObject prefab = Resources.Load(gearName)?.Cast<GameObject>();
            if (prefab == null)
            {
                ModComponentMapper.Logger.Log("While copying fields for '{0}', the prefab was null.");
            }
            else
            {
                T prefabComponent = prefab.GetComponent<T>();
                if (prefabComponent != null)
                {
                    CopyFields<T>(componentToUpdate, prefabComponent);
                }
            }
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
                MelonLoader.MelonLogger.LogError("There were no fields to copy!");
            }
        }
    }
}
