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
		// Zombie was here	
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
