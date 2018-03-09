using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ModComponentMapper
{
    internal class GearSpawnReader
    {
        private const string NUMBER = @"-?\d+(?:\.\d+)?";
        private const string VECTOR = NUMBER + @"\s*,\s*" + NUMBER + @"\s*,\s*" + NUMBER;

        private static readonly Regex LOOTTABLE_ENTRY_REGEX = new Regex(
            @"^item\s*=\s*(\w+)" +
            @"\W+w\s*=\s*(" + NUMBER + ")$");

        private static readonly Regex LOOTTABLE_REGEX = new Regex(@"^loottable\s*=\s*(\w+)$");
        private static readonly Regex SCENE_REGEX = new Regex(@"^scene\s*=\s*(\w+)$");

        private static readonly Regex SPAWN_REGEX = new Regex(
            @"^item\s*=\s*(\w+)" +
            @"(?:\W+p\s*=\s*(" + VECTOR + "))?" +
            @"(?:\W+r\s*=\s*(" + VECTOR + "))?" +
            @"(?:\W+\s*c\s*=\s*(\d+))?$");

        internal static void ReadDefinitions()
        {
            string gearSpawnDirectory = GetGearSpawnsDirectory();
            if (!Directory.Exists(gearSpawnDirectory))
            {
                LogUtils.Log("Gear spawn directory '" + gearSpawnDirectory + "' does not exist.");
                return;
            }

            string[] files = Directory.GetFiles(gearSpawnDirectory, "*.txt");
            foreach (string eachFile in files)
            {
                LogUtils.Log("Processing spawn file '" + eachFile + "'.");
                ProcessFile(eachFile);
            }
        }

        private static string GetGearSpawnsDirectory()
        {
            string modDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return Path.Combine(modDirectory, "gear-spawns");
        }

        private static string GetTrimmedLine(string line)
        {
            if (line == null)
            {
                return "";
            }

            return line.Trim().ToLower();
        }

        private static float ParseFloat(string value, float defaultValue, string line, string path)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            try
            {
                return float.Parse(value);
            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Could not parse '" + value + "' as numeric value in line " + line + " from file '" + path + "'.");
            }
        }

        private static int ParseInt(string value, int defaultValue, string line, string path)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            try
            {
                return int.Parse(value);
            }
            catch (System.Exception e)
            {
                throw new System.ArgumentException("Could not parse '" + value + "' as numeric value in line " + line + " from file '" + path + "'.");
            }
        }

        private static Vector3 ParseVector(string value, string line, string path)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Vector3.zero;
            }

            string[] components = value.Split(',');
            if (components.Length != 3)
            {
                throw new System.ArgumentException("A vector requires 3 components, but found " + components.Length + " in line '" + line + "' from file '" + path + "'.");
            }

            Vector3 result = new Vector3();
            result.x = ParseFloat(components[0].Trim(), 0, line, path);
            result.y = ParseFloat(components[1].Trim(), 0, line, path);
            result.z = ParseFloat(components[2].Trim(), 0, line, path);
            return result;
        }

        private static void ProcessFile(string path)
        {
            string scene = null;
            string loottable = null;

            string[] lines = File.ReadAllLines(path);
            foreach (string eachLine in lines)
            {
                var trimmedLine = GetTrimmedLine(eachLine);
                if (trimmedLine.Length == 0 || trimmedLine.StartsWith("#"))
                {
                    continue;
                }

                var match = SCENE_REGEX.Match(trimmedLine);
                if (match.Success)
                {
                    scene = match.Groups[1].Value;
                    loottable = null;
                    continue;
                }

                match = SPAWN_REGEX.Match(trimmedLine);
                if (match.Success)
                {
                    if (string.IsNullOrEmpty(scene))
                    {
                        throw new System.ArgumentException("No scene name defined before line '" + eachLine + "' from '" + path + "'. Did you forget a 'scene = <SceneName>'?");
                    }

                    GearSpawnInfo info = new GearSpawnInfo();
                    info.PrefabName = match.Groups[1].Value;
                    info.SpawnChance = ParseFloat(match.Groups[4].Value, 100, eachLine, path);
                    info.Position = ParseVector(match.Groups[2].Value, eachLine, path);
                    info.Rotation = Quaternion.Euler(ParseVector(match.Groups[3].Value, eachLine, path));

                    GearSpawner.AddGearSpawnInfo(scene, info);
                    continue;
                }

                match = LOOTTABLE_REGEX.Match(trimmedLine);
                if (match.Success)
                {
                    loottable = match.Groups[1].Value;
                    scene = null;
                    continue;
                }

                match = LOOTTABLE_ENTRY_REGEX.Match(trimmedLine);
                if (match.Success)
                {
                    if (string.IsNullOrEmpty(loottable))
                    {
                        throw new System.ArgumentException("No loottable name defined before line '" + eachLine + "' from '" + path + "'. Did you forget a 'loottable = <LootTableName>'?");
                    }

                    LootTableEntry entry = new LootTableEntry();
                    entry.PrefabName = match.Groups[1].Value;
                    entry.Weight = ParseInt(match.Groups[2].Value, 0, eachLine, path);
                    GearSpawner.AddLootTableEntry(loottable, entry);
                    continue;
                }

                throw new System.ArgumentException("Unrecognized line '" + eachLine + "' in '" + path + "'.");
            }
        }
    }
}