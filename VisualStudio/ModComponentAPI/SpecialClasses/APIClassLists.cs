using System.Linq;

namespace ModComponentAPI
{
	public static class APIClassLists
	{
		public static readonly string[] Behaviours = new string[]
		{
			"ModAccelerantComponent",
			"ModBurnableComponent",
			"ModCarryingCapacityComponent",
			"ModEvolveComponent",
			"ModFireStarterComponent",
			"ModHarvestableComponent",
			"ModMillableComponent",
			"ModRepairableComponent",
			"ModScentComponent",
			"ModSharpenableComponent",
			"ModStackableComponent",
			"ModTinderComponent"
		};
		public static readonly string[] Components = new string[]
		{
			"ModBedComponent",
			"ModBodyHarvestComponent",
			"ModCharcoalComponent",
			"ModClothingComponent",
			"ModCollectibleComponent",
			"ModCookableComponent",
			"ModCookingPotComponent",
			"ModExplosiveComponent",
			"ModFirstAidComponent",
			"ModFoodComponent",
			"ModGenericComponent",
			"ModGenericEquippableComponent",
			"ModLiquidComponent",
			"ModPowderComponent",
			"ModPurificationComponent",
			"ModRandomItemComponent",
			"ModRandomWeightedItemComponent",
			"ModResearchComponent",
			"ModRifleComponent",
			"ModToolComponent"
		};
		public static bool IsBehaviour(string className) => Behaviours.Contains<string>(className);
		public static bool IsComponent(string className) => Components.Contains<string>(className);
		public static int GetNumberOfBehaviours(string[] classList)
		{
			int count = 0;
			foreach (string className in classList)
			{
				if (IsBehaviour(className)) count++;
			}
			return count;
		}
		public static int GetNumberOfComponents(string[] classList)
		{
			int count = 0;
			foreach (string className in classList)
			{
				if (IsComponent(className)) count++;
			}
			return count;
		}
		public static bool ContainsBehaviour(string[] classList)
		{
			foreach (string className in classList)
			{
				if (IsBehaviour(className)) return true;
			}
			return false;
		}
		public static bool ContainsComponent(string[] classList)
		{
			foreach (string className in classList)
			{
				if (IsComponent(className)) return true;
			}
			return false;
		}
	}
}
