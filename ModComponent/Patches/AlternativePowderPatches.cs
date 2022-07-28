extern alias Hinterland;
using HarmonyLib;
using Hinterland;
using ModComponent.API.Components;

namespace ModComponent.Patches
{
	internal static class AlternativePowderPatches
	{
		[HarmonyPatch(typeof(PlayerManager), "AddPowderToInventory")]
		private static class PlayerManager_AddPowderToInventory
		{
			const string saltPrefabName = "GEAR_ModSalt";
			const string yeastPrefabName = "GEAR_ModYeast";

			private static bool Prefix(PlayerManager __instance, float amount, GearPowderType type)
			{
				ModPowderComponent.ModPowderType modPowderType = ModPowderComponent.GetPowderType(type);
				if (modPowderType == ModPowderComponent.ModPowderType.Gunpowder) return true;

				float num = amount;
				foreach (GearItemObject gearItemObject in GameManager.GetInventoryComponent().m_Items)
				{
					GearItem gearItem = gearItemObject;
					if (gearItem && gearItem.m_PowderItem && gearItem.m_PowderItem.m_Type == type)
					{
						num = gearItem.m_PowderItem.Add(num);
					}
				}

				string prefabName = null;
				if (modPowderType == ModPowderComponent.ModPowderType.Salt) prefabName = saltPrefabName;
				else if (modPowderType == ModPowderComponent.ModPowderType.Yeast) prefabName = yeastPrefabName;

				if (!Hinterland::Utils.IsZero(num, 0.0001f) && !string.IsNullOrEmpty(prefabName))
				{
					while (num > 0f)
					{
						GearItem gearItem2 = __instance.InstantiateItemInPlayerInventory(prefabName, 1);
						if (gearItem2 && gearItem2.m_PowderItem && gearItem2.m_PowderItem.m_Type == type)
						{
							gearItem2.m_PowderItem.m_WeightKG = 0f;
							num = gearItem2.m_PowderItem.Add(num);
						}
						else
						{
							if (gearItem2)
							{
								GameManager.GetInventoryComponent().DestroyGear(gearItem2.gameObject);
							}
							return false;
						}
					}
				}
				return false;
			}
		}
	}
}
