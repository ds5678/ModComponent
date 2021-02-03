using Harmony;
using ModComponentAPI;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
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
