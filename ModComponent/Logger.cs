using MelonLoader;
using System;
using System.Reflection;

namespace ModComponent
{
	internal static class Logger
	{
		private static bool reflectionSuccessful;
		private static bool isMelonLoader3;
		private static MethodInfo m2_LogMelonError;
		private static MethodInfo m3_Internal_Error;
		private static MethodInfo m3_RunErrorCallbacks;

		static Logger() => GetHiddenLogMethods();

		#region Log Functions
		//Regular Messages show in white
		internal static void Log(string message, params object[] parameters) => MelonLogger.Msg(message, parameters);


		//Warning Messages show in yellow
		internal static void LogWarning(string message, params object[] parameters) => MelonLogger.Warning(message, parameters);


		//Error Messages show in red
		internal static void LogError(string message, params object[] parameters) => MelonLogger.Error(message, parameters);


		//This is for when item packs error while loading.
		internal static void LogItemPackError(string namesection, string message, params object[] parameters)
		{
			if (!reflectionSuccessful)
			{
				LogError(message, parameters);
				return;
			}

			string txt = string.Format(message, parameters);
			if (isMelonLoader3)
			{
				m3_Internal_Error.Invoke(null, new object[] { namesection, txt });
				m3_RunErrorCallbacks.Invoke(null, new object[] { namesection, txt });
			}
			else
			{
				m2_LogMelonError.Invoke(null, new object[] { txt, namesection });
			}
		}


		//Blue Messages
		internal static void LogBlue(string message, params object[] parameters) => MelonLogger.Msg(ConsoleColor.Blue, message, parameters);


		//Green Messages
		internal static void LogGreen(string message, params object[] parameters) => MelonLogger.Msg(ConsoleColor.Green, message, parameters);


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
		#endregion

		private static void GetHiddenLogMethods()
		{
			Type type = typeof(MelonLogger);
			foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (method.Name == "LogMelonError")
				{
					reflectionSuccessful = true;
					isMelonLoader3 = false;
					m2_LogMelonError = method;
					return;
				}

				if (method.Name == "Internal_Error")
				{
					isMelonLoader3 = true;
					m3_Internal_Error = method;
					continue;
				}
				if (method.Name == "RunErrorCallbacks")
				{
					isMelonLoader3 = true;
					m3_RunErrorCallbacks = method;
					continue;
				}
			}
			reflectionSuccessful = m3_Internal_Error != null && m3_RunErrorCallbacks != null;
		}
	}
}