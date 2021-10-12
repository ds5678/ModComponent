namespace ModComponent.Mapper
{
	internal static class PackManager
	{
		internal static void SetItemPackNotWorking(string pathToAsset, string errorMessage)
		{
			Logger.LogItemPackError(System.IO.Path.GetFileNameWithoutExtension(pathToAsset), errorMessage);
		}
	}
}
