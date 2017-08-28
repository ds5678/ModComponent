using System;
using System.Linq;
using System.Reflection;

using Harmony;

using UnityEngine;

using ModComponentAPI;

namespace ModComponentMapper
{
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

        public static void InsertIntoLootTable(string lootTableName, GameObject prefab, int weight)
        {
            LootTable[] lootTables = Resources.FindObjectsOfTypeAll<LootTable>();
            LootTable lootTable = lootTables.First(l => l.name == lootTableName);
            if (lootTable == null)
            {
                Debug.Log("Could not find LootTable '" + lootTableName + "'.");
                return;
            }

            Debug.Log("Inserting '" + prefab.name + "' into LootTable '" + lootTable.name + "' with weight " + weight);

            for (int i = 0; i < lootTable.m_Prefabs.Count; i++)
            {
                if (lootTable.m_Weights[i] >= weight)
                {
                    lootTable.m_Prefabs.Insert(i, prefab);
                    lootTable.m_Weights.Insert(i, weight);
                    return;
                }
            }

            // append to the end
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

        public static void ExecuteMethod(object instance, string methodName, object[] parameters)
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

        public static T GetFieldValue<T>(object target, string fieldName)
        {
            FieldInfo fieldInfo = AccessTools.Field(target.GetType(), fieldName);
            if (fieldInfo != null)
            {
                return (T)fieldInfo.GetValue(target);
            }

            return default(T);
        }

        internal static T GetModComponent<T>(Component component) where T : ModComponent
        {
            return GetModComponent<T>(component ? component.gameObject : null);
        }

        internal static T GetModComponent<T>(GameObject gameObject) where T : ModComponent
        {
            if (gameObject == null)
            {
                return null;
            }

            return gameObject.GetComponent<T>();
        }

        internal static ModComponent GetModComponent(Component component)
        {
            return GetModComponent(component ? component.gameObject : null);
        }

        internal static ModComponent GetModComponent(GameObject gameObject)
        {
            if (gameObject == null)
            {
                return null;
            }

            return gameObject.GetComponent<ModComponent>();
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
}
