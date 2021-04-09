using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModComponentMapper
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
