using ModComponentUtils;
using UnityEngine;

namespace ModComponentAPI
{
	public class AttachBehaviour : MonoBehaviour
	{
		/// <summary>
		/// The name of the class to be attached.<br/>
		/// This class must extend `UnityEngine.MonoBehaviour`.<br/>
		/// If this is an assembly-qualified name (Namespace.TypeName,Assembly) it will be loaded from the given assembly.<br/>
		/// If the assembly is omitted (Namespace.TypeName), the type will be loaded from the first assembly that contains a type with the given name.
		/// </summary>
		public string BehaviourName;

		/// <summary>
		/// Should this fail if the behaviour cannot be loaded or attached?
		/// </summary>
		public bool FailOnError = true;

		public void Start()
		{
			try
			{
				Il2CppSystem.Type behaviourType = TypeResolver.ResolveIl2Cpp(BehaviourName, true);
				this.gameObject.AddComponent(behaviourType);
			}
			catch (System.Exception e)
			{
				Logger.LogError("Could not load behaviour '" + BehaviourName + "': " + e.Message);

				if (FailOnError) throw e;
			}
		}

		public AttachBehaviour(System.IntPtr intPtr) : base(intPtr) { }
	}
}