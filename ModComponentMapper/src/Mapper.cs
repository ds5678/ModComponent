using ModComponentAPI;
using ModComponentMapper.ComponentMapper;
using System;
using System.Collections.Generic;
using UnityEngine;

/* did a first pass through; HAS ISSUES!!!!!
 * ONE needs to be declared
 * Three issues
 * First was an m_range on a PlaceableItem; I just commented it out
 * Second appeared to have been caused by a change in the arguments a function requires;
 *     I added the additional required argument.
 * Third appeared to be just a small syntax error; I corrected it
 */

namespace ModComponentMapper
{
    public class MappedItem // needs declared
    {
        private GameObject gameObject;

        internal MappedItem(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public MappedItem RegisterInConsole(string displayName)
        {
            ModUtils.RegisterConsoleGearName(displayName, gameObject.name);

            return this;
        }
    }

    public class Mapper //does not need to be declared
    {
        private static List<ModBlueprint> blueprints = new List<ModBlueprint>();
        private static List<ModComponent> mappedItems = new List<ModComponent>();
        private static List<ModSkill> skills = new List<ModSkill>();

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
                Implementation.Log("Mapping {0}", prefab.name);

                InspectMapper.Configure(modComponent);
                HarvestableMapper.Configure(modComponent);
                RepairableMapper.Configure(modComponent);
                FireStarterMapper.Configure(modComponent);
                ConfigureAccelerant(modComponent);
                ConfigureStackable(modComponent);
                ConfigureBurnable(modComponent);
                ScentMapper.Configure(modComponent);
                SharpenableMapper.Configure(modComponent);
                EvolveMapper.Configure(modComponent);
                MillableMapper.Configure(modComponent);

                ConfigureEquippable(modComponent);
                ConfigureLiquidItem(modComponent);
                ConfigureFood(modComponent);
                CookableMapper.Configure(modComponent);
                ConfigureCookingPot(modComponent);
                ConfigureRifle(modComponent);
                ClothingMapper.ConfigureClothing(modComponent);
                FirstAidMapper.Configure(modComponent);
                ToolMapper.Configure(modComponent);
                BedMapper.Configure(modComponent);

                ConfigureGearItem(modComponent);

                mappedItems.Add(modComponent);

                PostProcess(modComponent);
            }

            return new MappedItem(prefab);
        }

        internal static LocalizedString CreateLocalizedString(string key)
        {
            return new LocalizedString()
            {
                m_LocalizationID = key
            };
        }

        internal static float GetDecayPerStep(float steps, float maxHP)
        {
            if (steps > 0)
            {
                return maxHP / steps;
            }

            return 0;
        }

        internal static void MapBlueprint(ModBlueprint modBlueprint)
        {
            BlueprintItem bpItem = GameManager.GetBlueprints().AddComponent<BlueprintItem>();
            if (bpItem == null)
            {
                throw new Exception("Error creating Blueprint");
            }

            bpItem.m_DurationMinutes = modBlueprint.DurationMinutes;
            bpItem.m_CraftingAudio = modBlueprint.CraftingAudio;

            bpItem.m_RequiredCraftingLocation = ModUtils.TranslateEnumValue<CraftingLocation,ModComponentAPI.CraftingLocation>(modBlueprint.RequiredCraftingLocation);
            bpItem.m_RequiresLitFire = modBlueprint.RequiresLitFire;
            bpItem.m_RequiresLight = modBlueprint.RequiresLight;

            bpItem.m_Locked = false;
            bpItem.m_AppearsInStoryOnly = false;

            bpItem.m_CraftedResultCount = modBlueprint.CraftedResultCount;
            bpItem.m_CraftedResult = ModUtils.GetItem<GearItem>(modBlueprint.CraftedResult);

            if (!string.IsNullOrEmpty(modBlueprint.RequiredTool))
            {
                bpItem.m_RequiredTool = ModUtils.GetItem<ToolsItem>(modBlueprint.RequiredTool);
            }
            bpItem.m_OptionalTools = ModUtils.NotNull(ModUtils.GetMatchingItems<ToolsItem>(modBlueprint.OptionalTools));

            bpItem.m_RequiredGear = ModUtils.NotNull(ModUtils.GetMatchingItems<GearItem>(modBlueprint.RequiredGear));
            bpItem.m_RequiredGearUnits = modBlueprint.RequiredGearUnits;
            bpItem.m_KeroseneLitersRequired = modBlueprint.KeroseneLitersRequired;
            bpItem.m_GunpowderKGRequired = modBlueprint.GunpowderKGRequired;

            bpItem.m_AppliedSkill = ModUtils.TranslateEnumValue<SkillType, ModComponentAPI.SkillType>(modBlueprint.AppliedSkill);
            bpItem.m_ImprovedSkill = ModUtils.TranslateEnumValue<SkillType, ModComponentAPI.SkillType>(modBlueprint.ImprovedSkill);
        }

