using Harmony;
using ModComponentAPI;

namespace ModComponentMapper.patches
{
	static class IntimidationBuffHelper
	{
		internal static void UpdateWolfIntimidationBuff()
		{
			float increaseFlee = 0;
			float decreaseAttack = 0;

			PlayerManager playerManager = GameManager.GetPlayerManagerComponent();

			for (int region = 0; region < (int)ClothingRegion.NumRegions; region++)
			{
				for (int layer = 0; layer < (int)ClothingLayer.NumLayers; layer++)
				{
					GearItem clothing = playerManager.GetClothingInSlot((ClothingRegion)region, (ClothingLayer)layer);

					if (clothing && clothing.m_WolfIntimidationBuff)
					{
						decreaseAttack += clothing.m_WolfIntimidationBuff.m_DecreaseAttackChancePercentagePoints;
						increaseFlee += clothing.m_WolfIntimidationBuff.m_IncreaseFleePercentagePoints;
					}
				}
			}

			playerManager.ApplyWolfIntimidationBuff(increaseFlee, decreaseAttack);
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]//not inlined
	internal class PlayerManager_PutOnClothingItem
	{
		private static void Prefix(PlayerManager __instance, GearItem gi, ClothingLayer layerToPutOn)
		{
			//Logger.Log("Clothing Layer {0}", layerToPutOn);
			if (gi?.m_ClothingItem == null || layerToPutOn == ClothingLayer.NumLayers) return;
			ClothingRegion region = gi.m_ClothingItem.m_Region;
			GearItem itemInSlot = __instance.GetClothingInSlot(region, layerToPutOn);
			if (itemInSlot) __instance.TakeOffClothingItem(itemInSlot);
		}
		private static void Postfix(GearItem gi)
		{
			ModClothingComponent modClothingComponent = ComponentUtils.GetComponent<ModClothingComponent>(gi);
			modClothingComponent?.OnPutOn?.Invoke();
			IntimidationBuffHelper.UpdateWolfIntimidationBuff();
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]//Not inlined
	internal class PlayerManager_TakeOffClothingItem
	{
		internal static void Postfix(GearItem gi)
		{
			ModClothingComponent modClothingComponent = ComponentUtils.GetComponent<ModClothingComponent>(gi);
			modClothingComponent?.OnTakeOff?.Invoke();
			IntimidationBuffHelper.UpdateWolfIntimidationBuff();
		}
	}

	[HarmonyPatch(typeof(ClothingSlot), "CheckForChangeLayer")]
	internal class ClothingSlot_CheckForChangeLayer
	{
		private static bool Prefix(ClothingSlot __instance)
		{
			ModClothingComponent clothingComponent = ComponentUtils.GetComponent<ModClothingComponent>(__instance.m_GearItem);
			if (clothingComponent == null)
			{
				if (__instance.m_GearItem != null)
				{
					int defaultDrawLayer = DefaultDrawLayers.GetDefaultDrawLayer(__instance.m_ClothingRegion, __instance.m_ClothingLayer);
					__instance.UpdatePaperDollTextureLayer(defaultDrawLayer);
				}
				return true;
			}

			int actualDrawLayer = clothingComponent.DrawLayer;
			__instance.UpdatePaperDollTextureLayer(actualDrawLayer);
			//Logger.Log("Set the draw layer for '{0}' to {1}", __instance.m_GearItem.name, actualDrawLayer);
			return false;
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "UpdateBuffDurations")]
	internal static class PlayerManager_UpdateBuffDurations
	{
		internal static void Postfix(PlayerManager __instance)
		{
			__instance.RemoveWolfIntimidationBuff();
		}
	}

	//TRANSPILER!!!!!!! <===========================================================================================================
	//Seems to be removing the wolf intimidation buff calculation
	/*[HarmonyPatch(typeof(PlayerManager), "UpdateBuffDurations")]//Exists
    internal class PlayerManager_UpdateBuffDurations
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                MethodInfo methodInfo = codes[i].operand as MethodInfo;
                if (methodInfo != null && methodInfo.Name == "GetWornWolfIntimidationClothing" && methodInfo.DeclaringType == typeof(PlayerManager))
                {
                    for (int j = 0; j < 17; j++)
                    {
                        codes.RemoveAt(i - 1);
                    }
                }
            }

            return codes;
        }
    }
    */
}
