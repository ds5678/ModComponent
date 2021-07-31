using HarmonyLib;

namespace ModComponentMapper.CraftingMenu
{
	internal enum ModCraftingCategory
	{
		All,
		FireStarting,
		FirstAid,
		Clothing,
		Tools,
		Materials,
		Food
	}


	internal static class CraftingMenuPatches
	{
		[HarmonyPatch(typeof(Panel_Crafting), "OnCategoryChanged")]
		internal static class Panel_Crafting_OnCategoryChanged
		{
			private static bool Prefix(Panel_Crafting __instance, int index)
			{
				if (index < 0) return false;
				__instance.m_CurrentCategory = (Panel_Crafting.Category)index;
				__instance.ApplyFilter();
				return false;
			}
		}

		[HarmonyPatch(typeof(Panel_Crafting), "ItemPassesFilter")]
		internal static class Panel_Crafting_ItemPassesFilter
		{
			private static bool Prefix(Panel_Crafting __instance, BlueprintItem bpi, ref bool __result)
			{
				__result = MethodReplacements.ItemPassesFilter(__instance, bpi);
				return false;
			}
		}

		[HarmonyPatch(typeof(Panel_Crafting), "HandleInput")]
		internal static class Panel_Crafting_HandleInput
		{
			private static bool Prefix(Panel_Crafting __instance)
			{
				MethodReplacements.HandleInput(__instance);
				return false;
			}
		}

		[HarmonyPatch(typeof(Panel_Crafting), "Start")]
		internal static class Panel_Crafting_Start
		{
			private static void Postfix(Panel_Crafting __instance)
			{
				CraftingCategoryNavigation categoryNavigation = __instance.m_CategoryNavigation;
				var buttonList = categoryNavigation.m_NavigationButtons;
				UIButton toolsButton = buttonList[buttonList.Count - 1];
				UIButton materialButton = toolsButton.Instantiate();
				UIButton foodButton = toolsButton.Instantiate();
				foodButton.name = "Button_Food";
				materialButton.name = "Button_Material";
				EventDelegate.Set(materialButton.onClick, new System.Action(() => categoryNavigation.OnNavigationChanged(materialButton)));
				EventDelegate.Set(foodButton.onClick, new System.Action(() => categoryNavigation.OnNavigationChanged(foodButton)));
				toolsButton.Move(0, -62, 0);
				materialButton.Move(0, -124, 0);
				buttonList.Add(materialButton);
				buttonList.Add(foodButton);

				materialButton.SetSpriteName("ico_crafting");
				foodButton.SetSpriteName("ico_Radial_food");
				buttonList[0].SetSpriteName("ico_Radial_pack");
			}
		}

		//Purely a bugfix
		[HarmonyPatch(typeof(CraftingRequirementQuantitySelect), "Enable")]
		internal static class CraftingRequirementQuantitySelect_Enable
		{
			private static void Postfix(CraftingRequirementQuantitySelect __instance, BlueprintItem bpi)
			{
				float keroseneNeeded = bpi.m_KeroseneLitersRequired;
				float gunpowderNeeded = bpi.m_GunpowderKGRequired;
				if (keroseneNeeded > 0)
				{
					int maxKeroseneUnits = (int)(GameManager.GetPlayerManagerComponent().GetTotalLiters(GearLiquidTypeEnum.Kerosene) / keroseneNeeded);
					if (__instance.m_Maximum > maxKeroseneUnits) __instance.m_Maximum = maxKeroseneUnits;
				}
				if (gunpowderNeeded > 0)
				{
					int maxGunpowderUnits = (int)(GameManager.GetPlayerManagerComponent().GetTotalPowderWeight(GearPowderType.Gunpowder) / gunpowderNeeded);
					if (__instance.m_Maximum > maxGunpowderUnits) __instance.m_Maximum = maxGunpowderUnits;
				}
			}
		}
	}
}
