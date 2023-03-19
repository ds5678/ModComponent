namespace ModComponent.Utils;

internal static class FileUtils
{
	internal static string GetRelativePath(string file, string directory)
	{
		if (file.StartsWith(directory))
		{
			return file.Substring(directory.Length + 1);
		}

		throw new ArgumentException("Could not determine relative path of '" + file + "' to '" + directory + "'.");
	}
}
