using ModComponent.Utils;
using System;
using System.Reflection;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public abstract class EquippableModComponent : ModBaseComponent
	{
		public EquippableModComponent(IntPtr intPtr) : base(intPtr) { }

		/// <summary>
		/// The GameObject to be used for representing the item while it is equipped.<br/>
		/// The position, rotation and scale of this prefab will be used for rendering. <br/>
		/// Use the 'Weapon Camera' to tune the values.
		/// </summary>
		public string EquippedModelPrefabName;

		/// <summary>
		/// The name of the type implementing the specific game logic of this item.<br/>
		/// If this is an assembly-qualified name (Namespace.TypeName,Assembly) it will be loaded from the given assembly.<br/>
		/// If the assembly is omitted (Namespace.TypeName), the type will be loaded from the first assembly that contains a type with the given name.<br/>
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

		#region EquippableCalls
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
		/// This runs when the player does certain things like enter/exit a car.
		/// </summary>
		public Action OnControlModeChangedWhileEquipped;
		#endregion

		#region MonobehaviourCalls
		/// <summary>
		/// According to the Unity documentation, Awake is the first script to run.
		/// </summary>
		public Action OnAwake;

		/// <summary>
		/// OnEnable is called whenever the game object is enabled.
		/// </summary>
		public Action OnEnabled;

		/// <summary>
		/// Start is only ever called once.
		/// </summary>
		public Action OnStart;

		/// <summary>
		/// Executes every frame.
		/// </summary>
		public Action OnUpdate;

		/// <summary>
		/// Executes after all the Update calls
		/// </summary>
		public Action OnLateUpdate;

		/// <summary>
		/// Executes when the gameobject is disabled
		/// </summary>
		public Action OnDisabled;
		#endregion

		/// <summary>
		/// Creates an action delegate from a method in the loaded Implementation.
		/// </summary>
		/// <param name="methodName"></param>
		/// <returns></returns>
		[HideFromIl2Cpp]
		protected Action CreateImplementationActionDelegate(string methodName)
		{
			MethodInfo methodInfo = Implementation.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if (methodInfo == null) return null;

			return (Action)Delegate.CreateDelegate(typeof(Action), Implementation, methodInfo);
		}


		protected virtual void Awake()
		{
			if (string.IsNullOrEmpty(ImplementationType)) return;

			//ignoring monobehaviour
			Type implementationTypeMono = TypeResolver.Resolve(ImplementationType, true);
			this.Implementation = Activator.CreateInstance(implementationTypeMono);

			//including monobehaviour
			/*Type implementationTypeMono = TypeResolver.Resolve(ImplementationType, false);
            Il2CppSystem.Type implementationTypeIl2Cpp = TypeResolver.ResolveIl2Cpp(ImplementationType, false);
            if (TypeResolver.InheritsFromMonobehaviour(implementationTypeIl2Cpp))
            {
                this.Implementation = this.gameObject.AddComponent(implementationTypeIl2Cpp);
            }
            else if (!(implementationTypeMono == null))
            {
                this.Implementation = Activator.CreateInstance(implementationTypeMono);
            }*/

			if (this.Implementation == null) return;

			OnEquipped = CreateImplementationActionDelegate("OnEquipped");
			OnUnequipped = CreateImplementationActionDelegate("OnUnequipped");

			OnPrimaryAction = CreateImplementationActionDelegate("OnPrimaryAction");
			OnSecondaryAction = CreateImplementationActionDelegate("OnSecondaryAction");

			OnControlModeChangedWhileEquipped = CreateImplementationActionDelegate("OnControlModeChangedWhileEquipped");

			OnAwake = CreateImplementationActionDelegate("OnAwake");
			OnEnabled = CreateImplementationActionDelegate("OnEnabled");
			OnStart = CreateImplementationActionDelegate("OnStart");
			OnUpdate = CreateImplementationActionDelegate("OnUpdate");
			OnLateUpdate = CreateImplementationActionDelegate("OnLateUpdate");
			OnDisabled = CreateImplementationActionDelegate("OnDisabled");


			FieldInfo fieldInfo = implementationTypeMono.GetField("EquippableModComponent", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if (fieldInfo != null && fieldInfo.FieldType == typeof(EquippableModComponent))
			{
				fieldInfo.SetValue(Implementation, this);
			}

			if (OnAwake != null) OnAwake.Invoke();
		}

		protected virtual void OnEnable()
		{
			if (OnEnabled != null) OnEnabled.Invoke();
		}

		protected virtual void Start()
		{
			if (OnStart != null) OnStart.Invoke();
		}

		protected virtual void Update()
		{
			if (OnUpdate != null) OnUpdate.Invoke();
		}

		protected virtual void LateUpdate()
		{
			if (OnLateUpdate != null) OnLateUpdate.Invoke();
		}

		protected virtual void OnDisable()
		{
			if (OnDisabled != null) OnDisabled.Invoke();
		}
	}
}
