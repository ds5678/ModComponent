namespace ModComponent.Mapper
{
	internal static class PackManager
	{
		internal static void SetItemPackNotWorking(string pathToZipFile, string errorMessage)
		{
			Logger.LogItemPackError(System.IO.Path.GetFileNameWithoutExtension(pathToZipFile), errorMessage);
		}
	}
}
