﻿using Il2Cpp;
using ModComponent.API.Components;
using System;
using UnityEngine;

namespace ModComponent.Mapper.ComponentMappers;

internal static class CookableMapper
{
	internal static void Configure(ModBaseComponent modComponent)
	{
		ModCookableComponent modCookableComponent = modComponent.TryCast<ModCookableComponent>();
		if (modCookableComponent == null || !modCookableComponent.Cooking)
		{
			return;
		}

		Cookable cookable = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<Cookable>(modCookableComponent);

		cookable.m_CookableType = ModComponent.Utils.EnumUtils.TranslateEnumValue<Cookable.CookableType, ModCookableComponent.CookableType>(modCookableComponent.Type);
		cookable.m_CookTimeMinutes = modCookableComponent.CookingMinutes;
		cookable.m_ReadyTimeMinutes = modCookableComponent.BurntMinutes;
		cookable.m_NumUnitsRequired = modCookableComponent.CookingUnitsRequired;
		cookable.m_PotableWaterRequiredLiters = modCookableComponent.CookingWaterRequired;
		cookable.m_WarmUpNearFireRange = 1.5f;

		cookable.m_CookAudio = ModComponent.Utils.ModUtils.DefaultIfEmpty(modCookableComponent.CookingAudio, GetDefaultCookAudio(modCookableComponent));
		cookable.m_PutInPotAudio = ModComponent.Utils.ModUtils.DefaultIfEmpty(modCookableComponent.StartCookingAudio, GetDefaultStartCookingAudio(modCookableComponent));

		Cookable template = ModComponent.Utils.ComponentUtils.GetComponentSafe<Cookable>(Resources.Load<GameObject>("GEAR_PinnacleCanPeaches"));
		cookable.m_MeshPotStyle = template?.m_MeshPotStyle;
		cookable.m_MeshCanStyle = template?.m_MeshCanStyle;
		cookable.m_LiquidMeshRenderer = template?.m_LiquidMeshRenderer;

		// either just heat or convert, but not both
		if (modCookableComponent.CookingResult == null)
		{
			// no conversion, just heating
			FoodItem foodItem = ModComponent.Utils.ComponentUtils.GetComponentSafe<FoodItem>(modCookableComponent);
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
				AutoMapper.MapModComponent(modCookableComponent.CookingResult);
				cookedGearItem = modCookableComponent.CookingResult.GetComponent<GearItem>();
			}

			cookable.m_CookedPrefab = cookedGearItem ?? throw new ArgumentException("CookingResult does not map to GearItem for prefab " + modCookableComponent.name);
		}
	}

	private static string GetDefaultCookAudio(ModCookableComponent modCookableComponent)
	{
		return modCookableComponent.Type switch
		{
			ModCookableComponent.CookableType.Grub => "Play_BoilingLiquidThickHeavy",
			ModCookableComponent.CookableType.Meat => "Play_FryingHeavy",
			_ => "Play_BoilingLiquidLight",
		};
	}

	private static string GetDefaultStartCookingAudio(ModCookableComponent modCookableComponent)
	{
		return modCookableComponent.Type switch
		{
			ModCookableComponent.CookableType.Grub => "Play_AddSlopToPot",
			ModCookableComponent.CookableType.Meat => "Play_AddMeatPan",
			_ => "Play_AddWaterToPot",
		};
	}
}