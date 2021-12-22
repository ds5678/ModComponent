using MelonLoader.TinyJSON;
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
				throw new Exception($"Could not load json for {name}");
			InitializeComponents(ref prefab, dict);
		}

		#region InitializeComponents
		public static void InitializeComponents(ref GameObject prefab, ProxyObject dict)
		{
			if (prefab == null)
				throw new ArgumentNullException(nameof(prefab));
			if (dict == null)
				throw new ArgumentNullException(nameof(dict));
			if (ComponentUtils.GetModComponent(prefab) != null) 
				return;

			#region Mod Components
			if (JsonUtils.ContainsKey(dict, "ModBedComponent"))
			{
				ModBedComponent newComponent = ComponentUtils.GetOrCreateComponent<ModBedComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModBodyHarvestComponent"))
			{
				ModBodyHarvestComponent newComponent = ComponentUtils.GetOrCreateComponent<ModBodyHarvestComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCharcoalComponent"))
			{
				ModCharcoalComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCharcoalComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModClothingComponent"))
			{
				ModClothingComponent newComponent = ComponentUtils.GetOrCreateComponent<ModClothingComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCollectibleComponent"))
			{
				ModCollectibleComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCollectibleComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCookableComponent"))
			{
				ModCookableComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCookableComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCookingPotComponent"))
			{
				ModCookingPotComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCookingPotComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModExplosiveComponent"))
			{
				ModExplosiveComponent newComponent = ComponentUtils.GetOrCreateComponent<ModExplosiveComponent>(prefab);
				newComponent.InitializeComponent(dict, "ModExplosiveComponent");
			}
			else if (JsonUtils.ContainsKey(dict, "ModFirstAidComponent"))
			{
				ModFirstAidComponent newComponent = ComponentUtils.GetOrCreateComponent<ModFirstAidComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModFoodComponent"))
			{
				ModFoodComponent newComponent = ComponentUtils.GetOrCreateComponent<ModFoodComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModGenericComponent"))
			{
				ModGenericComponent newComponent = ComponentUtils.GetOrCreateComponent<ModGenericComponent>(prefab);
				newComponent.InitializeComponent(dict, "ModGenericComponent");
			}
			else if (JsonUtils.ContainsKey(dict, "ModGenericEquippableComponent"))
			{
				ModGenericEquippableComponent newComponent = ComponentUtils.GetOrCreateComponent<ModGenericEquippableComponent>(prefab);
				newComponent.InitializeComponent(dict, "ModGenericEquippableComponent");
			}
			else if (JsonUtils.ContainsKey(dict, "ModLiquidComponent"))
			{
				ModLiquidComponent newComponent = ComponentUtils.GetOrCreateComponent<ModLiquidComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModPowderComponent"))
			{
				ModPowderComponent newComponent = ComponentUtils.GetOrCreateComponent<ModPowderComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModPurificationComponent"))
			{
				ModPurificationComponent newComponent = ComponentUtils.GetOrCreateComponent<ModPurificationComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModRandomItemComponent"))
			{
				ModRandomItemComponent newComponent = ComponentUtils.GetOrCreateComponent<ModRandomItemComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModRandomWeightedItemComponent"))
			{
				ModRandomWeightedItemComponent newComponent = ComponentUtils.GetOrCreateComponent<ModRandomWeightedItemComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModResearchComponent"))
			{
				ModResearchComponent newComponent = ComponentUtils.GetOrCreateComponent<ModResearchComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModToolComponent"))
			{
				ModToolComponent newComponent = ComponentUtils.GetOrCreateComponent<ModToolComponent>(prefab);
				newComponent.InitializeComponent(dict);
			}
			#endregion

			#region Behaviour Components
			if (JsonUtils.ContainsKey(dict, "ModAccelerantBehaviour"))
			{
				ModAccelerantBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModAccelerantBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModBurnableBehaviour"))
			{
				ModBurnableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModBurnableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModFireStarterBehaviour"))
			{
				ModFireStarterBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModFireStarterBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModTinderBehaviour"))
			{
				ModTinderBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModTinderBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict, "ModTinderComponent");
			}
			if (JsonUtils.ContainsKey(dict, "ModCarryingCapacityBehaviour"))
			{
				ModCarryingCapacityBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModCarryingCapacityBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModEvolveBehaviour"))
			{
				ModEvolveBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModEvolveBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModHarvestableBehaviour"))
			{
				ModHarvestableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModHarvestableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModMillableBehaviour"))
			{
				ModMillableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModMillableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModRepairableBehaviour"))
			{
				ModRepairableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModRepairableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModScentBehaviour"))
			{
				ModScentBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModScentBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModSharpenableBehaviour"))
			{
				ModSharpenableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModSharpenableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModStackableBehaviour"))
			{
				ModStackableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModStackableBehaviour>(prefab);
				newComponent.InitializeBehaviour(dict);
			}
			#endregion

			#region Modifications
			if (JsonUtils.ContainsKey(dict, "ChangeLayer"))
			{
				ChangeLayer newComponent = ComponentUtils.GetOrCreateComponent<ChangeLayer>(prefab);
				newComponent.InitializeModification(dict);
			}
			#endregion
		}
		#endregion
	}
}
