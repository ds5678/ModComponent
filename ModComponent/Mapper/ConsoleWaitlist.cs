using Il2Cpp;
using HarmonyLib;
using System.Collections.Generic;
using Il2CppNodeCanvas.Tasks.Actions;
using UnityEngine;
using Il2CppSystem.Collections.Generic;
using System.Security.AccessControl;
using MelonLoader;
using static Il2Cpp.ConsoleManagerSettings;

namespace ModComponent.Mapper;

internal static class ConsoleWaitlist
{
	private static System.Collections.Generic.List<ModConsoleName> commandWaitlist = new System.Collections.Generic.List<ModConsoleName>(0);

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
		// Zombie was here
		ConsoleManagerSettings foo = ScriptableObject.FindObjectOfType<ConsoleManagerSettings>();	

        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		list = foo.m_GearItemComponentNames.ToList();
        list.Add(prefabName);
        foo.m_GearItemComponentNames = list.ToArray();
      
		
		/* Zombie was here and left crying
            KeyToGearItem keytogear = new KeyToGearItem();
		keytogear.m_GearItemReference = foo;

		AssetReferenceGearItem newREfGear = new AssetReferenceGearItem();

        foo.m_CustomGearItemSearchTerms.AddItem()
		*/


        //ConsoleManager.m_CustomGearNameDictionary.Add(customName, realName);
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
