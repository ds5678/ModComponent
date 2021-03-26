using System;
using UnityEngine;

namespace ModComponentAPI
{
    public class ModCookingPotComponent : ModComponent
    {
        /// <summary>
        /// Can the item cook liquids?
        /// </summary>
        public bool CanCookLiquid;

        /// <summary>
        /// Can the item cook grub? <br/>
        /// Cookable canned food counts as grub.
        /// </summary>
        public bool CanCookGrub;

        /// <summary>
        /// Can the item cook meat?
        /// </summary>
        public bool CanCookMeat;

        /// <summary>
        /// The total water capacity of the item.
        /// </summary>
        public float Capacity;

        /// <summary>
        /// Template item to be used in the mapping process.
        /// </summary>
        public string Template;

        public Mesh SnowMesh;
        public Mesh WaterMesh;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModCookingPotComponent>(this);
        }

        public ModCookingPotComponent(IntPtr intPtr) : base(intPtr) { }
    }
}