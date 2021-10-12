using HarmonyLib;
using ModComponent.API.Components;
using ModComponent.Mapper;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Patches
{
	[HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsInternal")]//Not inlined
	internal static class PlayerManager_UnequipItemInHandsInternalPatch
	{
		internal static void Postfix(PlayerManager __instance)
		{
			GearEquipper.Unequip(ComponentUtils.GetEquippableModComponent(__instance.m_ItemInHands));
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsSkipAnimation")]//Not inlined
	internal static class PlayerManager_UnequipItemInHandsSkipAnimation
	{
		internal static void Prefix(PlayerManager __instance)
		{
			GearEquipper.OnUnequipped(ComponentUtils.GetEquippableModComponent(__instance.m_ItemInHands));
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "EquipItem")]//Exists
	internal static class PlayerManager_EquipItem
	{
		internal static void Prefix(PlayerManager __instance, GearItem gi)
		{
			ModBaseEquippableComponent equippable = ComponentUtils.GetEquippableModComponent(__instance.m_ItemInHands);
			if (equippable != null) __instance.UnequipItemInHands();
		}
		internal static void Postfix(PlayerManager __instance)
		{
			GearEquipper.Equip(ComponentUtils.GetEquippableModComponent(__instance.m_ItemInHands));
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "SetControlMode")]//Exists
	internal static class PlayerManagerSetControlModePatch
	{
		private static PlayerControlMode lastMode;

		internal static void Postfix(PlayerManager __instance, PlayerControlMode mode)
		{
			if (mode == lastMode) return;

			lastMode = mode;

			ModBaseEquippableComponent equippable = ComponentUtils.GetEquippableModComponent(__instance.m_ItemInHands);
			equippable?.OnControlModeChangedWhileEquipped?.Invoke();

		}
	}

	[HarmonyPatch(typeof(PlayerManager), "UseInventoryItem")]//Exists
	internal static class PlayerManagerUseInventoryItemPatch
	{
		internal static bool Prefix(PlayerManager __instance, GearItem gi)
		{
			if (ComponentUtils.GetComponentSafe<FirstPersonItem>(gi) != null) return true;

			if (ComponentUtils.GetEquippableModComponent(gi) == null) return true;

			var currentGi = __instance.m_ItemInHands;

			if (currentGi != null) __instance.UnequipItemInHands();

			if (gi != currentGi) __instance.EquipItem(gi, false);

			return false;
		}
	}

	[HarmonyPatch(typeof(InputManager), "ProcessFireAction")]
	internal static class InputManagerProcessFireActionPatch
	{
		public static bool Prefix(MonoBehaviour context)
		{
			PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
			if (playerManager == null || GameManager.ControlsLocked() || InterfaceManager.IsOverlayActiveImmediate() || !InputManager.GetFirePressed(context) || InputManager.GetFireReleased(context))
			{
				return true;
			}

			ModBaseEquippableComponent equippable = ComponentUtils.GetEquippableModComponent(playerManager.m_ItemInHands);
			if (equippable?.Implementation == null) return true;

			equippable.OnPrimaryAction?.Invoke();
			return false;
		}
	}


	[HarmonyPatch(typeof(InputManager), "ExecuteAltFire")]
	internal static class InputManagerExecuteAltFirePatch
	{
		public static bool Prefix()
		{
			PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
			if (playerManager == null || InterfaceManager.IsOverlayActiveImmediate() || playerManager.IsInPlacementMode() || playerManager.ItemInHandsPlaceable())
			{
				return true;
			}

			ModBaseEquippableComponent equippable = ComponentUtils.GetEquippableModComponent(playerManager.m_ItemInHands);
			if (equippable == null) return true;

			equippable.OnSecondaryAction?.Invoke();
			return false;
		}
	}

	[HarmonyPatch(typeof(EquipItemPopup), "AllowedToHideAmmoPopup")]//Exists
	internal static class EquipItemPopup_AllowedToHideAmmoPopup
	{
		internal static void Postfix(ref bool __result)
		{
			if (__result) return;

			__result = ComponentUtils.GetModComponent(GameManager.GetPlayerManagerComponent().m_ItemInHands) != null;
		}
	}

	[HarmonyPatch(typeof(Panel_Loading), "Enable")]//Exists
	internal static class PanelLoadingEnablePatch
	{
		public static void Prefix(bool enable)
		{
			if (enable)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				GearEquipper.Unequip(ComponentUtils.GetEquippableModComponent(playerManager.m_ItemInHands));
			}
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "ItemCanEquipInHands")]//Positive Caller Count
	internal static class PlayerManager_ItemCanEquipInHands
	{
		private static void Postfix(GearItem gi, ref bool __result)
		{
			if (__result || gi == null) return;
			ModBaseEquippableComponent equippable = ComponentUtils.GetEquippableModComponent(gi);
			if (equippable != null) __result = true;
		}
	}
}
