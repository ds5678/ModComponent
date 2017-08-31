using System;
using UnityEngine;

namespace ModComponentAPI
{
    public abstract class EquippableModComponent : ModComponent
    {
        [Header("Equipped")]
        [Tooltip("The GameObject to be used for representing the item while it is equipped. The position, rotation and scale of this prefab will be used for rendering. Use the 'Weapon Camera' to tune the values.")]
        public GameObject EquippedModelPrefab;

        [Header("Implementation")]
        [Tooltip("The name of the type implementing the specific game logic of this item. Use 'Namespace.TypeName,AssemblyName', e.g. 'RubberDuck.Implementation,Rubber-Duck'. Leave empty if this item needs no special game logic.")]
        public string ImplementationType;

        public GameObject EquippedModel { get; set; }

        [HideInInspector]
        public object Implementation;

        [HideInInspector]
        public Action OnEquipped;

        [HideInInspector]
        public Action OnUnequipped;

        [HideInInspector]
        public Action OnPrimaryAction;

        [HideInInspector]
        public Action OnSecondaryAction;

        [HideInInspector]
        public Action OnControlModeChangedWhileEquipped;

    }
}
