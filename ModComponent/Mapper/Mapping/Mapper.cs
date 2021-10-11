using ModComponentAPI;
using ModComponentMapper.ComponentMapper;
using ModComponent.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponentMapper
{
	public static class Mapper
	{
		private static List<ModBaseComponent> mappedItems = new List<ModBaseComponent>();

		public static void Map(string prefabName) => Map(Resources.Load(prefabName)?.TryCast<GameObject>());

		public static void Map(GameObject prefab)
		{
			if (prefab == null) throw new ArgumentException("The prefab was NULL.");

			ModBaseComponent modComponent = ComponentUtils.GetModComponent(prefab);
			if (modComponent == null)
			{
				throw new ArgumentException("Prefab " + prefab.name + " does not contain a ModComponent.");
			}

			if (prefab.GetComponent<GearItem>() == null)
			{
				ConfigureBehaviours(modComponent);

				EquippableMapper.Configure(modComponent);
				LiquidMapper.Configure(modComponent);
				PowderMapper.Configure(modComponent);
				FoodMapper.Configure(modComponent);
				CookableMapper.Configure(modComponent);
				CookingPotMapper.Configure(modComponent);
				ClothingMapper.Configure(modComponent);
				CollectibleMapper.Configure(modComponent);
				CharcoalMapper.Configure(modComponent);
				PurificationMapper.Configure(modComponent);
				ResearchMapper.Configure(modComponent);
				FirstAidMapper.Configure(modComponent);
				ToolMapper.Configure(modComponent);
				GenericEquippableMapper.Configure(modComponent);
				BedMapper.Configure(modComponent);
				BodyHarvestMapper.Configure(modComponent);

				InspectMapper.Configure(modComponent);
				ConfigureGearItem(modComponent);

				mappedItems.Add(modComponent);

				PostProcess(modComponent);
			}
		}

		internal static void ConfigureBehaviours(ModBaseComponent modComponent)
		{
			AccelerantMapper.Configure(modComponent);
			BurnableMapper.Configure(modComponent);
			FireStarterMapper.Configure(modComponent);
			TinderMapper.Configure(modComponent);
			CarryingCapacityMapper.Configure(modComponent);
			EvolveMapper.Configure(modComponent);
			HarvestableMapper.Configure(modComponent);
			MillableMapper.Configure(modComponent);
			RepairableMapper.Configure(modComponent);
			ScentMapper.Configure(modComponent);
			SharpenableMapper.Configure(modComponent);
			StackableMapper.Configure(modComponent);
		}

		internal static void ConfigureBehaviours(GameObject prefab)
		{
			if (prefab == null) throw new ArgumentException("The prefab was NULL.");

			ModBaseComponent modComponent = ComponentUtils.GetModComponent(prefab);
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

		private static void ConfigureGearItem(ModBaseComponent modComponent)
		{
			GearItem gearItem = ComponentUtils.GetOrCreateComponent<GearItem>(modComponent);

			gearItem.m_Type = GetGearType(modComponent);
			gearItem.m_WeightKG = modComponent.WeightKG;
			gearItem.m_MaxHP = modComponent.MaxHP;
			gearItem.m_DailyHPDecay = GetDecayPerStep(modComponent.DaysToDecay, modComponent.MaxHP);
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

		private static ConditionTableManager.ConditionTableType GetConditionTableType(ModBaseComponent modComponent)
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

		private static GearTypeEnum GetGearType(ModBaseComponent modComponent)
		{
			if (modComponent.InventoryCategory != InventoryCategory.Auto)
			{
				return EnumUtils.TranslateEnumValue<GearTypeEnum, InventoryCategory>(modComponent.InventoryCategory);
			}

			if (modComponent is ModToolComponent) return GearTypeEnum.Tool;

			if (modComponent is ModFoodComponent || modComponent is ModCookableComponent || (modComponent as ModLiquidComponent)?.LiquidType == ModLiquidComponent.LiquidKind.Water)
			{
				return GearTypeEnum.Food;
			}

			if (modComponent is ModClothingComponent) return GearTypeEnum.Clothing;

			if (ComponentUtils.GetComponent<ModFireMakingBaseBehaviour>(modComponent) != null || ComponentUtils.GetComponent<ModBurnableBehaviour>(modComponent) != null)
			{
				return GearTypeEnum.Firestarting;
			}

			return GearTypeEnum.Other;
		}

		private static void PostProcess(ModBaseComponent modComponent)
		{
			modComponent.gameObject.layer = vp_Layer.Gear;

			GearItem gearItem = modComponent.GetComponent<GearItem>();
			gearItem.m_SkinnedMeshRenderers = ModUtils.NotNull<SkinnedMeshRenderer>(gearItem.m_SkinnedMeshRenderers);

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

			ConsoleWaitlist.MaybeRegisterConsoleGearName(modComponent.GetEffectiveConsoleName(), modComponent.name);

			UnityEngine.Object.DontDestroyOnLoad(modComponent.gameObject);
		}
	}
}