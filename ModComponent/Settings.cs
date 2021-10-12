using ModSettings;

namespace ModComponent
{
	internal class Settings : JsonModSettings
	{
		internal static Settings instance = new Settings();

		[Name("Random Plastic Water Bottles")]
		[Description("If yes, plastic water bottles will have a random amount of water when you find them.")]
		public bool randomPlasticWaterBottles = false;

		[Name("Disable Random Item Spawns")]
		[Description("Set this to No. It's for new spawn point creation.")]
		public bool disableRandomItemSpawns = false;
	}
}
