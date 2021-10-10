namespace ModComponent.Utils
{
	internal class TypeResolver
	{
		public static System.Type Resolve(string name, bool throwErrorOnFailure)
		{
			System.Type result = System.Type.GetType(name, false);
			if (result != null) return result;

			System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (System.Reflection.Assembly eachAssembly in assemblies)
			{
				result = eachAssembly.GetType(name, false);
				if (result != null) return result;
			}

			if (throwErrorOnFailure) throw new System.ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
			else return null;
		}
		public static Il2CppSystem.Type ResolveIl2Cpp(string name, bool throwErrorOnFailure)
		{
			System.Type result = System.Type.GetType(name, false);
			if (result != null) return UnhollowerRuntimeLib.Il2CppType.From(result, false);

			System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (System.Reflection.Assembly eachAssembly in assemblies)
			{
				result = eachAssembly.GetType(name, false);
				if (result != null) return UnhollowerRuntimeLib.Il2CppType.From(result, false);
			}

			if (throwErrorOnFailure) throw new System.ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
			else return null;
		}
		public static bool InheritsFromMonobehaviour(Il2CppSystem.Type type)
		{
			if (type == null) return false;
			else return type.IsSubclassOf(UnhollowerRuntimeLib.Il2CppType.Of<UnityEngine.MonoBehaviour>());
		}
	}
}