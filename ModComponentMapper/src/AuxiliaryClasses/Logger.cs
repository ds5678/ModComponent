using MelonLoader;

namespace ModComponentMapper
{
    internal class Logger
    {
        //Regular Messages show in white
        internal static void Log(string message) => MelonLogger.Log(message);
        internal static void Log(string message, params object[] parameters) => MelonLogger.Log(message, parameters);


        //Warning Messages show in yellow
        internal static void LogWarning(string message) => MelonLogger.LogWarning(message);
        internal static void LogWarning(string message, params object[] parameters) => MelonLogger.LogWarning(message, parameters);


        //Error Messages show in red
        internal static void LogError(string message) => MelonLogger.LogError(message);
        internal static void LogError(string message, params object[] parameters) => MelonLogger.LogError(message, parameters);


        //Debug Messages show only when in debug mode
        internal static void LogDebug(string message)
        {
            if (ConfigurationManager.configurations.debugMode) Log(message);
        }
        internal static void LogDebug(string message, params object[] parameters)
        {
            if (ConfigurationManager.configurations.debugMode) Log(message, parameters);
        }
        //Not Debug Messages show only when not in debug mode
        internal static void LogNotDebug(string message)
        {
            if (!ConfigurationManager.configurations.debugMode) Log(message);
        }
        internal static void LogNotDebug(string message, params object[] parameters)
        {
            if (!ConfigurationManager.configurations.debugMode) Log(message, parameters);
        }
    }
}
