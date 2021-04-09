namespace ModComponentAPI
{
    public class TypeResolver
    {
        public static System.Type Resolve(string name, bool throwErrorOnFailure)
        {
            System.Type result = System.Type.GetType(name, false);
            if (result != null)
            {
                return result;
            }

            System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            foreach (System.Reflection.Assembly eachAssembly in assemblies)
            {
                result = eachAssembly.GetType(name, false);
                if (result != null)
                {
                    return result;
                }
            }

            if (throwErrorOnFailure) throw new System.ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
            else return null;
        }
        public static Il2CppSystem.Type ResolveIl2Cpp(string name, bool throwErrorOnFailure)
        {
            Il2CppSystem.Type result = Il2CppSystem.Type.GetType(name, false);
            if (result != null)
            {
                return result;
            }
            
            Il2CppSystem.Reflection.Assembly[] assemblies = Il2CppSystem.AppDomain.CurrentDomain.GetAssemblies();
            foreach (Il2CppSystem.Reflection.Assembly eachAssembly in assemblies)
            {
                result = eachAssembly.GetType(name, false);
                if (result != null)
                {
                    return result;
                }
            }

            if (throwErrorOnFailure) throw new System.ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
            else return null;
        }
    }
}