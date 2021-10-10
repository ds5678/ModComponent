using UnityEngine;

namespace ModComponent.SceneLoader.Shaders
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	internal class SubstituteShadersSingle : MonoBehaviour
	{
		public SubstituteShadersSingle(System.IntPtr intPtr) : base(intPtr) { }

		void Awake()
		{
			ShaderSubstitutionManager.ReplaceDummyShaders(this.gameObject, false);
		}
	}
}
