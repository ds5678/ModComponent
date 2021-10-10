using System;
using System.IO;

namespace ModComponent.Utils
{
	public static class FileUtils
	{
		public static string GetModsFolderPath()
		{
			return Path.Combine(MelonLoader.MelonUtils.GameDirectory, @"Mods");
		}

		public static string GetModComponentZipsFolderPath()
		{
			return Path.Combine(MelonLoader.MelonUtils.GameDirectory, @"Mods", @"ModComponentZips");
		}

		internal static string GetRelativePath(string file, string directory)
		{
			if (file.StartsWith(directory))
			{
				return file.Substring(directory.Length + 1);
			}

			throw new ArgumentException("Could not determine relative path of '" + file + "' to '" + directory + "'.");
		}

		internal static string GetPathRelativeToModsFolder(string fullPath)
		{
			return GetRelativePath(fullPath, GetModsFolderPath());
		}
	}
}
