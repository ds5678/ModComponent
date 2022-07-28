extern alias Hinterland;
using Hinterland;
using System;
using UnityEngine;

namespace ModComponent.AssetLoader;

[MelonLoader.RegisterTypeInIl2Cpp]
internal class SaveAtlas : MonoBehaviour
{
	public UIAtlas original;

	public SaveAtlas(IntPtr intPtr) : base(intPtr) { }
}
