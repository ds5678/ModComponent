using System;
using System.Reflection;
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

        public string EquippingAudio;

        [HideInInspector]
        public GameObject EquippedModel;

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

        void Awake()
        {
            if (ImplementationType == null || ImplementationType == string.Empty)
            {
                return;
            }

            Type implementationType = Type.GetType(ImplementationType);
            Implementation = Activator.CreateInstance(implementationType);
            if (Implementation == null)
            {
                return;
            }

            OnEquipped = CreateImplementationActionDelegate("OnEquipped");
            OnUnequipped = CreateImplementationActionDelegate("OnUnequipped");

            OnPrimaryAction = CreateImplementationActionDelegate("OnPrimaryAction");
            OnSecondaryAction = CreateImplementationActionDelegate("OnSecondaryAction");

            OnControlModeChangedWhileEquipped = CreateImplementationActionDelegate("OnControlModeChangedWhileEquipped");
        }

        private Action CreateImplementationActionDelegate(string methodName)
        {
            MethodInfo methodInfo = Implementation.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (methodInfo == null)
            {
                return null;
            }

            return (Action)Delegate.CreateDelegate(typeof(Action), Implementation, methodInfo);
        }
    }
}
