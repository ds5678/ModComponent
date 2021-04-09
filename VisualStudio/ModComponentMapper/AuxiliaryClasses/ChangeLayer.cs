using System;
using UnityEngine;

//Not sure why this was included

namespace ModComponentMapper
{
    public class ChangeLayer : MonoBehaviour
    {
        public int Layer;
        public bool Recursively;

        public void Start()
        {
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