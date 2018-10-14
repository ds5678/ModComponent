using System;

using UnityEngine;

namespace ModComponentAPI
{
    public class AttachBehaviour : MonoBehaviour
    {
        [Tooltip("The name of the class to be attached.\n" +
            "This class must extend UnityEngine.MonoBehaviour.\n" +
            "If this is an assembly-qualified name (Namespace.TypeName,Assembly) it will be loaded from the given assembly. If the assembly is omitted (Namespace.TypeName), the type will be loaded from the first assembly that contains a type with the given name.")]
        public string BehaviourName;

        [Tooltip("Should this fail if the behaviour cannot be loaded or attached?")]
        public bool FailOnError = true;

        public void Start()
        {
            try
            {
                Type behaviourType = TypeResolver.Resolve(BehaviourName);
                this.gameObject.AddComponent(behaviourType);
            }
            catch (Exception e)
            {
                Debug.LogError("[Mod-Component] Could not load behaviour '" + BehaviourName + "': " + e.Message);

                if (FailOnError)
                {
                    throw e;
                }
            }
        }
    }
}