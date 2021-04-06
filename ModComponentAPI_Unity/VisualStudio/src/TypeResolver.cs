using System;
using System.Reflection;

namespace ModComponentAPI
{
    /*public class TypeResolver
    {
        public static Type Resolve(string name)
        {
            Type result = Type.GetType(name, false);
            if (result != null)
            {
                return result;
            }

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly eachAssembly in assemblies)
            {
                result = eachAssembly.GetType(name, false);
                if (result != null)
                {
                    return result;
                }
            }

            throw new ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
        }
    }*/
}