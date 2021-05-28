namespace ModComponentUtils
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
			if (result != null) return From(result);

			System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (System.Reflection.Assembly eachAssembly in assemblies)
			{
				result = eachAssembly.GetType(name, false);
				if (result != null) return From(result);
			}

			if (throwErrorOnFailure) throw new System.ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
			else return null;
		}
		public static bool InheritsFromMonobehaviour(Il2CppSystem.Type type)
		{
			if (type is null) return false;
			else return type.IsSubclassOf(UnhollowerRuntimeLib.Il2CppType.Of<UnityEngine.MonoBehaviour>());
		}
		private static Il2CppSystem.Type TypeFromPointer(System.IntPtr classPointer, string typeName = "<unknown type>")
		{
			if (classPointer == System.IntPtr.Zero) return null;
			var il2CppType = UnhollowerBaseLib.IL2CPP.il2cpp_class_get_type(classPointer);
			if (il2CppType == System.IntPtr.Zero) return null;
			return Il2CppSystem.Type.internal_from_handle(il2CppType);
		}

		public static Il2CppSystem.Type From(System.Type type)
		{
			var pointer = ReadClassPointerForType(type);
			return TypeFromPointer(pointer, type.Name);
		}

		private static System.IntPtr ReadClassPointerForType(System.Type type)
		{
			if (type == typeof(void)) return UnhollowerBaseLib.Il2CppClassPointerStore<Il2CppSystem.Void>.NativeClassPtr;
			return (System.IntPtr)typeof(UnhollowerBaseLib.Il2CppClassPointerStore<>).MakeGenericType(type)
				.GetField(nameof(UnhollowerBaseLib.Il2CppClassPointerStore<int>.NativeClassPtr)).GetValue(null);
		}
	}
}