using MelonLoader.TinyJSON;
using ModComponent.API;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.API.Modifications;
using ModComponent.Utils;
using System;
using UnityEngine;

namespace ModComponent.Mapper
{
	internal static class ComponentJson
	{
		public static void InitializeComponents(ref GameObject prefab)
		{
			if (prefab == null)
				throw new ArgumentNullException(nameof(prefab));
			if (ComponentUtils.GetModComponent(prefab) != null)
				return;

			string name = NameUtils.RemoveGearPrefix(prefab.name);
			string data = JsonHandler.GetJsonText(name);
			ProxyObject dict = JSON.Load(data) as ProxyObject;
			if (dict == null)
				throw new Exception($"Json data for {name} was invalid");
			InitializeComponents(prefab, dict);
		}

		#region InitializeComponents
		private static void InitializeComponents(GameObject prefab, ProxyObject dict)
		{
			if (prefab == null)
				throw new ArgumentNullException(nameof(prefab));
			if (dict == null)
				throw new ArgumentNullException(nameof(dict));
			if (ComponentUtils.GetModComponent(prefab) != null)
				return;

			#region Mod Components
			if (dict.ContainsKey("ModBedComponent"))
			{
				ModBedComponent newComponent = ComponentUtils.GetOrCreateComponent<ModBedComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModBodyHarvestComponent"))
			{
				ModBodyHarvestComponent newComponent = ComponentUtils.GetOrCreateComponent<ModBodyHarvestComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModCharcoalComponent"))
			{
				ModCharcoalComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCharcoalComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModClothingComponent"))
			{
				ModClothingComponent newComponent = ComponentUtils.GetOrCreateComponent<ModClothingComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModCollectibleComponent"))
			{
				ModCollectibleComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCollectibleComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModCookableComponent"))
			{
				ModCookableComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCookableComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModCookingPotComponent"))
			{
				ModCookingPotComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCookingPotComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModExplosiveComponent"))
			{
				ModExplosiveComponent newComponent = ComponentUtils.GetOrCreateComponent<ModExplosiveComponent>(prefab);
				newComponent.InitializeComponent(dict, "ModExplosiveComponent");
			}
			else if (dict.ContainsKey("ModFirstAidComponent"))
			{
				ModFirstAidComponent newComponent = ComponentUtils.GetOrCreateComponent<ModFirstAidComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModFoodComponent"))
			{
				ModFoodComponent newComponent = ComponentUtils.GetOrCreateComponent<ModFoodComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModGenericComponent"))
			{
				ModGenericComponent newComponent = ComponentUtils.GetOrCreateComponent<ModGenericComponent>(prefab);
				newComponent.InitializeComponent(dict, "ModGenericComponent");
			}
			else if (dict.ContainsKey("ModGenericEquippableComponent"))
			{
				ModGenericEquippableComponent newComponent = ComponentUtils.GetOrCreateComponent<ModGenericEquippableComponent>(prefab);
				newComponent.InitializeComponent(dict, "ModGenericEquippableComponent");
			}
			else if (dict.ContainsKey("ModLiquidComponent"))
			{
				ModLiquidComponent newComponent = ComponentUtils.GetOrCreateComponent<ModLiquidComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModPowderComponent"))
			{
				ModPowderComponent newComponent = ComponentUtils.GetOrCreateComponent<ModPowderComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModPurificationComponent"))
			{
				ModPurificationComponent newComponent = ComponentUtils.GetOrCreateComponent<ModPurificationComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModRandomItemComponent"))
			{
				ModRandomItemComponent newComponent = ComponentUtils.GetOrCreateComponent<ModRandomItemComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModRandomWeightedItemComponent"))
			{
				ModRandomWeightedItemComponent newComponent = ComponentUtils.GetOrCreateComponent<ModRandomWeightedItemComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModResearchComponent"))
			{
				ModResearchComponent newComponent = ComponentUtils.GetOrCreateComponent<ModResearchComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (dict.ContainsKey("ModToolComponent"))
			{
				ModToolComponent newComponent = ComponentUtils.GetOrCreateComponent<ModToolComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			#endregion

			#region Behaviour Components
			if (dict.ContainsKey("ModAccelerantBehaviour"))
			{
				ModAccelerantBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModAccelerantBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			else if (dict.ContainsKey("ModBurnableBehaviour"))
			{
				ModBurnableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModBurnableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			else if (dict.ContainsKey("ModFireStarterBehaviour"))
			{
				ModFireStarterBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModFireStarterBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			else if (dict.ContainsKey("ModTinderBehaviour"))
			{
				ModTinderBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModTinderBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModCarryingCapacityBehaviour"))
			{
				ModCarryingCapacityBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModCarryingCapacityBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModEvolveBehaviour"))
			{
				ModEvolveBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModEvolveBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModHarvestableBehaviour"))
			{
				ModHarvestableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModHarvestableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModMillableBehaviour"))
			{
				ModMillableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModMillableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModRepairableBehaviour"))
			{
				ModRepairableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModRepairableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModScentBehaviour"))
			{
				ModScentBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModScentBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModSharpenableBehaviour"))
			{
				ModSharpenableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModSharpenableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (dict.ContainsKey("ModStackableBehaviour"))
			{
				ModStackableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModStackableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			#endregion

			#region Modifications
			if (dict.ContainsKey("ChangeLayer"))
			{
				ChangeLayer newComponent = ComponentUtils.GetOrCreateComponent<ChangeLayer>(prefab);
				newComponent.InitializeModification(dict);
			}
			#endregion
		}
		#endregion
	}
}
