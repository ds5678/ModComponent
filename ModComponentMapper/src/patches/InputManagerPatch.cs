using Harmony;

using ModComponentAPI;

namespace ModComponentMapper
{
    
    [HarmonyPatch(typeof(InputManager), "ProcessFireAction")]
    class InputManagerProcessFireActionPatch
    {
        public static bool Prefix()
        {
            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
            if (playerManager == null || InterfaceManager.IsOverlayActive() || !InputManager.GetFirePressed() || InputManager.GetFireReleased())
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
            if (playerManager == null || InterfaceManager.IsOverlayActive() || !InputManager.GetAltFirePressed() || InputManager.GetAltFireReleased() || playerManager.InPlaceMeshMode() || playerManager.ItemInHandsPlaceable())
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
