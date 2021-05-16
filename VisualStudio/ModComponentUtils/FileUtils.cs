using System;

namespace ModComponentUtils
{
	public static class FileUtils
	{
		public static string GetRelativePath(string file, string directory)
		{
			if (file.StartsWith(directory))
			{
				return file.Substring(directory.Length + 1);
			}

			throw new ArgumentException("Could not determine relative path of '" + file + "' to '" + directory + "'.");
		}
	}
}
