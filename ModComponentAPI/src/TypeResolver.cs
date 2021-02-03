using System;
using System.Reflection;

namespace ModComponentAPI
{
    public class TypeResolver
    {
        public static Il2CppSystem.Type Resolve(string name)
        {
            Il2CppSystem.Type result = Il2CppSystem.Type.GetType(name, false);
            if (result != null)
            {
                return result;
            }

            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //foreach (Assembly eachAssembly in assemblies)
            //{
            //    result = eachAssembly.GetType(name, false);
            //    if (result != null)
            //    {
            //        return result;
            //    }
            //}

            throw new ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
        }
    }
}