using ModComponent.Utils;
using System;
using System.Reflection;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModClothingComponent : ModBaseComponent
	{
		/// <summary>
		/// The body region this clothing item can be worn.
		/// </summary>
		public BodyRegion Region;

		/// <summary>
		/// The innermost layer at which the clothing item can be worn.<br/>
		/// From innermost to outermost: Base, Mid, Top, Top2.<br/>
		/// Examples: Legs + Base = Longjohns, Legs + Top = Pants; Feet + Mid = Socks, Feet + Top = Boots
		/// </summary>
		public Layer MinLayer;

		/// <summary>
		/// The outermost layer at which the clothing item can be worn.<br/>
		/// From innermost to outermost: Base, Mid, Top, Top2.<br/>
		/// Examples: Legs + Base = Longjohns, Legs + Top = Pants; Feet + Mid = Socks, Feet + Top = Boots
		/// </summary>
		public Layer MaxLayer;

		/// <summary>
		/// The type of sound to make when moving while wearing this clothing item.
		/// </summary>
		public MovementSounds MovementSound;

		/// <summary>
		/// The type footwear (as in Boots) this clothing item represents. Leave at 'None' if it is not a footwear item at all.
		/// </summary>
		public FootwearType Footwear;


		/// <summary>
		/// Number of days it takes for this clothing item to decay from 100% to 0% while being worn and outside. 0 means 'Does not decay from being worn'.
		/// </summary>
		public float DaysToDecayWornOutside;

		/// <summary>
		/// Number of days it takes for this clothing item to decay from 100% to 0% while being worn and inside. 0 means 'Does not decay from being worn'.
		/// </summary>
		public float DaysToDecayWornInside;


		/// <summary>
		/// Warmth bonus in degrees celsius when the clothing item is in perfect condition and completely dry.<br/>
		/// The actual bonus value will scale with condition and wetness.
		/// </summary>
		public float Warmth;

		/// <summary>
		/// Warmth bonus in degrees celsius when the clothing item is in perfect condition and completely wet.<br/>
		/// The actual bonus value will scale with condition and wetness.
		/// </summary>
		public float WarmthWhenWet;

		/// <summary>
		/// Windproof bonus in degrees celsius when the clothing item is in perfect condition and completely wet.<br/>
		/// The actual bonus value will scale with condition and wetness.
		/// </summary>
		public float Windproof;

		/// <summary>
		/// Damage reduction in per cent when receiving certain types of damage (e.g. a coat protects against wolves, but not falling).<br/>
		/// 100 means 'Receive no damage', 0 means 'Receive full damage'. Actual bonus will scale with condition.
		/// </summary>
		public float Toughness;

		/// <summary>
		/// Sprint stamina reduction in per cent. 100 means 'No sprint stamina', 0 means 'Full sprint stamina'.
		/// </summary>
		public float SprintBarReduction;

		/// <summary>
		/// How much water is repelled by this clothing item? 100 means 'never gets wet'
		/// </summary>
		public float Waterproofness;


		/// <summary>
		/// Decreases the chance that a wolf will attack. Only applies in certain situations.<br/>
		/// 100 means 'guaranteed not to attack'; 0 means 'same as without the buff'
		/// </summary>
		public int DecreaseAttackChance;

		/// <summary>
		/// Increases the chance that a wolf will flee immediately when spotting the player.<br/>
		/// 100 means 'guaranteed to flee'; 0 means 'same as without the buff'
		/// </summary>
		public int IncreaseFleeChance;


		/// <summary>
		/// Hours required to dry this clothing item next to a fire when it is completely wet.<br/>
		/// That's the same amount of time it takes to unfreeze, so a completely frozen item will take twice this time to be completely dry again.
		/// </summary>
		public float HoursToDryNearFire;

		/// <summary>
		/// Hours required to dry this clothing item without a fire when it is completely wet.<br/>
		/// That's the same amount of time it takes to unfreeze, so a completely frozen item will take twice this time to be completely dry again.
		/// </summary>
		public float HoursToDryWithoutFire;

		/// <summary>
		/// Hours required for this clothing to completely freeze once it got wet.
		/// </summary>
		public float HoursToFreeze;


		/// <summary>
		/// Base name of the texture to represent this clothing item in the paper doll view.<br/>
		/// All required actual texture paths will be derived from this name.
		/// </summary>
		public string MainTexture;

		/// <summary>
		/// Name of the blend texture used for the paper doll view.
		/// </summary>
		public string BlendTexture;

		/// <summary>
		/// Drawing layer (as in drawing order) to be used for this clothing item.<br/>
		/// Items with higher values are drawn over items with lower values.<br/>
		/// Set to zero for the default value on that slot.
		/// </summary>
		public int DrawLayer;


		/// <summary>
		/// The name of the type implementing the specific game logic of this item.<br/>
		/// Use 'Namespace.TypeName,AssemblyName', e.g. 'ClothingPack.SkiGogglesImplementation,Clothing-Pack'.<br/>
		/// Leave empty if this item needs no special game logic.
		/// </summary>
		public string ImplementationType;

		/// <summary>
		/// An instance of the implementation that contains OnPutOn and OnTakeOff.
		/// </summary>
		public object Implementation;

		/// <summary>
		/// The action that runs when a custom clothing item is put on.
		/// </summary>
		public Action OnPutOn;

		/// <summary>
		/// The action that runs when a custom clothing item is taken off.
		/// </summary>
		public Action OnTakeOff;

		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModClothingComponent>(this);

			if (string.IsNullOrEmpty(ImplementationType)) return;

			Type implementationType = TypeResolver.Resolve(ImplementationType, true);
			object implementation = Activator.CreateInstance(implementationType);
			if (implementation == null) return;

			Implementation = implementation;

			OnPutOn = CreateImplementationActionDelegate("OnPutOn");
			OnTakeOff = CreateImplementationActionDelegate("OnTakeOff");
		}

		[HideFromIl2Cpp]
		private Action CreateImplementationActionDelegate(string methodName)
		{
			MethodInfo methodInfo = Implementation.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			if (methodInfo == null) return null;

			return (Action)Delegate.CreateDelegate(typeof(Action), Implementation, methodInfo);
		}

		public ModClothingComponent(IntPtr intPtr) : base(intPtr) { }

		public enum BodyRegion
		{
			Head,
			Hands,
			Chest,
			Legs,
			Feet,
			Accessory,
		}

		public enum Layer
		{
			Base,
			Mid,
			Top,
			Top2,
		}

		public enum FootwearType
		{
			None,
			Boots,
			Deerskin,
			Shoes,
		}

		public enum MovementSounds
		{
			None,
			HeavyNylon,
			LeatherHide,
			LightCotton,
			LightNylon,
			SoftCloth,
			Wool,
		}
	}
}
