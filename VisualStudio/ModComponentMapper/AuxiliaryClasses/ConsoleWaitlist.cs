using Harmony;
using System.Collections.Generic;

namespace ModComponentMapper
{
	internal static class ConsoleWaitlist
	{
		private static List<string> commandWaitlistDisplay = new List<string>(0);
		private static List<string> commandWaitlistPrefab = new List<string>(0);

		public static void AddToWaitlist(string displayName, string prefabName)
		{
			commandWaitlistDisplay.Add(displayName);
			commandWaitlistPrefab.Add(prefabName);
		}
		public static bool IsConsoleManagerInitialized()
		{
			return ConsoleManager.m_Initialized;
		}
		public static void TryUpdateWaitlist()
		{
			if (commandWaitlistDisplay.Count > 0 && IsConsoleManagerInitialized())
			{
				for (int i = 0; i < commandWaitlistDisplay.Count; i++)
				{
					string displayName = commandWaitlistDisplay[i];
					string prefabName = commandWaitlistPrefab[i];
					NameUtils.RegisterConsoleGearName(displayName, prefabName);
				}
				commandWaitlistDisplay = new List<string>(0);
				commandWaitlistPrefab = new List<string>(0);
				Logger.Log("Console Commands added. The waitlist is empty.");
			}
		}

		[HarmonyPatch(typeof(GameManager), "Update")]
		internal static class UpdateConsoleCommands
		{
			private static void Postfix()
			{
				TryUpdateWaitlist();
			}
		}
	}
}
