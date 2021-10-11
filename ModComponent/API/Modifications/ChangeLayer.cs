using ModComponent.Utils;
using System;
using UnityEngine;

namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ChangeLayer : MonoBehaviour
	{
		public int Layer;
		public bool Recursively;

		public void Start()
		{
			CopyFieldHandler.UpdateFieldValues<ChangeLayer>(this);
			this.Invoke("SetLayer", 1);
		}

		internal void SetLayer()
		{
			vp_Layer.Set(this.gameObject, Layer, Recursively);
			Destroy(this);
		}

		public ChangeLayer(IntPtr intPtr) : base(intPtr) { }
	}
}