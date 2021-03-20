using System;
using System.Reflection;
using UnhollowerBaseLib.Attributes;

namespace ModComponentAPI
{
    public enum Region
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

    public enum Footwear
    {
        None,
        Boots,
        Deerskin,
        Shoes,
    }

    public enum MovementSound
    {
        None,
        HeavyNylon,
        LeatherHide,
        LightCotton,
        LightNylon,
        SoftCloth,
        Wool,
    }

    public class ModClothingComponent : ModComponent
    {
        //[Header("Wearing")]
        //[Tooltip("The body region this clothing item can be worn.")]
        public Region Region;
        //[Tooltip("The innermost layer at which the clothing item can be worn. From innermost to outermost: Base, Mid, Top, Top2. Examples: Legs + Base = Longjohns, Legs + Top = Pants; Feet + Mid = Socks, Feet + Top = Boots")]
        public Layer MinLayer;
        //[Tooltip("The outermost layer at which the clothing item can be worn. From innermost to outermost: Base, Mid, Top, Top2. Examples: Legs + Base = Longjohns, Legs + Top = Pants; Feet + Mid = Socks, Feet + Top = Boots")]
        public Layer MaxLayer;
        //[Tooltip("The type of sound to make when moving while wearing this clothing item.")]
        public MovementSound MovementSound;
        //[Tooltip("The type footwear (as in Boots) this clothing item represents. Leave at 'None' if it is not a footwear item at all.")]
        public Footwear Footwear;

        //[Header("Decay")]
        //[Tooltip("Number of days it takes for this clothing item to decay from 100% to 0% while being worn and outside. 0 means 'Does not decay from being worn'.")]
        public float DaysToDecayWornOutside;
        //[Tooltip("Number of days it takes for this clothing item to decay from 100% to 0% while being worn and inside. 0 means 'Does not decay from being worn'.")]
        public float DaysToDecayWornInside;

        //[Header("Protection")]
        //[Tooltip("Warmth bonus in degrees celsius when the clothing item is in perfect condition and completely dry. The actual bonus value will scale with condition and wetness.")]
        public float Warmth;
        //[Tooltip("Warmth bonus in degrees celsius when the clothing item is in perfect condition and completely wet. The actual bonus value will scale with condition and wetness.")]
        public float WarmthWhenWet;
        //[Tooltip("Windproof bonus in degrees celsius when the clothing item is in perfect condition and completely wet. The actual bonus value will scale with condition and wetness.")]
        public float Windproof;
        //[Tooltip("Damage reduction in per cent when receiving certain types of damage (e.g. a coat protects against wolves, but not falling). 100 means 'Receive no damage', 0 means 'Receive full damage'. Actual bonus will scale with condition.")]
        //[Range(0, 100)]
        public float Toughness;
        //[Tooltip("Sprint stamina reduction in per cent. 100 means 'No sprint stamina', 0 means 'Full sprint stamina'.")]
        //[Range(0, 100)]
        public float SprintBarReduction;
        //[Range(0, 100)]
        //[Tooltip("How much water is repelled by this clothing item? 100 means 'never gets wet'")]
        public float Waterproofness;

        //[Header("Wolf Intimidation")]
        //[Tooltip("Decreases the chance that a wolf will attack. Only applies in certain situations. 100 means 'guaranteed not to attack'; 0 means 'same as without the buff'")]
        //[Range(0, 100)]
        public int DecreaseAttackChance;
        //[Tooltip("Increases the chance that a wolf will flee immediately when spotting the player. 100 means 'guaranteed to flee'; 0 means 'same as without the buff'")]
        //[Range(0, 100)]
        public int IncreaseFleeChance;

        //[Header("Drying & Freezing")]
        //[Tooltip("Hours required to dry this clothing item next to a fire when it is completely wet. That's the same amount of time it takes to unfreeze, so a completely frozen item will take twice this time to be completely dry again.")]
        public float HoursToDryNearFire;
        //[Tooltip("Hours required to dry this clothing item without a fire when it is completely wet. That's the same amount of time it takes to unfreeze, so a completely frozen item will take twice this time to be completely dry again.")]
        public float HoursToDryWithoutFire;
        //[Tooltip("Hours required for this clothing to completely freeze once it got wet.")]
        public float HoursToFreeze;

        //[Header("Textures")]
        //[Tooltip("Base name of the texture to represent this clothing item in the paper doll view. All required actual texture paths will be derived from this name.")]
        public string MainTexture;
        //[Tooltip("Name of the blend texture used for the paper doll view.")]
        public string BlendTexture;
        //[Tooltip("Drawing layer (as in drawing order) to be used for this clothing item. Items with higher values are drawn over items with lower values. Set to zero for the default value on that slot.")]
        public int DrawLayer;

        //[Header("Implementation")]
        //[Tooltip("The name of the type implementing the specific game logic of this item. Use 'Namespace.TypeName,AssemblyName', e.g. 'ClothingPack.SkiGogglesImplementation,Clothing-Pack'. Leave empty if this item needs no special game logic.")]
        public string ImplementationType;

        //[HideInInspector]
        public object Implementation;

        //[HideInInspector]
        public Action OnPutOn;

        //[HideInInspector]
        public Action OnTakeOff;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModClothingComponent>(this);

            if (string.IsNullOrEmpty(ImplementationType))
            {
                return;
            }
            //MelonLoader.MelonLogger.Log("ImplementationType for '{0}' is not empty.", this.name);

            //Type implementationType = Type.GetType(ImplementationType);
            Type implementationType = TypeResolver.Resolve(ImplementationType);
            object implementation = Activator.CreateInstance(implementationType);
            if (implementation == null)
            {
                return;
            }

            Implementation = implementation;

            OnPutOn = CreateImplementationActionDelegate("OnPutOn");
            OnTakeOff = CreateImplementationActionDelegate("OnTakeOff");
        }

        [HideFromIl2Cpp]
        private Action CreateImplementationActionDelegate(string methodName)
        {
            MethodInfo methodInfo = Implementation.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (methodInfo == null)
            {
                return null;
            }

            return (Action)Delegate.CreateDelegate(typeof(Action), Implementation, methodInfo);
        }

        public ModClothingComponent(IntPtr intPtr) : base(intPtr) { }
    }
}
