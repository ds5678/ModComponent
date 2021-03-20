using ModComponentAPI;
using ModComponentMapper.ComponentMapper;
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

        public MappedItem RegisterInConsole(string displayName)
        {
            ModUtils.RegisterConsoleGearName(displayName, gameObject.name);

            return this;
        }
    }

    public class Mapper
    {
        private static List<ModBlueprint> blueprints = new List<ModBlueprint>();
        private static List<ModComponent> mappedItems = new List<ModComponent>();
        private static List<ModSkill> skills = new List<ModSkill>();

        public static MappedItem Map(string prefabName)
        {
            return Map(Resources.Load(prefabName)?.TryCast<GameObject>());
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
                Logger.Log("Mapping {0}", prefab.name);

                InspectMapper.Configure(modComponent);
                ConfigureBehaviours(modComponent);

                EquippableMapper.Configure(modComponent);
                LiquidMapper.Configure(modComponent);
                FoodMapper.Configure(modComponent);
                CookableMapper.Configure(modComponent);
                CookingPotMapper.Configure(modComponent);
                RifleMapper.Configure(modComponent);
                ClothingMapper.ConfigureClothing(modComponent);
                FirstAidMapper.Configure(modComponent);
                ToolMapper.Configure(modComponent);
                BedMapper.Configure(modComponent);
                BodyHarvestMapper.Configure(modComponent);

                ConfigureGearItem(modComponent);

                mappedItems.Add(modComponent);

                PostProcess(modComponent);
            }

            return new MappedItem(prefab);
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
            if (prefab == null)
            {
                throw new ArgumentException("The prefab was NULL.");
            }

            ModComponent modComponent = ModUtils.GetModComponent(prefab);
            if (modComponent == null)
            {
                throw new ArgumentException("Prefab " + prefab.name + " does not contain a ModComponent.");
            }

            ConfigureBehaviours(modComponent);
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

            bpItem.m_RequiredCraftingLocation = ModUtils.TranslateEnumValue<CraftingLocation, ModComponentAPI.CraftingLocation>(modBlueprint.RequiredCraftingLocation);
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

        internal static void MapSkill(ModSkill modSkill)
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
        }

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
                if(string.IsNullOrEmpty(sourcePath)) throw new ArgumentException("Validation of blueprint " + modBlueprint.name + " failed: " + e.Message + "\nThe blueprint was provided by '" + sourcePath + "', which may be out-of-date or installed incorrectly.");
                else throw new ArgumentException("Validation of blueprint " + modBlueprint.name + " failed: " + e.Message + "\nThe blueprint may be out-of-date or installed incorrectly.");
            }
        }

        private static void ConfigureGearItem(ModComponent modComponent)
        {
            GearItem gearItem = ModUtils.GetOrCreateComponent<GearItem>(modComponent);

            gearItem.m_Type = GetGearType(modComponent);
            gearItem.m_WeightKG = modComponent.WeightKG;
            gearItem.m_MaxHP = modComponent.MaxHP;
            gearItem.m_DailyHPDecay = GetDecayPerStep(modComponent.DaysToDecay, modComponent.MaxHP);
            //gearItem.OverrideGearCondition(ModUtils.TranslateEnumValue<GearStartCondition, InitialCondition>(modComponent.InitialCondition)); //<===================================
            gearItem.OverrideGearCondition(ModUtils.TranslateEnumValue<GearStartCondition, InitialCondition>(modComponent.InitialCondition), false);
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

            UnityEngine.Object.DontDestroyOnLoad(modComponent.gameObject);
        }
    }
}