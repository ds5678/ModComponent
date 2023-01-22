﻿using Il2Cpp;
using ModComponent.API.Components;
using UnityEngine;

namespace ModComponent.Mapper.ComponentMappers;

internal static class ToolMapper
{
	internal static void Configure(ModBaseComponent modComponent)
	{
		ModToolComponent modToolComponent = modComponent.TryCast<ModToolComponent>();
		if (modToolComponent == null)
		{
			return;
		}

		ToolsItem toolsItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<ToolsItem>(modToolComponent);

		toolsItem.m_ToolType = ModComponent.Utils.EnumUtils.TranslateEnumValue<ToolsItem.ToolType, ModToolComponent.ToolUsage>(modToolComponent.Usage);
		toolsItem.m_CuttingToolType = ModComponent.Utils.EnumUtils.TranslateEnumValue<ToolsItem.CuttingToolType, ModToolComponent.ToolKind>(modToolComponent.ToolType);

		toolsItem.m_CraftingAndRepairSkillModifier = modToolComponent.SkillBonus;
		toolsItem.m_CraftingAndRepairTimeModifier = modToolComponent.CraftingTimeMultiplier;
		toolsItem.m_DegradePerHourCrafting = modToolComponent.DegradePerHourCrafting;

		toolsItem.m_CanOnlyCraftAndRepairClothes = true;
		toolsItem.m_AppearInStoryOnly = false;

		ConfigureBodyHarvest(modToolComponent);
		ConfigureBreakDown(modToolComponent);
		ConfigureDegradeOnUse(modToolComponent);
		ConfigureForceLock(modToolComponent);
		ConfigureIceFishingHole(modToolComponent);
		ConfigureStruggleBonus(modToolComponent);
	}

	private static void ConfigureBodyHarvest(ModToolComponent modToolComponent)
	{
		if (!modToolComponent.CarcassHarvesting)
		{
			return;
		}

		BodyHarvestItem bodyHarvestItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<BodyHarvestItem>(modToolComponent);
		bodyHarvestItem.m_HarvestMeatMinutesPerKG = modToolComponent.MinutesPerKgMeat;
		bodyHarvestItem.m_HarvestFrozenMeatMinutesPerKG = modToolComponent.MinutesPerKgFrozenMeat;
		bodyHarvestItem.m_HarvestGutMinutesPerUnit = modToolComponent.MinutesPerGut;
		bodyHarvestItem.m_HarvestHideMinutesPerUnit = modToolComponent.MinutesPerHide;
		bodyHarvestItem.m_HPDecreasePerHourUse = modToolComponent.DegradePerHourHarvesting;
	}

	private static void ConfigureBreakDown(ModToolComponent modToolComponent)
	{
		BreakDownItem breakDownItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<BreakDownItem>(modToolComponent);
		breakDownItem.m_BreakDownTimeModifier = modToolComponent.BreakDownTimeMultiplier;

		string? templateName = GetTemplateToolName(modToolComponent);
		if (templateName != null)
		{
			AlternativeToolManager.AddToList(modToolComponent, templateName);
		}
	}

	private static void ConfigureDegradeOnUse(ModToolComponent modToolComponent)
	{
		DegradeOnUse degradeOnUse = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<DegradeOnUse>(modToolComponent);
		degradeOnUse.m_DegradeHP = Mathf.Max(degradeOnUse.m_DegradeHP, modToolComponent.DegradeOnUse);
	}

	private static void ConfigureForceLock(ModToolComponent modToolComponent)
	{
		if (!modToolComponent.ForceLocks)
		{
			return;
		}

		ForceLockItem forceLockItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<ForceLockItem>(modToolComponent);
		forceLockItem.m_ForceLockAudio = ModComponent.Utils.ModUtils.DefaultIfEmpty(modToolComponent.ForceLockAudio, "PLAY_LOCKERPRYOPEN1");
		forceLockItem.m_LocalizedProgressText = new LocalizedString() { m_LocalizationID = "GAMEPLAY_Forcing" };

		AlternativeToolManager.AddToList(modToolComponent, "GEAR_Prybar");
	}

	private static void ConfigureIceFishingHole(ModToolComponent modToolComponent)
	{
		if (!modToolComponent.IceFishingHole)
		{
			return;
		}

		IceFishingHoleClearItem iceFishingHoleClearItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<IceFishingHoleClearItem>(modToolComponent);
		iceFishingHoleClearItem.m_BreakIceAudio = ModComponent.Utils.ModUtils.DefaultIfEmpty(modToolComponent.IceFishingHoleAudio, "Play_IceBreakingChopping");
		iceFishingHoleClearItem.m_HPDecreaseToClear = modToolComponent.IceFishingHoleDegradeOnUse;
		iceFishingHoleClearItem.m_NumGameMinutesToClear = modToolComponent.IceFishingHoleMinutes;
	}

	private static void ConfigureStruggleBonus(ModToolComponent modToolComponent)
	{
		if (!modToolComponent.StruggleBonus)
		{
			return;
		}

		StruggleBonus struggleBonus = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<StruggleBonus>(modToolComponent);
		struggleBonus.m_BleedoutMinutesScale = modToolComponent.BleedoutMultiplier;
		struggleBonus.m_CanPuncture = modToolComponent.CanPuncture;
		struggleBonus.m_DamageScalePercent = modToolComponent.DamageMultiplier;
		struggleBonus.m_FleeChanceScale = modToolComponent.FleeChanceMultiplier;
		struggleBonus.m_TapIncrementScale = modToolComponent.TapMultiplier;
		struggleBonus.m_StruggleWeaponType = GetStruggleWeaponType(modToolComponent);
	}

	private static StruggleBonus.StruggleWeaponType GetStruggleWeaponType(ModToolComponent modToolComponent)
	{
		return modToolComponent.ToolType switch
		{
			ModToolComponent.ToolKind.Hatchet => StruggleBonus.StruggleWeaponType.Hatchet,
			ModToolComponent.ToolKind.Hammer => StruggleBonus.StruggleWeaponType.Hammer,
			ModToolComponent.ToolKind.Knife => StruggleBonus.StruggleWeaponType.Knife,
			_ => StruggleBonus.StruggleWeaponType.BareHands,
		};
	}

	private static string? GetTemplateToolName(ModToolComponent modToolComponent)
	{
		return modToolComponent.ToolType switch
		{
			ModToolComponent.ToolKind.HackSaw => "GEAR_Hacksaw",
			ModToolComponent.ToolKind.Hatchet => "GEAR_Hatchet",
			ModToolComponent.ToolKind.Hammer => "GEAR_Hammer",
			ModToolComponent.ToolKind.Knife => "GEAR_Knife",
			_ => null,
		};
	}
}