using ModComponentAPI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponentMapper
{
    public class MappedItem
    {
        private GameObject gameObject;

        internal MappedItem(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public MappedItem AddToLootTable(LootTableName lootTableName, int weight)
        {
            LootTableEntry entry = new LootTableEntry();
            entry.PrefabName = gameObject.name;
            entry.Weight = weight;
            GearSpawner.AddLootTableEntry(lootTableName.ToString(), entry);

            return this;
        }

        public MappedItem RegisterInConsole(string displayName)
        {
            ModUtils.RegisterConsoleGearName(displayName, gameObject.name);

            return this;
        }

        public MappedItem SpawnAt(SceneName sceneName, Vector3 position, Quaternion rotation, float chance = 100)
        {
            GearSpawnInfo spawnInfo = new GearSpawnInfo();
            spawnInfo.PrefabName = gameObject.name;
            spawnInfo.Position = position;
            spawnInfo.Rotation = rotation;
            spawnInfo.SpawnChance = chance;
            GearSpawner.AddGearSpawnInfo(sceneName.ToString(), spawnInfo);

            return this;
        }
    }

    public class Mapper
    {
        private static List<ModBlueprint> blueprints = new List<ModBlueprint>();
        private static List<ModComponent> mappedItems = new List<ModComponent>();

        public static MappedItem Map(string prefabName)
        {
            return Map((GameObject)Resources.Load(prefabName));
        }

        public static MappedItem Map(GameObject prefab)
        {
            if (prefab == null)
            {
                throw new ArgumentException("The prefab was NULL.");
            }

            ModComponent modComponent = ModUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                throw new ArgumentException("Prefab " + prefab.name + " does not contain a ModComponent.");
            }

            if (prefab.GetComponent<GearItem>() == null)
            {
                Log("Mapping {0}", prefab.name);

                ConfigureInspect(modComponent);
                ConfigureHarvestable(modComponent);
                ConfigureRepairable(modComponent);

                ConfigureEquippable(modComponent);
                ConfigureFood(modComponent);
                ConfigureCookable(modComponent);
                ConfigureRifle(modComponent);
                ConfigureClothing(modComponent);
                ConfigureFireStarter(modComponent);
                ConfigureAccelerant(modComponent);
                ConfigureGearItem(modComponent);

                mappedItems.Add(modComponent);
            }

            PostProcess(modComponent);

            return new MappedItem(prefab);
        }

        public static void MapBlueprint(ModBlueprint modBlueprint)
        {
            if (GameManager.GetBlueprints() == null)
            {
                throw new Exception("The Blueprints have not been loaded yet.");
            }

            BlueprintItem bpItem = GameManager.GetBlueprints().AddComponent<BlueprintItem>();
            if (bpItem == null)
            {
                throw new Exception("Error creating Blueprint");
            }

            bpItem.m_DurationMinutes = modBlueprint.DurationMinutes;
            bpItem.m_CraftingAudio = modBlueprint.CraftingAudio;

            bpItem.m_RequiresForge = modBlueprint.RequiresForge;
            bpItem.m_RequiresWorkbench = modBlueprint.RequiresWorkbench;
            bpItem.m_RequiresLight = modBlueprint.RequiresLight;

            bpItem.m_Locked = modBlueprint.Locked;

            bpItem.m_CraftedResultCount = modBlueprint.CraftedResultCount;
            bpItem.m_CraftedResult = GetItem<GearItem>(modBlueprint.CraftedResult);

            bpItem.m_RequiredTool = GetItem<ToolsItem>(modBlueprint.RequiredTool);
            bpItem.m_RequiredGear = GetItems<GearItem>(modBlueprint.RequiredGear);
            bpItem.m_OptionalTools = GetItems<ToolsItem>(modBlueprint.OptionalTools);
            bpItem.m_RequiredGearUnits = modBlueprint.RequiredGearUnits;
        }

        internal static void AddBlueprint(ModBlueprint modBlueprint)
        {
            blueprints.Add(modBlueprint);
        }

        internal static void MapBlueprints()
        {
            foreach (ModBlueprint modBlueprint in blueprints)
            {
                MapBlueprint(modBlueprint);
            }
        }

        private static void ConfigureAccelerant(ModComponent modComponent)
        {
            ModAccelerantComponent modAccelerantComponent = ModUtils.GetComponent<ModAccelerantComponent>(modComponent);
            if (modAccelerantComponent == null)
            {
                return;
            }

            FireStarterItem fireStarterItem = ModUtils.GetOrCreateComponent<FireStarterItem>(modAccelerantComponent);

            fireStarterItem.m_IsAccelerant = true;
            fireStarterItem.m_FireStartDurationModifier = modAccelerantComponent.DurationOffset;
            fireStarterItem.m_FireStartSkillModifier = modAccelerantComponent.SuccessModifier;
            fireStarterItem.m_ConsumeOnUse = modAccelerantComponent.DestroyedOnUse;
        }

        private static void ConfigureClothing(ModComponent modComponent)
        {
            ModClothingComponent modClothingItem = modComponent as ModClothingComponent;
            if (modClothingItem == null)
            {
                return;
            }

            ClothingItem clothingItem = ModUtils.GetOrCreateComponent<ClothingItem>(modClothingItem);

            clothingItem.m_DailyHPDecayWhenWornInside = GetDecayPerStep(modClothingItem.DaysToDecayWornInside, modClothingItem.MaxHP);
            clothingItem.m_DailyHPDecayWhenWornOutside = GetDecayPerStep(modClothingItem.DaysToDecayWornOutside, modClothingItem.MaxHP);
            clothingItem.m_DryBonusWhenNotWorn = 1.5f;
            clothingItem.m_DryPercentPerHour = 100f / modClothingItem.HoursToDryNearFire;
            clothingItem.m_DryPercentPerHourNoFire = 100f / modClothingItem.HoursToDryWithoutFire;
            clothingItem.m_FreezePercentPerHour = 100f / modClothingItem.HoursToFreeze;

            clothingItem.m_Region = ModUtils.TranslateEnumValue<ClothingRegion, Region>(modClothingItem.Region);
            clothingItem.m_MaxLayer = ModUtils.TranslateEnumValue<ClothingLayer, Layer>(modClothingItem.MaxLayer);
            clothingItem.m_MinLayer = ModUtils.TranslateEnumValue<ClothingLayer, Layer>(modClothingItem.MinLayer);
            clothingItem.m_FootwearType = ModUtils.TranslateEnumValue<FootwearType, Footwear>(modClothingItem.Footwear);
            clothingItem.m_WornMovementSoundCategory = ModUtils.TranslateEnumValue<ClothingMovementSound, MovementSound>(modClothingItem.MovementSound);

            clothingItem.m_PaperDollTextureName = modClothingItem.MainTexture;
            clothingItem.m_PaperDollBlendmapName = modClothingItem.BlendTexture;

            clothingItem.m_Warmth = modClothingItem.Warmth;
            clothingItem.m_WarmthWhenWet = modClothingItem.WarmthWhenWet;
            clothingItem.m_Waterproofness = modClothingItem.Waterproofness / 100f;
            clothingItem.m_Windproof = modClothingItem.Windproof;
            clothingItem.m_SprintBarReductionPercent = modClothingItem.SprintBarReduction;
            clothingItem.m_Toughness = modClothingItem.Toughness;
        }

        private static void ConfigureCookable(ModComponent modComponent)
        {
            ModCookableComponent modCookableComponent = modComponent as ModCookableComponent;
            if (modCookableComponent == null || !modCookableComponent.Cooking)
            {
                return;
            }

            Cookable cookable = ModUtils.GetOrCreateComponent<Cookable>(modCookableComponent);

            cookable.m_CookTimeMinutes = modCookableComponent.CookingMinutes;
            cookable.m_NumUnitsRequired = modCookableComponent.CookingUnitsRequired;
            cookable.m_PotableWaterRequiredLiters = modCookableComponent.CookingWaterRequired;
            cookable.m_CookAudio = ModUtils.DefaultIfEmpty(modCookableComponent.CookingAudio, "PLAY_BOILINGLIGHT");

            // either just heat or convert, but not both
            if (modCookableComponent.CookingResult == null)
            {
                // no conversion, just heating
                FoodItem foodItem = ModUtils.GetComponent<FoodItem>(modCookableComponent);
                if (foodItem != null)
                {
                    foodItem.m_HeatedWhenCooked = true;
                }
            }
            else
            {
                // no heating, but instead convert the item when cooking completes
                GearItem cookedGearItem = modCookableComponent.CookingResult.GetComponent<GearItem>();
                if (cookedGearItem == null)
                {
                    // not mapped yet, do it now
                    Mapper.Map(modCookableComponent.CookingResult);
                    cookedGearItem = modCookableComponent.CookingResult.GetComponent<GearItem>();
                }

                if (cookedGearItem == null)
                {
                    throw new ArgumentException("CookingResult does not map to GearItem for prefab " + modCookableComponent.name);
                }

                cookable.m_CookedPrefab = cookedGearItem;
            }
        }

        private static FirstPersonItem ConfiguredRifleFirstPersonItem(ModRifleComponent modRifleComponent)
        {
            FirstPersonItem result = ModUtils.GetOrCreateComponent<FirstPersonItem>(modRifleComponent);

            FirstPersonItem template = Resources.Load<GameObject>("GEAR_Rifle").GetComponent<FirstPersonItem>();

            result.m_FirstPersonObjectName = ModUtils.NormalizeName(modRifleComponent.name);
            result.m_UnWieldAudio = template.m_UnWieldAudio;
            result.m_WieldAudio = template.m_WieldAudio;
            result.m_PlayerStateTransitions = UnityEngine.Object.Instantiate(template.m_PlayerStateTransitions);
            result.Awake();

            return result;
        }

        private static void ConfigureEquippable(ModComponent modComponent)
        {
            EquippableModComponent equippableModComponent = modComponent as EquippableModComponent;
            if (equippableModComponent == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(equippableModComponent.InventoryActionLocalizationId) && string.IsNullOrEmpty(equippableModComponent.ImplementationType))
            {
                equippableModComponent.InventoryActionLocalizationId = "GAMEPLAY_Equip";
            }
        }

        private static void ConfigureFireStarter(ModComponent modComponent)
        {
            ModFireStarterComponent modFireStarterComponent = ModUtils.GetComponent<ModFireStarterComponent>(modComponent);
            if (modFireStarterComponent == null)
            {
                return;
            }

            FireStarterItem fireStarterItem = ModUtils.GetOrCreateComponent<FireStarterItem>(modFireStarterComponent);

            fireStarterItem.m_SecondsToIgniteTinder = modFireStarterComponent.SecondsToIgniteTinder;
            fireStarterItem.m_SecondsToIgniteTorch = modFireStarterComponent.SecondsToIgniteTorch;

            fireStarterItem.m_FireStartSkillModifier = modFireStarterComponent.SuccessModifier;

            fireStarterItem.m_ConditionDegradeOnUse = GetDecayPerStep(modFireStarterComponent.NumberOfUses, modComponent.MaxHP);
            fireStarterItem.m_ConsumeOnUse = modFireStarterComponent.DestroyedOnUse;
            fireStarterItem.m_RequiresSunLight = modFireStarterComponent.RequiresSunLight;
            fireStarterItem.m_OnUseSoundEvent = modFireStarterComponent.OnUseSoundEvent;
        }

        private static void ConfigureFood(ModComponent modComponent)
        {
            ModFoodComponent modFoodComponent = modComponent as ModFoodComponent;
            if (modFoodComponent == null)
            {
                return;
            }

            FoodItem foodItem = ModUtils.GetOrCreateComponent<FoodItem>(modFoodComponent);

            foodItem.m_CaloriesTotal = modFoodComponent.Calories;
            foodItem.m_CaloriesRemaining = modFoodComponent.Calories;
            foodItem.m_ReduceThirst = modFoodComponent.ThirstEffect;

            foodItem.m_ChanceFoodPoisoning = Mathf.Clamp01(modFoodComponent.FoodPoisoning / 100f);
            foodItem.m_ChanceFoodPoisoningLowCondition = Mathf.Clamp01(modFoodComponent.FoodPoisoningLowCondition / 100f);
            foodItem.m_DailyHPDecayInside = GetDecayPerStep(modFoodComponent.DaysToDecayIndoors, modFoodComponent.MaxHP);
            foodItem.m_DailyHPDecayOutside = GetDecayPerStep(modFoodComponent.DaysToDecayOutdoors, modFoodComponent.MaxHP);

            foodItem.m_TimeToEatSeconds = Mathf.Clamp(1, modFoodComponent.EatingTime, 10);
            foodItem.m_EatingAudio = modFoodComponent.EatingAudio;
            foodItem.m_OpenAndEatingAudio = modFoodComponent.EatingPackagedAudio;
            foodItem.m_Packaged = !string.IsNullOrEmpty(foodItem.m_OpenAndEatingAudio);

            foodItem.m_IsDrink = modFoodComponent.Drink;
            foodItem.m_IsFish = modFoodComponent.Fish;
            foodItem.m_IsMeat = modFoodComponent.Meat;
            foodItem.m_IsRawMeat = modFoodComponent.Raw;
            foodItem.m_IsNatural = modFoodComponent.Natural;
            foodItem.m_ParasiteRiskPercentIncrease = ModUtils.NotNull(modFoodComponent.ParasiteRiskIncrements);

            foodItem.m_PercentHeatLossPerMinuteIndoors = 1;
            foodItem.m_PercentHeatLossPerMinuteOutdoors = 2;

            if (modFoodComponent.Opening)
            {
                foodItem.m_GearRequiredToOpen = true;
                foodItem.m_OpenedWithCanOpener = modFoodComponent.OpeningWithCanOpener;
                foodItem.m_OpenedWithHatchet = modFoodComponent.OpeningWithHatchet;
                foodItem.m_OpenedWithKnife = modFoodComponent.OpeningWithKnife;

                if (modFoodComponent.OpeningWithSmashing)
                {
                    SmashableItem smashableItem = ModUtils.GetOrCreateComponent<SmashableItem>(modFoodComponent);
                    smashableItem.m_MinPercentLoss = 10;
                    smashableItem.m_MaxPercentLoss = 30;
                    smashableItem.m_TimeToSmash = 6;
                    smashableItem.m_SmashAudio = "Play_EatingSmashCan";
                }
            }

            if (modFoodComponent.AffectRest)
            {
                FatigueBuff fatigueBuff = ModUtils.GetOrCreateComponent<FatigueBuff>(modFoodComponent);
                fatigueBuff.m_InitialPercentDecrease = modFoodComponent.InstantRestChange;
                fatigueBuff.m_RateOfIncreaseScale = modFoodComponent.RestFactor;
                fatigueBuff.m_DurationHours = modFoodComponent.RestFactorMinutes / 60f;
            }

            if (modFoodComponent.AffectCold)
            {
                FreezingBuff freezingBuff = ModUtils.GetOrCreateComponent<FreezingBuff>(modFoodComponent);
                freezingBuff.m_InitialPercentDecrease = modFoodComponent.InstantColdChange;
                freezingBuff.m_RateOfIncreaseScale = modFoodComponent.ColdFactor;
                freezingBuff.m_DurationHours = modFoodComponent.ColdFactorMinutes / 60f;
            }

            if (modFoodComponent.ContainsAlcohol)
            {
                AlcoholComponent alcohol = ModUtils.GetOrCreateComponent<AlcoholComponent>(modFoodComponent);
                alcohol.AmountTotal = modFoodComponent.WeightKG * modFoodComponent.AlcoholPercentage * 0.01f;
                alcohol.AmountRemaining = alcohol.AmountTotal;
                alcohol.UptakeSeconds = modFoodComponent.AlcoholUptakeMinutes * 60;
            }

            HoverIconsToShow hoverIconsToShow = ModUtils.GetOrCreateComponent<HoverIconsToShow>(modFoodComponent);
            hoverIconsToShow.m_HoverIcons = new HoverIconsToShow.HoverIcons[] { HoverIconsToShow.HoverIcons.Food };
        }

        private static void ConfigureGearItem(ModComponent modComponent)
        {
            GearItem gearItem = ModUtils.GetOrCreateComponent<GearItem>(modComponent);

            gearItem.m_Type = GetGearType(modComponent);
            gearItem.m_WeightKG = modComponent.WeightKG;
            gearItem.m_MaxHP = modComponent.MaxHP;
            gearItem.m_DailyHPDecay = GetDecayPerStep(modComponent.DaysToDecay, modComponent.MaxHP);
            gearItem.OverrideGearCondition(GearStartCondition.Random);

            gearItem.m_LocalizedDisplayName = new LocalizedString();
            gearItem.m_LocalizedDisplayName.m_LocalizationID = modComponent.DisplayNameLocalizationId;

            gearItem.m_LocalizedDescription = new LocalizedString();
            gearItem.m_LocalizedDescription.m_LocalizationID = modComponent.DescriptionLocalizatonId;

            gearItem.m_PickUpAudio = modComponent.PickUpAudio;
            gearItem.m_StowAudio = modComponent.StowAudio;
            gearItem.m_PutBackAudio = modComponent.PickUpAudio;
            gearItem.m_WornOutAudio = modComponent.WornOutAudio;

            gearItem.m_ConditionTableType = GetConditionTableType(modComponent);
        }

        private static void ConfigureHarvestable(ModComponent modComponent)
        {
            ModHarvestableComponent modHarvestableComponent = ModUtils.GetComponent<ModHarvestableComponent>(modComponent);
            if (modHarvestableComponent == null)
            {
                return;
            }

            Harvest harvest = ModUtils.GetOrCreateComponent<Harvest>(modHarvestableComponent);
            harvest.m_Audio = modHarvestableComponent.Audio;
            harvest.m_DurationMinutes = modHarvestableComponent.Minutes;

            if (modHarvestableComponent.YieldNames.Length != modHarvestableComponent.YieldCounts.Length)
            {
                throw new ArgumentException("YieldNames and YieldCounts do not have the same length on gear item '" + modHarvestableComponent.name + "'.");
            }

            harvest.m_YieldGear = GetItems<GearItem>(modHarvestableComponent.YieldNames, modHarvestableComponent.name);
            harvest.m_YieldGearUnits = modHarvestableComponent.YieldCounts;
        }

        private static void ConfigureInspect(ModComponent modComponent)
        {
            if (!modComponent.InspectOnPickup)
            {
                return;
            }

            Inspect inspect = ModUtils.GetOrCreateComponent<Inspect>(modComponent);
            inspect.m_DistanceFromCamera = modComponent.InspectDistance;
            inspect.m_Scale = modComponent.InspectScale;
            inspect.m_Angles = modComponent.InspectAngles;
            inspect.m_Offset = modComponent.InspectOffset;
        }

        private static void ConfigureRepairable(ModComponent modComponent)
        {
            ModRepairableComponent modRepairableComponent = modComponent.GetComponent<ModRepairableComponent>();
            if (modRepairableComponent == null)
            {
                return;
            }

            Repairable repairable = ModUtils.GetOrCreateComponent<Repairable>(modRepairableComponent);
            repairable.m_RepairAudio = modRepairableComponent.Audio;
            repairable.m_DurationMinutes = modRepairableComponent.Minutes;
            repairable.m_ConditionIncrease = modRepairableComponent.Condition;

            if (modRepairableComponent.MaterialNames.Length != modRepairableComponent.MaterialCounts.Length)
            {
                throw new ArgumentException("MaterialNames and MaterialCounts do not have the same length on gear item '" + modRepairableComponent.name + "'.");
            }

            repairable.m_RequiredGear = GetItems<GearItem>(modRepairableComponent.MaterialNames, modRepairableComponent.name);
            repairable.m_RequiredGearUnits = modRepairableComponent.MaterialCounts;

            repairable.m_RepairToolChoices = GetItems<ToolsItem>(modRepairableComponent.RequiredTools, modRepairableComponent.name);
            repairable.m_RequiresToolToRepair = repairable.m_RepairToolChoices.Length > 0;
        }

        private static void ConfigureRifle(ModComponent modComponent)
        {
            ModRifleComponent modRifleComponent = modComponent as ModRifleComponent;
            if (modRifleComponent == null)
            {
                return;
            }

            GunItem gunItem = ModUtils.GetOrCreateComponent<GunItem>(modRifleComponent);

            gunItem.m_GunType = GunType.Rifle;
            gunItem.m_AmmoPrefab = (GameObject)Resources.Load("GEAR_RifleAmmoSingle");
            gunItem.m_AmmoSpriteName = "ico_units_ammo";

            gunItem.m_AccuracyRange = modRifleComponent.Range;
            gunItem.m_ClipSize = modRifleComponent.ClipSize;
            gunItem.m_DamageHP = modRifleComponent.DamagePerShot;
            gunItem.m_FiringRateSeconds = modRifleComponent.FiringDelay;
            gunItem.m_MuzzleFlash_FlashDelay = modRifleComponent.MuzzleFlashDelay;
            gunItem.m_MuzzleFlash_SmokeDelay = modRifleComponent.MuzzleSmokeDelay;
            gunItem.m_ReloadCoolDownSeconds = modRifleComponent.ReloadDelay;

            gunItem.m_DryFireAudio = "Play_RifleDryFire";
            gunItem.m_ImpactAudio = "Play_BulletImpacts";

            gunItem.m_SwayIncreasePerSecond = modRifleComponent.SwayIncrement;
            gunItem.m_SwayValueZeroFatigue = modRifleComponent.MinSway;
            gunItem.m_SwayValueMaxFatigue = modRifleComponent.MaxSway;

            Cleanable cleanable = ModUtils.GetOrCreateComponent<Cleanable>(modRifleComponent);
            cleanable.m_ConditionIncreaseMin = 2;
            cleanable.m_ConditionIncreaseMin = 5;
            cleanable.m_DurationMinutesMin = 15;
            cleanable.m_DurationMinutesMax = 5;
            cleanable.m_CleanAudio = "Play_RifleCleaning";
            cleanable.m_RequiresToolToClean = true;
            cleanable.m_CleanToolChoices = new ToolsItem[] { Resources.Load<GameObject>("GEAR_RifleCleaningKit").GetComponent<ToolsItem>() };

            FirstPersonItem firstPersonItem = ConfiguredRifleFirstPersonItem(modRifleComponent);

            ModAnimationStateMachine animation = ModUtils.GetOrCreateComponent<ModAnimationStateMachine>(modRifleComponent);
            animation.SetTransitions(firstPersonItem.m_PlayerStateTransitions);
        }

        private static ConditionTableManager.ConditionTableType GetConditionTableType(ModComponent modComponent)
        {
            if (modComponent is ModFoodComponent)
            {
                ModFoodComponent modFoodComponent = (ModFoodComponent)modComponent;
                if (modFoodComponent.Canned)
                {
                    return ConditionTableManager.ConditionTableType.CannedFood;
                }

                if (modFoodComponent.Meat)
                {
                    return ConditionTableManager.ConditionTableType.Meat;
                }

                if (!modFoodComponent.Natural && !modFoodComponent.Drink)
                {
                    return ConditionTableManager.ConditionTableType.DryFood;
                }

                return ConditionTableManager.ConditionTableType.Unknown;
            }

            return ConditionTableManager.ConditionTableType.Unknown;
        }

        private static float GetDecayPerStep(float steps, float maxHP)
        {
            if (steps > 0)
            {
                return maxHP / steps;
            }

            return 0;
        }

        private static GearTypeEnum GetGearType(ModComponent modComponent)
        {
            if (modComponent is ModToolComponent)
            {
                return GearTypeEnum.Tool;
            }

            if (modComponent is ModFoodComponent)
            {
                return GearTypeEnum.Food;
            }

            if (modComponent is ModCookableComponent)
            {
                return GearTypeEnum.Food;
            }

            if (modComponent is ModClothingComponent)
            {
                return GearTypeEnum.Clothing;
            }

            if (ModUtils.GetComponent<ModFireStartingComponent>(modComponent) != null)
            {
                return GearTypeEnum.Firestarting;
            }

            return GearTypeEnum.Other;
        }

        private static T GetItem<T>(string name, string reference = null)
        {
            GameObject gameObject = Resources.Load(name) as GameObject;
            if (gameObject == null)
            {
                throw new ArgumentException("Could not load '" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + ".");
            }

            T targetType = gameObject.GetComponent<T>();
            if (targetType == null)
            {
                throw new ArgumentException("'" + name + "'" + (reference != null ? " referenced by '" + reference : "'") + " is not a '" + typeof(T).Name + "'.");
            }

            return targetType;
        }

        private static T[] GetItems<T>(string[] names, string reference = null)
        {
            T[] result = new T[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                result[i] = GetItem<T>(names[i]);
            }

            return result;
        }

        private static void Log(string message, params object[] parameters)
        {
            LogUtils.Log("ModComponentMapper", message, parameters);
        }

        private static void PostProcess(ModComponent modComponent)
        {
            modComponent.gameObject.layer = vp_Layer.Gear;

            GearItem gearItem = modComponent.GetComponent<GearItem>();
            gearItem.m_SkinnedMeshRenderers = ModUtils.NotNull(gearItem.m_SkinnedMeshRenderers);
        }
    }
}