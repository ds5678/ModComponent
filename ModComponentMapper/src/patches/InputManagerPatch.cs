using Harmony;
using UnityEngine;
using ModComponentAPI;

/*did a first pass through
 * HAD ISSUES which I think I fixed
 * I think IsOverlayActive => IsOverlayActiveImmediate
 * I think InPlaceMeshMode => either IsInPlacementMode or IsInMeshPlacementMode
 * does not need to be declared
 */
//might have inlined methods
namespace ModComponentMapper
{
    
    [HarmonyPatch(typeof(InputManager), "ProcessFireAction")]//inlined?
    class InputManagerProcessFireActionPatch
    {
        public static bool Prefix(MonoBehaviour context)
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            //if (playerManager == null || GameManager.ControlsLocked() || InterfaceManager.IsOverlayActive() || !InputManager.GetFirePressed(context) || InputManager.GetFireReleased(context))
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


    [HarmonyPatch(typeof(InputManager), "ExecuteAltFire")]//inlined?
    class InputManagerExecuteAltFirePatch
    {
        public static bool Prefix()
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            //if (playerManager == null || InterfaceManager.IsOverlayActive() || playerManager.InPlaceMeshMode() || playerManager.ItemInHandsPlaceable())
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
}
