using MelonLoader.TinyJSON;
using ModComponent.Utils;
using System;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModFoodComponent : ModCookableComponent
	{
		/// <summary>
		/// 0 means 'Never'.<br/>
		/// This overrides the Basic Property 'DaysToDecay'.
		/// </summary>
		public int DaysToDecayOutdoors;

		/// <summary>
		/// 0 means 'Never'.<br/>
		/// This overrides the Basic Property 'DaysToDecay'.
		/// </summary>
		public int DaysToDecayIndoors;


		/// <summary>
		/// For one complete item with all servings.<br/>
		/// Calories remaining will scale with weight.
		/// </summary>
		public int Calories;


		/// <summary>
		/// Realtime seconds it takes to eat one item.
		/// </summary>
		public int EatingTime = 1;

		/// <summary>
		/// Sound to use when the item is either unpackaged or already open.
		/// </summary>
		public string EatingAudio;

		/// <summary>
		/// Sound to use when the item is still packaged and unopened.<br/>
		/// Leave empty for unpackaged food.
		/// </summary>
		public string EatingPackagedAudio;

		/// <summary>
		/// How does this affect your thirst?<br/>
		/// Represents change in percentage points.<br/>
		/// Negative values increase thirst; positive values reduce thirst.
		/// </summary>
		public int ThirstEffect;


		/// <summary>
		/// Chance in percent to contract food poisoning from an item above 20% condition.
		/// </summary>
		public int FoodPoisoning;

		/// <summary>
		/// Chance in percent to contract food poisoning from an item below 20% condition.
		/// </summary>
		public int FoodPoisoningLowCondition;


		/// <summary>
		/// Parasite Risk increments in percent for each unit eaten.<br/>
		/// Leave empty for no parasite risk.
		/// </summary>
		public float[] ParasiteRiskIncrements;


		/// <summary>
		/// Is the food item naturally occurring meat or plant?
		/// </summary>
		public bool Natural;

		/// <summary>
		/// Is the food item raw or cooked?
		/// </summary>
		public bool Raw;

		/// <summary>
		/// Is the food item something to drink?<br/>
		/// (This mainly affects the names of actions and position in the radial menu)
		/// </summary>
		public bool Drink;

		/// <summary>
		/// Is the food item meat directly from an animal?<br/>
		/// (E.g. wolf steak, but not beef jerky - mainly for statistics)
		/// </summary>
		public bool Meat;

		/// <summary>
		/// Is the food item fish directly from an animal?<br/>
		/// (E.g. salmon, but not canned sardines - mainly for statistics)
		/// </summary>
		public bool Fish;

		/// <summary>
		/// Is the food item canned?<br/>
		/// Canned items will yield a 'Recycled Can' when opened properly.
		/// </summary>
		public bool Canned;


		/// <summary>
		/// Does this item require a tool for opening it?<br/>
		/// If not enabled, the other settings in this section will be ignored.
		/// </summary>
		public bool Opening;

		/// <summary>
		/// Can it be opened with a can opener?
		/// </summary>
		public bool OpeningWithCanOpener;

		/// <summary>
		/// Can it be opened with a knife?
		/// </summary>
		public bool OpeningWithKnife;

		/// <summary>
		/// Can it be opened with a hatchet?
		/// </summary>
		public bool OpeningWithHatchet;

		/// <summary>
		/// Can it be opened by smashing?
		/// </summary>
		public bool OpeningWithSmashing;


		/// <summary>
		/// Does this item affect 'Condition' while sleeping?<br/>
		/// If not enabled, the other settings in this section will be ignored.
		/// </summary>
		public bool AffectCondition;

		/// <summary>
		/// How much additional condition is restored per hour?
		/// </summary>
		public float ConditionRestBonus = 2;

		/// <summary>
		/// Amount of in-game minutes the 'ConditionRestBonus' will be applied.
		/// </summary>
		public float ConditionRestMinutes = 360;


		/// <summary>
		/// Does this item affect 'Rest'?<br/>
		/// If not enabled, the other settings in this section will be ignored.
		/// </summary>
		public bool AffectRest;

		/// <summary>
		/// How much 'Rest' is restored/drained immediately after consuming the item.<br/>
		/// Represents change in percentage points.<br/>
		/// Negative values drain rest; positive values restore rest
		/// </summary>
		public float InstantRestChange;

		/// <summary>
		/// Amount of in-game minutes the 'RestFactor' will be applied.
		/// </summary>
		public int RestFactorMinutes = 60;


		/// <summary>
		/// Does this item affect 'Cold'?<br/>
		/// If not enabled, the other settings in this section will be ignored.
		/// </summary>
		public bool AffectCold;

		/// <summary>
		/// How much 'Cold' is restored/drained immediately after consuming the item.<br/>
		/// Represents change in percentage points.<br/>
		/// Negative values make it feel colder; positive values make it feel warmer.
		/// </summary>
		public float InstantColdChange = 20;

		/// <summary>
		/// Amount of in-game minutes the 'ColdFactor' will be applied.
		/// </summary>
		public int ColdFactorMinutes = 60;


		/// <summary>
		/// Does this item contain Alcohol?<br/>
		/// If not enabled, the other settings in this section will be ignored.
		/// </summary>
		public bool ContainsAlcohol;

		/// <summary>
		/// How much of the item's weight is alcohol?
		/// </summary>
		public float AlcoholPercentage;

		/// <summary>
		/// How many in-game minutes does it take for the alcohol to be fully absorbed?<br/>
		/// This is scaled by current hunger level (the hungrier the faster).<br/>
		/// The simulated blood alcohol level will slowly raise over this time.<br/>
		/// Real-life value is around 45 mins for liquids.
		/// </summary>
		public float AlcoholUptakeMinutes = 45;

		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModFoodComponent>(this);
		}

		public ModFoodComponent(IntPtr intPtr) : base(intPtr) { }

		[HideFromIl2Cpp]
		internal override void InitializeComponent(ProxyObject dict, string className = "ModFoodComponent")
		{
			base.InitializeComponent(dict, className);
			this.DaysToDecayOutdoors = dict.GetVariant(className, "DaysToDecayOutdoors");
			this.DaysToDecayIndoors = dict.GetVariant(className, "DaysToDecayIndoors");

			this.Calories = dict.GetVariant(className, "Calories");
			this.EatingTime = dict.GetVariant(className, "EatingTime");

			this.EatingAudio = dict.GetVariant(className, "EatingAudio");
			this.EatingPackagedAudio = dict.GetVariant(className, "EatingPackagedAudio");

			this.ThirstEffect = dict.GetVariant(className, "ThirstEffect");

			this.FoodPoisoning = dict.GetVariant(className, "FoodPoisoning");
			this.FoodPoisoningLowCondition = dict.GetVariant(className, "FoodPoisoningLowCondition");
			this.ParasiteRiskIncrements = dict.GetFloatArray(className, "ParasiteRiskIncrements");

			this.Natural = dict.GetVariant(className, "Natural");
			this.Raw = dict.GetVariant(className, "Raw");
			this.Drink = dict.GetVariant(className, "Drink");
			this.Meat = dict.GetVariant(className, "Meat");
			this.Fish = dict.GetVariant(className, "Fish");

			this.Canned = dict.GetVariant(className, "Canned");
			this.Opening = dict.GetVariant(className, "Opening");
			this.OpeningWithCanOpener = dict.GetVariant(className, "OpeningWithCanOpener");
			this.OpeningWithKnife = dict.GetVariant(className, "OpeningWithKnife");
			this.OpeningWithHatchet = dict.GetVariant(className, "OpeningWithHatchet");
			this.OpeningWithSmashing = dict.GetVariant(className, "OpeningWithSmashing");

			this.AffectCondition = dict.GetVariant(className, "AffectCondition");
			this.ConditionRestBonus = dict.GetVariant(className, "ConditionRestBonus");
			this.ConditionRestMinutes = dict.GetVariant(className, "ConditionRestMinutes");

			this.AffectRest = dict.GetVariant(className, "AffectRest");
			this.InstantRestChange = dict.GetVariant(className, "InstantRestChange");
			this.RestFactorMinutes = dict.GetVariant(className, "RestFactorMinutes");

			this.AffectCold = dict.GetVariant(className, "AffectCold");
			this.InstantColdChange = dict.GetVariant(className, "InstantColdChange");
			this.ColdFactorMinutes = dict.GetVariant(className, "ColdFactorMinutes");

			this.ContainsAlcohol = dict.GetVariant(className, "ContainsAlcohol");
			this.AlcoholPercentage = dict.GetVariant(className, "AlcoholPercentage");
			this.AlcoholUptakeMinutes = dict.GetVariant(className, "AlcoholUptakeMinutes");
		}
	}
}
