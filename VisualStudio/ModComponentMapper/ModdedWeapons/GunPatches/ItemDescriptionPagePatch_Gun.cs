using Harmony;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(ItemDescriptionPage), "CanExamine")]//positive caller count
	class ItemDescriptionPageCanExaminePatch
	{
		public static void Postfix(GearItem gi, ref bool __result)
		{
			// guns can always be examined
			__result |= ModComponentUtils.ComponentUtils.GetComponent<GunItem>(gi) != null;
		}
	}
}
