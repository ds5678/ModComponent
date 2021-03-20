using System;
using System.Reflection;
using UnhollowerBaseLib.Attributes;

namespace ModComponentAPI
{
    public class ModRifleComponent : EquippableModComponent
    {
        //[Header("Rifle/General")]
        //[Tooltip("The maximum number of round that can be loaded at any given time.")]
        public int ClipSize;
        //[Tooltip("Raw damage per shot in HP. This is modified by the target's damage modifier. The Rifle skill will increase the damage by 20% at the highest level!")]
        public int DamagePerShot;
        //[Tooltip("Range in meters. Beyond this distance, hits will not inflict damage. The Rifle skill will increase the range by 40% at the highest level!")]
        public int Range;

        //[Header("Rifle/Timing")]
        //[Tooltip("Time in seconds before the gun can shoot again.")]
        //[Range(0.0f, 10)]
        public float FiringDelay = 2.8f;
        //[Tooltip("Time in seconds before the gun can be used after loading it.")]
        //[Range(0.0f, 10)]
        public float ReloadDelay = 2f;
        //[Tooltip("Time in seconds before the gun can shoot after it went into 'Aim' mode.")]
        //[Range(0.0f, 10)]
        public float AimDelay = 0f;
        //[Tooltip("Time in seconds between firing the gun and the muzzle flash appearing")]
        //[Range(0.0f, 1)]
        public float MuzzleFlashDelay = 0.1f;
        //[Tooltip("Time in seconds between firing the gun and the muzzle smoke appearing")]
        //[Range(0.0f, 1)]
        public float MuzzleSmokeDelay = 0.15f;

        //[Header("Rifle/Sway")]
        //[Tooltip("The minimum amount of sway experience with zero fatigue. (unknown unit, Hunting Rifle: 0)")]
        //[Range(0, 1)]
        public float MinSway = 0;
        //[Tooltip("The minimum amount of sway experience with maximum fatigue. (unknown unit, Hunting Rifle: 0.75)")]
        //[Range(0, 1)]
        public float MaxSway = 0.75f;
        //[Tooltip("The amount sway increases for every second in 'Aim' mode. (unknown unit, Hunting Rifle: 0.1)")]
        //[Range(0, 1)]
        public float SwayIncrement = 0.1f;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModRifleComponent>(this);
            if (string.IsNullOrEmpty(ImplementationType))
            {
                return;
            }

            //GameObject equippedModelPrefab = Resources.Load(EquippedModelPrefabName)?.Cast<GameObject>();
            //if (equippedModelPrefab) EquippedModel = GameObject.Instantiate(equippedModelPrefab);

            Type implementationType = TypeResolver.Resolve(ImplementationType);
            this.Implementation = Activator.CreateInstance(implementationType);

            if (this.Implementation == null)
            {
                return;
            }

            OnEquipped = CreateImplementationActionDelegate("OnEquipped");
            OnUnequipped = CreateImplementationActionDelegate("OnUnequipped");

            OnPrimaryAction = CreateImplementationActionDelegate("OnPrimaryAction");
            OnSecondaryAction = CreateImplementationActionDelegate("OnSecondaryAction");

            OnControlModeChangedWhileEquipped = CreateImplementationActionDelegate("OnControlModeChangedWhileEquipped");

            FieldInfo fieldInfo = implementationType.GetField("EquippableModComponent", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (fieldInfo != null && fieldInfo.FieldType == typeof(EquippableModComponent))
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

        public ModRifleComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}
