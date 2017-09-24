using ModComponentAPI;
using System;
using UnityEngine;

namespace ModComponentMapper
{
    public class Mapper
    {
        public static MappedItem Map(string prefabName)
        {
            return Map((GameObject)Resources.Load(prefabName));
        }

        public static MappedItem Map(GameObject prefab)
        {
            ModComponent modComponent = ModUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                throw new ArgumentException("Given prefab does not contain a ModComponent.");
            }

            Log("Mapping {0}", prefab.name);

            ConfigureEquippable(modComponent);
            ConfigureInspect(modComponent);
            ConfigureFood(modComponent);
            ConfigureRifle(modComponent);
            ConfigureGearItem(modComponent);

            PostProcess(modComponent);

            return new MappedItem(prefab);
        }

        private static void PostProcess(ModComponent modComponent)
        {
            modComponent.gameObject.layer = vp_Layer.Gear;

            GearItem gearItem = modComponent.GetComponent<GearItem>();
            gearItem.m_SkinnedMeshRenderers = ModUtils.NotNull(gearItem.m_SkinnedMeshRenderers);
        }

        private static void ConfigureGearItem(ModComponent modComponent)
        {
            GameObject gameObject = modComponent.gameObject;

            GearItem gearItem = gameObject.GetComponent<GearItem>();
            if (gearItem == null)
            {
                gearItem = gameObject.AddComponent<GearItem>();
            }

            gearItem.m_Type = GetGearType(modComponent);
            gearItem.m_WeightKG = modComponent.WeightKG;
            gearItem.m_MaxHP = modComponent.MaxHP;
            gearItem.m_DailyHPDecay = modComponent.DaysToDecay > 0 ? modComponent.MaxHP / modComponent.DaysToDecay : 0;
            gearItem.OverrideGearCondition(GearStartCondition.Random);

            gearItem.m_LocalizedDisplayName = new LocalizedString();
            gearItem.m_LocalizedDisplayName.m_LocalizationID = modComponent.DisplayNameLocalizationId;

            gearItem.m_LocalizedDescription = new LocalizedString();
            gearItem.m_LocalizedDescription.m_LocalizationID = modComponent.DescriptionLocalizatonId;

            gearItem.m_PickUpAudio = modComponent.PickUpAudio;
            gearItem.m_StowAudio = modComponent.StowAudio;
            gearItem.m_PutBackAudio = modComponent.PickUpAudio;
            gearItem.m_WornOutAudio = modComponent.WornOutAudio;
        }

        private static void ConfigureFood(ModComponent modComponent)
        {
            ModFoodComponent modFoodComponent = modComponent as ModFoodComponent;
            if (modFoodComponent == null)
            {
                return;
            }

            FoodItem foodItem = modFoodComponent.gameObject.AddComponent<FoodItem>();

            foodItem.m_CaloriesTotal = modFoodComponent.Calories;
            foodItem.m_CaloriesRemaining = modFoodComponent.Calories;
            foodItem.m_ReduceThirst = modFoodComponent.ThirstEffect;

            foodItem.m_ChanceFoodPoisoning = Mathf.Clamp01(modFoodComponent.FoodPoisoning / 100f);
            foodItem.m_ChanceFoodPoisoningLowCondition = Mathf.Clamp01(modFoodComponent.FoodPoisoningLowCondition / 100f);
            foodItem.m_DailyHPDecayInside = modFoodComponent.DaysToDecayIndoors > 0 ? modFoodComponent.MaxHP / modFoodComponent.DaysToDecayIndoors : 0;
            foodItem.m_DailyHPDecayOutside = modFoodComponent.DaysToDecayOutdoors > 0 ? modFoodComponent.MaxHP / modFoodComponent.DaysToDecayOutdoors : 0;

            foodItem.m_TimeToEatSeconds = Mathf.Clamp(1, modFoodComponent.EatingTime, 10);
            foodItem.m_EatingAudio = modFoodComponent.EatingAudio;
            foodItem.m_OpenAndEatingAudio = modFoodComponent.EatingPackagedAudio;
            foodItem.m_Packaged = !string.IsNullOrEmpty(foodItem.m_OpenAndEatingAudio);

            if (modFoodComponent.Cooking)
            {
                foodItem.m_HeatedWhenCooked = true;
                foodItem.m_PercentHeatLossPerMinuteIndoors = 1;
                foodItem.m_PercentHeatLossPerMinuteOutdoors = 2;

                Cookable cookable = foodItem.gameObject.AddComponent<Cookable>();
                cookable.m_CookTimeMinutes = modFoodComponent.CookingMinutes;
                cookable.m_NumUnitsRequired = modFoodComponent.CookingUnitsRequired;
                cookable.m_PotableWaterRequiredLiters = modFoodComponent.CookingWaterRequired;
                cookable.m_CookAudio = modFoodComponent.CookingAudio ?? "PLAY_BOILINGLIGHT";
            }

            if (modFoodComponent.Opening)
            {
                foodItem.m_GearRequiredToOpen = true;
                foodItem.m_OpenedWithCanOpener = modFoodComponent.OpeningWithCanOpener;
                foodItem.m_OpenedWithHatchet = modFoodComponent.OpeningWithHatchet;
                foodItem.m_OpenedWithKnife = modFoodComponent.OpeningWithKnife;

                if (modFoodComponent.OpeningWithSmashing)
                {
                    SmashableItem smashableItem = foodItem.gameObject.AddComponent<SmashableItem>();
                    smashableItem.m_MinPercentLoss = 10;
                    smashableItem.m_MaxPercentLoss = 30;
                    smashableItem.m_TimeToSmash = 6;
                    smashableItem.m_SmashAudio = "Play_EatingSmashCan";
                }
            }

            foodItem.m_IsDrink = modFoodComponent.Drink;
            foodItem.m_IsFish = modFoodComponent.Fish;
            foodItem.m_IsMeat = modFoodComponent.Meat;
            foodItem.m_IsRawMeat = modFoodComponent.Raw;
            foodItem.m_IsNatural = modFoodComponent.Natural;
            foodItem.m_ParasiteRiskPercentIncrease = ModUtils.NotNull(modFoodComponent.ParasiteRiskIncrements);

            if (modFoodComponent.AffectRest)
            {
                FatigueBuff fatigueBuff = foodItem.gameObject.AddComponent<FatigueBuff>();
                fatigueBuff.m_InitialPercentDecrease = modFoodComponent.InstantRestChange;
                fatigueBuff.m_RateOfIncreaseScale = modFoodComponent.RestFactor;
                fatigueBuff.m_DurationHours = modFoodComponent.RestFactorMinutes / 60f;
            }

            if (modFoodComponent.ContainsAlcohol)
            {
                AlcoholComponent alcohol = foodItem.gameObject.AddComponent<AlcoholComponent>();
                alcohol.AmountTotal = modFoodComponent.WeightKG * modFoodComponent.AlcoholPercentage * 0.01f;
                alcohol.AmountRemaining = alcohol.AmountTotal;
                alcohol.UptakeSeconds = modFoodComponent.AlcoholUptakeMinutes * 60;
            }
        }