        internal static void MapBlueprints()
        {
            GameObject blueprintsManager = GameManager.GetBlueprints();
            if (blueprintsManager == null)
            {
                return;
            }

            foreach (ModBlueprint modBlueprint in blueprints)
            {
                MapBlueprint(modBlueprint);
            }
        }

        /*internal static void MapSkill(ModSkill modSkill)
        {
            SerializableSkill skill = new GameObject().AddComponent<SerializableSkill>();

            skill.name = modSkill.name;
            skill.m_LocalizedDisplayName = CreateLocalizedString(modSkill.DisplayName);
            skill.m_SkillType = (SkillType)GameManager.GetSkillsManager().GetNumSkills();
            skill.m_SkillIcon = modSkill.Icon;
            skill.m_SkillIconBackground = modSkill.Image;
            skill.m_SkillImage = modSkill.Image;
            skill.m_TierPoints = new int[] { 0, modSkill.PointsLevel2, modSkill.PointsLevel3, modSkill.PointsLevel4, modSkill.PointsLevel5 };
            skill.m_TierLocalizedBenefits = CreateLocalizedStrings(modSkill.EffectsLevel1, modSkill.EffectsLevel2, modSkill.EffectsLevel3, modSkill.EffectsLevel4, modSkill.EffectsLevel5);
            skill.m_TierLocalizedDescriptions = CreateLocalizedStrings(modSkill.DescriptionLevel1, modSkill.DescriptionLevel2, modSkill.DescriptionLevel3, modSkill.DescriptionLevel4, modSkill.DescriptionLevel5);

            //ModUtils.ExecuteMethod(GameManager.GetSkillsManager(), "InstantiateSkillPrefab", skill.gameObject);
            GameManager.GetSkillsManager().InstantiateSkillPrefab(skill.gameObject);
        }

        internal static void MapSkills()
        {
            SkillsManager skillsManager = GameManager.GetSkillsManager();
            if (skillsManager == null)
            {
                return;
            }

            foreach (ModSkill eachModSkill in skills)
            {
                MapSkill(eachModSkill);
            }
        }*/

        internal static void RegisterBlueprint(ModBlueprint modBlueprint, string sourcePath)
        {
            ValidateBlueprint(modBlueprint, sourcePath);

            blueprints.Add(modBlueprint);
        }

        internal static void RegisterSkill(ModSkill modSkill)
        {
            skills.Add(modSkill);
        }

