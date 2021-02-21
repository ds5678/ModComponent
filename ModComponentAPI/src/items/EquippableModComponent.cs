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
        public Il2CppSystem.Action OnEquipped;

        //[HideInInspector]
        public Il2CppSystem.Action OnUnequipped;

        //[HideInInspector]
        public Il2CppSystem.Action OnPrimaryAction;

        //[HideInInspector]
        public Il2CppSystem.Action OnSecondaryAction;

        //[HideInInspector]
        public Il2CppSystem.Action OnControlModeChangedWhileEquipped;

        [Obsolete]
        void Awake()
        {
            return;
            ImplementationType = "RubberDuck.RubberDuckImplementation";
            string filepath = @"C:\Program Files (x86)\Steam\steamapps\common\TheLongDark\Mods\auto-mapped\RubberDuck\Rubber-Duck.dll";

            MelonLoader.MelonLogger.Log("Equippable awake actually runs!!!!!!");
            //MelonLoader.MelonLogger.Log("ImplementationType: '{0}'", ImplementationType?.ToString());
            MelonLoader.MelonLogger.Log("Name: {0}", this.name);
            MelonLoader.MelonLogger.Log("EquippingAudio: '{0}'", EquippingAudio?.ToString());
            MelonLoader.MelonLogger.Log("NameID: '{0}'", this.DisplayNameLocalizationId);

            if (string.IsNullOrEmpty(ImplementationType))
            {
                MelonLoader.MelonLogger.Log("ImplementationType was null!!!!!!!!!!!!!");
                return;
            }

            Il2CppSystem.Type implementationType = TypeResolver.Resolve(ImplementationType,filepath);
            //Il2CppSystem.Type implementationType = UnhollowerRuntimeLib.Il2CppType.Of<RubberDuckTest>();
            MelonLoader.MelonLogger.Log("implementationType: '{0}'", implementationType?.ToString());

            if (implementationType.IsSubclassOf(MonoBehaviour.Il2CppType))
            {
                MelonLoader.MelonLogger.Log("It is a subclass of MonoBehaviour.");
                this.Implementation = this.gameObject.AddComponent(implementationType);
            }
            else
            {
                MelonLoader.MelonLogger.Log("It is not a subclass of MonoBehaviour.");
                this.Implementation = Il2CppSystem.Activator.CreateInstance(implementationType);
            }

            if (this.Implementation == null)
            {
                MelonLoader.MelonLogger.Log("Implementation is null!!!!!!!!!!!!!!!!");
                return;
            }

            MelonLoader.MelonLogger.Log("Trying to create delegates");
            OnEquipped = CreateImplementationActionDelegate("OnEquipped");
            OnUnequipped = CreateImplementationActionDelegate("OnUnequipped");

            OnPrimaryAction = CreateImplementationActionDelegate("OnPrimaryAction");
            OnSecondaryAction = CreateImplementationActionDelegate("OnSecondaryAction");

            OnControlModeChangedWhileEquipped = CreateImplementationActionDelegate("OnControlModeChangedWhileEquipped");
            MelonLoader.MelonLogger.Log("Delegates created");

            UnhollowerBaseLib.Il2CppReferenceArray<Il2CppSystem.Reflection.PropertyInfo> properties = implementationType.GetProperties();
            foreach (Il2CppSystem.Reflection.PropertyInfo property in properties)
            {
                MelonLoader.MelonLogger.Log(property.Name);
                if (property.Name == "EquippableModComponent")
                {
                    
                }
            }

            Il2CppSystem.Reflection.FieldInfo fieldInfo = null;
            UnhollowerBaseLib.Il2CppReferenceArray<Il2CppSystem.Reflection.FieldInfo> fields = implementationType.GetFields();
            MelonLoader.MelonLogger.Log(fields.Length);
            foreach(Il2CppSystem.Reflection.FieldInfo field in fields)
            {
                MelonLoader.MelonLogger.Log(field.Name);
                if (field.Name == "EquippableModComponent")
                {
                    fieldInfo = field;
                }
            }
            //Il2CppSystem.Reflection.FieldInfo fieldInfo = implementationType.GetField("EquippableModComponent", Il2CppSystem.Reflection.BindingFlags.Instance); //, Il2CppSystem.Reflection.BindingFlags.NonPublic , Il2CppSystem.Reflection.BindingFlags.Public);
            if (fieldInfo != null && fieldInfo.FieldType == EquippableModComponent.Il2CppType)
            {
                MelonLoader.MelonLogger.Log("Setting field info");
                fieldInfo.SetValue(Implementation, this);
            }
            else if(fieldInfo == null)
            {
                MelonLoader.MelonLogger.Log("Field Info was null");
            }
            else
            {
                MelonLoader.MelonLogger.Log(fieldInfo.FieldType.ToString());
            }

            //RubberDuckTest rubberDuckTest = Implementation.Cast<RubberDuckTest>();
            //rubberDuckTest.ModComponent = this;

            MelonLoader.MelonLogger.Log("End of Awake");
        }

        [HideFromIl2Cpp]
        private Il2CppSystem.Action CreateImplementationActionDelegate(string methodName)
        {
            Il2CppSystem.Reflection.MethodInfo methodInfo = null;
            UnhollowerBaseLib.Il2CppReferenceArray<Il2CppSystem.Reflection.MethodInfo> methods = Implementation.GetIl2CppType().GetMethods();
            foreach(Il2CppSystem.Reflection.MethodInfo method in methods)
            {
                //MelonLoader.MelonLogger.Log(method.Name);
                if(method.Name == methodName)
                {
                    methodInfo = method;
                }
            }


            //Il2CppSystem.Reflection.MethodInfo methodInfo = Implementation.GetIl2CppType().GetMethod(methodName);//, Il2CppSystem.Reflection.BindingFlags.Instance);//| BindingFlags.NonPublic | BindingFlags.Public);
            if (methodInfo == null)
            {
                MelonLoader.MelonLogger.Log("Could not create a delegate for '{0}'", methodName);
                return null;
            }
            //MelonLoader.MelonLogger.Log("Right before delegate creation");
            Il2CppSystem.Delegate d = Il2CppSystem.Delegate.CreateDelegate(UnhollowerRuntimeLib.Il2CppType.Of<Il2CppSystem.Action>(), Implementation, methodInfo);
            //MelonLoader.MelonLogger.Log("Right before delegate casting");
            Il2CppSystem.Action action = d.Cast<Il2CppSystem.Action>();
            //MelonLoader.MelonLogger.Log("Right before return");
            MelonLoader.MelonLogger.Log("Delegate created for '{0}'", methodName);
            return action;
        }

        /*[HideFromIl2Cpp]
        private Action CreateImplementationActionDelegate(string methodName, string filepath)
        {
            MelonLoader.MelonLogger.Log("got inside");
            System.Type type = Assembly.LoadFile(filepath).GetType(ImplementationType);
            MelonLoader.MelonLogger.Log("Loaded Type");
            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            MelonLoader.MelonLogger.Log("Got method");
            if (methodInfo == null)
            {
                MelonLoader.MelonLogger.Log("Could not create a delegate for '{0}", methodName);
                return null;
            }
            MelonLoader.MelonLogger.Log("Right before the end");
            //return (Action)Delegate.CreateDelegate(typeof(Action), Implementation, methodInfo);
            return (Action)Delegate.CreateDelegate(typeof(Action), methodInfo);
        }*/

        public EquippableModComponent(IntPtr intPtr) : base(intPtr) { }
    }
}
