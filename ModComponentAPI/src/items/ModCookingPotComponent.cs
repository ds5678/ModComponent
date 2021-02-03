using UnityEngine;
using System;

namespace ModComponentAPI
{
    public class ModCookingPotComponent : ModComponent
    {
        public bool CanCookLiquid;
        public bool CanCookGrub;
        public bool CanCookMeat;

        //[Range(0.0f, 10.0f)]
        public float Capacity;

        public string Template;

        public Mesh SnowMesh;
        public Mesh WaterMesh;

        public ModCookingPotComponent(IntPtr intPtr) : base(intPtr) { }
    }
}