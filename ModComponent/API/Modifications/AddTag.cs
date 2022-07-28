using UnityEngine;

namespace ModComponent.API.Modifications;

[MelonLoader.RegisterTypeInIl2Cpp]
public class AddTag : MonoBehaviour
{
	public string Tag;

	public void Awake()
	{
		gameObject.tag = Tag;
	}

	public AddTag(System.IntPtr intPtr) : base(intPtr) { }
}