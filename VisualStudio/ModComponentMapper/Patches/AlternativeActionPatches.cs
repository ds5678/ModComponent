using Harmony;
using ModComponentAPI;

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcessAltFire")]//Exists
    internal class PlayerManager_InteractiveObjectsProcessAltFire
    {
        internal static bool Prefix(PlayerManager __instance)
        {
            AlternativeAction alternativeAction = ComponentUtils.GetComponent<AlternativeAction>(__instance.m_InteractiveObjectUnderCrosshair);
            if (alternativeAction == null)
            {
                return true;
            }

            alternativeAction.Execute();
            return false;
        }
    }
}
