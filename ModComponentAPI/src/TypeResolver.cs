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

            System.Type systemresult = null;
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly eachAssembly in assemblies)
            {
                systemresult = eachAssembly.GetType(name, false);
                if (systemresult != null)
                {
                    MelonLoader.MelonLogger.Log("Found it!!!!!!!!!!!!!!!!!!!!!!!!");
                    //return UnhollowerRuntimeLib.Il2CppType.Of<>();
                }
            }

            throw new ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
        }

        public static Il2CppSystem.Type Resolve(string typeFullName,string filepath)
        {
            try
            {
                Assembly testAssembly = Assembly.LoadFile(filepath);
                if (testAssembly == null) Implementation.Log("null assembly");

                Type myType = testAssembly.GetType(typeFullName);
                if (myType == null) Implementation.Log("{0} has null type",typeFullName);

                MethodInfo mymethod = myType.GetMethod("GetIl2CppSystemType");
                if (mymethod == null) Implementation.Log("{0} does not have a GetIl2CppSystemType method.",typeFullName);

                return (Il2CppSystem.Type)mymethod.Invoke(null, null);
            }

            catch (System.IO.FileNotFoundException)
            {
                Implementation.Log("The file cannot be found.");
            }

            catch (System.BadImageFormatException)
            {
                Implementation.Log("The file is not an assembly.");
            }

            catch (System.IO.FileLoadException)
            {
                Implementation.Log("The assembly has already been loaded.");
            }
            return null;
        }
    }
}