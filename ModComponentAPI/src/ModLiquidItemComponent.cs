using UnityEngine;

namespace ModComponentAPI
{
    public enum LiquidType {
        Water,
        Kerosene
    }

    public class ModLiquidItemComponent : ModComponent
    {
        [Header("Liquid Item")]
        public float MaxLiters;
        public LiquidType LiquidType = LiquidType.Water;
    }
}
