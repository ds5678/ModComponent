using MelonLoader;
using System.Reflection;

namespace ModComponent;

internal static class Logger
{
	private static bool reflectionSuccessful;
	private static MethodInfo? method_Internal_Error;
	private static MethodInfo? method_RunErrorCallbacks;

	static Logger()
	{
		GetHiddenLogMethods();
		Log($"Reflection Successful: {reflectionSuccessful}");
	}

	#region Log Functions
	//Regular Messages show in white
	internal static void Log(string message) => Implementation.instance.LoggerInstance.Msg(message);

	//Warning Messages show in yellow
	internal static void LogWarning(string message) => Implementation.instance.LoggerInstance.Warning(message);

	//Error Messages show in red
	internal static void LogError(string message) => Implementation.instance.LoggerInstance.Error(message);

	//This is for when item packs error while loading.
	internal static void LogItemPackError(string namesection, string message)
	{
		if (!reflectionSuccessful)
		{
			LogError(message);
			return;
		}

		string txt = string.Format(message);
		method_Internal_Error?.Invoke(null, new object[] { namesection, txt });
		method_RunErrorCallbacks?.Invoke(null, new object[] { namesection, txt });
	}

	//Blue Messages
	internal static void LogBlue(string message) => Implementation.instance.LoggerInstance.Msg(ConsoleColor.Blue, message);

	//Green Messages
	internal static void LogGreen(string message) => Implementation.instance.LoggerInstance.Msg(ConsoleColor.Green, message);

	//Debug Messages show only when in debug mode
	internal static void LogDebug(string message)
	{
#if DEBUG
		Log(message);
#endif
	}
	//Not Debug Messages show only when not in debug mode
	internal static void LogNotDebug(string message)
	{
#if !DEBUG
		Log(message);
#endif
	}
	#endregion

	private static void GetHiddenLogMethods()
	{
		Type type = typeof(MelonLogger);
		foreach (MethodInfo method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
		{
			if (method.Name == "Internal_Error")
			{
				method_Internal_Error = method;
				continue;
			}
			if (method.Name == "RunErrorCallbacks")
			{
				method_RunErrorCallbacks = method;
				continue;
			}
		}
		reflectionSuccessful = method_Internal_Error != null && method_RunErrorCallbacks != null;
	}
}