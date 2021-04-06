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
        
        [Tooltip("The type of liquid this item contains.")]
        public LiquidType LiquidType = LiquidType.Water;

        [Tooltip("The capacity of this container in liters")]
        public float LiquidCapacityLiters;

        [Tooltip("If true, this container will have a random initial quantity.")]
        public bool RandomizeQuantity = false;

        [Tooltip("If initial quantity not randomized, it will have this amount initially.")]
        public float LiquidLiters = 0f;
    }
}
