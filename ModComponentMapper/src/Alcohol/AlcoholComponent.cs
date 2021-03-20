using Harmony;
using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponentMapper
{
    public class AlcoholComponent : MonoBehaviour
    {
        public float AmountTotal;
        public float AmountRemaining;
        public float UptakeSeconds;
        public AlcoholComponent(IntPtr intPtr) : base(intPtr) { }

        [HideFromIl2Cpp]
        public void Apply(float normalizedValue)
        {
            float amountConsumed = AmountTotal * normalizedValue;
            AmountRemaining -= amountConsumed;
            ModHealthManager.DrankAlcohol(amountConsumed, UptakeSeconds);
        }
    }

    [HarmonyPatch(typeof(GearItem), "ApplyBuffs")]//positive caller count
    internal class AlcoholComponentHook
    {
        public static void Postfix(GearItem __instance, float normalizedValue)
        {
            AlcoholComponent alcoholComponent = ModUtils.GetComponent<AlcoholComponent>(__instance);
            if (alcoholComponent != null)
            {
                alcoholComponent.Apply(normalizedValue);
            }
        }
    }

    [HarmonyPatch(typeof(FoodItem), "Deserialize")]//Exists
    internal class FoodITtemDeserialize
    {
        public static void Postfix(FoodItem __instance)
        {
            AlcoholComponent alcoholComponent = ModUtils.GetComponent<AlcoholComponent>(__instance);
            if (alcoholComponent != null)
            {
                alcoholComponent.AmountRemaining = __instance.m_CaloriesRemaining / __instance.m_CaloriesTotal * alcoholComponent.AmountTotal;
            }
        }
    }
}
