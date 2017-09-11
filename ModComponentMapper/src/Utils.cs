using System;
using System.Linq;
using System.Reflection;

using Harmony;

using UnityEngine;

using ModComponentAPI;

namespace ModComponentMapper
{
    public enum LootTableName
    {
        LootTableHumanCorpse,
        LootTableBackPack,
        LootTableBathroomCabinet,
        LootTableCabinet,
        LootTableCashRegister,
        LootTableDresser,
        LootTableFileCabinet,
        LootTableFirstAidKit,
        LootTableFishingDrawer,
        LootTableFreezer,
        LootTableFridge,
        LootTableKitchenCupboard,
        LootTableLaundry,
        LootTableLocker,
        LootTableLockerLocked,
        LootTableMetalBox,
        LootTableOven,
        LootTablePlasticBox,
        LootTableSafe,
        LootTableToolChest_sml,
        LootTableToolChest,
        LootTableToolChestDrawer,
        LootTableWardrobe_regular,
        LootTableWorkbench,
    }

    public class ModUtils
    {
        public static T[] NotNull<T>(T[] array)
        {
            if (array == null)
            {
                return new T[0];
            }

            return array;
        }

        public static void FreezePlayer()
        {
            GameManager.GetVpFPSPlayer().Controller.m_Controller.SimpleMove(Vector3.zero);
            GameManager.GetPlayerManagerComponent().DisableCharacterController();
        }

        public static void UnfreezePlayer()
        {
            GameManager.GetPlayerManagerComponent().EnableCharacterController();
        }

        public static void PlayAudio(string audioName)
        {
            if (audioName != null)
            {
                GameAudioManager.PlaySound(audioName, InterfaceManager.GetSoundEmitter());
            }
        }

        public static void InsertIntoLootTable(LootTableName lootTableName, GameObject prefab, int weight)
        {
            LootTable[] lootTables = Resources.FindObjectsOfTypeAll<LootTable>();
            LootTable lootTable = lootTables.First(l => l.name == lootTableName.ToString());
            if (lootTable == null)
            {
                Debug.Log("Could not find LootTable '" + lootTable + "'.");
                return;
            }

            Debug.Log("Inserting '" + prefab.name + "' into LootTable '" + lootTable.name + "' with weight " + weight);
            lootTable.m_Prefabs.Add(prefab);
            lootTable.m_Weights.Add(weight);
        }

        public static void RegisterConsoleGearName(string displayName, string prefabName)
        {
            ConsoleManager.Initialize();
            ExecuteStaticMethod(typeof(ConsoleManager), "AddCustomGearName", new object[] { displayName.ToLower(), prefabName.ToLower() });
        }

        public static void ExecuteStaticMethod(Type type, string methodName, object[] parameters)
        {
            MethodInfo methodInfo = AccessTools.Method(type, methodName, AccessTools.GetTypes(parameters));
            methodInfo.Invoke(null, parameters);
        }

        public static void ExecuteMethod(object instance, string methodName, params object[] parameters)
        {
            MethodInfo methodInfo = AccessTools.Method(instance.GetType(), methodName, AccessTools.GetTypes(parameters));
            methodInfo.Invoke(instance, parameters);
        }

        public static void SetFieldValue(object target, string fieldName, object value)
        {
            FieldInfo fieldInfo = AccessTools.Field(target.GetType(), fieldName);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(target, value);
            }
        }

        public static T GetStaticFieldValue<T>(Type type, string fieldName)
        {
            FieldInfo fieldInfo = AccessTools.Field(type, fieldName);
            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(null);
            }

