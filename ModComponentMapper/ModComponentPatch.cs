using System;

using Harmony;
using UnityEngine;

using ModComponentAPI;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(ModComponent), "Awake")]
    internal class ModComponentPatch
    {
        public static void Postfix(ModComponent __instance)
        {
            ConfiguredGearItem(__instance);
            ConfigureInspect(__instance);
            ConfigureFood(__instance);
            ConfigureTool(__instance);

            __instance.GetComponent<GearItem>().Awake();

            PostProcess(__instance);
        }

        private static void PostProcess(ModComponent modComponent)
        {
            modComponent.gameObject.layer = 17;

            GearItem gearItem = modComponent.GetComponent<GearItem>();
            gearItem.m_SkinnedMeshRenderers = ModUtils.NotNull(gearItem.m_SkinnedMeshRenderers);
        }

        private static void ConfiguredGearItem(ModComponent modComponent)
        {
            GameObject gameObject = modComponent.gameObject;

            GearItem gearItem = gameObject.AddComponent<GearItem>();
            gearItem.m_Type = GetGearType(modComponent);
            gearItem.m_WeightKG = modComponent.WeightKG;
            gearItem.m_MaxHP = modComponent.MaxHP;
            gearItem.m_DailyHPDecay = modComponent.DaysToDecay > 0 ? modComponent.MaxHP / modComponent.DaysToDecay : 0;
            gearItem.m_StartCondition = GearStartCondition.Random;

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

            foodItem.m_EatingAudio = modFoodComponent.EatingAudio;
            foodItem.m_TimeToEatSeconds = Mathf.Clamp(1, modFoodComponent.EatingTime, 10);
            foodItem.m_OpenAndEatingAudio = modFoodComponent.EatingPackagedAudio;
            foodItem.m_Packaged = foodItem.m_OpenAndEatingAudio != null;

            foodItem.m_HeatedWhenCooked = modFoodComponent.CanBeHeated;
            foodItem.m_PercentHeatLossPerMinuteIndoors = 1;
            foodItem.m_PercentHeatLossPerMinuteOutdoors = 2;

            foodItem.m_IsDrink = modFoodComponent.Drink;
            foodItem.m_IsFish = modFoodComponent.Fish;
            foodItem.m_IsMeat = modFoodComponent.Meat;
            foodItem.m_IsRawMeat = modFoodComponent.Raw;
            foodItem.m_IsNatural = modFoodComponent.Natural;
            foodItem.m_ParasiteRiskPercentIncrease = ModUtils.NotNull(modFoodComponent.ParasiteRiskIncrements);
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
        }

        private static void ConfigureTool(ModComponent modComponent)
        {
            ModToolComponent modToolComponent = modComponent as ModToolComponent;
            if (modToolComponent == null)
            {
                return;
            }

            if (modToolComponent.ImplementationType == null || modToolComponent.ImplementationType == string.Empty)
            {
                return;
            }

            Type implementationType = Type.GetType(modToolComponent.ImplementationType);
            object implementation = Activator.CreateInstance(implementationType);
            if (implementation == null)
            {
                return;
            }

            modComponent.Implementation = implementation;

            ModUtils.SetFieldValue(implementation, "GameObject", modComponent.gameObject);

            modComponent.OnEquipped = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnEquipped");
            modComponent.OnUnequipped = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnUnequipped");

            modComponent.OnPrimaryAction = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnPrimaryAction");
            modComponent.OnSecondaryAction = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnSecondaryAction");

            modComponent.OnControlModeChangedWhileEquipped = (Action)ModUtils.CreateDelegate(typeof(Action), implementation, "OnControlModeChangedWhileEquipped");
        }
    }
}
