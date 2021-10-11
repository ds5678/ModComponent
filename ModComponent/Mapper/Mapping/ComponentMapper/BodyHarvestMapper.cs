using ModComponent.API;
using ModComponent.API.Components;
using UnityEngine;

namespace ModComponent.Mapper.ComponentMapper
{
	internal static class BodyHarvestMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModBodyHarvestComponent modBodyHarvestComponent = modComponent.TryCast<ModBodyHarvestComponent>();
			if (modBodyHarvestComponent == null) return;

			BodyHarvest bodyHarvest = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<BodyHarvest>(modBodyHarvestComponent);
			bodyHarvest.m_AllowDecay = false;
			bodyHarvest.m_CanCarry = modBodyHarvestComponent.CanCarry;
			bodyHarvest.m_CanQuarter = modBodyHarvestComponent.CanQuarter;
			bodyHarvest.m_DamageSide = BaseAi.DamageSide.DamageSideNone;
			bodyHarvest.m_GutAvailableUnits = modBodyHarvestComponent.GutQuantity;
			bodyHarvest.m_GutPrefab = Resources.Load(modBodyHarvestComponent.GutPrefab)?.Cast<GameObject>();
			bodyHarvest.m_GutWeightKgPerUnit = modBodyHarvestComponent.GutWeightKgPerUnit;
			bodyHarvest.m_HarvestAudio = modBodyHarvestComponent.HarvestAudio;
			bodyHarvest.m_HideAvailableUnits = modBodyHarvestComponent.HideQuantity;
			bodyHarvest.m_HidePrefab = Resources.Load(modBodyHarvestComponent.HidePrefab)?.Cast<GameObject>();
			bodyHarvest.m_HideWeightKgPerUnit = modBodyHarvestComponent.HideWeightKgPerUnit;
			bodyHarvest.m_IsBigCarry = false;
			bodyHarvest.m_LocalizedDisplayName = ModComponent.Utils.NameUtils.CreateLocalizedString(modBodyHarvestComponent.DisplayNameLocalizationId);
			bodyHarvest.m_MeatAvailableMaxKG = modBodyHarvestComponent.MeatAvailableMaxKG;
			bodyHarvest.m_MeatAvailableMinKG = modBodyHarvestComponent.MeatAvailableMinKG;
			bodyHarvest.m_MeatPrefab = Resources.Load(modBodyHarvestComponent.MeatPrefab)?.Cast<GameObject>();
			bodyHarvest.m_PercentFrozen = 0f;
			bodyHarvest.m_QuarterAudio = modBodyHarvestComponent.QuarterAudio;
			bodyHarvest.m_QuarterBagMeatCapacityKG = modBodyHarvestComponent.QuarterBagMeatCapacityKG;
			bodyHarvest.m_QuarterBagWasteMultiplier = modBodyHarvestComponent.QuarterBagWasteMultiplier;
			bodyHarvest.m_QuarterDurationMinutes = modBodyHarvestComponent.QuarterDurationMinutes;
			bodyHarvest.m_QuarterObjectPrefab = Resources.Load(modBodyHarvestComponent.QuarterObjectPrefab)?.Cast<GameObject>();
			bodyHarvest.m_QuarterPrefabSpawnAngle = modBodyHarvestComponent.QuarterPrefabSpawnAngle;
			bodyHarvest.m_QuarterPrefabSpawnRadius = modBodyHarvestComponent.QuarterPrefabSpawnRadius;
			bodyHarvest.m_Ravaged = false;
			bodyHarvest.m_StartFrozen = false;
			bodyHarvest.m_StartRavaged = false;
			bodyHarvest.m_StartConditionMax = 100f;
			bodyHarvest.m_StartConditionMin = 95f;
		}
	}
}
