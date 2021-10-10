using Type = UnhollowerRuntimeLib.Il2CppType;

namespace ModComponentMapper
{
	internal class TestFunctions
	{
		internal static void TestFunction()
		{
			//MelonLoader.MelonLogger.Log(InheritsFromMonobehaviour(Type.Of<ModComponentAPI.ModComponent>()).ToString());
			//Il2CppSystem.Type type = ResolveIl2Cpp("ModComponentAPI.ModComponent", true);
			//MelonLoader.MelonLogger.Log(InheritsFromMonobehaviour(type).ToString());
		}
		public static Il2CppSystem.Type ResolveIl2Cpp(string name, bool throwErrorOnFailure)
		{
			System.Type result = System.Type.GetType(name, false);
			if (result != null) return Type.From(result);

			System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (System.Reflection.Assembly eachAssembly in assemblies)
			{
				result = eachAssembly.GetType(name, false);
				if (result != null) return Type.From(result);
			}

			if (throwErrorOnFailure) throw new System.ArgumentException("Could not resolve type '" + name + "'. Are you missing an assembly?");
			else return null;
		}
		public static bool InheritsFromMonobehaviour(Il2CppSystem.Type type)
		{
			return type.IsSubclassOf(Type.Of<UnityEngine.MonoBehaviour>());
		}
	}
}
