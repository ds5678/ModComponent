namespace AssetLoader
{
	internal static class Logger
	{
		private static string GetFormattedString(string message, params object[] parameters)
		{
			return "[AssetLoader] " + string.Format(message, parameters);
		}

		//Regular Messages show in white
		internal static void Log(string message, params object[] parameters) => ModComponentMain.Logger.Log(GetFormattedString(message, parameters));


		//Warning Messages show in yellow
		internal static void LogWarning(string message, params object[] parameters) => ModComponentMain.Logger.LogWarning(GetFormattedString(message, parameters));


		//Error Messages show in red
		internal static void LogError(string message, params object[] parameters) => ModComponentMain.Logger.LogError(GetFormattedString(message, parameters));


		//Blue Messages
		internal static void LogBlue(string message, params object[] parameters) => ModComponentMain.Logger.LogBlue(GetFormattedString(message, parameters));


		//Debug Messages show only when in debug mode
		internal static void LogDebug(string message, params object[] parameters) => ModComponentMain.Logger.LogDebug(GetFormattedString(message, parameters));

		//Not Debug Messages show only when not in debug mode
		internal static void LogNotDebug(string message, params object[] parameters) => ModComponentMain.Logger.LogNotDebug(GetFormattedString(message, parameters));
	}
}
