using System;
using UnityEngine;

namespace ModComponent.AssetLoader
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class SaveAtlas : MonoBehaviour
	{
		public UIAtlas original;

		public SaveAtlas(IntPtr intPtr) : base(intPtr) { }
	}
}
