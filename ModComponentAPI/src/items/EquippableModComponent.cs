using System;
using System.Reflection;
using UnityEngine;
using UnhollowerBaseLib.Attributes;

namespace ModComponentAPI
{
    public abstract class EquippableModComponent : ModComponent
    {
        //[Header("Equipped")]
        //[Tooltip("The GameObject to be used for representing the item while it is equipped. The position, rotation and scale of this prefab will be used for rendering. Use the 'Weapon Camera' to tune the values.")]
        public GameObject EquippedModelPrefab;

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
        public Il2CppSystem.Object Implementation;

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

        [Obsolete]
        void Awake()
        {
            if (string.IsNullOrEmpty(ImplementationType))
            {
                return;
            }

            Il2CppSystem.Type implementationType = TypeResolver.Resolve(ImplementationType);
            if (implementationType.IsSubclassOf(MonoBehaviour.Il2CppType))
            {
                this.Implementation = this.gameObject.AddComponent(implementationType);
            }
            else
            {
                this.Implementation = Il2CppSystem.Activator.CreateInstance(implementationType);
            }

            if (this.Implementation == null)
            {
                return;
            }

            OnEquipped = CreateImplementationActionDelegate("OnEquipped");
            OnUnequipped = CreateImplementationActionDelegate("OnUnequipped");

            OnPrimaryAction = CreateImplementationActionDelegate("OnPrimaryAction");
            OnSecondaryAction = CreateImplementationActionDelegate("OnSecondaryAction");

            OnControlModeChangedWhileEquipped = CreateImplementationActionDelegate("OnControlModeChangedWhileEquipped");

            Il2CppSystem.Reflection.FieldInfo fieldInfo = implementationType.GetField("EquippableModComponent", Il2CppSystem.Reflection.BindingFlags.Instance); //, Il2CppSystem.Reflection.BindingFlags.NonPublic , Il2CppSystem.Reflection.BindingFlags.Public);
            if (fieldInfo != null && fieldInfo.FieldType == EquippableModComponent.Il2CppType)
            {
                fieldInfo.SetValue(Implementation, this);
            }
        }

        [HideFromIl2Cpp]
        private Action CreateImplementationActionDelegate(string methodName)
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
