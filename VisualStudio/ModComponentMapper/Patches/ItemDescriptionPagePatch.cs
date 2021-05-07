using Harmony;
using ModComponentAPI;

namespace ModComponentMapper.patches
{
	[HarmonyPatch(typeof(ItemDescriptionPage), "GetEquipButtonLocalizationId")]//positive caller count
	class ItemDescriptionPageGetEquipButtonLocalizationIdPatch
	{
		public static void Postfix(GearItem gi, ref string __result)
		{
			if (__result != string.Empty) return;

			ModComponent modComponent = ComponentUtils.GetModComponent(gi);
			if (modComponent != null)
			{
				//Strangely, this is what allows items to be equipped
				__result = modComponent.InventoryActionLocalizationId;
			}
		}
	}

	[HarmonyPatch(typeof(ItemDescriptionPage), "CanExamine")]//positive caller count
	class ItemDescriptionPageCanExaminePatch
	{
		public static void Postfix(GearItem gi, ref bool __result)
		{
			// guns can always be examined
			__result |= ComponentUtils.GetComponent<GunItem>(gi) != null;
		}
	}
}
