using Il2Cpp;
using HarmonyLib;

using System.Collections.Generic;
using System.Security.AccessControl;

namespace ModComponent.Mapper;

internal static class ConsoleWaitlist
{
	private static List<ModConsoleName> commandWaitlist = new List<ModConsoleName>(0);

	public static void AddToWaitlist(string displayName, string prefabName)
	{
		commandWaitlist.Add(new ModConsoleName(displayName, prefabName));
	}

	public static bool IsConsoleManagerInitialized() => ConsoleManager.m_Initialized;

	public static void TryUpdateWaitlist()
	{
		if (commandWaitlist.Count > 0 && IsConsoleManagerInitialized())
		{
			foreach (ModConsoleName modConsoleName in commandWaitlist)
			{
				RegisterConsoleGearName(modConsoleName.displayName, modConsoleName.prefabName);
			}
			commandWaitlist.Clear();
			Logger.Log("Console Commands added. The waitlist is empty.");
		}
	}

	internal static void MaybeRegisterConsoleGearName(string displayName, string prefabName)
	{
		if (IsConsoleManagerInitialized())
		{
			RegisterConsoleGearName(displayName, prefabName);
		}
		else
		{
			AddToWaitlist(displayName, prefabName);
		}
	}

	private static void RegisterConsoleGearName(string displayName, string prefabName)
	{
		ConsoleManager.m_SearchStringToGearNames.Add(displayName,prefabName);
//		ConsoleManager.AddCustomGearName(displayName.ToLower(), prefabName.ToLower());
	}

	[HarmonyPatch(typeof(ConsoleManager), nameof(ConsoleManager.Initialize))]
	internal static class UpdateConsoleCommands
	{
		private static void Postfix()
		{
			TryUpdateWaitlist();
		}
	}

	internal struct ModConsoleName
	{
		public readonly string displayName;
		public readonly string prefabName;
		public ModConsoleName(string displayName, string prefabName)
		{
			this.displayName = displayName;
			this.prefabName = prefabName;
		}
	}
}
