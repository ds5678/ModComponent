using System;

namespace ModComponent.Utils
{
	public static class EquipItemPopupUtils
	{
		public static void ShowItemPopups(String primaryAction, String secondaryAction, bool showAmmo, bool showReload, bool showHolster)
		{
			EquipItemPopup equipItemPopup = InterfaceManager.m_Panel_HUD.m_EquipItemPopup;
			ShowItemIcons(equipItemPopup, primaryAction, secondaryAction, showAmmo);
			equipItemPopup.OnOverlappingDecalChange(true);

			if (Utils.IsGamepadActive())
			{
				//Logger.Log("Gamepad active");
				equipItemPopup.m_ButtonPromptFire.ShowPromptForKey(primaryAction, "Fire");
				MaybeRepositionFireButtonPrompt(equipItemPopup, secondaryAction);
				equipItemPopup.m_ButtonPromptAltFire.ShowPromptForKey(secondaryAction, "AltFire");
				MaybeRepositionAltFireButtonPrompt(equipItemPopup, primaryAction);
			}
			else
			{
				//Logger.Log("Gamepad not active");
				equipItemPopup.m_ButtonPromptFire.ShowPromptForKey(secondaryAction, "AltFire");
				MaybeRepositionFireButtonPrompt(equipItemPopup, primaryAction);
				equipItemPopup.m_ButtonPromptAltFire.ShowPromptForKey(primaryAction, "Interact");
				MaybeRepositionAltFireButtonPrompt(equipItemPopup, secondaryAction);
			}

			string reloadText = showReload ? Localization.Get("GAMEPLAY_Reload") : string.Empty;
			equipItemPopup.m_ButtonPromptReload.ShowPromptForKey(reloadText, "Reload");

			string holsterText = showHolster ? Localization.Get("GAMEPLAY_HolsterPrompt") : string.Empty;
			equipItemPopup.m_ButtonPromptHolster.ShowPromptForKey(holsterText, "Holster");
		}

		internal static void MaybeRepositionAltFireButtonPrompt(EquipItemPopup equipItemPopup, String otherAction)
		{
			equipItemPopup.MaybeRepositionAltFireButtonPrompt(otherAction);
		}

		internal static void MaybeRepositionFireButtonPrompt(EquipItemPopup equipItemPopup, String otherAction)
		{
			equipItemPopup.MaybeRepositionFireButtonPrompt(otherAction);
		}

		internal static void ShowItemIcons(EquipItemPopup equipItemPopup, String primaryAction, String secondaryAction, bool showAmmo)
		{
			equipItemPopup.ShowItemIcons(primaryAction, secondaryAction, showAmmo);
		}
	}
}
