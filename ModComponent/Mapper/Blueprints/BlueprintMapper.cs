using ModComponentAPI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponentMapper
{
	internal static class BlueprintMapper
	{
		private static List<ModBlueprint> blueprints = new List<ModBlueprint>();
		internal static void MapBlueprint(ModBlueprint modBlueprint)
		{
			BlueprintItem bpItem = GameManager.GetBlueprints().AddComponent<BlueprintItem>();
			if (bpItem == null)
			{
				throw new Exception("Error creating Blueprint");
			}

			bpItem.m_DurationMinutes = modBlueprint.DurationMinutes;
			bpItem.m_CraftingAudio = modBlueprint.CraftingAudio;

			bpItem.m_RequiredCraftingLocation = ModComponent.Utils.EnumUtils.TranslateEnumValue<CraftingLocation, ModComponentAPI.CraftingLocation>(modBlueprint.RequiredCraftingLocation);
			bpItem.m_RequiresLitFire = modBlueprint.RequiresLitFire;
			bpItem.m_RequiresLight = modBlueprint.RequiresLight;

			bpItem.m_Locked = false;
			bpItem.m_AppearsInStoryOnly = false;

			bpItem.m_CraftedResultCount = modBlueprint.CraftedResultCount;
			bpItem.m_CraftedResult = ModComponent.Utils.ModUtils.GetItem<GearItem>(modBlueprint.CraftedResult);

			if (!string.IsNullOrEmpty(modBlueprint.RequiredTool))
			{
				bpItem.m_RequiredTool = ModComponent.Utils.ModUtils.GetItem<ToolsItem>(modBlueprint.RequiredTool);
			}
			bpItem.m_OptionalTools = ModComponent.Utils.ModUtils.NotNull(ModComponent.Utils.ModUtils.GetMatchingItems<ToolsItem>(modBlueprint.OptionalTools));

			bpItem.m_RequiredGear = ModComponent.Utils.ModUtils.NotNull(ModComponent.Utils.ModUtils.GetMatchingItems<GearItem>(modBlueprint.RequiredGear));
			bpItem.m_RequiredGearUnits = modBlueprint.RequiredGearUnits;
			bpItem.m_KeroseneLitersRequired = modBlueprint.KeroseneLitersRequired;
			bpItem.m_GunpowderKGRequired = modBlueprint.GunpowderKGRequired;

			bpItem.m_AppliedSkill = ModComponent.Utils.EnumUtils.TranslateEnumValue<SkillType, ModComponentAPI.SkillType>(modBlueprint.AppliedSkill);
			bpItem.m_ImprovedSkill = ModComponent.Utils.EnumUtils.TranslateEnumValue<SkillType, ModComponentAPI.SkillType>(modBlueprint.ImprovedSkill);
		}

		internal static void MapBlueprints()
		{
			GameObject blueprintsManager = GameManager.GetBlueprints();
			if (blueprintsManager == null) return;

			foreach (ModBlueprint modBlueprint in blueprints)
			{
				MapBlueprint(modBlueprint);
			}
		}

		internal static void RegisterBlueprint(ModBlueprint modBlueprint, string sourcePath)
		{
			ValidateBlueprint(modBlueprint, sourcePath);

			blueprints.Add(modBlueprint);
		}

		internal static void ValidateBlueprint(ModBlueprint modBlueprint, string sourcePath)
		{
			try
			{
				ModComponent.Utils.ModUtils.GetItem<GearItem>(modBlueprint.CraftedResult);

				if (!string.IsNullOrEmpty(modBlueprint.RequiredTool))
				{
					ModComponent.Utils.ModUtils.GetItem<ToolsItem>(modBlueprint.RequiredTool);
				}

				if (modBlueprint.OptionalTools != null)
				{
					ModComponent.Utils.ModUtils.GetMatchingItems<ToolsItem>(modBlueprint.OptionalTools);
				}

				ModComponent.Utils.ModUtils.GetMatchingItems<GearItem>(modBlueprint.RequiredGear);

			}
			catch (Exception e)
			{
				if (string.IsNullOrEmpty(sourcePath)) throw new ArgumentException("Validation of blueprint failed: " + e.Message + "\nThe blueprint was provided by '" + sourcePath + "', which may be out-of-date or installed incorrectly.");
				else throw new ArgumentException("Validation of blueprint failed: " + e.Message + "\nThe blueprint may be out-of-date or installed incorrectly.");
			}
		}
	}
}
