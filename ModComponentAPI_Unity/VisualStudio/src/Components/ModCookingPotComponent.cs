using UnityEngine;

namespace ModComponentAPI
{
    public class ModCookingPotComponent : ModComponent
    {
        [Tooltip("Can the item cook liquids?")]
        public bool CanCookLiquid;
        [Tooltip("Can the item cook grub? Cookable canned food counts as grub.")]
        public bool CanCookGrub;
        [Tooltip("Can the item cook meat?")]
        public bool CanCookMeat;

        [Range(0.0f, 10.0f)]
        [Tooltip("The total water capacity of the item.")]
        public float Capacity;

        [Tooltip("Template item to be used in the mapping process.")]
        public string Template;

        public Mesh SnowMesh;
        public Mesh WaterMesh;
    }
}