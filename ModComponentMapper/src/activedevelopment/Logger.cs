using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace ModComponentMapper
{
    internal class Logger
    {
        //Regular Messages show in white
        internal static void Log(string message)
        {
            MelonLogger.Log(message);
        }
        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            Log(preformattedMessage);
        }


        //Warning Messages show in yellow
        internal static void LogWarning(string message)
        {
            MelonLogger.LogWarning(message);
        }
        internal static void LogWarning(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            LogWarning(preformattedMessage);
        }


        //Error Messages show in red
        internal static void LogError(string message)
        {
            MelonLogger.LogError(message);
        }
        internal static void LogError(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            LogError(preformattedMessage);
        }


        //Debug Messages show only when in debug mode
        internal static void LogDebug(string message)
        {
            if (ConfigurationManager.configurations.debugMode)
            {
                MelonLogger.Log(message);
            }
        }
        internal static void LogDebug(string message, params object[] parameters)
        {
            if (ConfigurationManager.configurations.debugMode)
            {
                string preformattedMessage = string.Format(message, parameters);
                LogDebug(preformattedMessage);
            }
        }
    }
}
