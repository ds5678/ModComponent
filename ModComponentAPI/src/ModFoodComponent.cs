using UnityEngine;

namespace ModComponentAPI
{
    public class ModFoodComponent : ModComponent
    {
        [Header("Decay")]
        [Tooltip("0 means 'Never'. This overrides the Basic Property 'DaysToDecay'.")]
        public int DaysToDecayOutdoors;
        [Tooltip("0 means 'Never'. This overrides the Basic Property 'DaysToDecay'.")]
        public int DaysToDecayIndoors;

        [Header("Calories")]
        [Tooltip("For one complete item. Calories remaining will scale with weight.")]
        public int Calories;

        [Header("Eating")]
        [Tooltip("Realtime seconds it takes to eat one complete item.")]
        [Range(1, 10)]
        public int EatingTime = 1;
        [Range(-100, 100)]
        [Tooltip("How does this affect your thirst? Negative values increase thirst, positive values reduce thirst.")]
        public int ThirstEffect;

        [Header("Food Poisoning")]
        [Range(0, 100)]
        [Tooltip("Chance in percent to contract food poisoning from an item above 20% condition")]
        public int FoodPoisoning;
        [Range(0, 100)]
        [Tooltip("Chance in percent to contract food poisoning from an item below 20% condition")]
        public int FoodPoisoningLowCondition;

        [Header("Parasites")]
        [Tooltip("Parasite Risk increments in percent for each unit eaten. Leave empty for no parasite risk at all.")]
        public float[] ParasiteRiskIncrements;

        [Header("Audio/Eating")]
        [Tooltip("Sound to use when the item is either unpackaged or already open")]
        public string EatingAudio;
        [Tooltip("Sound to use when the item is still packaged and unopened. Leave empty for unpackaged food")]
        public string EatingPackagedAudio;

        [Header("Type")]
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

        [Header("Cooking")]
        public bool CanBeHeated;

        [Header("Fatigue")]
        [Tooltip("Does this item affect 'Rest'? If not enabled, the other settings in this section will be ignored.")]
        public bool AffectRest;
        [Range(-100, 100)]
        [Tooltip("How much 'Rest' is restored/drained immediately after consuming the item. -100 means 'Drain all', 0 means 'None', 100 means 'Restore all'.")]
        public float InstantRestChange;
        [Tooltip("Factor for scaling how fast 'Rest' is drained after the item was consumed. Values below 1 drain less 'Rest' than normal; values above 1 drain more 'Rest' than normal. Applies to standing, sprinting, ...")]
        [Range(0, 10)]
        public float RestFactor;
        [Tooltip("Amount of in-game minutes the 'RestScale' will be applied.")]
        [Range(0, 600)]
        public int RestFactorMinutes;

        [Header("Alcohol")]
        [Tooltip("Does this item contain Alcohol? If not enabled, the other settings in this section will be ignored.")]
        public bool ContainsAlcohol;
        [Tooltip("How much of the item's weight is alcohol")]
        [Range(0, 100)]
        public float AlcoholPercentage;
        [Tooltip("How many in-game minutes does it take for the alcohol to be fully absorbed. This is scaled by current hunger level (the hungrier the faster). The simulated blood alcohol level will slowly raise over this time. Real-life value is around 45 mins for liquids.")]
        [Range(15, 120)]
        public float AlcoholUptakeMinutes = 45;
    }
}
