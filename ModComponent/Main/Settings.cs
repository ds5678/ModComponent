using ModSettings;

namespace ModComponent.Main
{
	internal class Settings : JsonModSettings
	{
		internal static Settings instance = new Settings();

		[Name("Random Plastic Water Bottles")]
		[Description("If yes, plastic water bottles will have a random amount of water when you find them.")]
		public bool randomPlasticWaterBottles = false;

		[Name("Crafting Menu Scroll Steps")]
		[Description("Number of steps moved in the crafting menu for one scroll. Default = 7")]
		[Slider(1, 7)]
		public int numCraftingSteps = 1;

		[Name("Disable Random Item Spawns")]
		[Description("Set this to No. It's for new spawn point creation.")]
		public bool disableRandomItemSpawns = false;
	}
}
