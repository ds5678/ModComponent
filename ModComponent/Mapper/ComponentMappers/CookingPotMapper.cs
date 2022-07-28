extern alias Hinterland;
using Hinterland;
using ModComponent.API.Components;
using UnityEngine;

namespace ModComponent.Mapper.ComponentMappers
{
	internal static class CookingPotMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModCookingPotComponent modCookingPotComponent = modComponent.TryCast<ModCookingPotComponent>();
			if (modCookingPotComponent == null) return;

			CookingPotItem cookingPotItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<CookingPotItem>(modComponent);

			cookingPotItem.m_WaterCapacityLiters = modCookingPotComponent.Capacity;
			cookingPotItem.m_CanCookGrub = modCookingPotComponent.CanCookGrub;
			cookingPotItem.m_CanCookLiquid = modCookingPotComponent.CanCookLiquid;
			cookingPotItem.m_CanCookMeat = modCookingPotComponent.CanCookMeat;
			cookingPotItem.m_CanOnlyWarmUpFood = false;

			CookingPotItem template = ModComponent.Utils.ModUtils.GetItem<CookingPotItem>(modCookingPotComponent.Template, modComponent.name);
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

			GameObject grubMesh = Object.Instantiate(template.m_GrubMeshFilter.gameObject, cookingPotItem.transform);
			cookingPotItem.m_GrubMeshFilter = grubMesh.GetComponent<MeshFilter>();
			cookingPotItem.m_GrubMeshRenderer = grubMesh.GetComponent<MeshRenderer>();

			PlaceableItem placeableItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<PlaceableItem>(modComponent);
			//placeableItem.m_Range = template.GetComponent<PlaceableItem>()?.m_Range ?? 3; //<============================================
		}
	}
}
