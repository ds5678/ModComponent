using Harmony;
using System;
using UnityEngine;

namespace ModComponentMapper
{
	internal static class AlcoholPatches
	{
		[HarmonyPatch(typeof(SaveGameSystem), "RestoreGlobalData")]
		internal class SaveGameSystem_RestoreGlobalData
		{
			public static void Postfix(string name)
			{
				string serializedProxy = SaveGameSlots.LoadDataFromSlot(name, "ModHealthManager");
				SaveProxy proxy = new SaveProxy();
				if (!string.IsNullOrEmpty(serializedProxy))
				{
					proxy = MelonLoader.TinyJSON.JSON.Load(serializedProxy).Make<SaveProxy>();
				}
				ModHealthManager.SetData(GetData(proxy));
			}

			private static ModHealthManagerData GetData(SaveProxy proxy)
			{
				if (proxy == null || string.IsNullOrEmpty(proxy.data)) return null;

				return MelonLoader.TinyJSON.JSON.Load(proxy.data).Make<ModHealthManagerData>();
			}
		}

		[HarmonyPatch(typeof(SaveGameSystem), "SaveGlobalData")]//Exists
		internal class SaveGameSystem_SaveGlobalData
		{
			public static void Postfix(SaveSlotType gameMode, string name)
			{
				SaveProxy proxy = new SaveProxy();
				proxy.data = MelonLoader.TinyJSON.JSON.Dump(ModHealthManager.GetData());
				SaveGameSlots.SaveDataToSlot(gameMode, SaveGameSystem.m_CurrentEpisode, SaveGameSystem.m_CurrentGameId, name, "ModHealthManager", MelonLoader.TinyJSON.JSON.Dump(proxy));
			}
		}

		[HarmonyPatch(typeof(StatusBar), "GetRateOfChange")]//positive caller count
		internal class StatusBar_GetRateOfChange
		{
			private static void Postfix(StatusBar __instance, ref float __result)
			{
				if (__instance.m_StatusBarType == StatusBar.StatusBarType.Fatigue)
				{
					var fatigueMonitor = ModHealthManager.GetFatigueMonitor();
					__result = fatigueMonitor.getRateOfChange();
				}
				else if (__instance.m_StatusBarType == StatusBar.StatusBarType.Thirst)
				{
					var thirstMonitor = ModHealthManager.GetThirstMonitor();
					__result = thirstMonitor.getRateOfChange();
				}
			}
		}

		[HarmonyPatch(typeof(Condition), "UpdateBlurEffect")]//positive caller count
		internal class Condition_UpdateBlurEffect
		{
			public static void Prefix(Condition __instance, ref float percentCondition, ref bool lowHealthStagger)
			{
				lowHealthStagger = percentCondition <= __instance.m_HPToStartBlur || ModHealthManager.ShouldStagger();
				percentCondition = Math.Min(percentCondition, __instance.m_HPToStartBlur * (1 - ModHealthManager.GetAlcoholBlurValue()) + 0.01f);

				if (!lowHealthStagger)
				{
					GameManager.GetVpFPSCamera().m_BasePitch = Mathf.Lerp(GameManager.GetVpFPSCamera().m_BasePitch, 0.0f, 0.01f);
					GameManager.GetVpFPSCamera().m_BaseRoll = Mathf.Lerp(GameManager.GetVpFPSCamera().m_BaseRoll, 0.0f, 0.01f);
				}
			}
		}

		[HarmonyPatch(typeof(Freezing), "CalculateBodyTemperature")]//positive caller count
		internal class Freezing_CalculateBodyTemperature
		{
			public static void Postfix(ref float __result)
			{
				__result += ModHealthManager.GetBodyTempBonus();
			}
		}

		[HarmonyPatch(typeof(Frostbite), "CalculateBodyTemperatureWithoutClothing")]//positive caller count
		internal class Frostbite_CalculateBodyTemperatureWithoutClothing
		{
			public static void Postfix(ref float __result)
			{
				__result += ModHealthManager.GetFrostbiteTempBonus();
			}
		}

		[HarmonyPatch(typeof(GameManager), "Start")]//runs
		internal class GameManagerStartPatch
		{
			public static void Postfix(PlayerManager __instance)
			{
				ModHealthManager.instance = __instance.gameObject.AddComponent<ModHealthManager>();
			}
		}
	}
}
