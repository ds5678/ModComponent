using MelonLoader;
using System;
using System.Linq;

namespace ModComponent.Utils;

public static class EnumUtils
{
	public static T ParseEnum<T>(string text) where T : Enum
	{
		return (T)Enum.Parse(typeof(T), text, true);
	}

	public static T TranslateEnumValue<T, E>(E value) where T : Enum where E : Enum
	{ 
        return (T)Enum.Parse(typeof(T), Enum.GetName(typeof(E), value));
	}

	public static T GetMaxValue<T>() where T : Enum
	{
		return Enum.GetValues(typeof(T)).Cast<T>().Max();
	}

	public static T GetMinValue<T>() where T : Enum
	{
		return Enum.GetValues(typeof(T)).Cast<T>().Min();
	}
}
