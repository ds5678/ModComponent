using UnityEngine;

namespace SceneLoader.Shaders
{
	internal class SubstituteShadersRecursive : MonoBehaviour
	{
		public SubstituteShadersRecursive(System.IntPtr intPtr) : base(intPtr) { }

		void Awake()
		{
			ShaderSubstitutionManager.ReplaceDummyShaders(this.gameObject, true);
		}
	}
}
