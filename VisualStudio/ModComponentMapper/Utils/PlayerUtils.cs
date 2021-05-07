namespace ModComponentMapper
{
	public enum PlayerGender
	{
		Female,
		Male,
		Unknown
	}
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
			if (GameManager.GetPlayerManagerComponent() is null) return PlayerGender.Unknown;
			if (InterfaceManager.m_Panel_OptionsMenu?.m_State is null) return PlayerGender.Unknown;
			if (GameManager.GetPlayerManagerComponent().m_VoicePersona != InterfaceManager.m_Panel_OptionsMenu.m_State.m_VoicePersona) return PlayerGender.Unknown;

			if (InterfaceManager.m_Panel_OptionsMenu.m_State.m_VoicePersona == VoicePersona.Female) return PlayerGender.Female;
			else return PlayerGender.Male;
		}
	}
}
