using ModComponentAPI;
using ModComponentMapper.ComponentMapper;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponentMapper
{
    public static class Mapper
    {
        private static List<ModComponent> mappedItems = new List<ModComponent>();

        public static void Map(string prefabName) => Map(Resources.Load(prefabName)?.TryCast<GameObject>());

        public static void Map(GameObject prefab)
        {
            if (prefab == null) throw new ArgumentException("The prefab was NULL.");

            ModComponent modComponent = ComponentUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                throw new ArgumentException("Prefab " + prefab.name + " does not contain a ModComponent.");
            }

            bool hasModPlaceHolder = !(ComponentUtils.GetComponent<ModPlaceHolderComponent>(prefab) is null);
            if (prefab.GetComponent<GearItem>() is null || hasModPlaceHolder)
            {
                Logger.Log("Mapping {0}", prefab.name);

                ConfigureBehaviours(modComponent);

                EquippableMapper.Configure(modComponent);
                LiquidMapper.Configure(modComponent);
                PowderMapper.Configure(modComponent);
                FoodMapper.Configure(modComponent);
                CookableMapper.Configure(modComponent);
                CookingPotMapper.Configure(modComponent);
                RifleMapper.Configure(modComponent);
                ClothingMapper.ConfigureClothing(modComponent);
                FirstAidMapper.Configure(modComponent);
                ToolMapper.Configure(modComponent);
                BedMapper.Configure(modComponent);
                BodyHarvestMapper.Configure(modComponent);

                if (hasModPlaceHolder) return;

                InspectMapper.Configure(modComponent);
                ConfigureGearItem(modComponent);

                mappedItems.Add(modComponent);

                PostProcess(modComponent);
            }
        }

        internal static void ConfigureBehaviours(ModComponent modComponent)
        {
            HarvestableMapper.Configure(modComponent);
            RepairableMapper.Configure(modComponent);
            FireStarterMapper.Configure(modComponent);
            AccelerantMapper.Configure(modComponent);
            StackableMapper.Configure(modComponent);
            BurnableMapper.Configure(modComponent);
            ScentMapper.Configure(modComponent);
            SharpenableMapper.Configure(modComponent);
            EvolveMapper.Configure(modComponent);
            MillableMapper.Configure(modComponent);
        }

        internal static void ConfigureBehaviours(GameObject prefab)
        {
            if (prefab == null) throw new ArgumentException("The prefab was NULL.");

            ModComponent modComponent = ComponentUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                throw new ArgumentException("Prefab " + prefab.name + " does not contain a ModComponent.");
            }

            ConfigureBehaviours(modComponent);
        }

        internal static float GetDecayPerStep(float steps, float maxHP)
        {
            if (steps > 0) return maxHP / steps;
            else return 0;
        }

        private static void ConfigureGearItem(ModComponent modComponent)
        {
            GearItem gearItem = ComponentUtils.GetOrCreateComponent<GearItem>(modComponent);

            gearItem.m_Type = GetGearType(modComponent);
            gearItem.m_WeightKG = modComponent.WeightKG;
            gearItem.m_MaxHP = modComponent.MaxHP;
            gearItem.m_DailyHPDecay = GetDecayPerStep(modComponent.DaysToDecay, modComponent.MaxHP);
            //gearItem.OverrideGearCondition(EnumUtils.TranslateEnumValue<GearStartCondition, InitialCondition>(modComponent.InitialCondition)); //<===================================
            gearItem.OverrideGearCondition(EnumUtils.TranslateEnumValue<GearStartCondition, InitialCondition>(modComponent.InitialCondition), false);
            // OverrideGearCondition wanted to know if the item had been picked up yet; since Awake hadn't been called yet, I put false

            gearItem.m_LocalizedDisplayName = NameUtils.CreateLocalizedString(modComponent.DisplayNameLocalizationId);
            gearItem.m_LocalizedDescription = NameUtils.CreateLocalizedString(modComponent.DescriptionLocalizatonId);

            gearItem.m_PickUpAudio = modComponent.PickUpAudio;
            gearItem.m_StowAudio = modComponent.StowAudio;
            gearItem.m_PutBackAudio = modComponent.PickUpAudio;
            gearItem.m_WornOutAudio = modComponent.WornOutAudio;

            gearItem.m_ConditionTableType = GetConditionTableType(modComponent);
            gearItem.m_ScentIntensity = ScentMapper.GetScentIntensity(modComponent);

            gearItem.Awake();
        }

        private static ConditionTableManager.ConditionTableType GetConditionTableType(ModComponent modComponent)
        {
            if (modComponent is ModFoodComponent)
            {
                ModFoodComponent modFoodComponent = (ModFoodComponent)modComponent;
                if (modFoodComponent.Canned) return ConditionTableManager.ConditionTableType.CannedFood;

                if (modFoodComponent.Meat) return ConditionTableManager.ConditionTableType.Meat;

                if (!modFoodComponent.Natural && !modFoodComponent.Drink) return ConditionTableManager.ConditionTableType.DryFood;

                return ConditionTableManager.ConditionTableType.Unknown;
            }

            return ConditionTableManager.ConditionTableType.Unknown;
        }

        private static GearTypeEnum GetGearType(ModComponent modComponent)
        {
            if (modComponent.InventoryCategory != InventoryCategory.Auto)
            {
                return EnumUtils.TranslateEnumValue<GearTypeEnum, InventoryCategory>(modComponent.InventoryCategory);
            }

            if (modComponent is ModToolComponent) return GearTypeEnum.Tool;

            if (modComponent is ModFoodComponent || modComponent is ModCookableComponent || (modComponent as ModLiquidComponent)?.LiquidType == LiquidType.Water)
            {
                return GearTypeEnum.Food;
            }

            if (modComponent is ModClothingComponent) return GearTypeEnum.Clothing;

            if (ComponentUtils.GetComponent<ModFireStartingComponent>(modComponent) != null || ComponentUtils.GetComponent<ModBurnableComponent>(modComponent) != null)
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

            NameUtils.RegisterConsoleGearName(modComponent.GetEffectiveConsoleName(), modComponent.name);

            UnityEngine.Object.DontDestroyOnLoad(modComponent.gameObject);
        }
    }
}