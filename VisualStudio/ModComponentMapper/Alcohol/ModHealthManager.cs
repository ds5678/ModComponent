using System;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponentMapper
{
	internal class ModHealthManager : MonoBehaviour
	{
		private const float MIN_PERMILLE_FOR_BLUR = 0.5f;
		private const float MAX_PERMILLE_FOR_BLUR = 2.5f;
		private const float MAX_VALUE_FOR_BLUR = 0.992f;
		private const float MIN_PERMILLE_FOR_STAGGERING = 1f;

		private const float ALCOHOL_TO_PERMILLE = 18;
		private const float MIN_UPTAKE_SCALE = 0.1f;

		private const float PERMILLE_REDUCTION_PER_GAME_SECOND = 0.15f / 3600f;
		private const float THIRST_PER_PERMILLE_SECOND = 25f / 3600f;
		private const float FATIGUE_PER_PERMILLE_SECOND = 4.583f / 3600f;
		private const float FROSTBITE_TEMP_BONUS_PER_PERMILLE = 3;
		private const float BODY_TEMP_BONUS_PER_PERMILLE = -2;

		internal static ModHealthManager instance;

		private List<AlcoholUptake> alcoholUptakes = new List<AlcoholUptake>();
		private float permille;

		private StatMonitor thirstMonitor = new StatMonitor();
		private StatMonitor fatigueMonitor = new StatMonitor();

		void Awake()
		{
			ResetStatMonitors();
		}

		public ModHealthManager(IntPtr intPtr) : base(intPtr) { }
		static ModHealthManager() => UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentMapper.ModHealthManager>(false);

		public static void DrankAlcohol(float amount, float uptakeGameSeconds)
		{
			if (instance == null)
			{
				Logger.Log("ModHealthManager not initialized.");
				return;
			}

			Hunger hunger = GameManager.GetHungerComponent();
			float hungerScale = Mathf.Clamp01(Math.Max(MIN_UPTAKE_SCALE, hunger.GetCalorieReserves() / hunger.m_MaxReserveCalories));
			float scaledUptakeGameSeconds = uptakeGameSeconds * hungerScale;
			instance.alcoholUptakes.Add(AlcoholUptake.Create(amount, scaledUptakeGameSeconds));
		}

		public static float GetAlcoholPermille() => instance.permille;
		public static StatMonitor GetFatigueMonitor() => instance.fatigueMonitor;
		public static StatMonitor GetThirstMonitor() => instance.thirstMonitor;
		internal static float GetBodyTempBonus() => instance.permille * BODY_TEMP_BONUS_PER_PERMILLE;
		internal static float GetFrostbiteTempBonus() => instance.permille * FROSTBITE_TEMP_BONUS_PER_PERMILLE;
		internal static bool ShouldStagger() => GetAlcoholPermille() >= MIN_PERMILLE_FOR_STAGGERING;

		internal static float GetAlcoholBlurValue()
		{
			return Mathf.Clamp01((instance.permille - MIN_PERMILLE_FOR_BLUR) / (MAX_PERMILLE_FOR_BLUR - MIN_PERMILLE_FOR_BLUR)) * MAX_VALUE_FOR_BLUR;
		}

		internal static ModHealthManagerData GetData()
		{
			ModHealthManagerData result = new ModHealthManagerData();

			result.alcoholPermille = instance.permille;
			result.uptakes = instance.alcoholUptakes.ToArray<AlcoholUptake>();

			return result;
		}

		internal static void SetData(ModHealthManagerData data)
		{
			if (data == null) return;

			instance.permille = NotNan(data.alcoholPermille, "Nan in ModHealthManager.SetData");

			instance.alcoholUptakes.Clear();
			if (data.uptakes != null)
			{
				instance.alcoholUptakes.AddRange(data.uptakes);
			}

			instance.ResetStatMonitors();
		}

		internal static float NotNan(float number, string message = "")
		{
			if (float.IsNaN(number))
			{
				if (!string.IsNullOrEmpty(message)) Logger.LogError(message);
				else Logger.LogError("Nan value found in ModHealthManager");
				return 0;
			}
			else return number;
		}

		internal void Update()
		{
			float elapsedGameSeconds = NotNan(GameManager.GetTimeOfDayComponent().GetTODSeconds(Time.deltaTime), "Nan in ModHealthManager.Update");
			if (elapsedGameSeconds <= 0) return;

			UpdateStatMonitors(elapsedGameSeconds);
			ProcessAlcohol(elapsedGameSeconds);
		}

		#region ConsoleCommands

		internal static void Initialize()
		{
			//Two sets of commands because different regions don't express this in the same unit
			uConsole.RegisterCommand("set_alcohol_permille", new Action(Console_SetAlcoholPermille));
			uConsole.RegisterCommand("set_alcohol_percent", new Action(Console_SetAlcoholPercent));
			uConsole.RegisterCommand("get_alcohol_permille", new Action(Console_GetAlcoholPermille));
			uConsole.RegisterCommand("get_alcohol_percent", new Action(Console_GetAlcoholPercent));
		}

		private static void Console_SetAlcoholPercent()
		{
			if (uConsole.GetNumParameters() != 1)
			{
				uConsole.Log("  exactly one parameter required");
				return;
			}

			instance.permille = uConsole.GetFloat() * 10f;
		}

		private static void Console_SetAlcoholPermille()
		{
			if (uConsole.GetNumParameters() != 1)
			{
				uConsole.Log("  exactly one parameter required");
				return;
			}

			instance.permille = uConsole.GetFloat();
		}

		private static void Console_GetAlcoholPercent()
		{
			uConsole.Log(string.Format("  Current alcohol percent: {0}", GetAlcoholPermille() / 10f));
		}

		private static void Console_GetAlcoholPermille()
		{
			uConsole.Log(string.Format("  Current alcohol permille: {0}", GetAlcoholPermille()));
		}
		#endregion

		[HideFromIl2Cpp]
		private void ProcessAlcohol(float elapsedGameSeconds)
		{
			for (int i = alcoholUptakes.Count - 1; i >= 0; i--)
			{
				AlcoholUptake uptake = alcoholUptakes[i];
				permille += NotNan(elapsedGameSeconds * uptake.amountPerGameSecond * ALCOHOL_TO_PERMILLE, "Nan in ProcessAlcohol addition");
				uptake.remainingGameSeconds -= elapsedGameSeconds;

				if (uptake.remainingGameSeconds <= 0) alcoholUptakes.RemoveAt(i);
			}

			if (permille > 0)
			{
				permille -= NotNan(elapsedGameSeconds * PERMILLE_REDUCTION_PER_GAME_SECOND, "Nan in ProcessAlcohol subtraction");
			}

			GameManager.GetThirstComponent().AddThirst(elapsedGameSeconds * permille * THIRST_PER_PERMILLE_SECOND);
			GameManager.GetFatigueComponent().AddFatigue(elapsedGameSeconds * permille * FATIGUE_PER_PERMILLE_SECOND);
		}

		[HideFromIl2Cpp]
		private void ResetStatMonitors()
		{
			thirstMonitor.value = GameManager.GetThirstComponent().m_CurrentThirst;
			thirstMonitor.hourlyBaseline = GameManager.GetThirstComponent().m_ThirstIncreasePerDay / 24 * GameManager.GetExperienceModeManagerComponent().GetThirstRateScale();
			thirstMonitor.hourlyChange = 0;
			thirstMonitor.offset = 1;
			thirstMonitor.scale = 0.2f;
			thirstMonitor.debug = true;

			fatigueMonitor.value = GameManager.GetFatigueComponent().m_CurrentFatigue;
			fatigueMonitor.hourlyBaseline = GameManager.GetFatigueComponent().m_FatigueIncreasePerHourStanding * GameManager.GetExperienceModeManagerComponent().GetFatigueRateScale();
			fatigueMonitor.hourlyBaseline = 0;
			fatigueMonitor.hourlyChange = 0;
			fatigueMonitor.offset = 0;
			fatigueMonitor.scale = 1;
			fatigueMonitor.scale = GameManager.GetExperienceModeManagerComponent().GetFatigueRateScale();
		}

		[HideFromIl2Cpp]
		private void UpdateStatMonitors(float elapsedGameSeconds)
		{
			thirstMonitor.Update(GameManager.GetThirstComponent().m_CurrentThirst, elapsedGameSeconds);
			fatigueMonitor.Update(GameManager.GetFatigueComponent().m_CurrentFatigue, elapsedGameSeconds);
		}
	}
}
