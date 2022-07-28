using UnityEngine;

namespace ModComponent.SceneLoader.Shaders;

[MelonLoader.RegisterTypeInIl2Cpp]
internal sealed class SubstituteShadersRecursive : MonoBehaviour
{
	public SubstituteShadersRecursive(System.IntPtr intPtr) : base(intPtr) { }

	void Awake()
	{
		ShaderSubstitutionManager.ReplaceDummyShaders(this.gameObject, true);
	}
}
