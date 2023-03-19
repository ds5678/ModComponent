using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;

namespace ModComponent.Mapper;

internal static class ConsoleWaitlist
{
	private static List<ModConsoleName> commandWaitlist = new List<ModConsoleName>(0);

	public static void AddToWaitlist(string displayName, string prefabName, GearType gearType)
	{
		commandWaitlist.Add(new ModConsoleName(displayName, prefabName, gearType));
	}

	public static bool IsConsoleManagerInitialized() => ConsoleManager.m_Initialized;

	public static void TryUpdateWaitlist()
	{
		if (commandWaitlist.Count > 0 && IsConsoleManagerInitialized())
		{
			foreach (ModConsoleName modConsoleName in commandWaitlist)
			{
				RegisterConsoleGearName(modConsoleName.displayName, modConsoleName.prefabName, modConsoleName.gearType);
			}
			commandWaitlist.Clear();
			Logger.Log("Console Commands added. The waitlist is empty.");
		}
	}

	internal static void MaybeRegisterConsoleGearName(string displayName, string prefabName, GearType gearType)
	{
		if (IsConsoleManagerInitialized())
		{
			RegisterConsoleGearName(displayName, prefabName, gearType);
		}
		else
		{
			AddToWaitlist(displayName, prefabName, gearType);
		}
	}

	private static void RegisterConsoleGearName(string displayName, string prefabName, GearType gearType)
	{

		// add gearitem to console search strings
		ConsoleManager.m_SearchStringToGearNames.Add(displayName.ToLower(), prefabName);
		ConsoleManager.m_SearchStringToGearNames.Add(prefabName.ToLower(), prefabName);
		//		MelonLoader.MelonLogger.Warning("m_SearchStringToGearNames | " + displayName + " | " + prefabName);

		string addToKey = "ModComponent";

		switch (gearType)
		{
			case GearType.Clothing:
				addToKey = "ClothingItem";
				break;
			case GearType.Tool:
				addToKey = "ToolsItem";
				break;
			case GearType.Firestarting:
				addToKey = "FireStarterItem";
				break;
			case GearType.Food:
				addToKey = "FoodItem";
				break;
			case GearType.FirstAid:
				addToKey = "FirstAidItem";
				break;
		}

		// add gearitem to the detected component lookup list
		Il2CppSystem.Collections.Generic.List<string> list = new();
		if (!ConsoleManager.m_ComponentNameToGearNames.ContainsKey(addToKey))
		{
			ConsoleManager.m_ComponentNameToGearNames.Add(addToKey, list);
		}
		ConsoleManager.m_ComponentNameToGearNames[addToKey].Add(prefabName);

		// add the gearitem to the console (add,gear_add) autocomplete list
		foreach (uConsoleCommandParameterSet cps in uConsoleAutoComplete.m_CommandParameterSets)
		{
			if (cps.m_Commands.Contains("add") && cps.m_Commands.Contains("gear_add"))
			{
				cps.m_AllowedParameters.Add(displayName.ToLower());
			}
		}

	}

	[HarmonyPatch(typeof(ConsoleManager), nameof(ConsoleManager.Initialize))]
	internal static class UpdateConsoleCommands
	{
		private static void Postfix()
		{
			TryUpdateWaitlist();
		}
	}

	internal readonly struct ModConsoleName
	{
		public readonly string displayName;
		public readonly string prefabName;
		public readonly GearType gearType;
		public ModConsoleName(string displayName, string prefabName, GearType gearType)
		{
			this.displayName = displayName;
			this.prefabName = prefabName;
			this.gearType = gearType;
		}
	}
}
