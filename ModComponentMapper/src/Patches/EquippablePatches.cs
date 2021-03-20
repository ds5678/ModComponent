using Harmony;
using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsInternal")]//Not inlined
    internal class PlayerManager_UnequipItemInHandsInternalPatch
    {
        internal static void Postfix(PlayerManager __instance)
        {
            GearEquipper.Unequip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsSkipAnimation")]//Not inlined
    internal class PlayerManager_UnequipItemInHandsSkipAnimation
    {
        internal static void Prefix(PlayerManager __instance)
        {
            GearEquipper.OnUnequipped(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "EquipItem")]//Exists
    internal class PlayerManager_EquipItem
    {
        internal static void Prefix(PlayerManager __instance, GearItem gi)
        {
            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(__instance.m_ItemInHands);
            if (equippable != null)
            {
                __instance.UnequipItemInHands();
            }
        }
        internal static void Postfix(PlayerManager __instance)
        {
            //Logger.Log("equip item");
            GearEquipper.Equip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "SetControlMode")]//Exists
    internal class PlayerManagerSetControlModePatch
    {
        private static PlayerControlMode lastMode;

        internal static void Postfix(PlayerManager __instance, PlayerControlMode mode)
        {
            if (mode == lastMode)
            {
                return;
            }

            lastMode = mode;

            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(__instance.m_ItemInHands);
            equippable?.OnControlModeChangedWhileEquipped?.Invoke();

        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UseInventoryItem")]//Exists
    internal class PlayerManagerUseInventoryItemPatch
    {
        internal static bool Prefix(PlayerManager __instance, GearItem gi)
        {
            if (ModUtils.GetComponent<FirstPersonItem>(gi) != null)
            {
                return true;
            }

            if (ModUtils.GetEquippableModComponent(gi) == null)
            {
                return true;
            }

            var currentGi = __instance.m_ItemInHands;
            if (currentGi != null)
            {
                __instance.UnequipItemInHands();
            }

            if (gi != currentGi)
            {
                __instance.EquipItem(gi, false);
            }

            return false;
        }
    }

    [HarmonyPatch(typeof(InputManager), "ProcessFireAction")]
    class InputManagerProcessFireActionPatch
    {
        public static bool Prefix(MonoBehaviour context)
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            if (playerManager == null || GameManager.ControlsLocked() || InterfaceManager.IsOverlayActiveImmediate() || !InputManager.GetFirePressed(context) || InputManager.GetFireReleased(context))
            {
                return true;
            }

            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(playerManager.m_ItemInHands);
            if (equippable == null || equippable.Implementation == null)
            {
                return true;
            }

            equippable.OnPrimaryAction?.Invoke();
            return false;
        }
    }


    [HarmonyPatch(typeof(InputManager), "ExecuteAltFire")]
    class InputManagerExecuteAltFirePatch
    {
        public static bool Prefix()
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            if (playerManager == null || InterfaceManager.IsOverlayActiveImmediate() || playerManager.IsInPlacementMode() || playerManager.ItemInHandsPlaceable())
            {
                return true;
            }

            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(playerManager.m_ItemInHands);
            if (equippable == null)
            {
                return true;
            }

            equippable.OnSecondaryAction?.Invoke();
            return false;
        }
    }

    [HarmonyPatch(typeof(EquipItemPopup), "AllowedToHideAmmoPopup")]//Exists
    internal class EquipItemPopup_AllowedToHideAmmoPopup
    {
        internal static void Postfix(ref bool __result)
        {
            if (__result)
            {
                return;
            }

            __result = ModUtils.GetModComponent(GameManager.GetPlayerManagerComponent().m_ItemInHands) != null;
        }
    }

    [HarmonyPatch(typeof(Panel_Loading), "Enable")]//Exists
    internal class PanelLoadingEnablePatch
    {
        public static void Prefix(bool enable)
        {
            if (enable)
            {
                PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
                GearEquipper.Unequip(ModUtils.GetComponent<EquippableModComponent>(playerManager.m_ItemInHands));
            }
        }
    }
}
