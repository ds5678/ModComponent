using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetLoader
{
	public static class AlternateNameManager
	{
		private static Dictionary<string, string> alternateNames = new Dictionary<string, string>();

		/// <summary>
		/// Adds a pair of names to the replacement dictionary.
		/// </summary>
		/// <param name="originalName">The name of the asset to be replaced.</param>
		/// <param name="alternateName">The name of the replacement asset.</param>
		/// <remarks>
		/// The asset with the alternate name must either be a custom asset or be an asset in the same bundle as the original asset.<br/>
		/// WARNING: If the alternate asset is an invalid replacement, it can break parts of the game.
		/// </remarks>
		public static void AddAlternateName(string originalName, string alternateName)
		{
			if (ContainsKey(originalName))
			{
				Logger.LogWarning($"AlternateNameManager already has an entry for '{originalName}'. Replacing...");
				alternateNames[originalName] = alternateName;
			}
			else alternateNames.Add(originalName,alternateName);
		}
		public static string GetAlternateName(string originalName)
		{
			if (ContainsKey(originalName)) return alternateNames[originalName];
			else return null;
		}
		public static bool ContainsKey(string originalName) => alternateNames.ContainsKey(originalName);
	}
}
