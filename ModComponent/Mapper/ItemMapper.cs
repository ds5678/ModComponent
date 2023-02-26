using Il2Cpp;
using Il2CppTLD.Gear;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;
using ModComponent.Mapper.BehaviourMappers;
using ModComponent.Mapper.ComponentMappers;
using ModComponent.Utils;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

namespace ModComponent.Mapper;

internal static class ItemMapper
{
	private static readonly List<ModBaseComponent> mappedItems = new();

	public static void Map(string prefabName)
	{
		UnityEngine.Object @object = Resources.Load(prefabName);
		if (@object == null)
		{
			throw new ArgumentException($"Prefab {prefabName} not found");
		}

		GameObject? prefab = @object.TryCast<GameObject>();
		if (prefab == null)
		{
			throw new ArgumentException($"Prefab {prefabName} is not a GameObject");
		}

		Map(prefab);
	}

	public static void Map(GameObject prefab)
	{
		if (prefab == null)
		{
			throw new ArgumentException("The prefab was NULL.");
		}

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
		if (prefab == null)
		{
			throw new ArgumentException("The prefab was NULL.");
		}

		ModBaseComponent modComponent = ComponentUtils.GetModComponent(prefab);
		if (modComponent == null)
		{
			throw new ArgumentException("Prefab " + prefab.name + " does not contain a ModComponent.");
		}

		ConfigureBehaviours(modComponent);
	}

	internal static float GetDecayPerStep(float steps, float maxHP)
	{
		return steps > 0 ? maxHP / steps : 0;
	}

	private static void ConfigureGearItem(ModBaseComponent modComponent)
	{
		GearItem gearItem = ComponentUtils.GetOrCreateComponent<GearItem>(modComponent);

		// patch to add in gearItem.GearItemData
		if (gearItem.GearItemData == null)
		{
			Il2CppTLD.Gear.GearItemData gid = ScriptableObject.CreateInstance<GearItemData>();
			gearItem.m_GearItemData = gid;
		}


		string guid = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(modComponent.name))).Replace("-", "");
		gearItem.GearItemData.m_PrefabReference = new AssetReferenceGearItem(guid.ToLower());

		gearItem.m_DisplayNameOverrideLocID = modComponent.name;
		gearItem.GearItemData.m_Type = GetGearType(modComponent);
		gearItem.GearItemData.m_BaseWeightKG = modComponent.WeightKG;
		gearItem.GearItemData.m_MaxHP = modComponent.MaxHP;
		gearItem.GearItemData.m_DailyHPDecay = GetDecayPerStep(modComponent.DaysToDecay, modComponent.MaxHP);
		gearItem.OverrideGearCondition(EnumUtils.TranslateEnumValue<GearStartCondition, ModBaseComponent.StartingCondition>(modComponent.InitialCondition), false);
		// OverrideGearCondition wanted to know if the item had been picked up yet; since Awake hadn't been called yet, I put false

		gearItem.GearItemData.m_LocalizedName = NameUtils.CreateLocalizedString(modComponent.DisplayNameLocalizationId);
		gearItem.GearItemData.m_LocalizedDescription = NameUtils.CreateLocalizedString(modComponent.DescriptionLocalizatonId);

		gearItem.GearItemData.m_PickupAudio = ModUtils.GetWwiseEventFromString(modComponent.PickUpAudio);
		gearItem.GearItemData.m_StowAudio = ModUtils.GetWwiseEventFromString(modComponent.StowAudio);
		gearItem.GearItemData.m_PutBackAudio = ModUtils.GetWwiseEventFromString(modComponent.PutBackAudio);
		gearItem.GearItemData.m_WornOutAudio = ModUtils.GetWwiseEventFromString(modComponent.WornOutAudio);

		gearItem.GearItemData.m_ConditionType = GetConditionTableType(modComponent);
		gearItem.GearItemData.m_ScentIntensity = ScentMapper.GetScentIntensity(modComponent);

		gearItem.Awake();


	}

	private static ConditionTableType GetConditionTableType(ModBaseComponent modComponent)
	{
		ModFoodComponent modFoodComponent = modComponent.TryCast<ModFoodComponent>();
		if (modFoodComponent != null)
		{
			if (modFoodComponent.Canned)
			{
				return ConditionTableType.CannedFood;
			}

			if (modFoodComponent.Meat)
			{
				return ConditionTableType.Meat;
			}

			if (!modFoodComponent.Natural && !modFoodComponent.Drink)
			{
				return ConditionTableType.DryFood;
			}

			return ConditionTableType.Unknown;
		}

		return ConditionTableType.Unknown;
	}

	private static GearType GetGearType(ModBaseComponent modComponent)
	{
		if (modComponent.InventoryCategory != ModBaseComponent.ItemCategory.Auto)
		{
			return EnumUtils.TranslateEnumValue<GearType, ModBaseComponent.ItemCategory>(modComponent.InventoryCategory);
		}

		if (modComponent is ModToolComponent)
		{
			return GearType.Tool;
		}

		if (modComponent is ModFoodComponent || modComponent is ModCookableComponent || (modComponent as ModLiquidComponent)?.LiquidType == ModLiquidComponent.LiquidKind.Water)
		{
			return GearType.Food;
		}

		if (modComponent is ModClothingComponent)
		{
			return GearType.Clothing;
		}

		if (ComponentUtils.GetComponentSafe<ModFireMakingBaseBehaviour>(modComponent) != null || ComponentUtils.GetComponentSafe<ModBurnableBehaviour>(modComponent) != null)
		{
			return GearType.Firestarting;
		}

		return GearType.Other;
	}

	private static void PostProcess(ModBaseComponent modComponent)
	{
		modComponent.gameObject.layer = vp_Layer.Gear;
		GearItem gearItem = modComponent.GetComponent<GearItem>();
		gearItem.m_SkinnedMeshRenderers = ModUtils.NotNull<SkinnedMeshRenderer>(gearItem.m_SkinnedMeshRenderers);

		GameObject template = GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject;
		MeshRenderer meshRenderer = template.GetComponentInChildren<MeshRenderer>();

		foreach (MeshRenderer? eachMeshRenderer in gearItem.m_MeshRenderers)
		{
			foreach (Material? eachMaterial in eachMeshRenderer.materials)
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

		ConsoleWaitlist.MaybeRegisterConsoleGearName(modComponent.GetEffectiveConsoleName(), modComponent.name, gearItem.GearItemData.m_Type);

		UnityEngine.Object.DontDestroyOnLoad(modComponent.gameObject);
	}
}