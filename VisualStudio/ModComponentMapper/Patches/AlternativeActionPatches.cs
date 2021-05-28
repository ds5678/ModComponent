using Harmony;
using ModComponentAPI;

namespace ModComponentMapper.Patches
{
	[HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcessInteraction")]
	internal class PlayerManager_InteractiveObjectsProcessInteraction
	{
		internal static void Prefix(PlayerManager __instance)
		{
			AlternativeAction alternativeAction = ModComponentUtils.ComponentUtils.GetComponent<AlternativeAction>(__instance.m_InteractiveObjectUnderCrosshair);
			if (alternativeAction is null) return;

			alternativeAction.ExecutePrimary();
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcessAltFire")]
	internal class PlayerManager_InteractiveObjectsProcessAltFire
	{
		internal static void Prefix(PlayerManager __instance)
		{
			AlternativeAction alternativeAction = ModComponentUtils.ComponentUtils.GetComponent<AlternativeAction>(__instance.m_InteractiveObjectUnderCrosshair);
			if (alternativeAction is null) return;

			alternativeAction.ExecuteSecondary();
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcess")]
	internal class PlayerManager_InteractiveObjectsProcess
	{
		internal static void Postfix(PlayerManager __instance)
		{
			AlternativeAction alternativeAction = ModComponentUtils.ComponentUtils.GetComponent<AlternativeAction>(__instance.m_InteractiveObjectUnderCrosshair);
			if (alternativeAction is null) return;

			if (InputManager.GetKeyDown(InputManager.m_CurrentContext, ModComponentMain.Settings.options.tertiaryKeyCode))
			{
				alternativeAction.ExecuteTertiary();
			}
		}
	}
}
