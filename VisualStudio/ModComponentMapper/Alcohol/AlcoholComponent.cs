using HarmonyLib;
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
		static AlcoholComponent() => UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentMapper.AlcoholComponent>(false);

		[HideFromIl2Cpp]
		public void Apply(float normalizedValue)
		{
			float amountConsumed = AmountTotal * normalizedValue;
			AmountRemaining -= amountConsumed;
			ModHealthManager.DrankAlcohol(amountConsumed, UptakeSeconds);
		}

		[HideFromIl2Cpp]
		internal static void UpdateAlcoholValues(FoodItem foodItem)
		{
			AlcoholComponent alcoholComponent = ModComponentUtils.ComponentUtils.GetComponent<AlcoholComponent>(foodItem);
			if (alcoholComponent != null)
			{
				ModComponentUtils.CopyFieldHandler.UpdateFieldValues<AlcoholComponent>(alcoholComponent);
				alcoholComponent.AmountRemaining = foodItem.m_CaloriesRemaining / foodItem.m_CaloriesTotal * alcoholComponent.AmountTotal;
			}
		}
	}

	[HarmonyPatch(typeof(GearItem), "ApplyBuffs")]//positive caller count
	internal static class AlcoholComponentHook
	{
		public static void Postfix(GearItem __instance, float normalizedValue)
		{
			AlcoholComponent alcoholComponent = ModComponentUtils.ComponentUtils.GetComponent<AlcoholComponent>(__instance);
			if (alcoholComponent != null)
			{
				alcoholComponent.Apply(normalizedValue);
			}
		}
	}

	[HarmonyPatch(typeof(FoodItem), "Deserialize")]//Exists
	internal static class FoodItem_Deserialize
	{
		public static void Postfix(FoodItem __instance)
		{
			AlcoholComponent.UpdateAlcoholValues(__instance);
		}
	}

	[HarmonyPatch(typeof(FoodItem), "Awake")]//Exists
	internal static class FoodItem_Awake
	{
		public static void Postfix(FoodItem __instance)
		{
			AlcoholComponent.UpdateAlcoholValues(__instance);
		}
	}
}
