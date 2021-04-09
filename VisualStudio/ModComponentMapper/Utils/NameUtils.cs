namespace ModComponentMapper
{
    public static class NameUtils
    {
        public static LocalizedString CreateLocalizedString(string key)
        {
            return new LocalizedString() { m_LocalizationID = key };
        }
        public static LocalizedString[] CreateLocalizedStrings(params string[] keys)
        {
            LocalizedString[] result = new LocalizedString[keys.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = CreateLocalizedString(keys[i]);
            }

            return result;
        }
        public static string AddCraftingIconPrefix(string name)
        {
            return "ico_CraftItem__" + name;
        }

        public static string RemoveCraftingIconPrefix(string iconFileName)
        {
            return iconFileName.Replace("ico_CraftItem__", "");
        }

        public static string AddGearPrefix(string name)
        {
            return "GEAR_" + name;
        }

        /// <summary>
        /// Returns a string with the 'GEAR_' prefix removed.
        /// </summary>
        /// <param name="gearName">The gear's name, ie 'GEAR_SampleItem'</param>
        public static string RemoveGearPrefix(string gearName)
        {
            return gearName.Replace("GEAR_", "");
        }

        public static string NormalizeName(string name)
        {
            if (name == null) return null;
            else return name.Replace("(Clone)", "").Trim();
        }

        internal static void RegisterConsoleGearName(string displayName, string prefabName)
        {
            if (ConsoleWaitlist.IsConsoleManagerInitialized())
            {
                ConsoleManager.AddCustomGearName(displayName.ToLower(), prefabName.ToLower());
            }
            else ConsoleWaitlist.AddToWaitlist(displayName, prefabName);
        }
    }
}
