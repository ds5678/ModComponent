extern alias Hinterland;
using HarmonyLib;
using Hinterland;
using ModComponent.API.Components;

namespace ModComponent.Patches;

[HarmonyPatch(typeof(ItemDescriptionPage), "GetEquipButtonLocalizationId")]//positive caller count
internal static class ItemDescriptionPageGetEquipButtonLocalizationIdPatch
{
	public static void Postfix(GearItem gi, ref string __result)
	{
		if (!string.IsNullOrEmpty(__result))
		{
			return;
		}

		ModBaseComponent modComponent = ModComponent.Utils.ComponentUtils.GetModComponent(gi);
		if (modComponent != null)
		{
			//Strangely, this is what allows items to be equipped
			__result = modComponent.InventoryActionLocalizationId;
		}
	}
}
