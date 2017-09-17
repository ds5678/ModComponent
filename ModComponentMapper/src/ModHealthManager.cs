using Harmony;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ModComponentMapper
{
    internal class ModHealthManager : MonoBehaviour
    {
        private const float MIN_PROMILLE_FOR_BLUR = 0.4f;
        private const float MAX_PROMILLE_FOR_BLUR = 2.5f;
        private const float MIN_PROMILLE_FOR_STAGGERING = 0.8f;

        private const float ALCOHOL_TO_PROMILLE = 18;
        private const float MIN_UPTAKE_SCALE = 0.1f;

        private const float PROMILLE_REDUCTION_PER_GAME_SECOND = 0.15f / 3600f;
        private const float THIRST_PER_PROMILLE_SECOND = 25f / 3600f;

        internal static ModHealthManager instance;

        private List<AlcoholUptake> alcoholUptakes = new List<AlcoholUptake>();
        private float promille;
        private float forcedPromille = -1;

        private StatMonitor thirstMonitor = new StatMonitor();

        ModHealthManager()
        {
            thirstMonitor.hourlyBaseline = GameManager.GetThirstComponent().m_ThirstIncreasePerDay / 24;
            thirstMonitor.scale = 0.2f;
        }

        public void Update()
        {
            float elapsedGameSeconds = GameManager.GetTimeOfDayComponent().GetTODSeconds(Time.deltaTime);
            if (elapsedGameSeconds <= 0)
            {
                return;
            }

            UpdateStatMonitors(elapsedGameSeconds);
            ProcessAlcohol(elapsedGameSeconds);
        }

        public static void DrankAlcohol(float amount, float uptakeGameSeconds)
        {
            if (instance == null)
            {
                Debug.Log("ModHealthManager not initialized.");
                return;
            }

            Hunger hunger = GameManager.GetHungerComponent();
            float hungerScale = Mathf.Clamp01(Math.Max(MIN_UPTAKE_SCALE, hunger.GetCalorieReserves() / hunger.m_MaxReserveCalories));
            float scaledUptakeGameSeconds = uptakeGameSeconds * hungerScale;
            instance.alcoholUptakes.Add(AlcoholUptake.Create(amount, scaledUptakeGameSeconds));
        }

        public static float getAlcoholPromille()
        {
            return instance.promille;
        }

        public static StatMonitor GetThirstMonitor()
        {
            return instance.thirstMonitor;
        }

        internal static float getAlcoholBlurValue()
        {
            return Mathf.Clamp01((instance.promille - MIN_PROMILLE_FOR_BLUR) / (MAX_PROMILLE_FOR_BLUR - MIN_PROMILLE_FOR_BLUR));
        }

        internal static bool shouldStagger()
        {
            return getAlcoholPromille() >= MIN_PROMILLE_FOR_STAGGERING;
        }

        private void UpdateStatMonitors(float elapsedGameSeconds)
        {
            thirstMonitor.Update(GameManager.GetThirstComponent().m_CurrentThirst, elapsedGameSeconds);
        }

        private void ProcessAlcohol(float elapsedGameSeconds)
        {
            for (int i = alcoholUptakes.Count - 1; i >= 0; i--)
            {
                AlcoholUptake uptake = alcoholUptakes[i];
                promille += elapsedGameSeconds * uptake.amountPerGameSecond * ALCOHOL_TO_PROMILLE;
                uptake.remainingGameSeconds -= elapsedGameSeconds;

                if (uptake.remainingGameSeconds <= 0)
                {
                    alcoholUptakes.RemoveAt(i);
                }
            }

            if (promille > 0)
            {
                promille -= elapsedGameSeconds * PROMILLE_REDUCTION_PER_GAME_SECOND;
            }

            if (forcedPromille >= 0)
            {
                promille = forcedPromille;
            }

            GameManager.GetThirstComponent().AddThirst(elapsedGameSeconds * promille * THIRST_PER_PROMILLE_SECOND);
        }

        internal static ModHealthManagerData GetData()
        {
            ModHealthManagerData result = new ModHealthManagerData();

            result.alcoholPromille = instance.promille;
            result.uptakes = instance.alcoholUptakes.ToArray<AlcoholUptake>();

            return result;
        }

        internal static void SetData(ModHealthManagerData data)
        {
            if (data == null)
            {
                return;
            }

            instance.promille = data.alcoholPromille;

            instance.alcoholUptakes.Clear();
            if (data.uptakes != null)
            {
                instance.alcoholUptakes.AddRange(data.uptakes);
            }
        }
    }

    public class AlcoholUptake
    {
        public static AlcoholUptake Create(float amount, float gameSeconds)
        {
            AlcoholUptake result = new AlcoholUptake();

            result.amountPerGameSecond = amount / gameSeconds;
            result.remainingGameSeconds = gameSeconds;

            return result;
        }

        public float amountPerGameSecond;
        public float remainingGameSeconds;
    }

    [HarmonyPatch(typeof(Condition), "UpdateBlurEffect")]
    internal class Condition_UpdateBlurEffect
    {
        public static void Prefix(Condition __instance, ref float percentCondition, ref bool lowHealthStagger)
        {
            lowHealthStagger = percentCondition <= __instance.m_HPToStartBlur || ModHealthManager.shouldStagger();
            percentCondition = Math.Min(percentCondition, __instance.m_HPToStartBlur * (1 - ModHealthManager.getAlcoholBlurValue()) + 0.01f);

            if (!lowHealthStagger)
            {
                GameManager.GetVpFPSCamera().m_BasePitch = Mathf.Lerp(GameManager.GetVpFPSCamera().m_BasePitch, 0.0f, 0.01f);
                GameManager.GetVpFPSCamera().m_BaseRoll = Mathf.Lerp(GameManager.GetVpFPSCamera().m_BaseRoll, 0.0f, 0.01f);
            }
        }
    }

    [HarmonyPatch(typeof(GameManager), "Start")]
    internal class GameManagerPatch
    {
        public static void Postfix(PlayerManager __instance)
        {
            ModHealthManager.instance = __instance.gameObject.AddComponent<ModHealthManager>();
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "SaveGlobalData")]
    internal class SaveGameSystem_SaveGlobalData
    {
        public static void Postfix(string name)
        {
            SaveProxy proxy = new SaveProxy();
            proxy.data = JsonConvert.SerializeObject(ModHealthManager.GetData());

            SaveGameSlots.SaveDataToSlot(name, "ModHealthManager", JsonConvert.SerializeObject(proxy));
        }
    }

    [HarmonyPatch(typeof(SaveGameSystem), "RestoreGlobalData")]
    internal class SaveGameSystem_RestoreGlobalData
    {
        public static void Postfix(string name)
        {
            string serializedProxy = SaveGameSlots.LoadDataFromSlot(name, "ModHealthManager");
            SaveProxy proxy = JsonConvert.DeserializeObject<SaveProxy>(serializedProxy);
            ModHealthManager.SetData(GetData(proxy));
        }

        private static ModHealthManagerData GetData(SaveProxy proxy)
        {
            if (proxy == null || string.IsNullOrEmpty(proxy.data))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ModHealthManagerData>(proxy.data);
        }
    }

    [HarmonyPatch(typeof(StatusBar), "GetRateOfChangeThirst")]
    internal class StatusBarGetRateOfChangeThirst
    {
        public static bool Prefix(StatusBar __instance, ref float __result)
        {
            if (__instance.m_StatusBarType == StatusBar.StatusBarType.Thirst)
            {
                var thirstMonitor = ModHealthManager.GetThirstMonitor();
                __result = thirstMonitor.getRateOfChange();
                
                return false;
            }

            return true;
        }
    }

    internal class StatMonitor
    {
        public float hourlyBaseline;
        public float scale;
        public float value;
        public float hourlyChange;

        public void Update(float currentValue, float elapsedGameSeconds)
        {
            float delta = currentValue - value;
            hourlyChange = Mathf.Lerp(hourlyChange, 3600f * delta / elapsedGameSeconds, 0.05f);
            value = currentValue;
        }

        public float getRateOfChange()
        {
            return (hourlyChange - hourlyBaseline) * scale;
        }
    }

    public class ModHealthManagerData
    {
        public float alcoholPromille;
        public AlcoholUptake[] uptakes;
    }

    public class SaveProxy
    {
        public string data;
    }
}
