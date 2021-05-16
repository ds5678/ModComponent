using System;

namespace ModComponentUtils
{
	public static class EnumUtils
	{
		public static T ParseEnum<T>(string text) where T : Enum
		{
			return (T)Enum.Parse(typeof(T), text, true);
		}

		internal static T TranslateEnumValue<T, E>(E value)
		{
			return (T)Enum.Parse(typeof(T), Enum.GetName(typeof(E), value));
		}
	}
}
