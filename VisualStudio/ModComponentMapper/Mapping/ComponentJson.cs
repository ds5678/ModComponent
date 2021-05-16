using MelonLoader.TinyJSON;
using ModComponentAPI;
using ModComponentUtils;
using System;
using UnityEngine;

namespace ModComponentMapper
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
			if (ComponentUtils.GetModComponent(prefab) != null || dict is null) return;

			//Mod Components
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModBedComponent"))
			{
				ModBedComponent newComponent = prefab.AddComponent<ModBedComponent>();
				InitializeBedComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModBodyHarvestComponent"))
			{
				ModBodyHarvestComponent newComponent = prefab.AddComponent<ModBodyHarvestComponent>();
				InitializeBodyHarvestComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModClothingComponent"))
			{
				ModClothingComponent newComponent = prefab.AddComponent<ModClothingComponent>();
				InitializeClothingComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModCollectibleComponent"))
			{
				ModCollectibleComponent newComponent = prefab.AddComponent<ModCollectibleComponent>();
				InitializeCollectibleComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModCookableComponent"))
			{
				ModCookableComponent newComponent = prefab.AddComponent<ModCookableComponent>();
				InitializeCookableComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModCookingPotComponent"))
			{
				ModCookingPotComponent newComponent = prefab.AddComponent<ModCookingPotComponent>();
				InitializeCookingPotComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModExplosiveComponent"))
			{
				ModExplosiveComponent newComponent = prefab.AddComponent<ModExplosiveComponent>();
				InitializeExplosiveComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModFirstAidComponent"))
			{
				ModFirstAidComponent newComponent = prefab.AddComponent<ModFirstAidComponent>();
				InitializeFirstAidComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModFoodComponent"))
			{
				ModFoodComponent newComponent = prefab.AddComponent<ModFoodComponent>();
				InitializeFoodComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModGenericComponent"))
			{
				ModGenericComponent newComponent = prefab.AddComponent<ModGenericComponent>();
				InitializeGenericComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModGenericEquippableComponent"))
			{
				ModGenericEquippableComponent newComponent = prefab.AddComponent<ModGenericEquippableComponent>();
				InitializeGenericEquippableComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModLiquidComponent"))
			{
				ModLiquidComponent newComponent = prefab.AddComponent<ModLiquidComponent>();
				InitializeLiquidComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModPowderComponent"))
			{
				ModPowderComponent newComponent = prefab.AddComponent<ModPowderComponent>();
				InitializePowderComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModRandomItemComponent"))
			{
				ModRandomItemComponent newComponent = prefab.AddComponent<ModRandomItemComponent>();
				InitializeRandomItemComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModRandomWeightedItemComponent"))
			{
				ModRandomWeightedItemComponent newComponent = prefab.AddComponent<ModRandomWeightedItemComponent>();
				InitializeRandomWeightedItemComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModRifleComponent"))
			{
				ModRifleComponent newComponent = prefab.AddComponent<ModRifleComponent>();
				InitializeRifleComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModToolComponent"))
			{
				ModToolComponent newComponent = prefab.AddComponent<ModToolComponent>();
				InitializeToolComponent(newComponent, dict);
			}


			//Behaviour Components
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModAccelerantComponent"))
			{
				ModAccelerantComponent newComponent = prefab.AddComponent<ModAccelerantComponent>();
				InitializeAccelerantComponent(newComponent, dict);
			}
			else if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModFireStarterComponent"))
			{
				ModFireStarterComponent newComponent = prefab.AddComponent<ModFireStarterComponent>();
				InitializeFireStarterComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModBurnableComponent"))
			{
				ModBurnableComponent newComponent = prefab.AddComponent<ModBurnableComponent>();
				InitializeBurnableComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModEvolveComponent"))
			{
				ModEvolveComponent newComponent = prefab.AddComponent<ModEvolveComponent>();
				InitializeEvolveComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModHarvestableComponent"))
			{
				ModHarvestableComponent newComponent = prefab.AddComponent<ModHarvestableComponent>();
				InitializeHarvestableComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModMillableComponent"))
			{
				ModMillableComponent newComponent = prefab.AddComponent<ModMillableComponent>();
				InitializeMillableComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModRepairableComponent"))
			{
				ModRepairableComponent newComponent = prefab.AddComponent<ModRepairableComponent>();
				InitializeRepairableComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModScentComponent"))
			{
				ModScentComponent newComponent = prefab.AddComponent<ModScentComponent>();
				InitializeScentComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModSharpenableComponent"))
			{
				ModSharpenableComponent newComponent = prefab.AddComponent<ModSharpenableComponent>();
				InitializeSharpenableComponent(newComponent, dict);
			}
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ModStackableComponent"))
			{
				ModStackableComponent newComponent = prefab.AddComponent<ModStackableComponent>();
				InitializeStackableComponent(newComponent, dict);
			}

			//Modifications
			if (ModComponentUtils.JsonUtils.ContainsKey(dict, "ChangeLayer"))
			{
				ChangeLayer newComponent = prefab.AddComponent<ChangeLayer>();
				InitializeChangeLayer(newComponent, dict);
			}
		}
		#endregion

		//************// 
		// COMPONENTS //
		//************//
		#region Components
		private static void InitializeBaseComponent(ModComponent modComponent, ProxyObject dict, string inheritanceName)
		{
			modComponent.ConsoleName = NameUtils.RemoveGearPrefix(modComponent.gameObject.name);
			//modComponent.DisplayNameLocalizationId = dict[inheritanceName]["DisplayNameLocalizationId"];
			ModComponentUtils.JsonUtils.TrySetString(ref modComponent.DisplayNameLocalizationId, dict, inheritanceName, "DisplayNameLocalizationId");
			modComponent.DescriptionLocalizatonId = dict[inheritanceName]["DescriptionLocalizatonId"];
			modComponent.InventoryActionLocalizationId = dict[inheritanceName]["InventoryActionLocalizationId"];
			modComponent.WeightKG = dict[inheritanceName]["WeightKG"];
			modComponent.DaysToDecay = dict[inheritanceName]["DaysToDecay"];
			modComponent.MaxHP = dict[inheritanceName]["MaxHP"];
			modComponent.InitialCondition = ModComponentUtils.EnumUtils.ParseEnum<InitialCondition>(dict[inheritanceName]["InitialCondition"]);
			modComponent.InventoryCategory = ModComponentUtils.EnumUtils.ParseEnum<InventoryCategory>(dict[inheritanceName]["InventoryCategory"]);
			modComponent.PickUpAudio = dict[inheritanceName]["PickUpAudio"];
			modComponent.PutBackAudio = dict[inheritanceName]["PutBackAudio"];
			modComponent.StowAudio = dict[inheritanceName]["StowAudio"];
			modComponent.WornOutAudio = dict[inheritanceName]["WornOutAudio"];
			modComponent.InspectOnPickup = dict[inheritanceName]["InspectOnPickup"];
			modComponent.InspectDistance = dict[inheritanceName]["InspectDistance"];
			modComponent.InspectAngles = ModComponentUtils.JsonUtils.MakeVector(dict[inheritanceName]["InspectAngles"]);
			modComponent.InspectOffset = ModComponentUtils.JsonUtils.MakeVector(dict[inheritanceName]["InspectOffset"]);
			modComponent.InspectScale = ModComponentUtils.JsonUtils.MakeVector(dict[inheritanceName]["InspectScale"]);
			modComponent.NormalModel = ModComponentUtils.ModUtils.GetChild(modComponent.gameObject, dict[inheritanceName]["NormalModel"]);
			modComponent.InspectModel = ModComponentUtils.ModUtils.GetChild(modComponent.gameObject, dict[inheritanceName]["InspectModel"]);
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
			modBed.PackedMesh = ModComponentUtils.ModUtils.GetChild(modBed.gameObject, dict[className]["PackedMesh"]);
			modBed.UsableMesh = ModComponentUtils.ModUtils.GetChild(modBed.gameObject, dict[className]["UsableMesh"]);
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

		private static void InitializeClothingComponent(ModClothingComponent modClothing, ProxyObject dict, string className = "ModClothingComponent")
		{
			InitializeBaseComponent(modClothing, dict, className);
			modClothing.Region = (Region)Enum.Parse(typeof(Region), dict[className]["Region"], true);
			modClothing.MinLayer = (Layer)Enum.Parse(typeof(Layer), dict[className]["MinLayer"], true);
			modClothing.MaxLayer = (Layer)Enum.Parse(typeof(Layer), dict[className]["MaxLayer"], true);
			modClothing.MovementSound = ModComponentUtils.EnumUtils.ParseEnum<MovementSound>(dict[className]["MovementSound"]);
			modClothing.Footwear = ModComponentUtils.EnumUtils.ParseEnum<Footwear>(dict[className]["Footwear"]);
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
			ModComponentUtils.JsonUtils.TrySetString(ref modCollectible.HudMessageLocalizationId, dict, className, "HudMessageLocalizationId");
			ModComponentUtils.JsonUtils.TrySetString(ref modCollectible.NarrativeTextLocalizationId, dict, className, "NarrativeTextLocalizationId");
			ModComponentUtils.JsonUtils.TrySetEnum<ModCollectibleComponent.Alignment>(ref modCollectible.TextAlignment, dict, className, "TextAlignment");
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
			modCookable.type = ModComponentUtils.EnumUtils.ParseEnum<CookableType>(dict[className]["Type"]);
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
			modFirstAid.FirstAidType = ModComponentUtils.EnumUtils.ParseEnum<FirstAidType>(dict[className]["FirstAidType"]);
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
			modFood.ParasiteRiskIncrements = ModComponentUtils.JsonUtils.MakeFloatArray(dict[className]["ParasiteRiskIncrements"] as ProxyArray);

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
			modLiquid.LiquidType = ModComponentUtils.EnumUtils.ParseEnum<LiquidType>(dict[className]["LiquidType"]);
			modLiquid.LiquidCapacityLiters = dict[className]["LiquidCapacityLiters"];
			modLiquid.RandomizeQuantity = dict[className]["RandomizedQuantity"];
			modLiquid.LiquidLiters = Mathf.Clamp(dict[className]["LiquidLiters"], 0f, modLiquid.LiquidCapacityLiters); //overridden if Randomized
		}

		private static void InitializePowderComponent(ModPowderComponent modPowder, ProxyObject dict, string className = "ModPowderComponent")
		{
			InitializeBaseComponent(modPowder, dict, className);
			modPowder.PowderType = ModComponentUtils.EnumUtils.ParseEnum<PowderType>(dict[className]["PowderType"]);
			modPowder.CapacityKG = dict[className]["CapacityKG"];
			modPowder.ChanceFull = dict[className]["ChanceFull"];
		}

		private static void InitializeRandomItemComponent(ModRandomItemComponent modRandomItem, ProxyObject dict, string className = "ModRandomItemComponent")
		{
			InitializeBaseComponent(modRandomItem, dict, className);
			ModComponentUtils.JsonUtils.TrySetStringArray(ref modRandomItem.ItemNames, dict, className, "ItemNames");
		}

		private static void InitializeRandomWeightedItemComponent(ModRandomWeightedItemComponent modRandomWeightedItem, ProxyObject dict, string className = "ModRandomWeightedItemComponent")
		{
			InitializeBaseComponent(modRandomWeightedItem, dict, className);
			ModComponentUtils.JsonUtils.TrySetStringArray(ref modRandomWeightedItem.ItemNames, dict, className, "ItemNames");
			ModComponentUtils.JsonUtils.TrySetIntArray(ref modRandomWeightedItem.ItemWeights, dict, className, "ItemWeights");
		}

		private static void InitializeRifleComponent(ModRifleComponent modRifle, ProxyObject dict, string className = "ModRifleComponent")
		{
			InitializeEquippableComponent(modRifle, dict, className);
			modRifle.ClipSize = dict[className]["ClipSize"];
			modRifle.DamagePerShot = dict[className]["DamagePerShot"];
			modRifle.Range = dict[className]["Range"];
			modRifle.FiringDelay = dict[className]["FiringDelay"];
			modRifle.ReloadDelay = dict[className]["ReloadDelay"];
			modRifle.AimDelay = dict[className]["AimDelay"];
			modRifle.MuzzleFlashDelay = dict[className]["MuzzleFlashDelay"];
			modRifle.MuzzleSmokeDelay = dict[className]["MuzzleSmokeDelay"];
			modRifle.MinSway = dict[className]["MinSway"];
			modRifle.MaxSway = dict[className]["MaxSway"];
			modRifle.SwayIncrement = dict[className]["SwayIncrement"];
		}

		private static void InitializeToolComponent(ModToolComponent modTool, ProxyObject dict, string className = "ModToolComponent")
		{
			InitializeEquippableComponent(modTool, dict, className);
			modTool.ToolType = ModComponentUtils.EnumUtils.ParseEnum<ToolType>(dict[className]["ToolType"]);
			modTool.DegradeOnUse = dict[className]["DegradeOnUse"];
			modTool.Usage = ModComponentUtils.EnumUtils.ParseEnum<Usage>(dict[className]["Usage"]);
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
		private static void InitializeAccelerantComponent(ModAccelerantComponent modAccelerant, ProxyObject dict, string className = "ModAccelerantComponent")
		{
			modAccelerant.DestroyedOnUse = dict[className]["DestroyedOnUse"];
			modAccelerant.DurationOffset = dict[className]["DurationOffset"];
			modAccelerant.SuccessModifier = dict[className]["SuccessModifier"];
		}

		private static void InitializeBurnableComponent(ModBurnableComponent modBurnable, ProxyObject dict, string className = "ModBurnableComponent")
		{
			modBurnable.BurningMinutes = dict[className]["BurningMinutes"];
			modBurnable.BurningMinutesBeforeAllowedToAdd = dict[className]["BurningMinutesBeforeAllowedToAdd"];
			modBurnable.SuccessModifier = dict[className]["SuccessModifier"];
			modBurnable.TempIncrease = dict[className]["TempIncrease"];
		}

		private static void InitializeEvolveComponent(ModEvolveComponent modEvolve, ProxyObject dict, string className = "ModEvolveComponent")
		{
			modEvolve.TargetItemName = dict[className]["TargetItemName"];
			modEvolve.EvolveHours = dict[className]["EvolveHours"];
			modEvolve.IndoorsOnly = dict[className]["IndoorsOnly"];
		}

		private static void InitializeFireStarterComponent(ModFireStarterComponent modFireStarter, ProxyObject dict, string className = "ModFireStarterComponent")
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

		private static void InitializeHarvestableComponent(ModHarvestableComponent modHarvestable, ProxyObject dict, string className = "ModHarvestableComponent")
		{
			modHarvestable.Audio = dict[className]["Audio"];
			modHarvestable.Minutes = dict[className]["Minutes"];
			modHarvestable.YieldCounts = ModComponentUtils.JsonUtils.MakeIntArray(dict[className]["YieldCounts"] as ProxyArray);
			modHarvestable.YieldNames = ModComponentUtils.JsonUtils.MakeStringArray(dict[className]["YieldNames"] as ProxyArray);
			modHarvestable.RequiredToolNames = ModComponentUtils.JsonUtils.MakeStringArray(dict[className]["RequiredToolNames"] as ProxyArray);
		}

		private static void InitializeMillableComponent(ModMillableComponent modMillable, ProxyObject dict, string className = "ModMillableComponent")
		{
			modMillable.RepairDurationMinutes = dict[className]["RepairDurationMinutes"];
			modMillable.RepairRequiredGear = ModComponentUtils.JsonUtils.MakeStringArray(dict[className]["RepairRequiredGear"] as ProxyArray);
			modMillable.RepairRequiredGearUnits = ModComponentUtils.JsonUtils.MakeIntArray(dict[className]["RepairRequiredGearUnits"] as ProxyArray);
			modMillable.CanRestoreFromWornOut = dict[className]["CanRestoreFromWornOut"];
			modMillable.RecoveryDurationMinutes = dict[className]["RecoveryDurationMinutes"];
			modMillable.RestoreRequiredGear = ModComponentUtils.JsonUtils.MakeStringArray(dict[className]["RestoreRequiredGear"] as ProxyArray);
			modMillable.RestoreRequiredGearUnits = ModComponentUtils.JsonUtils.MakeIntArray(dict[className]["RestoreRequiredGearUnits"] as ProxyArray);
			modMillable.skill = ModComponentUtils.EnumUtils.ParseEnum<ModComponentAPI.SkillType>(dict[className]["Skill"]);
		}

		private static void InitializeRepairableComponent(ModRepairableComponent modRepairable, ProxyObject dict, string className = "ModRepairableComponent")
		{
			modRepairable.Audio = dict[className]["Audio"];
			modRepairable.Minutes = dict[className]["Minutes"];
			modRepairable.Condition = dict[className]["Condition"];
			modRepairable.RequiredTools = ModComponentUtils.JsonUtils.MakeStringArray(dict[className]["RequiredTools"] as ProxyArray);
			modRepairable.MaterialNames = ModComponentUtils.JsonUtils.MakeStringArray(dict[className]["MaterialNames"] as ProxyArray);
			modRepairable.MaterialCounts = ModComponentUtils.JsonUtils.MakeIntArray(dict[className]["MaterialCounts"] as ProxyArray);
		}

		private static void InitializeScentComponent(ModScentComponent modScent, ProxyObject dict, string className = "ModScentComponent")
		{
			modScent.scentCategory = ModComponentUtils.EnumUtils.ParseEnum<ScentCategory>(dict[className]["ScentCategory"]);
		}

		private static void InitializeSharpenableComponent(ModSharpenableComponent modSharpenable, ProxyObject dict, string className = "ModSharpenableComponent")
		{
			modSharpenable.Audio = dict[className]["Audio"];
			modSharpenable.MinutesMin = dict[className]["MinutesMin"];
			modSharpenable.MinutesMax = dict[className]["MinutesMax"];
			modSharpenable.ConditionMin = dict[className]["ConditionMin"];
			modSharpenable.ConditionMax = dict[className]["ConditionMax"];
			modSharpenable.Tools = ModComponentUtils.JsonUtils.MakeStringArray(dict[className]["Tools"] as ProxyArray);
		}

		private static void InitializeStackableComponent(ModStackableComponent modStackable, ProxyObject dict, string className = "ModStackableComponent")
		{
			modStackable.MultipleUnitTextID = dict[className]["MultipleUnitTextId"];
			modStackable.StackSprite = dict[className]["StackSprite"];
			modStackable.SingleUnitTextID = dict[className]["SingleUnitTextId"];
			modStackable.UnitsPerItem = dict[className]["UnitsPerItem"];
			modStackable.ChanceFull = dict[className]["ChanceFull"];
		}
		#endregion

		//***************// 
		// MODIFICATIONS //
		//***************//
		#region Modifications
		private static void InitializeChangeLayer(ChangeLayer changeLayer, ProxyObject dict, string className = "ChangeLayer")
		{
			ModComponentUtils.JsonUtils.TrySetBool(ref changeLayer.Recursively, dict, className, "Recursively");
			ModComponentUtils.JsonUtils.TrySetInt(ref changeLayer.Layer, dict, className, "Layer");
		}
		#endregion
	}
}