            return default(T);
        }

        public static T GetFieldValue<T>(object target, string fieldName)
        {
            FieldInfo fieldInfo = AccessTools.Field(target.GetType(), fieldName);
            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(target);
            }

            return default(T);
        }

        public static string NormalizeName(string name)
        {
            if (name == null)
            {
                return null;
            }

            return name.Replace("(Clone)", "").Trim();
        }

        internal static EquippableModComponent GetEquippableModComponent(Component component)
        {
            return GetComponent<EquippableModComponent>(component);
        }

        internal static EquippableModComponent GetEquippableModComponent(GameObject gameObject)
        {
            return GetComponent<EquippableModComponent>(gameObject);
        }

        public static T GetComponent<T>(Component component)
        {
            return GetComponent<T>(component ? component.gameObject : null);
        }

        public static T GetComponent<T>(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return default(T);
            }

            return gameObject.GetComponent<T>();
        }

        internal static ModComponent GetModComponent(Component component)
        {
            return GetModComponent(component ? component.gameObject : null);
        }

        internal static ModComponent GetModComponent(GameObject gameObject)
        {
            return GetComponent<ModComponent>(gameObject);
        }

        internal static Delegate CreateDelegate(Type delegateType, object target, string methodName)
        {
            MethodInfo methodInfo = target.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (methodInfo == null)
            {
                return null;
            }

            return Delegate.CreateDelegate(delegateType, target, methodInfo);
        }
    }

    public class UIUtils
    {
        public static UITexture CreateOverlay(Texture2D texture)
        {
            UIRoot root = UIRoot.list[0];
            UIPanel panel = NGUITools.AddChild<UIPanel>(root.gameObject);

            UITexture result = NGUITools.AddChild<UITexture>(panel.gameObject);
            result.mainTexture = texture;

            Vector2 windowSize = panel.GetWindowSize();
            result.width = (int)windowSize.x;
            result.height = (int)windowSize.y;

            return result;
        }
    }

    public class EquipItemPopupUtils
    {
        public static void ShowItemPopups(String primaryAction, String secondaryAction, bool showAmmo, bool showDuration, bool showReload, bool showHolster)
        {
            EquipItemPopup equipItemPopup = InterfaceManager.m_Panel_HUD.m_EquipItemPopup;
            ShowItemIcons(equipItemPopup, primaryAction, secondaryAction, showAmmo, showDuration);

            if (Utils.IsGamepadActive())
            {
                equipItemPopup.m_ButtonPromptFire.ShowPromptForKey(primaryAction, "Fire");
                MaybeRepositionFireButtonPrompt(equipItemPopup, secondaryAction);
                equipItemPopup.m_ButtonPromptAltFire.ShowPromptForKey(secondaryAction, "AltFire");
                MaybeRepositionAltFireButtonPrompt(equipItemPopup, primaryAction);
            }
            else
            {
                equipItemPopup.m_ButtonPromptFire.ShowPromptForKey(secondaryAction, "AltFire");
                MaybeRepositionFireButtonPrompt(equipItemPopup, primaryAction);
                equipItemPopup.m_ButtonPromptAltFire.ShowPromptForKey(primaryAction, "Interact");
                MaybeRepositionAltFireButtonPrompt(equipItemPopup, secondaryAction);
            }

            string reloadText = showReload ? Localization.Get("GAMEPLAY_Reload") : string.Empty;
            equipItemPopup.m_ButtonPromptReload.ShowPromptForKey(reloadText, "Reload");

            string holsterText = showHolster ? Localization.Get("GAMEPLAY_HolsterPrompt") : string.Empty;
            equipItemPopup.m_ButtonPromptHolster.ShowPromptForKey(holsterText, "Holster");
        }

        internal static void MaybeRepositionFireButtonPrompt(EquipItemPopup equipItemPopup, String otherAction)
        {
            ModUtils.ExecuteMethod(equipItemPopup, "MaybeRepositionFireButtonPrompt", new object[] { otherAction, });
        }

        internal static void MaybeRepositionAltFireButtonPrompt(EquipItemPopup __instance, String otherAction)
        {
            ModUtils.ExecuteMethod(__instance, "MaybeRepositionAltFireButtonPrompt", new object[] { otherAction, });
        }

        internal static void ShowItemIcons(EquipItemPopup equipItemPopup, String primaryAction, String secondaryAction, bool showAmmo, bool showDuration)
        {
            ModUtils.ExecuteMethod(equipItemPopup, "ShowItemIcons", new object[] { primaryAction, secondaryAction, showAmmo, showDuration });
        }
    }

    public class LogUtils
    {
        public static void Log(string component, string message)
        {
            Debug.LogFormat("[{0}] {1}", component, message);
        }

        public static void Log(string component, string message, object[] parameters)
        {
            string preformattedMessage = string.Format("[{0}] {1}", component, message);
            Debug.LogFormat(preformattedMessage, parameters);
        }
    }
}
