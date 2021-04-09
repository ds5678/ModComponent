using Harmony;
using ModComponentAPI;
using System;

namespace ModComponentMapper.patches
{
    /*[HarmonyPatch(typeof(Hunger), "AddReserveCaloriesOverTime")]//Exists
    class HungerPatch
    {
        private static void Prefix(FoodItem fi, ref float calories)
        {
            Logger.Log("Servings!!!!!!!!!!!!!!!!!!!");
            //ModFoodComponent modFoodComponent = ComponentUtils.GetComponent<ModFoodComponent>(fi);

            //if (modFoodComponent != null && modFoodComponent.Servings > 1)
            //{
            //    calories = Math.Min(calories, (float)modFoodComponent.Calories / modFoodComponent.Servings);
            //}
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UseFoodInventoryItem")]
    internal class HungerTest1
    {
        private static void Prefix(GearItem gi)
        {
            ModFoodComponent modFoodComponent = ComponentUtils.GetComponent<ModFoodComponent>(gi);
            FoodItem foodItem = gi.m_FoodItem;
            Hunger hunger = GameManager.GetHungerComponent();
            Thirst thirst = GameManager.GetThirstComponent();

            FillTracker.actualMaxCalories = hunger.m_MaxReserveCalories;
            FillTracker.actualThirst = thirst.m_CurrentThirst;
            Logger.Log("Max calories before: '{0}'", FillTracker.actualMaxCalories);

            if (modFoodComponent && modFoodComponent.Servings > 1)
            {
                float calorieSpace = hunger.m_MaxReserveCalories - hunger.m_CurrentReserveCalories;
                Logger.Log("Calorie Space: '{0}'", calorieSpace);
                float maxCaloriesToAdd = Math.Min(calorieSpace, (float)modFoodComponent.Calories / modFoodComponent.Servings);
                Logger.Log("Max Calories to add: '{0}'", maxCaloriesToAdd);
                hunger.m_MaxReserveCalories = hunger.m_CurrentReserveCalories + maxCaloriesToAdd;
                FillTracker.isConsumingServing = true;
                Logger.Log("Reassigned Max Reserve Calories: '{0}'", hunger.m_MaxReserveCalories);

                if (foodItem.m_ReduceThirst > 0f && foodItem.m_IsDrink)
                {

                }
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "OnEatingComplete")]
    internal class PlayerManager_OnEatingComplete
    {
        private static void Postfix()
        {
            Hunger hunger = GameManager.GetHungerComponent();
            Logger.Log("Before reverting Max Reserve Calories: '{0}'", hunger.m_MaxReserveCalories);
            hunger.m_MaxReserveCalories = FillTracker.actualMaxCalories;
            FillTracker.isConsumingServing = false;
        }
    }

    [HarmonyPatch(typeof(StatusBar), "GetFillValue")]
    internal class StatusBar_GetFillValue
    {
        private static void Postfix(StatusBar __instance, ref float __result)
        {
            if (FillTracker.isConsumingServing && __instance.m_StatusBarType == StatusBar.StatusBarType.Hunger)
            {
                float calories = Math.Max(GameManager.GetHungerComponent().m_CurrentReserveCalories, 0f);
                __result = calories / FillTracker.actualMaxCalories;
            }
        }
    }

    internal static class FillTracker
    {
        public static float actualMaxCalories;
        public static float actualThirst;
        public static bool isConsumingServing = false;
    }*/


}
