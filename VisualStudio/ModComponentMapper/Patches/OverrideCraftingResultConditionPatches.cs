using HarmonyLib;

namespace ModComponentMapper.Patches
{
	internal static class WatchHandleCraftingSuccess
	{
		internal static bool isExecuting = false;
	}

	[HarmonyPatch(typeof(Panel_Crafting), "HandleCraftingSuccess")]
	internal static class Panel_Crafting_CraftingEnd
	{
		private static void Prefix()
		{
			WatchHandleCraftingSuccess.isExecuting = true;
		}
		private static void Postfix()
		{
			WatchHandleCraftingSuccess.isExecuting = false;
		}
	}
	[HarmonyPatch(typeof(PlayerManager), "InstantiateItemInPlayerInventoryInternal")]
	internal static class PlayerManager_InstantiateItemInPlayerInventory
	{
		private static void Postfix(ref GearItem __result, float condition)
		{
			if (WatchHandleCraftingSuccess.isExecuting && condition < 0)
			{
				__result.m_CurrentHP = __result.m_MaxHP;
			}
		}
	}
}
