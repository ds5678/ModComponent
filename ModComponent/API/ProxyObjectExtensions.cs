using MelonLoader.TinyJSON;
using ModComponent.Utils;
using System;
using System.Collections.Generic;

namespace ModComponent.API
{
    internal static class ProxyObjectExtensions
    {
        internal static Variant GetVariant(this ProxyObject dict, string className, string fieldName)
        {
            Variant subDict;
            try
            {
                subDict = dict[className];
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"The json doesn't have an entry for '{className}'", ex);
            }
            try
            {
                return subDict[fieldName];
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception($"The '{className}' entry in the json doesn't have a field for '{fieldName}'", ex);
            }
        }

        internal static T GetEnum<T>(this ProxyObject dict, string className, string fieldName) where T : Enum
        {
            return EnumUtils.ParseEnum<T>(dict.GetVariant(className,fieldName));
        }
    }
}
