namespace ModComponentAPI
{
	internal static class Logger
	{
		private static string GetFormattedString(string message, params object[] parameters)
		{
			return "[API] " + string.Format(message, parameters);
		}

		//Regular Messages show in white
		internal static void Log(string message, params object[] parameters) => ModComponent.Main.Logger.Log(GetFormattedString(message, parameters));


		//Warning Messages show in yellow
		internal static void LogWarning(string message, params object[] parameters) => ModComponent.Main.Logger.LogWarning(GetFormattedString(message, parameters));


		//Error Messages show in red
		internal static void LogError(string message, params object[] parameters) => ModComponent.Main.Logger.LogError(GetFormattedString(message, parameters));


		//Item Pack Error Messages are only for when an item pack causes an error while loading
		internal static void LogItemPackError(string namesection, string message, params object[] parameters) => ModComponent.Main.Logger.LogItemPackError(namesection, message, parameters);


		//Blue Messages
		internal static void LogBlue(string message, params object[] parameters) => ModComponent.Main.Logger.LogBlue(GetFormattedString(message, parameters));


		//Green Messages
		internal static void LogGreen(string message, params object[] parameters) => ModComponent.Main.Logger.LogGreen(GetFormattedString(message, parameters));


		//Debug Messages show only when in debug mode
		internal static void LogDebug(string message, params object[] parameters) => ModComponent.Main.Logger.LogDebug(GetFormattedString(message, parameters));

		//Not Debug Messages show only when not in debug mode
		internal static void LogNotDebug(string message, params object[] parameters) => ModComponent.Main.Logger.LogNotDebug(GetFormattedString(message, parameters));
	}
}
