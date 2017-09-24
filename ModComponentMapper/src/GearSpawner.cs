using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModComponentMapper
{
    public struct GearSpawnInfo
    {
        public string PrefabName;
        public Vector3 Position;
        public Quaternion Rotation;
        public float SpawnChance;
    }

    public class GearSpawner
    {
        private static Dictionary<string, List<GearSpawnInfo>> gearSpawnInfos = new Dictionary<string, List<GearSpawnInfo>>();

        internal static void SpawnGearForScene(string sceneName)
        {
            IEnumerable<GearSpawnInfo> sceneGearSpawnInfos = GetSpawnInfos(sceneName);
            if (sceneGearSpawnInfos == null)
            {
                return;
            }

            foreach (GearSpawnInfo eachGearSpawnInfo in sceneGearSpawnInfos)
            {
                Object prefab = Resources.Load(eachGearSpawnInfo.PrefabName);
                if (prefab == null)
                {
                    Log("Could not find prefab '{0}' to spawn in scene '{1}'.", eachGearSpawnInfo.PrefabName, sceneName);
                    continue;
                }

                if (Random.Range(0, 1) > eachGearSpawnInfo.SpawnChance)
                {
                    continue;
                }

                Object gear = Object.Instantiate(prefab, eachGearSpawnInfo.Position, eachGearSpawnInfo.Rotation);
                gear.name = prefab.name;
            }
        }

        internal static void AddGearSpawnInfo(string sceneName, GearSpawnInfo gearSpawnInfo)
        {
            if (!gearSpawnInfos.ContainsKey(sceneName))
            {
                gearSpawnInfos.Add(sceneName, new List<GearSpawnInfo>());
            }

            List<GearSpawnInfo> sceneGearSpawnInfos = gearSpawnInfos[sceneName];
            sceneGearSpawnInfos.Add(gearSpawnInfo);
        }

        public static IEnumerable<GearSpawnInfo> GetSpawnInfos(string sceneName)
        {
            List<GearSpawnInfo> result;
            gearSpawnInfos.TryGetValue(sceneName, out result);
            return result;
        }

        private static void Log(string message)
        {
            LogUtils.Log("GearSpawner", message);
        }

        private static void Log(string message, params object[] parameters)
        {
            LogUtils.Log("GearSpawner", message, parameters);
        }
    }

    internal class SceneManagerPatch
    {
        public static void OnLoad()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene != null && !string.IsNullOrEmpty(scene.name) && mode == LoadSceneMode.Single)
            {
                GearSpawner.SpawnGearForScene(scene.name);
            }
        }
    }
}
