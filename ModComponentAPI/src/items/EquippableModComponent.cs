using System;
using System.Reflection;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponentAPI
{
    public abstract class EquippableModComponent : ModComponent
    {
        /// <summary>
        /// The GameObject to be used for representing the item while it is equipped.
        /// The position, rotation and scale of this prefab will be used for rendering. Use the 'Weapon Camera' to tune the values.
        /// </summary>
        public string EquippedModelPrefabName;

        /// <summary>
        /// The name of the type implementing the specific game logic of this item.
        /// If this is an assembly-qualified name (Namespace.TypeName,Assembly) it will be loaded from the given assembly.
        /// If the assembly is omitted (Namespace.TypeName), the type will be loaded from the first assembly that contains a type with the given name.
        /// If the given type is a UnityEngine.MonoBehaviour, it will be attached to this GameObject.
        /// Leave empty if this item needs no special game logic.
        /// </summary>
        public string ImplementationType;

        /// <summary>
        /// The audio that plays when this item is equipped.
        /// </summary>
        public string EquippingAudio;

        /// <summary>
        /// The model shown while the item is equipped.
        /// </summary>
        public GameObject EquippedModel;

        /// <summary>
        /// The object containing any specific game logic for this item.
        /// </summary>
        public System.Object Implementation;

        /// <summary>
        /// Ran when the item is equipped.
        /// </summary>
        public Action OnEquipped;

        /// <summary>
        /// Ran when the item is unequipped.
        /// </summary>
        public Action OnUnequipped;

        /// <summary>
        /// Ran when the left mouse button is pressed.
        /// </summary>
        public Action OnPrimaryAction;

        /// <summary>
        /// Ran when the right mouse button is pressed.
        /// </summary>
        public Action OnSecondaryAction;

        /// <summary>
        /// 
        /// </summary>
        public Action OnControlModeChangedWhileEquipped;


        /// <summary>
        /// Creates an action delegate from a method in the loaded Implementation.
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        [HideFromIl2Cpp]
        protected Action CreateImplementationActionDelegate(string methodName)
        {
            MethodInfo methodInfo = Implementation.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (methodInfo == null)
            {
                return null;
            }

            return (Action)Delegate.CreateDelegate(typeof(Action), Implementation, methodInfo);
        }

        public EquippableModComponent(IntPtr intPtr) : base(intPtr) { }
    }
}
