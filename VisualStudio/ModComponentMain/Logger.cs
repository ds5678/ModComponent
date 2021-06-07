using MelonLoader;
using System;
using System.Reflection;

namespace ModComponentMain
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
		internal static void Log(string message, params object[] parameters) => MelonLogger.Log(message, parameters);


		//Warning Messages show in yellow
		internal static void LogWarning(string message, params object[] parameters) => MelonLogger.LogWarning(message, parameters);


		//Error Messages show in red
		internal static void LogError(string message, params object[] parameters) => MelonLogger.LogError(message, parameters);


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
		internal static void LogBlue(string message, params object[] parameters) => MelonLogger.Log(ConsoleColor.Blue, message, parameters);


		//Green Messages
		internal static void LogGreen(string message, params object[] parameters) => MelonLogger.Log(ConsoleColor.Green, message, parameters);


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
			MelonLogger.Log("");
			MelonLogger.Log("Attempting to get hidden log methods");
			Type type = typeof(MelonLogger);
			foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				MelonLogger.Log($"'{method.Name}'");

				if (method.Name == "LogMelonError")
				{
					reflectionSuccessful = true;
					isMelonLoader3 = false;
					m2_LogMelonError = method;
					MelonLogger.Log("Found LogMelonError");
					return;
				}

				if (method.Name == "Internal_Error")
				{
					isMelonLoader3 = true;
					m3_Internal_Error = method;
					MelonLogger.Log("Found Internal_Error");
					continue;
				}
				if (method.Name == "RunErrorCallbacks")
				{
					isMelonLoader3 = true;
					m3_RunErrorCallbacks = method;
					MelonLogger.Log("Found RunErrorCallbacks");
					continue;
				}
			}
			reflectionSuccessful = m3_Internal_Error != null && m3_RunErrorCallbacks != null;
			MelonLogger.Log($"Reflection Successful: {reflectionSuccessful}");
			MelonLogger.Log("");
		}
	}
}