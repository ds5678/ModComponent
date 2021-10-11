using UnityEngine;

namespace ModComponent.Mapper.CraftingMenu
{
	internal static class MethodReplacements
	{
		public static bool ItemPassesFilter(Panel_Crafting __instance, BlueprintItem bpi)
		{
			switch ((ModCraftingCategory)__instance.m_CurrentCategory)
			{
				case ModCraftingCategory.All:
					return true;
				case ModCraftingCategory.FireStarting:
					return bpi.m_CraftedResult.PassesFilterType(GearTypeEnum.Firestarting);
				case ModCraftingCategory.FirstAid:
					return bpi.m_CraftedResult.PassesFilterType(GearTypeEnum.FirstAid);
				case ModCraftingCategory.Clothing:
					return bpi.m_CraftedResult.PassesFilterType(GearTypeEnum.Clothing);
				case ModCraftingCategory.Tools:
					return bpi.m_CraftedResult.PassesFilterType(GearTypeEnum.Tool) || bpi.m_CraftedResult.PassesFilterType(GearTypeEnum.Other);
				case ModCraftingCategory.Materials:
					return bpi.m_CraftedResult.PassesFilterType(GearTypeEnum.Material);
				case ModCraftingCategory.Food:
					return bpi.m_CraftedResult.PassesFilterType(GearTypeEnum.Food);
				default:
					return false;
			}
		}

		public static void HandleInput(Panel_Crafting __instance)
		{
			if (InputManager.GetEscapePressed(__instance))
			{
				__instance.OnBackButton();
				return;
			}
			float menuMovementVertical;
			if (global::Utils.IsGamepadActive())
			{
				if (InputManager.GetOpenActionsPanelPressed(__instance))
				{
					__instance.OnBackButton();
					return;
				}
				if (InputManager.GetAltFirePressed(__instance))
				{
					__instance.OnInventoryNav();
					return;
				}
				if (InputManager.GetFirePressed(__instance))
				{
					if (InterfaceManager.IsUsingSurvivalTabs())
					{
						__instance.OnJournalNav();
						return;
					}
					__instance.OnMissionNav();
					return;
				}
				else
				{
					if (__instance.m_CurrentNavArea == Panel_Crafting.NavArea.Category)
					{
						if (InputManager.GetContinuePressed(__instance))
						{
							__instance.SetNavigationArea(Panel_Crafting.NavArea.Blueprint);
							return;
						}
					}
					else
					{
						if (InputManager.GetInventoryExaminePressed(__instance))
						{
							__instance.OnBeginCrafting();
							return;
						}
						if (InputManager.GetInventoryFilterLeftPressed(__instance))
						{
							__instance.m_RequirementContainer.OnPrevious();
							return;
						}
						if (InputManager.GetInventoryFilterRightPressed(__instance))
						{
							__instance.m_RequirementContainer.OnNext();
							return;
						}
						if (InputManager.GetInventoryDropPressed(__instance))
						{
							__instance.m_RequirementContainer.HandleNavigation();
							return;
						}
						if (InputManager.GetInventorySortPressed(__instance))
						{
							Panel_Crafting.Filter filter = __instance.m_CurrentFilter + 1;
							if (filter >= Panel_Crafting.Filter.Count)
							{
								filter = Panel_Crafting.Filter.All;
							}
							__instance.SetFilter(filter);
							return;
						}
					}
					menuMovementVertical = global::Utils.GetMenuMovementVertical(__instance, true, true);
				}
			}
			else
			{
				float axisScrollWheel = InputManager.GetAxisScrollWheel(__instance);
				int numBlueprintDisplays = __instance.m_BlueprintDisplays.Count; // 7
				int numFilteredItems = __instance.m_FilteredBlueprintItems.Count; // total number of blueprints in that filtered list
				if (!global::Utils.IsZero(axisScrollWheel, 0.0001f) && numFilteredItems > numBlueprintDisplays)
				{
					int maxChange = Mathf.Clamp(ModComponent.Main.Settings.instance.numCraftingSteps, 1, numBlueprintDisplays);
					int num = __instance.m_CurrentBlueprintDisplayOffset;
					num += ((axisScrollWheel < 0f) ? maxChange : (-maxChange));
					num = Mathf.Clamp(num, 0, numFilteredItems - numBlueprintDisplays);
					if (num != __instance.m_CurrentBlueprintIndex)
					{
						__instance.m_CurrentBlueprintIndex += num - __instance.m_CurrentBlueprintDisplayOffset;
						__instance.m_CurrentBlueprintDisplayOffset = num;
						__instance.RefreshBlueprintDisplayList();
						__instance.RefreshSelectedBlueprint();
					}
					return;
				}
				menuMovementVertical = global::Utils.GetMenuMovementVertical(__instance, true, true);
				if (__instance.m_CurrentNavArea != Panel_Crafting.NavArea.Blueprint)
				{
					__instance.SetNavigationArea(Panel_Crafting.NavArea.Blueprint);
				}
			}
			if (!global::Utils.IsZero(menuMovementVertical, 0.0001f))
			{
				__instance.HandleVerticalNavigation(menuMovementVertical);
			}
		}
	}
}
