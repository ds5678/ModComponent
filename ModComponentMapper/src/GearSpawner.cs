using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//did a first pass through; didn't find anything
//MIGHT have some classes that need to be declared

namespace ModComponentMapper
{
    public struct GearSpawnInfo //Might need to be declared
    {
        public Vector3 Position;
        public string PrefabName;
        public Quaternion Rotation;
        public float SpawnChance;
    }

    public struct LootTableEntry //Might need to be declared
    {
        public string PrefabName;
        public int Weight;
    }

    public class GearSpawner //does not need to be declared
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

        internal static void AddLootTableEntry(string lootTable, LootTableEntry entry)
        {
            string normalizedLootTableName = GetNormalizedLootTableName(lootTable);

            if (!lootTableEntries.ContainsKey(normalizedLootTableName))
            {
                lootTableEntries.Add(normalizedLootTableName, new List<LootTableEntry>());
            }

            entry.PrefabName = NormalizePrefabName(entry.PrefabName);
            entry.Weight = Mathf.Clamp(entry.Weight, 0, int.MaxValue);

            lootTableEntries[normalizedLootTableName].Add(entry);
        }

        internal static void Initialize()
        {
            GearSpawnReader.ReadDefinitions();

            Implementation.OnSceneReady += PrepareScene;
        }

        private static void AddEntries(LootTable lootTable, List<LootTableEntry> entries)
        {
            foreach (LootTableEntry eachEntry in entries)
            {
                int index = GetIndex(lootTable, eachEntry.PrefabName);
                if (index != -1)
                {
                    lootTable.m_Weights[index] = eachEntry.Weight;
                    continue;
                }

                GameObject prefab = Resources.Load(eachEntry.PrefabName).Cast<GameObject>();
                if (prefab == null)
                {
                    Implementation.Log("Could not find prefab '{0}'.", eachEntry.PrefabName);
                    continue;
                }

                lootTable.m_Prefabs.Add(prefab);
                lootTable.m_Weights.Add(eachEntry.Weight);
            }
        }

        private static void ConfigureLootTables(string sceneName)
        {
            LootTable[] lootTables = Resources.FindObjectsOfTypeAll<LootTable>();
            foreach (LootTable eachLootTable in lootTables)
            {
                List<LootTableEntry> entries;
                if (lootTableEntries.TryGetValue(eachLootTable.name.ToLower(), out entries))
                {
                    AddEntries(eachLootTable, entries);
                }
            }
        }

        private static int GetIndex(LootTable lootTable, string prefabName)
        {
            for (int i = 0; i < lootTable.m_Prefabs.Count; i++)
            {
                if (lootTable.m_Prefabs[i] != null && lootTable.m_Prefabs[i].name.Equals(prefabName, System.StringComparison.InvariantCultureIgnoreCase))
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

        private static string GetNormalizedLootTableName(string lootTable)
        {
            if (lootTable.StartsWith("Loot", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return lootTable.ToLower();
            }

            if (lootTable.StartsWith("Cargo", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return "loot" + lootTable.ToLower();
            }

            return "loottable" + lootTable.ToLower();
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

        private static string NormalizePrefabName(string prefabName)
        {
            if (!prefabName.StartsWith("gear_", System.StringComparison.InvariantCultureIgnoreCase))
            {
                return "gear_" + prefabName;
            }

            return prefabName;
        }

        private static void PrepareScene()
        {
            if (ModUtils.IsNonGameScene())
            {
                return;
            }

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            string normalizedSceneName = GetNormalizedSceneName(GameManager.m_ActiveScene);
            ConfigureLootTables(normalizedSceneName);
            SpawnGearForScene(normalizedSceneName);

            stopwatch.Stop();
            Implementation.Log("Spawned items for scene '{0}' in {1} ms", GameManager.m_ActiveScene, stopwatch.ElapsedMilliseconds);
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
                    Implementation.Log("Could not find prefab '{0}' to spawn in scene '{1}'.", eachGearSpawnInfo.PrefabName, sceneName);
                    continue;
                }

                if (Utils.RollChance(eachGearSpawnInfo.SpawnChance))
                {
                    Object gear = Object.Instantiate(prefab, eachGearSpawnInfo.Position, eachGearSpawnInfo.Rotation);
                    gear.name = prefab.name;
                }
            }
        }
    }
}