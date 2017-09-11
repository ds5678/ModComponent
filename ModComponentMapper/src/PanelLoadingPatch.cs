using Harmony;

using ModComponentAPI;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Panel_Loading), "Enable")]
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
