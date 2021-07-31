using HarmonyLib;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(ItemDescriptionPage), "CanExamine")]
	internal static class ItemDescriptionPageCanExaminePatch
	{
		public static void Postfix(GearItem gi, ref bool __result)
		{
			__result |= ModComponentUtils.ComponentUtils.GetComponent<GunItem>(gi) != null;
		}
	}
}
