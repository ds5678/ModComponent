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
                ModToolComponent modToolComponent = ModUtils.GetModComponent<ModToolComponent>(playerManager.m_ItemInHands);
                if (modToolComponent == null)
                {
                    return;
                }

                modToolComponent.OnUnequipped?.Invoke();
            }
        }
    }
}
