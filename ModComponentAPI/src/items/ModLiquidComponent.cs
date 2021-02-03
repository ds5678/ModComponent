using UnityEngine;

namespace ModComponentAPI
{
    public enum LiquidType {
        Water,
        Kerosene
    }

    public class ModLiquidComponent : ModComponent
    {
        //[Header("Liquid Item")]
        public float MaxLiters;
        public LiquidType LiquidType = LiquidType.Water;
        public ModLiquidComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}
