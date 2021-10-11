namespace ModComponent.Mapper.InformationMenu
{
	internal static class PackManager
	{
		internal static void SetItemPackNotWorking(string pathToAsset, string errorMessage)
		{
			Logger.LogItemPackError(System.IO.Path.GetFileNameWithoutExtension(pathToAsset), errorMessage);
		}
	}
}
