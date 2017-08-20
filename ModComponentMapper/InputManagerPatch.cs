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

            ModComponent modComponent = ModUtils.GetModComponent(playerManager.m_ItemInHands);
            if (modComponent == null)
            {
                return true;
            }

            modComponent.OnPrimaryAction?.Invoke();
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

            GearItem gi = playerManager.m_ItemInHands;
            if (gi == null)
            {
                return true;
            }

            ModComponent modComponent = ModUtils.GetModComponent(playerManager.m_ItemInHands);
            if (modComponent == null)
            {
                return true;
            }

            modComponent.OnSecondaryAction?.Invoke();
            return false;
        }
    }
}
