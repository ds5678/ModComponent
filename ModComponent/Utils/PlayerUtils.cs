using Il2Cpp;


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

	public static PlayerGender GetPlayerGender()
	{
		if (GameManager.GetPlayerManagerComponent() == null)
		{
			return PlayerGender.Unknown;
		}

		if (InterfaceManager.GetPanel<Panel_OptionsMenu>()?.State == null)
		{
			return PlayerGender.Unknown;
		}

		if (PlayerManager.m_VoicePersona == VoicePersona.Female)
		{
			return PlayerGender.Female;
		}
		else
		{
			return PlayerGender.Male;
		}
	}
}
