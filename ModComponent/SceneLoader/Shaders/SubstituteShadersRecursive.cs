using UnityEngine;

namespace SceneLoader.Shaders
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	internal class SubstituteShadersRecursive : MonoBehaviour
	{
		public SubstituteShadersRecursive(System.IntPtr intPtr) : base(intPtr) { }

		void Awake()
		{
			ShaderSubstitutionManager.ReplaceDummyShaders(this.gameObject, true);
		}
	}
}
