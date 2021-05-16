using MelonLoader;

namespace ModComponentMain
{
	internal static class Logger
	{
		//Regular Messages show in white
		internal static void Log(string message, params object[] parameters) => MelonLogger.Log(message, parameters);


		//Warning Messages show in yellow
		internal static void LogWarning(string message, params object[] parameters) => MelonLogger.LogWarning(message, parameters);


		//Error Messages show in red
		internal static void LogError(string message, params object[] parameters) => MelonLogger.LogError(message, parameters);


		//Blue Messages
		internal static void LogBlue(string message, params object[] parameters) => MelonLogger.Log(System.ConsoleColor.Blue, message, parameters);


		//Debug Messages show only when in debug mode
		internal static void LogDebug(string message, params object[] parameters)
		{
#if DEBUG
			Log(message, parameters);
#endif
		}
		//Not Debug Messages show only when not in debug mode
		internal static void LogNotDebug(string message, params object[] parameters)
		{
#if !DEBUG
			Log(message, parameters);
#endif
		}
	}
}