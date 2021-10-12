using MelonLoader.TinyJSON;
using ModComponent.Utils;
using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponent.API.Modifications
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

		[HideFromIl2Cpp]
		internal void InitializeModification(ProxyObject dict, string className = "ChangeLayer")
		{
			JsonUtils.TrySetBool(ref this.Recursively, dict, className, "Recursively");
			JsonUtils.TrySetInt(ref this.Layer, dict, className, "Layer");
		}
	}
}