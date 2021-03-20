using System;
using UnityEngine;

namespace ModComponentAPI
{
    public abstract class EquippableModComponent : ModComponent
    {
        //[Header("Equipped")]
        //[Tooltip("The GameObject to be used for representing the item while it is equipped. The position, rotation and scale of this prefab will be used for rendering. Use the 'Weapon Camera' to tune the values.")]
        public string EquippedModelPrefabName;

        //[Header("Implementation")]
        //[Tooltip("The name of the type implementing the specific game logic of this item.\n" +
        //    "If this is an assembly-qualified name (Namespace.TypeName,Assembly) it will be loaded from the given assembly. If the assembly is omitted (Namespace.TypeName), the type will be loaded from the first assembly that contains a type with the given name.\n" +
        //    "If the given type is a UnityEngine.MonoBehaviour, it will be attached to this GameObject.\n" +
        //    "Leave empty if this item needs no special game logic.")]
        public string ImplementationType;

        public string EquippingAudio;

        //[HideInInspector]
        public GameObject EquippedModel;

        //[HideInInspector]
        public System.Object Implementation;

        //[HideInInspector]
        public Action OnEquipped;

        //[HideInInspector]
        public Action OnUnequipped;

        //[HideInInspector]
        public Action OnPrimaryAction;

        //[HideInInspector]
        public Action OnSecondaryAction;

        //[HideInInspector]
        public Action OnControlModeChangedWhileEquipped;


        public EquippableModComponent(IntPtr intPtr) : base(intPtr) { }
    }
}
