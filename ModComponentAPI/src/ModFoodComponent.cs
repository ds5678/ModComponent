using UnityEngine;

namespace ModComponentAPI
{
    public class ModFoodComponent : ModCookableComponent
    {
        [Header("Food/Decay")]
        [Tooltip("0 means 'Never'. This overrides the Basic Property 'DaysToDecay'.")]
        public int DaysToDecayOutdoors;
        [Tooltip("0 means 'Never'. This overrides the Basic Property 'DaysToDecay'.")]
        public int DaysToDecayIndoors;

        [Header("Food/Calories")]
        [Tooltip("For one complete item with all servings. Calories remaining will scale with weight.")]
        public int Calories;
        [Tooltip("The number of servings contained in this item. Each consumation will be limited to one serving. 1 means 'Comsume completely' - the way all pre-existing food items work.")]
        [Range(1, 10)]
        public int Servings = 1;

        [Header("Food/Eating")]
        [Tooltip("Realtime seconds it takes to eat one complete serving.")]
        [Range(1, 10)]
        public int EatingTime = 1;
        [Tooltip("Sound to use when the item is either unpackaged or already open")]
        public string EatingAudio;
        [Tooltip("Sound to use when the item is still packaged and unopened. Leave empty for unpackaged food")]
        public string EatingPackagedAudio;
        [Tooltip("How does this affect your thirst? Represents change in percentage points. Negative values increase thirst, positive values reduce thirst.")]
        [Range(-100, 100)]
        public int ThirstEffect;

        [Header("Food/Food Poisoning")]
        [Range(0, 100)]
        [Tooltip("Chance in percent to contract food poisoning from an item above 20% condition")]
        public int FoodPoisoning;
        [Range(0, 100)]
        [Tooltip("Chance in percent to contract food poisoning from an item below 20% condition")]
        public int FoodPoisoningLowCondition;

        [Header("Food/Parasites")]
        [Tooltip("Parasite Risk increments in percent for each unit eaten. Leave empty for no parasite risk.")]
        public float[] ParasiteRiskIncrements;

        [Header("Food/Type")]
        [Tooltip("Is the food item naturally occurring meat or plant?")]
        public bool Natural;
        [Tooltip("Is the food item raw or cooked?")]
        public bool Raw;
        [Tooltip("Is the food item something to drink? (This mainly affects the names of actions and position in the radial menu)")]
        public bool Drink;
        [Tooltip("Is the food item meat directly from an animal? (E.g. wolf steak, but not beef jerky - mainly for statistics)")]
        public bool Meat;
        [Tooltip("Is the food item fish directly from an animal? (E.g. salmon, but not canned sardines - mainly for statistics)")]
        public bool Fish;
        [Tooltip("Is the food item canned?")]
        public bool Canned;

        [Header("Food/Opening")]
        [Tooltip("Does this item require a tool for opening it? If not enabled, the other settings in this section will be ignored.")]
        public bool Opening;
        [Tooltip("Can it be opened with a can opener?")]
        public bool OpeningWithCanOpener;
        [Tooltip("Can it be opened with a knife?")]
        public bool OpeningWithKnife;
        [Tooltip("Can it be opened with a hatchet?")]
        public bool OpeningWithHatchet;
        [Tooltip("Can it be opened by smashing?")]
        public bool OpeningWithSmashing;

        [Header("Food/Fatigue")]
        [Tooltip("Does this item affect 'Rest'? If not enabled, the other settings in this section will be ignored.")]
        public bool AffectRest;
        [Range(-100, 100)]
        [Tooltip("How much 'Rest' is restored/drained immediately after consuming the item. Represents change in percentage points. Negative values drain rest, positive values restore rest.")]
        public float InstantRestChange;
        [Tooltip("Factor for scaling how fast 'Rest' is drained after the item was consumed. Values below 1 drain less 'Rest' than normal, values above 1 drain more 'Rest' than normal. Applies to standing, sprinting, ...")]
        [Range(0, 10)]
        public float RestFactor = 1;
        [Tooltip("Amount of in-game minutes the 'RestFactor' will be applied.")]
        [Range(1, 600)]
        public int RestFactorMinutes = 60;

        [Header("Food/Cold")]
        [Tooltip("Does this item affect 'Cold'? If not enabled, the other settings in this section will be ignored.")]
        public bool AffectCold;
        [Range(-100, 100)]
        [Tooltip("How much 'Cold' is restored/drained immediately after consuming the item. Represents change in percentage points. Negative values make it feel colder, positive values make it feel warmer.")]
        public float InstantColdChange = 20;
        [Tooltip("Factor for scaling how fast 'Cold' is drained after the item was consumed. Values below 1 drain less 'Cold' than normal, values above 1 drain more 'Cold' than normal.")]
        [Range(0, 10)]
        public float ColdFactor = 0.5f;
        [Tooltip("Amount of in-game minutes the 'ColdFactor' will be applied.")]
        [Range(1, 600)]
        public int ColdFactorMinutes = 60;

        [Header("Food/Alcohol")]
        [Tooltip("Does this item contain Alcohol? If not enabled, the other settings in this section will be ignored.")]
        public bool ContainsAlcohol;
        [Tooltip("How much of the item's weight is alcohol?")]
        [Range(0, 100)]
        public float AlcoholPercentage;
        [Tooltip("How many in-game minutes does it take for the alcohol to be fully absorbed? This is scaled by current hunger level (the hungrier the faster). The simulated blood alcohol level will slowly raise over this time. Real-life value is around 45 mins for liquids.")]
        [Range(15, 120)]
        public float AlcoholUptakeMinutes = 45;
    }
}
