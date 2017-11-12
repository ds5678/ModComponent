using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModComponentMapper
{
    public struct GearSpawnInfo
    {
        public Vector3 Position;
        public string PrefabName;
        public Quaternion Rotation;
        public float SpawnChance;
    }

    public class GearSpawner
    {
        private static Dictionary<string, List<GearSpawnInfo>> gearSpawnInfos = new Dictionary<string, List<GearSpawnInfo>>();
        private static Dictionary<string, List<LootTableEntry>> lootTableEntries = new Dictionary<string, List<LootTableEntry>>();

        internal static void AddGearSpawnInfo(string sceneName, GearSpawnInfo gearSpawnInfo)
        {
            string normalizedSceneName = GetNormalizedSceneName(sceneName);
            if (!gearSpawnInfos.ContainsKey(normalizedSceneName))
            {
                gearSpawnInfos.Add(normalizedSceneName, new List<GearSpawnInfo>());
            }

            List<GearSpawnInfo> sceneGearSpawnInfos = gearSpawnInfos[normalizedSceneName];
            sceneGearSpawnInfos.Add(gearSpawnInfo);
        }

        internal static void AddLootTableEntry(string lootTable, GameObject prefab, int weight)
        {
            if (!lootTableEntries.ContainsKey(lootTable))
            {
                lootTableEntries.Add(lootTable, new List<LootTableEntry>());
            }

            LootTableEntry entry = new LootTableEntry();
            entry.Prefab = prefab;
            entry.Weight = weight;
            lootTableEntries[lootTable].Add(entry);
        }

        internal static void PrepareScene(string sceneName)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            string normalizedSceneName = GetNormalizedSceneName(sceneName);
            ConfigureLootTables(normalizedSceneName);
            SpawnGearForScene(normalizedSceneName);

            stopwatch.Stop();
            Log("Prepared scene '{0}' in {1} ms", sceneName, stopwatch.ElapsedMilliseconds);
        }

        private static void AddEntries(LootTable lootTable, List<LootTableEntry> entries)
        {
            foreach (LootTableEntry eachEntry in entries)
            {
                int index = GetIndex(lootTable, eachEntry.Prefab);
                if (index == -1)
                {
                    lootTable.m_Prefabs.Add(eachEntry.Prefab);
                    lootTable.m_Weights.Add(eachEntry.Weight);
                }
                else
                {
                    lootTable.m_Weights[index] = eachEntry.Weight;
                }
            }
        }

        private static void ConfigureLootTables(string sceneName)
        {
            LootTable[] lootTables = Resources.FindObjectsOfTypeAll<LootTable>();
            foreach (LootTable eachLootTable in lootTables)
            {
                List<LootTableEntry> entries;
                if (lootTableEntries.TryGetValue(eachLootTable.name, out entries))
                {
                    AddEntries(eachLootTable, entries);
                }
            }
        }

        private static int GetIndex(LootTable lootTable, GameObject prefab)
        {
            for (int i = 0; i < lootTable.m_Prefabs.Count; i++)
            {
                if (lootTable.m_Prefabs[i] != null && lootTable.m_Prefabs[i].name == prefab.name)
                {
                    return i;
                }
            }

            return -1;
        }

        private static string GetNormalizedGearName(string gearName)
        {
            if (gearName != null && !gearName.ToLower().StartsWith("gear_"))
            {
                return "gear_" + gearName;
            }

            return gearName;
        }

        private static string GetNormalizedSceneName(string sceneName)
        {
            return sceneName.ToLower();
        }

        private static IEnumerable<GearSpawnInfo> GetSpawnInfos(string sceneName)
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

        private static void SpawnGearForScene(string sceneName)
        {
            IEnumerable<GearSpawnInfo> sceneGearSpawnInfos = GetSpawnInfos(sceneName);
            if (sceneGearSpawnInfos == null)
            {
                return;
            }

            foreach (GearSpawnInfo eachGearSpawnInfo in sceneGearSpawnInfos)
            {
                string normalizedGearName = GetNormalizedGearName(eachGearSpawnInfo.PrefabName);
                Object prefab = Resources.Load(normalizedGearName);

                if (prefab == null)
                {
                    Log("Could not find prefab '{0}' to spawn in scene '{1}'.", eachGearSpawnInfo.PrefabName, sceneName);
                    continue;
                }

                if (Utils.RollChance(eachGearSpawnInfo.SpawnChance))
                {
                    Object gear = Object.Instantiate(prefab, eachGearSpawnInfo.Position, eachGearSpawnInfo.Rotation);
                    gear.name = prefab.name;
                }
            }
        }

        private struct LootTableEntry
        {
            public GameObject Prefab;
            public int Weight;
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
            if (scene != null && !string.IsNullOrEmpty(scene.name) && scene.name != "Empty" && mode == LoadSceneMode.Single)
            {
                GearSpawner.PrepareScene(scene.name);
            }
        }
    }
}