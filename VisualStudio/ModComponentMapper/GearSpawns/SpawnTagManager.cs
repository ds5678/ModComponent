using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModComponentMapper
{
    public static class SpawnTagManager
    {
        private static Dictionary<string, Func<DifficultyLevel,FirearmAvailability, GearSpawnInfo, float>> taggedFunctions = new Dictionary<string, Func<DifficultyLevel,FirearmAvailability, GearSpawnInfo, float>>();
        
        public static void AddToTaggedFunctions(string tag, Func<DifficultyLevel, FirearmAvailability, GearSpawnInfo, float> function)
        {
            string tagToLower = tag.ToLower();
            if(tagToLower == "none")
            {
                Logger.LogError("The spawn tag 'None' is reserved for ModComponent internal workings.");
            }
            else if (taggedFunctions.ContainsKey(tagToLower))
            {
                Logger.LogError("Spawn tag already registered. Overwriting...");
                taggedFunctions[tagToLower] = function;
            }
            else taggedFunctions.Add(tagToLower, function);
        }
        public static Func<DifficultyLevel, FirearmAvailability, GearSpawnInfo, float> GetTaggedFunction(string tag)
        {
            if (taggedFunctions.ContainsKey(tag)) return taggedFunctions[tag];
            else return null;
        }
        public static bool ContainsTag(string tag) => taggedFunctions.ContainsKey(tag);
    }
}
