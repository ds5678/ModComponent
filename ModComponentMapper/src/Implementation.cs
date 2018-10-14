using System.Reflection;
using UnityEngine;

namespace ModComponentMapper
{
    public class Implementation
    {
        public static void OnLoad()
        {
            LogUtils.Log("Version {0}", Assembly.GetExecutingAssembly().GetName().Version);

            AutoMapper.Initialize();
            ModHealthManager.Initialize();
            GearSpawner.Initialize();
            BlueprintReader.Initialize();
        }
    }
}