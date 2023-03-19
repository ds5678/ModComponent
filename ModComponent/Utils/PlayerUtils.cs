using Il2Cpp;
using UnityEngine.Analytics;

namespace ModComponent.Utils;

public static class PlayerUtils
{
	public static void FreezePlayer()
	{
		GameManager.GetPlayerManagerComponent().m_FreezeMovement = true;
	}

	public static void UnfreezePlayer()
	{
		GameManager.GetPlayerManagerComponent().m_FreezeMovement = false;
	}

	public static Gender GetPlayerGender()
	{
		if (GameManager.GetPlayerManagerComponent() == null)
		{
			return Gender.Unknown;
		}

		if (InterfaceManager.GetPanel<Panel_OptionsMenu>()?.State == null)
		{
			return Gender.Unknown;
		}

		if (PlayerManager.m_VoicePersona == VoicePersona.Female)
		{
			return Gender.Female;
		}
		else
		{
			return Gender.Male;
		}
	}
}
