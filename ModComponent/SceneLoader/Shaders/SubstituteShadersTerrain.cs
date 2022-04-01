using UnityEngine;

namespace ModComponent.SceneLoader.Shaders
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	internal sealed class SubstituteShadersTerrain : MonoBehaviour
	{
		public SubstituteShadersTerrain(System.IntPtr intPtr) : base(intPtr) { }

		void Awake()
		{
			Terrain terrain = this.GetComponent<Terrain>();
			if (terrain != null)
			{
				ShaderSubstitutionManager.ReplaceDummyShaders(terrain);
			}
		}
	}
}
