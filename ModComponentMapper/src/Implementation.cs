using UnityEngine;
using MelonLoader;
using System.IO;

//does not need to be declared

namespace ModComponentMapper
{
    public class Implementation : MelonMod
    {
        public static event SceneReady OnSceneReady;

        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            InjectClasses();

            JsonHandler.RegisterDirectory(AutoMapper.GetAutoMapperDirectory());
            //JsonHandler.LogDirectoryContents();

            AutoMapper.Initialize();
            ModHealthManager.Initialize();
            GearSpawner.Initialize();
            BlueprintReader.Initialize();

        }

        internal static void InjectClasses()
        {
            //UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<SaveData.SaveData>(); //no intptr constructor
            //UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<SkillsDataObject>(); //no intptr constructor
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<AlcoholComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ChangeLayer>();
            //UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ExecutionValve>();//no intptr constructor
            //UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<MappedItem>();//no intptr constructor
            //UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<AlcoholUptake>();//no intptr constructor
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModAnimationStateMachine>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<FixMuzzleFlashTransformParent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<RestoreMaterialQueue>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModHealthManager>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModFireStartingComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModAccelerantComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModBurnableComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModEvolveComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModFireStarterComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModHarvestableComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModMillableComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModRepairableComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModSaveBehaviour>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModScentComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModSharpenableComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModStackableComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.EquippableModComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModBedComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModClothingComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModCookableComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModCookingPotComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModExplosiveComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModFirstAidComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModFoodComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModGenericComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModLiquidComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModRifleComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModToolComponent>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.AddTag>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.AlternativeAction>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.AttachBehaviour>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModBlueprint>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.ModSkill>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentAPI.PlayAkSound>();
            //UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<SkillsDataObject>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<SaveProxy>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<SaveData.SaveData>();
        }

        internal static string GetModsFolderPath()
        {
            return Path.GetFullPath(typeof(MelonMod).Assembly.Location + @"\..\..\Mods");
        }

        internal static void Log(string message)
        {
            //Debug.Log("[ModComponent] :" + message);
            MelonLogger.Log(message);
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