        internal static void ValidateBlueprint(ModBlueprint modBlueprint, string sourcePath)
        {
            try
            {
                ModUtils.GetItem<GearItem>(modBlueprint.CraftedResult);
                
                if (!string.IsNullOrEmpty(modBlueprint.RequiredTool))
                {
                    ModUtils.GetItem<ToolsItem>(modBlueprint.RequiredTool);
                }
                
                if (modBlueprint.OptionalTools != null)
                {
                    ModUtils.GetMatchingItems<ToolsItem>(modBlueprint.OptionalTools);
                }
                
                ModUtils.GetMatchingItems<GearItem>(modBlueprint.RequiredGear);
                
            }
            catch (Exception e)
            {
                throw new ArgumentException("Validation of blueprint " + modBlueprint.name + " failed: " + e.Message + "\nThe blueprint was provided by '" + sourcePath + "', which may be out-of-date or installed incorrectly.");
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

        private static void ConfigureBurnable(ModComponent modComponent)
        {
            ModBurnableComponent modBurnableComponent = ModUtils.GetComponent<ModBurnableComponent>(modComponent);
            if (modBurnableComponent == null)
            {
                return;
            }

            FuelSourceItem fuelSourceItem = ModUtils.GetOrCreateComponent<FuelSourceItem>(modComponent);
            fuelSourceItem.m_BurnDurationHours = modBurnableComponent.BurningMinutes / 60f;
            fuelSourceItem.m_FireAgeMinutesBeforeAdding = modBurnableComponent.BurningMinutesBeforeAllowedToAdd;
            fuelSourceItem.m_FireStartSkillModifier = modBurnableComponent.SuccessModifier;
            fuelSourceItem.m_HeatIncrease = modBurnableComponent.TempIncrease;
            fuelSourceItem.m_HeatInnerRadius = 2.5f;
            fuelSourceItem.m_HeatOuterRadius = 6f;
        }

        private static void ConfigureCookingPot(ModComponent modComponent)
        {
            ModCookingPotComponent modCookingPotComponent = modComponent.TryCast<ModCookingPotComponent>();
            if (modCookingPotComponent == null)
            {
                return;
            }

            CookingPotItem cookingPotItem = ModUtils.GetOrCreateComponent<CookingPotItem>(modComponent);

            cookingPotItem.m_WaterCapacityLiters = modCookingPotComponent.Capacity;
            cookingPotItem.m_CanCookGrub = modCookingPotComponent.CanCookGrub;
            cookingPotItem.m_CanCookLiquid = modCookingPotComponent.CanCookLiquid;
            cookingPotItem.m_CanCookMeat = modCookingPotComponent.CanCookMeat;
            cookingPotItem.m_CanOnlyWarmUpFood = false;

            CookingPotItem template = ModUtils.GetItem<CookingPotItem>(modCookingPotComponent.Template, modComponent.name);
            cookingPotItem.m_BoilingTimeMultiplier = template.m_BoilingTimeMultiplier;
            cookingPotItem.m_BoilWaterPotMaterialsList = template.m_BoilWaterPotMaterialsList;
            cookingPotItem.m_BoilWaterReadyMaterialsList = template.m_BoilWaterReadyMaterialsList;
            cookingPotItem.m_ConditionPercentDamageFromBoilingDry = template.m_ConditionPercentDamageFromBoilingDry;
            cookingPotItem.m_ConditionPercentDamageFromBurningFood = template.m_ConditionPercentDamageFromBurningFood;
            cookingPotItem.m_CookedCalorieMultiplier = template.m_CookedCalorieMultiplier;
            cookingPotItem.m_CookingTimeMultiplier = template.m_CookingTimeMultiplier;
            cookingPotItem.m_GrubMeshType = template.m_GrubMeshType;
            cookingPotItem.m_LampOilMultiplier = template.m_LampOilMultiplier;
            cookingPotItem.m_MeltSnowMaterialsList = template.m_MeltSnowMaterialsList;
            cookingPotItem.m_NearFireWarmUpCookingTimeMultiplier = template.m_NearFireWarmUpCookingTimeMultiplier;
            cookingPotItem.m_NearFireWarmUpReadyTimeMultiplier = template.m_NearFireWarmUpReadyTimeMultiplier;
            cookingPotItem.m_ParticlesItemCooking = template.m_ParticlesItemCooking;
            cookingPotItem.m_ParticlesItemReady = template.m_ParticlesItemReady;
            cookingPotItem.m_ParticlesItemRuined = template.m_ParticlesItemRuined;
            cookingPotItem.m_ParticlesSnowMelting = template.m_ParticlesSnowMelting;
            cookingPotItem.m_ParticlesWaterBoiling = template.m_ParticlesWaterBoiling;
            cookingPotItem.m_ParticlesWaterReady = template.m_ParticlesWaterReady;
            cookingPotItem.m_ParticlesWaterRuined = template.m_ParticlesWaterRuined;
            cookingPotItem.m_ReadyTimeMultiplier = template.m_ReadyTimeMultiplier;
            cookingPotItem.m_RuinedFoodMaterialsList = template.m_RuinedFoodMaterialsList;
            cookingPotItem.m_SnowMesh = modCookingPotComponent.SnowMesh;
            cookingPotItem.m_WaterMesh = modCookingPotComponent.WaterMesh;

            GameObject grubMesh = UnityEngine.Object.Instantiate(template.m_GrubMeshFilter.gameObject, cookingPotItem.transform);
            cookingPotItem.m_GrubMeshFilter = grubMesh.GetComponent<MeshFilter>();
            cookingPotItem.m_GrubMeshRenderer = grubMesh.GetComponent<MeshRenderer>();

            PlaceableItem placeableItem = ModUtils.GetOrCreateComponent<PlaceableItem>(modComponent);
            //placeableItem.m_Range = template.GetComponent<PlaceableItem>()?.m_Range ?? 3; //<============================================
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
            EquippableModComponent equippableModComponent = modComponent.TryCast<EquippableModComponent>();
            if (equippableModComponent == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(equippableModComponent.InventoryActionLocalizationId)
                && !string.IsNullOrEmpty(equippableModComponent.ImplementationType)
                && equippableModComponent.EquippedModelPrefab != null)
            {
                equippableModComponent.InventoryActionLocalizationId = "GAMEPLAY_Equip";
            }
        }

        private static void ConfigureFood(ModComponent modComponent)
        {
            ModFoodComponent modFoodComponent = modComponent.TryCast<ModFoodComponent>();
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
            foodItem.m_ChanceFoodPoisoningRuined = Mathf.Clamp01(modFoodComponent.FoodPoisoningLowCondition / 100f);
            foodItem.m_DailyHPDecayInside = GetDecayPerStep(modFoodComponent.DaysToDecayIndoors, modFoodComponent.MaxHP);
            foodItem.m_DailyHPDecayOutside = GetDecayPerStep(modFoodComponent.DaysToDecayOutdoors, modFoodComponent.MaxHP);

            foodItem.m_TimeToEatSeconds = Mathf.Clamp(1, modFoodComponent.EatingTime, 10);
            foodItem.m_TimeToOpenAndEatSeconds = Mathf.Clamp(1, modFoodComponent.EatingTime, 10) + 5;
            foodItem.m_EatingAudio = modFoodComponent.EatingAudio;
            foodItem.m_OpenAndEatingAudio = modFoodComponent.EatingPackagedAudio;
            foodItem.m_Packaged = !string.IsNullOrEmpty(foodItem.m_OpenAndEatingAudio);

            foodItem.m_IsDrink = modFoodComponent.Drink;
            foodItem.m_IsFish = modFoodComponent.Fish;
            foodItem.m_IsMeat = modFoodComponent.Meat;
            foodItem.m_IsRawMeat = modFoodComponent.Raw;
            foodItem.m_IsNatural = modFoodComponent.Natural;
            foodItem.m_MustConsumeAll = false;
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

                if (modFoodComponent.Canned)
                {
                    foodItem.m_GearPrefabHarvestAfterFinishEatingNormal = Resources.Load<GameObject>("GEAR_RecycledCan");
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

            if (modFoodComponent.AffectCondition)
            {
                ConditionRestBuff conditionRestBuff = ModUtils.GetOrCreateComponent<ConditionRestBuff>(modFoodComponent);
                conditionRestBuff.m_ConditionRestBonus = modFoodComponent.ConditionRestBonus;
                conditionRestBuff.m_NumHoursRestAffected = modFoodComponent.ConditionRestMinutes / 60f;
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
            //gearItem.OverrideGearCondition(ModUtils.TranslateEnumValue<GearStartCondition, InitialCondition>(modComponent.InitialCondition)); //<===================================
            gearItem.OverrideGearCondition( ModUtils.TranslateEnumValue<GearStartCondition, InitialCondition>(modComponent.InitialCondition) , false);
            // OverrideGearCondition wanted to know if the item had been picked up yet; since Awake hadn't been called yet, I put false

            gearItem.m_LocalizedDisplayName = CreateLocalizedString(modComponent.DisplayNameLocalizationId);
            gearItem.m_LocalizedDescription = CreateLocalizedString(modComponent.DescriptionLocalizatonId);

            gearItem.m_PickUpAudio = modComponent.PickUpAudio;
            gearItem.m_StowAudio = modComponent.StowAudio;
            gearItem.m_PutBackAudio = modComponent.PickUpAudio;
            gearItem.m_WornOutAudio = modComponent.WornOutAudio;

            gearItem.m_ConditionTableType = GetConditionTableType(modComponent);
            gearItem.m_ScentIntensity = ScentMapper.GetScentIntensity(modComponent);

            gearItem.Awake();
        }

        private static void ConfigureLiquidItem(ModComponent modComponent)
        {
            ModLiquidComponent modLiquidItemComponent = modComponent.TryCast<ModLiquidComponent>();
            if (modLiquidItemComponent == null)
            {
                return;
            }

            LiquidItem liquidItem = ModUtils.GetOrCreateComponent<LiquidItem>(modComponent);
            liquidItem.m_LiquidCapacityLiters = modLiquidItemComponent.MaxLiters;
            liquidItem.m_LiquidType = ModUtils.TranslateEnumValue<GearLiquidTypeEnum, LiquidType>(modLiquidItemComponent.LiquidType);
            liquidItem.m_RandomizeQuantity = false;
            liquidItem.m_LiquidLiters = 0;
            liquidItem.m_DrinkingAudio = "Play_DrinkWater";
            liquidItem.m_TimeToDrinkSeconds = 4;
        }

        private static void ConfigureRifle(ModComponent modComponent)
        {
            ModRifleComponent modRifleComponent = modComponent.TryCast<ModRifleComponent>();
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

        private static void ConfigureStackable(ModComponent modComponent)
        {
            ModStackableComponent modStackableComponent = ModUtils.GetComponent<ModStackableComponent>(modComponent);
            if (modStackableComponent == null)
            {
                return;
            }

            StackableItem stackableItem = ModUtils.GetOrCreateComponent<StackableItem>(modComponent);

            stackableItem.m_LocalizedMultipleUnitText = new LocalizedString { m_LocalizationID = modStackableComponent.MultipleUnitTextID };

            if (string.IsNullOrEmpty(modStackableComponent.SingleUnitTextID))
            {
                stackableItem.m_LocalizedSingleUnitText = new LocalizedString { m_LocalizationID = modComponent.DisplayNameLocalizationId };
            }
            else
            {
                stackableItem.m_LocalizedSingleUnitText = new LocalizedString { m_LocalizationID = modStackableComponent.SingleUnitTextID };
            }

            stackableItem.m_StackSpriteName = modStackableComponent.StackSprite;

            stackableItem.m_ShareStackWithGear = new StackableItem[0];
            stackableItem.m_Units = modStackableComponent.UnitsPerItem;
            stackableItem.m_UnitsPerItem = modStackableComponent.UnitsPerItem;
        }

        private static LocalizedString[] CreateLocalizedStrings(params string[] keys)
        {
            LocalizedString[] result = new LocalizedString[keys.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = CreateLocalizedString(keys[i]);
            }

            return result;
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

        private static GearTypeEnum GetGearType(ModComponent modComponent)
        {
            if (modComponent.InventoryCategory != InventoryCategory.Auto)
            {
                return ModUtils.TranslateEnumValue<GearTypeEnum, InventoryCategory>(modComponent.InventoryCategory);
            }

            if (modComponent is ModToolComponent)
            {
                return GearTypeEnum.Tool;
            }

            if (modComponent is ModFoodComponent || modComponent is ModCookableComponent || (modComponent as ModLiquidComponent)?.LiquidType == LiquidType.Water)
            {
                return GearTypeEnum.Food;
            }

            if (modComponent is ModClothingComponent)
            {
                return GearTypeEnum.Clothing;
            }

            if (ModUtils.GetComponent<ModFireStartingComponent>(modComponent) != null || ModUtils.GetComponent<ModBurnableComponent>(modComponent) != null)
            {
                return GearTypeEnum.Firestarting;
            }

            return GearTypeEnum.Other;
        }

        private static void PostProcess(ModComponent modComponent)
        {
            modComponent.gameObject.layer = vp_Layer.Gear;

            GearItem gearItem = modComponent.GetComponent<GearItem>();
            //gearItem.m_SkinnedMeshRenderers = ModUtils.NotNull(gearItem.m_SkinnedMeshRenderers); //<================================================================
            gearItem.m_SkinnedMeshRenderers = ModUtils.NotNull<SkinnedMeshRenderer>(gearItem.m_SkinnedMeshRenderers);
            //I think this should be fine. It appears to just be a syntax error.

            GameObject template = Resources.Load<GameObject>("GEAR_CoffeeCup");
            MeshRenderer meshRenderer = template.GetComponentInChildren<MeshRenderer>();

            foreach (var eachMeshRenderer in gearItem.m_MeshRenderers)
            {
                foreach (var eachMaterial in eachMeshRenderer.materials)
                {
                    if (eachMaterial.shader.name == "Standard")
                    {
                        eachMaterial.shader = meshRenderer.material.shader;
                        eachMaterial.shaderKeywords = meshRenderer.material.shaderKeywords;

                        if (eachMaterial.GetTexture("_dmg_texture") == null)
                        {
                            eachMaterial.SetTexture("_dmg_texture", eachMaterial.GetTexture("_MainTex"));
                        }
                    }
                }
            }

            ModUtils.RegisterConsoleGearName(modComponent.GetEffectiveConsoleName(), modComponent.name);

            if (modComponent.Radial != Radial.None)
            {
                RadialConfigurator.RegisterGear(modComponent.Radial, modComponent.name);
            }

            UnityEngine.Object.DontDestroyOnLoad(modComponent.gameObject);
        }
    }
}