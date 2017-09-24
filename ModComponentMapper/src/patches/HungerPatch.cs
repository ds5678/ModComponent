using Harmony;
using ModComponentAPI;
using System;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Hunger), "AddReserveCaloriesOverTime")]
    class HungerPatch
    {
        public static void Prefix(FoodItem fi, ref float calories)
        {
            ModFoodComponent modFoodComponent = ModUtils.GetComponent<ModFoodComponent>(fi);

            if (modFoodComponent != null && modFoodComponent.Servings > 1)
            {
                float requestedCalories = calories;
                calories = Math.Min(calories, (float)modFoodComponent.Calories / modFoodComponent.Servings);
                UnityEngine.Debug.Log("Consuming multi-serving food item: adjusted consumed calories from " + requestedCalories + " to " + calories + " (of " + fi.m_CaloriesRemaining + " calories remaining)");
            }
        }
    }
}
