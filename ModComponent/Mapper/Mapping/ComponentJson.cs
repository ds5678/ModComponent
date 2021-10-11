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
	public static class ComponentJson
	{
		public static void InitializeComponents(ref GameObject prefab)
		{
			if (ComponentUtils.GetModComponent(prefab) != null) return;

			string name = NameUtils.RemoveGearPrefix(prefab.name);
			string data = JsonHandler.GetJsonText(name);
			ProxyObject dict = JSON.Load(data) as ProxyObject;
			InitializeComponents(ref prefab, dict);
		}

		#region InitializeComponents
		public static void InitializeComponents(ref GameObject prefab, ProxyObject dict)
		{
			if (ComponentUtils.GetModComponent(prefab) != null || dict == null) return;

			#region Mod Components
			if (JsonUtils.ContainsKey(dict, "ModBedComponent"))
			{
				ModBedComponent newComponent = ComponentUtils.GetOrCreateComponent<ModBedComponent>(prefab);
				InitializeBedComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModBodyHarvestComponent"))
			{
				ModBodyHarvestComponent newComponent = ComponentUtils.GetOrCreateComponent<ModBodyHarvestComponent>(prefab);
				InitializeBodyHarvestComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCharcoalComponent"))
			{
				ModCharcoalComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCharcoalComponent>(prefab);
				InitializeCharcoalComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModClothingComponent"))
			{
				ModClothingComponent newComponent = ComponentUtils.GetOrCreateComponent<ModClothingComponent>(prefab);
				InitializeClothingComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCollectibleComponent"))
			{
				ModCollectibleComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCollectibleComponent>(prefab);
				InitializeCollectibleComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCookableComponent"))
			{
				ModCookableComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCookableComponent>(prefab);
				InitializeCookableComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModCookingPotComponent"))
			{
				ModCookingPotComponent newComponent = ComponentUtils.GetOrCreateComponent<ModCookingPotComponent>(prefab);
				InitializeCookingPotComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModExplosiveComponent"))
			{
				ModExplosiveComponent newComponent = ComponentUtils.GetOrCreateComponent<ModExplosiveComponent>(prefab);
				InitializeExplosiveComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModFirstAidComponent"))
			{
				ModFirstAidComponent newComponent = ComponentUtils.GetOrCreateComponent<ModFirstAidComponent>(prefab);
				InitializeFirstAidComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModFoodComponent"))
			{
				ModFoodComponent newComponent = ComponentUtils.GetOrCreateComponent<ModFoodComponent>(prefab);
				InitializeFoodComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModGenericComponent"))
			{
				ModGenericComponent newComponent = ComponentUtils.GetOrCreateComponent<ModGenericComponent>(prefab);
				InitializeGenericComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModGenericEquippableComponent"))
			{
				ModGenericEquippableComponent newComponent = ComponentUtils.GetOrCreateComponent<ModGenericEquippableComponent>(prefab);
				InitializeGenericEquippableComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModLiquidComponent"))
			{
				ModLiquidComponent newComponent = ComponentUtils.GetOrCreateComponent<ModLiquidComponent>(prefab);
				InitializeLiquidComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModPowderComponent"))
			{
				ModPowderComponent newComponent = ComponentUtils.GetOrCreateComponent<ModPowderComponent>(prefab);
				InitializePowderComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModPurificationComponent"))
			{
				ModPurificationComponent newComponent = ComponentUtils.GetOrCreateComponent<ModPurificationComponent>(prefab);
				InitializePurificationComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModRandomItemComponent"))
			{
				ModRandomItemComponent newComponent = ComponentUtils.GetOrCreateComponent<ModRandomItemComponent>(prefab);
				InitializeRandomItemComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModRandomWeightedItemComponent"))
			{
				ModRandomWeightedItemComponent newComponent = ComponentUtils.GetOrCreateComponent<ModRandomWeightedItemComponent>(prefab);
				InitializeRandomWeightedItemComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModResearchComponent"))
			{
				ModResearchComponent newComponent = ComponentUtils.GetOrCreateComponent<ModResearchComponent>(prefab);
				InitializeResearchComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModToolComponent"))
			{
				ModToolComponent newComponent = ComponentUtils.GetOrCreateComponent<ModToolComponent>(prefab);
				InitializeToolComponent(newComponent, dict);
			}
			#endregion

			#region Behaviour Components
			if (JsonUtils.ContainsKey(dict, "ModAccelerantComponent"))
			{
				ModAccelerantBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModAccelerantBehaviour>(prefab);
				InitializeAccelerantComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModBurnableComponent"))
			{
				ModBurnableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModBurnableBehaviour>(prefab);
				InitializeBurnableComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModFireStarterComponent"))
			{
				ModFireStarterBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModFireStarterBehaviour>(prefab);
				InitializeFireStarterComponent(newComponent, dict);
			}
			else if (JsonUtils.ContainsKey(dict, "ModTinderComponent"))
			{
				ModTinderBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModTinderBehaviour>(prefab);
				InitializeTinderComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModCarryingCapacityComponent"))
			{
				ModCarryingCapacityBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModCarryingCapacityBehaviour>(prefab);
				InitializeCarryingCapacityComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModEvolveComponent"))
			{
				ModEvolveBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModEvolveBehaviour>(prefab);
				InitializeEvolveComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModHarvestableComponent"))
			{
				ModHarvestableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModHarvestableBehaviour>(prefab);
				InitializeHarvestableComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModMillableComponent"))
			{
				ModMillableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModMillableBehaviour>(prefab);
				InitializeMillableComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModRepairableComponent"))
			{
				ModRepairableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModRepairableBehaviour>(prefab);
				InitializeRepairableComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModScentComponent"))
			{
				ModScentBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModScentBehaviour>(prefab);
				InitializeScentComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModSharpenableComponent"))
			{
				ModSharpenableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModSharpenableBehaviour>(prefab);
				InitializeSharpenableComponent(newComponent, dict);
			}
			if (JsonUtils.ContainsKey(dict, "ModStackableComponent"))
			{
				ModStackableBehaviour newComponent = ComponentUtils.GetOrCreateComponent<ModStackableBehaviour>(prefab);
				InitializeStackableComponent(newComponent, dict);
			}
			#endregion

			#region Modifications
			if (JsonUtils.ContainsKey(dict, "ChangeLayer"))
			{
				ChangeLayer newComponent = ComponentUtils.GetOrCreateComponent<ChangeLayer>(prefab);
				InitializeChangeLayer(newComponent, dict);
			}
			#endregion
		}
		#endregion

		//************// 
		// COMPONENTS //
		//************//
		#region Components
		private static void InitializeBaseComponent(ModBaseComponent modComponent, ProxyObject dict, string inheritanceName)
		{
			modComponent.ConsoleName = NameUtils.RemoveGearPrefix(modComponent.gameObject.name);
			JsonUtils.TrySetString(ref modComponent.DisplayNameLocalizationId, dict, inheritanceName, "DisplayNameLocalizationId");
			modComponent.DescriptionLocalizatonId = dict[inheritanceName]["DescriptionLocalizatonId"];
			modComponent.InventoryActionLocalizationId = dict[inheritanceName]["InventoryActionLocalizationId"];
			modComponent.WeightKG = dict[inheritanceName]["WeightKG"];
			modComponent.DaysToDecay = dict[inheritanceName]["DaysToDecay"];
			modComponent.MaxHP = dict[inheritanceName]["MaxHP"];
			modComponent.InitialCondition = EnumUtils.ParseEnum<ModBaseComponent.StartingCondition>(dict[inheritanceName]["InitialCondition"]);
			modComponent.InventoryCategory = EnumUtils.ParseEnum<ModBaseComponent.ItemCategory>(dict[inheritanceName]["InventoryCategory"]);
			modComponent.PickUpAudio = dict[inheritanceName]["PickUpAudio"];
			modComponent.PutBackAudio = dict[inheritanceName]["PutBackAudio"];
			modComponent.StowAudio = dict[inheritanceName]["StowAudio"];
			modComponent.WornOutAudio = dict[inheritanceName]["WornOutAudio"];
			modComponent.InspectOnPickup = dict[inheritanceName]["InspectOnPickup"];
			modComponent.InspectDistance = dict[inheritanceName]["InspectDistance"];
			modComponent.InspectAngles = JsonUtils.MakeVector(dict[inheritanceName]["InspectAngles"]);
			modComponent.InspectOffset = JsonUtils.MakeVector(dict[inheritanceName]["InspectOffset"]);
			modComponent.InspectScale = JsonUtils.MakeVector(dict[inheritanceName]["InspectScale"]);
			modComponent.NormalModel = ModUtils.GetChild(modComponent.gameObject, dict[inheritanceName]["NormalModel"]);
			modComponent.InspectModel = ModUtils.GetChild(modComponent.gameObject, dict[inheritanceName]["InspectModel"]);
		}

		private static void InitializeEquippableComponent(EquippableModComponent equippable, ProxyObject dict, string inheritanceName)
		{
			InitializeBaseComponent(equippable, dict, inheritanceName);
			equippable.EquippedModelPrefabName = dict[inheritanceName]["EquippedModelPrefab"];
			equippable.ImplementationType = dict[inheritanceName]["ImplementationType"];
			equippable.EquippingAudio = dict[inheritanceName]["EquippingAudio"];
		}

		private static void InitializeBedComponent(ModBedComponent modBed, ProxyObject dict, string className = "ModBedComponent")
		{
			InitializeBaseComponent(modBed, dict, className);
			modBed.ConditionGainPerHour = dict[className]["ConditionGainPerHour"];
			modBed.AdditionalConditionGainPerHour = dict[className]["AdditionalConditionGainPerHour"];
			modBed.WarmthBonusCelsius = dict[className]["WarmthBonusCelsius"];
			modBed.DegradePerHour = dict[className]["DegradePerHour"];
			modBed.BearAttackModifier = dict[className]["BearAttackModifier"];
			modBed.WolfAttackModifier = dict[className]["WolfAttackModifier"];
			modBed.OpenAudio = dict[className]["OpenAudio"];
			modBed.CloseAudio = dict[className]["CloseAudio"];
			modBed.PackedMesh = ModUtils.GetChild(modBed.gameObject, dict[className]["PackedMesh"]);
			modBed.UsableMesh = ModUtils.GetChild(modBed.gameObject, dict[className]["UsableMesh"]);
		}

		private static void InitializeBodyHarvestComponent(ModBodyHarvestComponent modBodyHarvest, ProxyObject dict, string className = "ModBodyHarvestComponent")
		{
			InitializeBaseComponent(modBodyHarvest, dict, className);
			modBodyHarvest.CanCarry = dict[className]["CanCarry"];
			modBodyHarvest.HarvestAudio = dict[className]["HarvestAudio"];

			modBodyHarvest.GutPrefab = dict[className]["GutPrefab"];
			modBodyHarvest.GutQuantity = dict[className]["GutQuantity"];
			modBodyHarvest.GutWeightKgPerUnit = dict[className]["GutWeightKgPerUnit"];

			modBodyHarvest.HidePrefab = dict[className]["HidePrefab"];
			modBodyHarvest.HideQuantity = dict[className]["HideQuantity"];
			modBodyHarvest.HideWeightKgPerUnit = dict[className]["HideWeightKgPerUnit"];

			modBodyHarvest.MeatPrefab = dict[className]["MeatPrefab"];
			modBodyHarvest.MeatAvailableMinKG = dict[className]["MeatAvailableMinKG"];
			modBodyHarvest.MeatAvailableMaxKG = dict[className]["MeatAvailableMaxKG"];

			modBodyHarvest.CanQuarter = false;
			modBodyHarvest.QuarterAudio = "";
			modBodyHarvest.QuarterBagMeatCapacityKG = 0f;
			modBodyHarvest.QuarterBagWasteMultiplier = 0f;
			modBodyHarvest.QuarterDurationMinutes = 1f;
			modBodyHarvest.QuarterObjectPrefab = "";
			modBodyHarvest.QuarterPrefabSpawnAngle = 0f;
			modBodyHarvest.QuarterPrefabSpawnRadius = 1f;

		}

		private static void InitializeCharcoalComponent(ModCharcoalComponent modCharcoal, ProxyObject dict, string className = "ModCharcoalComponent")
		{
			InitializeBaseComponent(modCharcoal, dict, className);
			JsonUtils.TrySetFloat(ref modCharcoal.SurveyGameMinutes, dict, className, "SurveyGameMinutes");
			JsonUtils.TrySetFloat(ref modCharcoal.SurveyRealSeconds, dict, className, "SurveyRealSeconds");
			JsonUtils.TrySetFloat(ref modCharcoal.SurveySkillExtendedHours, dict, className, "SurveySkillExtendedHours");
			JsonUtils.TrySetString(ref modCharcoal.SurveyLoopAudio, dict, className, "SurveyLoopAudio");
		}

		private static void InitializeClothingComponent(ModClothingComponent modClothing, ProxyObject dict, string className = "ModClothingComponent")
		{
			InitializeBaseComponent(modClothing, dict, className);
			modClothing.Region = EnumUtils.ParseEnum < ModClothingComponent.BodyRegion>(dict[className]["Region"]);
			modClothing.MinLayer = EnumUtils.ParseEnum < ModClothingComponent.Layer>(dict[className]["MinLayer"]);
			modClothing.MaxLayer = EnumUtils.ParseEnum < ModClothingComponent.Layer>(dict[className]["MaxLayer"]);
			modClothing.MovementSound = EnumUtils.ParseEnum<ModClothingComponent.MovementSounds>(dict[className]["MovementSound"]);
			modClothing.Footwear = EnumUtils.ParseEnum<ModClothingComponent.FootwearType>(dict[className]["Footwear"]);
			modClothing.DaysToDecayWornOutside = dict[className]["DaysToDecayWornOutside"];
			modClothing.DaysToDecayWornInside = dict[className]["DaysToDecayWornInside"];
			modClothing.Warmth = dict[className]["Warmth"];
			modClothing.WarmthWhenWet = dict[className]["WarmthWhenWet"];
			modClothing.Windproof = dict[className]["Windproof"];
			modClothing.Waterproofness = dict[className]["Waterproofness"];
			modClothing.Toughness = dict[className]["Toughness"];
			modClothing.SprintBarReduction = dict[className]["SprintBarReduction"];
			modClothing.DecreaseAttackChance = dict[className]["DecreaseAttackChance"];
			modClothing.IncreaseFleeChance = dict[className]["IncreaseFleeChance"];
			modClothing.HoursToDryNearFire = dict[className]["HoursToDryNearFire"];
			modClothing.HoursToDryWithoutFire = dict[className]["HoursToDryWithoutFire"];
			modClothing.HoursToFreeze = dict[className]["HoursToFreeze"];
			modClothing.MainTexture = dict[className]["MainTexture"];
			modClothing.BlendTexture = dict[className]["BlendTexture"];
			modClothing.DrawLayer = dict[className]["DrawLayer"];
			modClothing.ImplementationType = dict[className]["ImplementationType"];
		}

		private static void InitializeCollectibleComponent(ModCollectibleComponent modCollectible, ProxyObject dict, string className = "ModCollectibleComponent")
		{
			InitializeBaseComponent(modCollectible, dict, className);
			JsonUtils.TrySetString(ref modCollectible.HudMessageLocalizationId, dict, className, "HudMessageLocalizationId");
			JsonUtils.TrySetString(ref modCollectible.NarrativeTextLocalizationId, dict, className, "NarrativeTextLocalizationId");
			JsonUtils.TrySetEnum<ModCollectibleComponent.Alignment>(ref modCollectible.TextAlignment, dict, className, "TextAlignment");
		}

		private static void InitializeCookableComponent(ModCookableComponent modCookable, ProxyObject dict, string className = "ModCookableComponent")
		{
			InitializeBaseComponent(modCookable, dict, className);
			modCookable.BurntMinutes = dict[className]["BurntMinutes"];
			modCookable.Cooking = dict[className]["Cooking"];
			modCookable.CookingAudio = dict[className]["CookingAudio"];
			modCookable.StartCookingAudio = dict[className]["StartCookingAudio"];
			modCookable.CookingMinutes = dict[className]["CookingMinutes"];
			if (string.IsNullOrEmpty(dict[className]["CookingResult"]))
			{
				modCookable.CookingResult = null;
			}
			else
			{
				modCookable.CookingResult = Resources.Load(dict[className]["CookingResult"]).Cast<GameObject>();
			}
			modCookable.CookingUnitsRequired = dict[className]["CookingUnitsRequired"];
			modCookable.CookingWaterRequired = dict[className]["CookingWaterRequired"];
			modCookable.Type = EnumUtils.ParseEnum<ModCookableComponent.CookableType>(dict[className]["Type"]);
		}

		private static void InitializeCookingPotComponent(ModCookingPotComponent modCookingPot, ProxyObject dict, string className = "ModCookingPotComponent")
		{
			InitializeBaseComponent(modCookingPot, dict, className);
			modCookingPot.CanCookLiquid = dict[className]["CanCookLiquid"];
			modCookingPot.CanCookGrub = dict[className]["CanCookGrub"];
			modCookingPot.CanCookMeat = dict[className]["CanCookMeat"];
			modCookingPot.Capacity = dict[className]["Capacity"];
			modCookingPot.Template = dict[className]["Template"];
			modCookingPot.SnowMesh = null;// GetChild(modCookingPot.gameObject, dict[className]["SnowMesh"]).GetComponent<MeshFilter>().mesh;
			modCookingPot.WaterMesh = null; // GetChild(modCookingPot.gameObject, dict[className]["WaterMesh"]).GetComponent<MeshFilter>().mesh;
		}

		private static void InitializeExplosiveComponent(ModExplosiveComponent modExplosive, ProxyObject dict, string className = "ModExplosiveComponent")
		{
			InitializeEquippableComponent(modExplosive, dict, className);
		}

		private static void InitializeFirstAidComponent(ModFirstAidComponent modFirstAid, ProxyObject dict, string className = "ModFirstAidComponent")
		{
			InitializeBaseComponent(modFirstAid, dict, className);
			modFirstAid.ProgressBarMessage = dict[className]["ProgressBarMessage"];
			modFirstAid.RemedyText = dict[className]["RemedyText"];
			modFirstAid.InstantHealing = dict[className]["InstantHealing"];
			modFirstAid.FirstAidType = EnumUtils.ParseEnum<ModFirstAidComponent.FirstAidKind>(dict[className]["FirstAidType"]);
			modFirstAid.TimeToUseSeconds = dict[className]["TimeToUseSeconds"];
			modFirstAid.UnitsPerUse = dict[className]["UnitsPerUse"];
			modFirstAid.UseAudio = dict[className]["UseAudio"];
		}

		private static void InitializeFoodComponent(ModFoodComponent modFood, ProxyObject dict, string className = "ModFoodComponent")
		{
			InitializeCookableComponent(modFood, dict, className);
			modFood.DaysToDecayOutdoors = dict[className]["DaysToDecayOutdoors"];
			modFood.DaysToDecayIndoors = dict[className]["DaysToDecayIndoors"];

			modFood.Calories = dict[className]["Calories"];
			modFood.EatingTime = dict[className]["EatingTime"];

			modFood.EatingAudio = dict[className]["EatingAudio"];
			modFood.EatingPackagedAudio = dict[className]["EatingPackagedAudio"];

			modFood.ThirstEffect = dict[className]["ThirstEffect"];

			modFood.FoodPoisoning = dict[className]["FoodPoisoning"];
			modFood.FoodPoisoningLowCondition = dict[className]["FoodPoisoningLowCondition"];
			modFood.ParasiteRiskIncrements = JsonUtils.MakeFloatArray(dict[className]["ParasiteRiskIncrements"] as ProxyArray);

			modFood.Natural = dict[className]["Natural"];
			modFood.Raw = dict[className]["Raw"];
			modFood.Drink = dict[className]["Drink"];
			modFood.Meat = dict[className]["Meat"];
			modFood.Fish = dict[className]["Fish"];

			modFood.Canned = dict[className]["Canned"];
			modFood.Opening = dict[className]["Opening"];
			modFood.OpeningWithCanOpener = dict[className]["OpeningWithCanOpener"];
			modFood.OpeningWithKnife = dict[className]["OpeningWithKnife"];
			modFood.OpeningWithHatchet = dict[className]["OpeningWithHatchet"];
			modFood.OpeningWithSmashing = dict[className]["OpeningWithSmashing"];

			modFood.AffectCondition = dict[className]["AffectCondition"];
			modFood.ConditionRestBonus = dict[className]["ConditionRestBonus"];
			modFood.ConditionRestMinutes = dict[className]["ConditionRestMinutes"];

			modFood.AffectRest = dict[className]["AffectRest"];
			modFood.InstantRestChange = dict[className]["InstantRestChange"];
			modFood.RestFactor = dict[className]["RestFactor"];
			modFood.RestFactorMinutes = dict[className]["RestFactorMinutes"];

			modFood.AffectCold = dict[className]["AffectCold"];
			modFood.InstantColdChange = dict[className]["InstantColdChange"];
			modFood.ColdFactor = dict[className]["ColdFactor"];
			modFood.ColdFactorMinutes = dict[className]["ColdFactorMinutes"];

			modFood.ContainsAlcohol = dict[className]["ContainsAlcohol"];
			modFood.AlcoholPercentage = dict[className]["AlcoholPercentage"];
			modFood.AlcoholUptakeMinutes = dict[className]["AlcoholUptakeMinutes"];
		}

		private static void InitializeGenericComponent(ModGenericComponent modGeneric, ProxyObject dict)
		{
			InitializeBaseComponent(modGeneric, dict, "ModGenericComponent");
		}

		private static void InitializeGenericEquippableComponent(ModGenericEquippableComponent modGenericEquippable, ProxyObject dict)
		{
			InitializeEquippableComponent(modGenericEquippable, dict, "ModGenericEquippableComponent");
		}

		private static void InitializeLiquidComponent(ModLiquidComponent modLiquid, ProxyObject dict, string className = "ModLiquidComponent")
		{
			InitializeBaseComponent(modLiquid, dict, className);
			modLiquid.LiquidType = EnumUtils.ParseEnum<ModLiquidComponent.LiquidKind>(dict[className]["LiquidType"]);
			modLiquid.LiquidCapacityLiters = dict[className]["LiquidCapacityLiters"];
			modLiquid.RandomizeQuantity = dict[className]["RandomizedQuantity"];
			modLiquid.LiquidLiters = Mathf.Clamp(dict[className]["LiquidLiters"], 0f, modLiquid.LiquidCapacityLiters); //overridden if Randomized
		}

		private static void InitializePowderComponent(ModPowderComponent modPowder, ProxyObject dict, string className = "ModPowderComponent")
		{
			InitializeBaseComponent(modPowder, dict, className);
			modPowder.PowderType = EnumUtils.ParseEnum<ModPowderComponent.ModPowderType>(dict[className]["PowderType"]);
			modPowder.CapacityKG = dict[className]["CapacityKG"];
			modPowder.ChanceFull = dict[className]["ChanceFull"];
		}

		private static void InitializePurificationComponent(ModPurificationComponent modPurification, ProxyObject dict, string className = "ModPurificationComponent")
		{
			InitializeBaseComponent(modPurification, dict, className);
			JsonUtils.TrySetFloat(ref modPurification.LitersPurify, dict, className, "LitersPurify");
			JsonUtils.TrySetFloat(ref modPurification.ProgressBarDurationSeconds, dict, className, "ProgressBarDurationSeconds");
			JsonUtils.TrySetString(ref modPurification.ProgressBarLocalizationID, dict, className, "ProgressBarLocalizationID");
			JsonUtils.TrySetString(ref modPurification.PurifyAudio, dict, className, "PurifyAudio");
		}

		private static void InitializeRandomItemComponent(ModRandomItemComponent modRandomItem, ProxyObject dict, string className = "ModRandomItemComponent")
		{
			InitializeBaseComponent(modRandomItem, dict, className);
			JsonUtils.TrySetStringArray(ref modRandomItem.ItemNames, dict, className, "ItemNames");
		}

		private static void InitializeRandomWeightedItemComponent(ModRandomWeightedItemComponent modRandomWeightedItem, ProxyObject dict, string className = "ModRandomWeightedItemComponent")
		{
			InitializeBaseComponent(modRandomWeightedItem, dict, className);
			JsonUtils.TrySetStringArray(ref modRandomWeightedItem.ItemNames, dict, className, "ItemNames");
			JsonUtils.TrySetIntArray(ref modRandomWeightedItem.ItemWeights, dict, className, "ItemWeights");
		}

		private static void InitializeResearchComponent(ModResearchComponent modResearch, ProxyObject dict, string className = "ModResearchComponent")
		{
			InitializeBaseComponent(modResearch, dict, className);
			JsonUtils.TrySetEnum<ModComponent.API.SkillType>(ref modResearch.SkillType, dict, className, "SkillType");
			JsonUtils.TrySetInt(ref modResearch.TimeRequirementHours, dict, className, "TimeRequirementHours");
			JsonUtils.TrySetInt(ref modResearch.SkillPoints, dict, className, "SkillPoints");
			JsonUtils.TrySetInt(ref modResearch.NoBenefitAtSkillLevel, dict, className, "NoBenefitAtSkillLevel");
			JsonUtils.TrySetString(ref modResearch.ReadAudio, dict, className, "ReadAudio");
		}

		private static void InitializeToolComponent(ModToolComponent modTool, ProxyObject dict, string className = "ModToolComponent")
		{
			InitializeEquippableComponent(modTool, dict, className);
			modTool.ToolType = EnumUtils.ParseEnum<ModToolComponent.ToolKind>(dict[className]["ToolType"]);
			modTool.DegradeOnUse = dict[className]["DegradeOnUse"];
			modTool.Usage = EnumUtils.ParseEnum<ModToolComponent.ToolUsage>(dict[className]["Usage"]);
			modTool.SkillBonus = dict[className]["SkillBonus"];

			modTool.CraftingTimeMultiplier = dict[className]["CraftingTimeMultiplier"];
			modTool.DegradePerHourCrafting = dict[className]["DegradePerHourCrafting"];

			modTool.BreakDown = dict[className]["BreakDown"];
			modTool.BreakDownTimeMultiplier = dict[className]["BreakDownTimeMultiplier"];

			modTool.ForceLocks = dict[className]["ForceLocks"];
			modTool.ForceLockAudio = dict[className]["ForceLockAudio"];

			modTool.IceFishingHole = dict[className]["IceFishingHole"];
			modTool.IceFishingHoleDegradeOnUse = dict[className]["IceFishingHoleDegradeOnUse"];
			modTool.IceFishingHoleMinutes = dict[className]["IceFishingHoleMinutes"];
			modTool.IceFishingHoleAudio = dict[className]["IceFishingHoleAudio"];

			modTool.CarcassHarvesting = dict[className]["CarcassHarvesting"];
			modTool.MinutesPerKgMeat = dict[className]["MinutesPerKgMeat"];
			modTool.MinutesPerKgFrozenMeat = dict[className]["MinutesPerKgFrozenMeat"];
			modTool.MinutesPerHide = dict[className]["MinutesPerHide"];
			modTool.MinutesPerGut = dict[className]["MinutesPerGut"];
			modTool.DegradePerHourHarvesting = dict[className]["DegradePerHourHarvesting"];

			modTool.StruggleBonus = dict[className]["StruggleBonus"];
			modTool.DamageMultiplier = dict[className]["DamageMultiplier"];
			modTool.FleeChanceMultiplier = dict[className]["FleeChanceMultiplier"];
			modTool.TapMultiplier = dict[className]["TapMultiplier"];
			modTool.CanPuncture = dict[className]["CanPuncture"];
			modTool.BleedoutMultiplier = dict[className]["BleedoutMultiplier"];
		}
		#endregion

		//************// 
		// BEHAVIOURS //
		//************//
		#region Behaviours
		private static void InitializeAccelerantComponent(ModAccelerantBehaviour modAccelerant, ProxyObject dict, string className = "ModAccelerantComponent")
		{
			modAccelerant.DestroyedOnUse = dict[className]["DestroyedOnUse"];
			modAccelerant.DurationOffset = dict[className]["DurationOffset"];
			modAccelerant.SuccessModifier = dict[className]["SuccessModifier"];
		}

		private static void InitializeBurnableComponent(ModBurnableBehaviour modBurnable, ProxyObject dict, string className = "ModBurnableComponent")
		{
			modBurnable.BurningMinutes = dict[className]["BurningMinutes"];
			modBurnable.BurningMinutesBeforeAllowedToAdd = dict[className]["BurningMinutesBeforeAllowedToAdd"];
			modBurnable.SuccessModifier = dict[className]["SuccessModifier"];
			modBurnable.TempIncrease = dict[className]["TempIncrease"];
			JsonUtils.TrySetFloat(ref modBurnable.DurationOffset, dict, className, "DurationOffset");
		}

		private static void InitializeCarryingCapacityComponent(ModCarryingCapacityBehaviour modCarry, ProxyObject dict, string className = "ModCarryingCapacityComponent")
		{
			modCarry.MaxCarryCapacityKGBuff = dict[className]["MaxCarryCapacityKGBuff"];
		}

		private static void InitializeEvolveComponent(ModEvolveBehaviour modEvolve, ProxyObject dict, string className = "ModEvolveComponent")
		{
			modEvolve.TargetItemName = dict[className]["TargetItemName"];
			modEvolve.EvolveHours = dict[className]["EvolveHours"];
			modEvolve.IndoorsOnly = dict[className]["IndoorsOnly"];
		}

		private static void InitializeFireStarterComponent(ModFireStarterBehaviour modFireStarter, ProxyObject dict, string className = "ModFireStarterComponent")
		{
			modFireStarter.DestroyedOnUse = dict[className]["DestroyedOnUse"];
			modFireStarter.NumberOfUses = dict[className]["NumberOfUses"];
			modFireStarter.OnUseSoundEvent = dict[className]["OnUseSoundEvent"];
			modFireStarter.RequiresSunLight = dict[className]["RequiresSunLight"];
			modFireStarter.RuinedAfterUse = dict[className]["RuinedAfterUse"];
			modFireStarter.SecondsToIgniteTinder = dict[className]["SecondsToIgniteTinder"];
			modFireStarter.SecondsToIgniteTorch = dict[className]["SecondsToIgniteTorch"];
			modFireStarter.SuccessModifier = dict[className]["SuccessModifier"];
		}

		private static void InitializeHarvestableComponent(ModHarvestableBehaviour modHarvestable, ProxyObject dict, string className = "ModHarvestableComponent")
		{
			modHarvestable.Audio = dict[className]["Audio"];
			modHarvestable.Minutes = dict[className]["Minutes"];
			modHarvestable.YieldCounts = JsonUtils.MakeIntArray(dict[className]["YieldCounts"] as ProxyArray);
			modHarvestable.YieldNames = JsonUtils.MakeStringArray(dict[className]["YieldNames"] as ProxyArray);
			modHarvestable.RequiredToolNames = JsonUtils.MakeStringArray(dict[className]["RequiredToolNames"] as ProxyArray);
		}

		private static void InitializeMillableComponent(ModMillableBehaviour modMillable, ProxyObject dict, string className = "ModMillableComponent")
		{
			modMillable.RepairDurationMinutes = dict[className]["RepairDurationMinutes"];
			modMillable.RepairRequiredGear = JsonUtils.MakeStringArray(dict[className]["RepairRequiredGear"] as ProxyArray);
			modMillable.RepairRequiredGearUnits = JsonUtils.MakeIntArray(dict[className]["RepairRequiredGearUnits"] as ProxyArray);
			modMillable.CanRestoreFromWornOut = dict[className]["CanRestoreFromWornOut"];
			modMillable.RecoveryDurationMinutes = dict[className]["RecoveryDurationMinutes"];
			modMillable.RestoreRequiredGear = JsonUtils.MakeStringArray(dict[className]["RestoreRequiredGear"] as ProxyArray);
			modMillable.RestoreRequiredGearUnits = JsonUtils.MakeIntArray(dict[className]["RestoreRequiredGearUnits"] as ProxyArray);
			modMillable.Skill = EnumUtils.ParseEnum<ModComponent.API.SkillType>(dict[className]["Skill"]);
		}

		private static void InitializeRepairableComponent(ModRepairableBehaviour modRepairable, ProxyObject dict, string className = "ModRepairableComponent")
		{
			modRepairable.Audio = dict[className]["Audio"];
			modRepairable.Minutes = dict[className]["Minutes"];
			modRepairable.Condition = dict[className]["Condition"];
			modRepairable.RequiredTools = JsonUtils.MakeStringArray(dict[className]["RequiredTools"] as ProxyArray);
			modRepairable.MaterialNames = JsonUtils.MakeStringArray(dict[className]["MaterialNames"] as ProxyArray);
			modRepairable.MaterialCounts = JsonUtils.MakeIntArray(dict[className]["MaterialCounts"] as ProxyArray);
		}

		private static void InitializeScentComponent(ModScentBehaviour modScent, ProxyObject dict, string className = "ModScentComponent")
		{
			modScent.scentCategory = EnumUtils.ParseEnum<ModScentBehaviour.ScentCategory>(dict[className]["ScentCategory"]);
		}

		private static void InitializeSharpenableComponent(ModSharpenableBehaviour modSharpenable, ProxyObject dict, string className = "ModSharpenableComponent")
		{
			modSharpenable.Audio = dict[className]["Audio"];
			modSharpenable.MinutesMin = dict[className]["MinutesMin"];
			modSharpenable.MinutesMax = dict[className]["MinutesMax"];
			modSharpenable.ConditionMin = dict[className]["ConditionMin"];
			modSharpenable.ConditionMax = dict[className]["ConditionMax"];
			modSharpenable.Tools = JsonUtils.MakeStringArray(dict[className]["Tools"] as ProxyArray);
		}

		private static void InitializeStackableComponent(ModStackableBehaviour modStackable, ProxyObject dict, string className = "ModStackableComponent")
		{
			modStackable.MultipleUnitTextID = dict[className]["MultipleUnitTextId"];
			modStackable.StackSprite = dict[className]["StackSprite"];
			modStackable.SingleUnitTextID = dict[className]["SingleUnitTextId"];
			modStackable.UnitsPerItem = dict[className]["UnitsPerItem"];
			modStackable.ChanceFull = dict[className]["ChanceFull"];
		}

		private static void InitializeTinderComponent(ModTinderBehaviour modTinder, ProxyObject dict, string className = "ModTinderComponent")
		{
			JsonUtils.TrySetFloat(ref modTinder.SuccessModifier, dict, className, "SuccessModifier");
			JsonUtils.TrySetFloat(ref modTinder.DurationOffset, dict, className, "DurationOffset");
		}
		#endregion

		//***************// 
		// MODIFICATIONS //
		//***************//
		#region Modifications
		private static void InitializeChangeLayer(ChangeLayer changeLayer, ProxyObject dict, string className = "ChangeLayer")
		{
			JsonUtils.TrySetBool(ref changeLayer.Recursively, dict, className, "Recursively");
			JsonUtils.TrySetInt(ref changeLayer.Layer, dict, className, "Layer");
		}
		#endregion
	}
}
