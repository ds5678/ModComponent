using UnityEngine;

namespace ModComponentMapper
{
    public class Implementation
    {
        public static event SceneReady OnSceneReady;

        public static void OnLoad()
        {
            Log("Version {0}", System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);

            AutoMapper.Initialize();
            ModHealthManager.Initialize();
            GearSpawner.Initialize();
            BlueprintReader.Initialize();
        }

        internal static void Log(string message)
        {
            Debug.LogFormat("[ModComponent] {0}", message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            Log(preformattedMessage);
        }

        internal static void SceneReady()
        {
            Log("Invoking 'SceneReady' for scene '{0}' ...", GameManager.m_ActiveScene);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            OnSceneReady?.Invoke();

            stopwatch.Stop();
            Log("Completed 'SceneReady' for scene '{0}' in {1} ms", GameManager.m_ActiveScene, stopwatch.ElapsedMilliseconds);
        }

        internal static void UpdateWolfIntimidationBuff()
        {
            float increaseFlee = 0;
            float decreaseAttack = 0;

            PlayerManager playerManager = GameManager.GetPlayerManagerComponent();

            for (int region = 0; region < (int)ClothingRegion.NumRegions; region++)
            {
                for (int layer = 0; layer < (int)ClothingLayer.NumLayers; layer++)
                {
                    GearItem clothing = playerManager.GetClothingInSlot((ClothingRegion)region, (ClothingLayer)layer);

                    if (clothing && clothing.m_WolfIntimidationBuff)
                    {
                        decreaseAttack += clothing.m_WolfIntimidationBuff.m_DecreaseAttackChancePercentagePoints;
                        increaseFlee += clothing.m_WolfIntimidationBuff.m_IncreaseFleePercentagePoints;
                    }
                }
            }

            playerManager.ApplyWolfIntimidationBuff(increaseFlee, decreaseAttack);
        }
    }
}