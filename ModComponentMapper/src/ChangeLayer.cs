using UnityEngine;
using System;

//did a first pass through; didn't find anything
//NEEDS to be declared for MelonLoader

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