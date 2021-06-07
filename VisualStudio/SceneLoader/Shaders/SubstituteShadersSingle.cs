using UnityEngine;

namespace SceneLoader.Shaders
{
	internal class SubstituteShadersSingle : MonoBehaviour
	{
		public SubstituteShadersSingle(System.IntPtr intPtr) : base(intPtr) { }

		void Awake()
		{
			ShaderSubstitutionManager.ReplaceDummyShaders(this.gameObject, false);
		}
	}
}
