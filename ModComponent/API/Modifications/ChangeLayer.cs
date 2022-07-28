extern alias Hinterland;
using Hinterland;
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
			this.Recursively = dict.GetVariant(className, "Recursively");
			this.Layer = dict.GetVariant(className, "Layer");
		}
	}
}