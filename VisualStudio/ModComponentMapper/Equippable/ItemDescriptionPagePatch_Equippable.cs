using HarmonyLib;
using ModComponentAPI;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(ItemDescriptionPage), "GetEquipButtonLocalizationId")]//positive caller count
	internal static class ItemDescriptionPageGetEquipButtonLocalizationIdPatch
	{
		public static void Postfix(GearItem gi, ref string __result)
		{
			if (__result != string.Empty) return;

			ModComponent modComponent = ModComponentUtils.ComponentUtils.GetModComponent(gi);
			if (modComponent != null)
			{
				//Strangely, this is what allows items to be equipped
				__result = modComponent.InventoryActionLocalizationId;
			}
		}
	}
}
