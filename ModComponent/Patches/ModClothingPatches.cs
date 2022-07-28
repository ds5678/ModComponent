extern alias Hinterland;
using HarmonyLib;
using Hinterland;
using ModComponent.API.Components;
using ModComponent.Mapper;

namespace ModComponent.Patches;

[HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]//not inlined
internal static class PlayerManager_PutOnClothingItem
{
	private static void Prefix(PlayerManager __instance, GearItem gi, ClothingLayer layerToPutOn)
	{
		if (gi?.m_ClothingItem == null || layerToPutOn == ClothingLayer.NumLayers)
		{
			return;
		}

		ClothingRegion region = gi.m_ClothingItem.m_Region;
		GearItem itemInSlot = __instance.GetClothingInSlot(region, layerToPutOn);
		if (itemInSlot)
		{
			__instance.TakeOffClothingItem(itemInSlot);
		}
	}
	private static void Postfix(GearItem gi)
	{
		ModClothingComponent modClothingComponent = ModComponent.Utils.ComponentUtils.GetComponentSafe<ModClothingComponent>(gi);
		modClothingComponent?.OnPutOn?.Invoke();
	}
}

[HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]//Not inlined
internal static class PlayerManager_TakeOffClothingItem
{
	internal static void Postfix(GearItem gi)
	{
		ModClothingComponent modClothingComponent = ModComponent.Utils.ComponentUtils.GetComponentSafe<ModClothingComponent>(gi);
		modClothingComponent?.OnTakeOff?.Invoke();
	}
}

[HarmonyPatch(typeof(ClothingSlot), "CheckForChangeLayer")]
internal static class ClothingSlot_CheckForChangeLayer
{
	private static bool Prefix(ClothingSlot __instance)
	{
		int defaultDrawLayer = DefaultDrawLayers.GetDefaultDrawLayer(__instance.m_ClothingRegion, __instance.m_ClothingLayer);

		ModClothingComponent clothingComponent = ModComponent.Utils.ComponentUtils.GetComponentSafe<ModClothingComponent>(__instance.m_GearItem);
		if (clothingComponent == null)
		{
			if (__instance.m_GearItem != null)
			{
				__instance.UpdatePaperDollTextureLayer(defaultDrawLayer);
			}
			return true;
		}

		int actualDrawLayer = clothingComponent.DrawLayer > 0 ? clothingComponent.DrawLayer : defaultDrawLayer;
		__instance.UpdatePaperDollTextureLayer(actualDrawLayer);
		return false;
	}
}
