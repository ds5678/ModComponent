using Harmony;
using UnityEngine;

using ModComponentAPI;

namespace ModComponentMapper
{
    
    [HarmonyPatch(typeof(InputManager), "ProcessFireAction")]
    class InputManagerProcessFireActionPatch
    {
        public static bool Prefix(MonoBehaviour context)
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            if (playerManager == null || GameManager.ControlsLocked() || InterfaceManager.IsOverlayActive() || !InputManager.GetFirePressed(context) || InputManager.GetFireReleased(context))
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
            if (playerManager == null || InterfaceManager.IsOverlayActive() || playerManager.InPlaceMeshMode() || playerManager.ItemInHandsPlaceable())
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
}
