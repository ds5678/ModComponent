using Harmony;
using ModComponentAPI;
using System;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    /*[HarmonyPatch(typeof(Hunger), "AddReserveCaloriesOverTime")]//Exists
    class HungerPatch
    {
        private static void Prefix(FoodItem fi, ref float calories)
        {
            Logger.Log("Servings!!!!!!!!!!!!!!!!!!!");
            //ModFoodComponent modFoodComponent = ModUtils.GetComponent<ModFoodComponent>(fi);

            //if (modFoodComponent != null && modFoodComponent.Servings > 1)
            //{
            //    calories = Math.Min(calories, (float)modFoodComponent.Calories / modFoodComponent.Servings);
            //}
        }
    }

    [HarmonyPatch(typeof(Hunger),"AddReserveCalories")]
    internal class HungerTest1
    {
        private static void Postfix()
        {
            //Logger.Log("AddReserveCalories");
        }
    }

    [HarmonyPatch(typeof(Hunger), "ClearAddReserveCaloriesOverTime")]
    internal class HungerTest2
    {
        private static void Postfix()
        {
            Logger.Log("ClearAddReserveCaloriesOverTime");
        }
    }

    [HarmonyPatch(typeof(Hunger), "GetCalorieReserves")]
    internal class HungerTest3
    {
        private static void Postfix()
        {
            Logger.Log("GetCalorieReserves");
        }
    }

    [HarmonyPatch(typeof(Hunger), "GetCaloriesToAddOverTime")]
    internal class HungerTest4
    {
        private static void Postfix()
        {
            Logger.Log("GetCaloriesToAddOverTime");
        }
    }

    [HarmonyPatch(typeof(Hunger), "IsAddingCaloriesOverTime")]
    internal class HungerTest5
    {
        private static void Postfix()
        {
            Logger.Log("IsAddingCaloriesOverTime");
        }
    }

    [HarmonyPatch(typeof(Hunger), "IsEatingInProgress")]
    internal class HungerTest6
    {
        private static void Postfix()
        {
            Logger.Log("IsEatingInProgress");
        }
    }

    [HarmonyPatch(typeof(Hunger), "ItemBeingEatenAffectsThirst")]
    internal class HungerTest7
    {
        private static void Postfix(Hunger __instance)
        {
            Logger.Log("ItemBeingEatenAffectsThirst");
            Logger.Log(__instance.m_CaloriesToAddOverTime.ToString());
            //FoodItem fi = __instance.GetItemBeingEaten();
            //if(fi != null)
            //{
             //   ModFoodComponent modFoodComponent = ModUtils.GetComponent<ModFoodComponent>(fi);

            //    if (modFoodComponent != null && modFoodComponent.Servings > 1)
            //    {
            //        __instance.m_CaloriesToAddOverTime = Math.Min(__instance.m_CaloriesToAddOverTime, (float)modFoodComponent.Calories / modFoodComponent.Servings);
            //    }
            //}
        }
    }*/
}
