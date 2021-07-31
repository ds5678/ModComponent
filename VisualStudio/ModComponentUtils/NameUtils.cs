namespace ModComponentUtils
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

		internal static string AddCraftingIconPrefix(string name)
		{
			return "ico_CraftItem__" + name;
		}

		internal static string RemoveCraftingIconPrefix(string iconFileName)
		{
			return iconFileName.Replace("ico_CraftItem__", "");
		}

		internal static string AddGearPrefix(string name)
		{
			return "GEAR_" + name;
		}

		/// <summary>
		/// Returns a string with the 'GEAR_' prefix removed.
		/// </summary>
		/// <param name="gearName">The gear's name, ie 'GEAR_SampleItem'</param>
		internal static string RemoveGearPrefix(string gearName)
		{
			return gearName.Replace("GEAR_", "");
		}

		/// <summary>
		/// Removes "(Clone)" from the name and trims any whitespace
		/// </summary>
		/// <param name="name"></param>
		/// <returns>Returns a new string without "(Clone)" or the whitespace</returns>
		public static string NormalizeName(string name)
		{
			if (name == null) return null;
			else return name.Replace("(Clone)", "").Trim();
		}
	}
}
