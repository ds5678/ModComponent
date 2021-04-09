using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModSettings;

namespace ModComponentMapper
{
    internal class ModComponentSettings : JsonModSettings
    {
        [Section("Loose Item Spawn Probability Multipliers")]
        [Name("Pilgram / Very High Loot Custom")]
        [Description("Scales the default probability of finding MODDED spawns on this game mode. Setting to zero disables loose item spawns on this game mode. Doesn't affect container loot. Can be overridden by mod-specific settings.")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:F2}")]
        public float pilgramSpawnProbabilityMultiplier = 1f;

        [Name("Voyager / High Loot Custom")]
        [Description("Scales the default probability of finding MODDED spawns on this game mode. Setting to zero disables loose item spawns on this game mode. Doesn't affect container loot. Can be overridden by mod-specific settings.")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:F2}")]
        public float voyagerSpawnProbabilityMultiplier = 0.6f;

        [Name("Stalker / Medium Loot Custom")]
        [Description("Scales the default probability of finding MODDED spawns on this game mode. Setting to zero disables loose item spawns on this game mode. Doesn't affect container loot. Can be overridden by mod-specific settings.")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:F2}")]
        public float stalkerSpawnProbabilityMultiplier = 0.4f;

        [Name("Interloper / Low Loot Custom")]
        [Description("Scales the default probability of finding MODDED spawns on this game mode. Setting to zero disables loose item spawns on this game mode. Doesn't affect container loot. Can be overridden by mod-specific settings.")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:F2}")]
        public float interloperSpawnProbabilityMultiplier = 0.2f;

        [Name("Wintermute")]
        [Description("Scales the default probability of finding MODDED spawns on this game mode. Setting to zero disables loose item spawns on this game mode. Doesn't affect container loot. Can be overridden by mod-specific settings.")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:F2}")]
        public float storySpawnProbabilityMultiplier = 1f;

        [Name("Challenges")]
        [Description("Scales the default probability of finding MODDED spawns on this game mode. Setting to zero disables loose item spawns on this game mode. Doesn't affect container loot. Can be overridden by mod-specific settings.")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:F2}")]
        public float challengeSpawnProbabilityMultiplier = 1f;

        [Section("Advanced")]
        [Name("Debug Mode")]
        [Description("Leave this on if you want technical support. Only affects the Melonloader log output.")]
        public bool debugMode = true;

        [Name("Always Spawn Loose Items")]
        [Description("For testing and new spawn point creation. Converts all loose spawn points into guaranteed spawns. Does not necessarily work on existing saves.")]
        public bool alwaysSpawnItems = false;
    }
    
    internal static class Settings
    {
        internal static ModComponentSettings options;
        internal static void OnLoad()
        {
            options = new ModComponentSettings();
            options.AddToModSettings("ModComponent");
        }
    }
}