        private static void ConfigureRifle(ModComponent modComponent)
        {
            ModRifleComponent modRifleComponent = modComponent as ModRifleComponent;
            if (modRifleComponent == null)
            {
                return;
            }

            GunItem gunItem = modRifleComponent.gameObject.AddComponent<GunItem>();

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

            Cleanable cleanable = modRifleComponent.gameObject.AddComponent<Cleanable>();
            cleanable.m_ConditionIncreaseMin = 2;
            cleanable.m_ConditionIncreaseMin = 5;
            cleanable.m_DurationMinutesMin = 15;
            cleanable.m_DurationMinutesMax = 5;
            cleanable.m_CleanAudio = "Play_RifleCleaning";
            cleanable.m_RequiresToolToClean = true;
            cleanable.m_CleanToolChoices = new ToolsItem[] { Resources.Load<GameObject>("GEAR_RifleCleaningKit").GetComponent<ToolsItem>() };

            FirstPersonItem firstPersonItem = ConfiguredRifleFirstPersonItem(modRifleComponent);

            ModAnimationStateMachine animation = modRifleComponent.gameObject.AddComponent<ModAnimationStateMachine>();
            animation.SetTransitions(firstPersonItem.m_PlayerStateTransitions);
        }

        private static FirstPersonItem ConfiguredRifleFirstPersonItem(ModRifleComponent modRifleComponent)
        {
            FirstPersonItem result = modRifleComponent.gameObject.AddComponent<FirstPersonItem>();

            FirstPersonItem template = Resources.Load<GameObject>("GEAR_Rifle").GetComponent<FirstPersonItem>();

            result.m_FirstPersonObjectName = ModUtils.NormalizeName(modRifleComponent.name);
            result.m_UnWieldAudio = template.m_UnWieldAudio;
            result.m_WieldAudio = template.m_WieldAudio;
            result.m_PlayerStateTransitions = UnityEngine.Object.Instantiate(template.m_PlayerStateTransitions);
            result.Awake();

            return result;
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

            return GearTypeEnum.Other;
        }

        private static void ConfigureInspect(ModComponent modComponent)
        {
            if (!modComponent.InspectOnPickup)
            {
                return;
            }

            GameObject gameObject = modComponent.gameObject;

            Inspect inspect = gameObject.AddComponent<Inspect>();
            inspect.m_DistanceFromCamera = modComponent.InspectDistance;
            inspect.m_Scale = modComponent.InspectScale;
            inspect.m_Angles = modComponent.InspectAngles;
            inspect.m_Offset = modComponent.InspectOffset;
        }

        private static void ConfigureEquippable(ModComponent modComponent)
        {
            EquippableModComponent equippable = modComponent as EquippableModComponent;
            if (equippable == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(modComponent.InventoryActionLocalizationId))
            {
                modComponent.InventoryActionLocalizationId = "GAMEPLAY_Equip";
            }

            if (equippable.ImplementationType == null || equippable.ImplementationType == string.Empty)
            {
                return;
            }

            Type implementationType = Type.GetType(equippable.ImplementationType);
            object implementation = Activator.CreateInstance(implementationType);
            if (implementation == null)
            {
                return;
            }

            ModUtils.SetFieldValue(implementation, "ModComponent", modComponent);

            equippable.Implementation = implementation;

            equippable.OnEquipped = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnEquipped");
            equippable.OnUnequipped = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnUnequipped");

            equippable.OnPrimaryAction = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnPrimaryAction");
            equippable.OnSecondaryAction = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnSecondaryAction");

            equippable.OnControlModeChangedWhileEquipped = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnControlModeChangedWhileEquipped");
        }

        private static void Log(string message, params object[] parameters)
        {
            LogUtils.Log("ModComponentMapper", message, parameters);
        }
    }

    public class MappedItem
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

        public MappedItem AddToLootTable(LootTableName lootTableName, int weight)
        {
            ModUtils.InsertIntoLootTable(lootTableName, gameObject, weight);

            return this;
        }

        public MappedItem SpawnAt(SceneName sceneName, Vector3 position, Quaternion rotation, float chance = 1)
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
}
