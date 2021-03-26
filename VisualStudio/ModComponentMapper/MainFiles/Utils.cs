using Harmony;
using ModComponentAPI;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ModComponentMapper
{
    public static class EquipItemPopupUtils
    {
        public static void ShowItemPopups(String primaryAction, String secondaryAction, bool showAmmo, bool showReload, bool showHolster)
        {
            EquipItemPopup equipItemPopup = InterfaceManager.m_Panel_HUD.m_EquipItemPopup;
            ShowItemIcons(equipItemPopup, primaryAction, secondaryAction, showAmmo);
            equipItemPopup.OnOverlappingDecalChange(true);

            if (Utils.IsGamepadActive())
            {
                //Logger.Log("Gamepad active");
                equipItemPopup.m_ButtonPromptFire.ShowPromptForKey(primaryAction, "Fire");
                MaybeRepositionFireButtonPrompt(equipItemPopup, secondaryAction);
                equipItemPopup.m_ButtonPromptAltFire.ShowPromptForKey(secondaryAction, "AltFire");
                MaybeRepositionAltFireButtonPrompt(equipItemPopup, primaryAction);
            }
            else
            {
                //Logger.Log("Gamepad not active");
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

        internal static void MaybeRepositionAltFireButtonPrompt(EquipItemPopup __instance, String otherAction)
        {
            __instance.MaybeRepositionAltFireButtonPrompt(otherAction);
        }

        internal static void MaybeRepositionFireButtonPrompt(EquipItemPopup equipItemPopup, String otherAction)
        {
            equipItemPopup.MaybeRepositionFireButtonPrompt(otherAction);
        }

        internal static void ShowItemIcons(EquipItemPopup equipItemPopup, String primaryAction, String secondaryAction, bool showAmmo)
        {
            equipItemPopup.ShowItemIcons(primaryAction, secondaryAction, showAmmo);
        }
    }

    public static class ModUtils
    {
        public static void CopyFields<T>(T copyTo, T copyFrom)
        {
            Type typeOfT = typeof(T);
            FieldInfo[] fieldInfos = typeOfT.GetFields();
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                fieldInfo.SetValue(copyTo, fieldInfo.GetValue(copyFrom));
            }
            if (fieldInfos.Length == 0)
            {
                MelonLoader.MelonLogger.LogError("There were no fields to copy!");
            }
        }

        public static string DefaultIfEmpty(string value, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : value;
        }

        public static void FreezePlayer()
        {
            GameManager.GetPlayerManagerComponent().m_FreezeMovement = true;
        }

        public static T GetComponent<T>(Component component) where T : Component
        {
            return GetComponent<T>(component ? component.gameObject : null);
        }

        public static T GetComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject == null) return default(T);
            else return gameObject.GetComponent<T>();
        }

        public static T GetOrCreateComponent<T>(Component component) where T : Component
        {
            return GetOrCreateComponent<T>(component ? component.gameObject : null);
        }

        public static T GetOrCreateComponent<T>(GameObject gameObject) where T : Component
        {
            T result = GetComponent<T>(gameObject);

            if (result == null) result = gameObject.AddComponent<T>();

            return result;
        }

        public static Skill GetSerializableSkillByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            SkillsManager skillsManager = GameManager.GetSkillsManager();
            if (skillsManager == null) return null;

            for (int i = 0; i < skillsManager.GetNumSkills(); i++)
            {
                Skill skill = skillsManager.GetSkillFromIndex(i);
                SerializableSkill serializableSkill = skill?.TryCast<SerializableSkill>();
                if (name == serializableSkill?.name) return skill;
            }

            return null;
        }

        public static Skill GetSkillByName(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            SkillsManager skillsManager = GameManager.GetSkillsManager();
            if (skillsManager == null) return null;

            for (int i = 0; i < skillsManager.GetNumSkills(); i++)
            {
                Skill skill = skillsManager.GetSkillFromIndex(i);
                if (name == skill.name) return skill;
            }

            return null;
        }

        public static bool IsNonGameScene()
        {
            return GameManager.m_ActiveScene == null || GameManager.m_ActiveScene == "MainMenu" || GameManager.m_ActiveScene == "Boot" || GameManager.m_ActiveScene == "Empty";
        }

        public static string NormalizeName(string name)
        {
            if (name == null) return null;
            else return name.Replace("(Clone)", "").Trim();
        }

        public static T[] NotNull<T>(T[] array)
        {
            if (array == null) return new T[0];
            else return array;
        }

        public static void PlayAudio(string audioName)
        {
            if (audioName != null) GameAudioManager.PlaySound(audioName, InterfaceManager.GetSoundEmitter());
        }

        public static void UnfreezePlayer()
        {
            GameManager.GetPlayerManagerComponent().m_FreezeMovement = false;
        }

        internal static bool AlmostZero(float value)
        {
            return Mathf.Abs(value) < 0.001f;
        }

        internal static Delegate CreateDelegate(Type delegateType, object target, string methodName)
        {
            MethodInfo methodInfo = target.GetType().GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (methodInfo == null) return null;
            else return Delegate.CreateDelegate(delegateType, target, methodInfo);
        }

        public static GameObject GetChild(GameObject gameObject, string childName)
        {
            if (string.IsNullOrEmpty(childName))
            {
                return null;
            }
            return gameObject.transform.FindChild(childName).gameObject;
        }

        public static GameObject GetInChildren(GameObject parent, string childName)
        {
            if (string.IsNullOrEmpty(childName)) return null;
            Transform transform = parent.transform;
            for(int i = 0; i < transform.childCount; i++)
            {
                GameObject child = transform.GetChild(i).gameObject;
                if (child.name == childName) return child;
                else if(child.transform.childCount > 0)
                {
                    GameObject grandChild = GetInChildren(child, childName);
                    if (grandChild != null) return grandChild;
                }
            }
            return null;
        }

        internal static EquippableModComponent GetEquippableModComponent(Component component)
        {
            return GetComponent<EquippableModComponent>(component);
        }

        internal static EquippableModComponent GetEquippableModComponent(GameObject gameObject)
        {
            return GetComponent<EquippableModComponent>(gameObject);
        }

        internal static T GetItem<T>(string name, string reference = null)
        {
            GameObject gameObject = Resources.Load(name).Cast<GameObject>();
            if (gameObject == null)
            {
                throw new ArgumentException("Could not load '" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + ".");
            }

            T targetType = gameObject.GetComponent<T>();
            if (targetType == null)
            {
                throw new ArgumentException("'" + name + "'" + (reference != null ? " referenced by '" + reference + "'" : "") + " is not a '" + typeof(T).Name + "'.");
            }

            return targetType;
        }

        internal static T[] GetItems<T>(string[] names, string reference = null)
        {
            T[] result = new T[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                result[i] = GetItem<T>(names[i], reference);
            }

            return result;
        }

        internal static T GetMatchingItem<T>(string name, string reference = null)
        {
            try
            {
                return GetItem<T>(name, reference);
            }
            catch (ArgumentException e)
            {
                Logger.Log(e.Message);
                return default(T);
            }
        }

        internal static T[] GetMatchingItems<T>(string[] names, string reference = null)
        {
            names = ModUtils.NotNull(names);

            List<T> values = new List<T>();

            for (int i = 0; i < names.Length; i++)
            {
                var matchingItem = GetMatchingItem<T>(names[i], reference);
                if (matchingItem != null)
                {
                    values.Add(matchingItem);
                }
            }

            return values.ToArray();
        }

        internal static ModComponent GetModComponent(Component component)
        {
            return GetModComponent(component ? component.gameObject : null);
        }

        internal static ModComponent GetModComponent(GameObject gameObject)
        {
            return GetComponent<ModComponent>(gameObject);
        }

        internal static void RegisterConsoleGearName(string displayName, string prefabName)
        {
            if (ConsoleWaitlist.IsConsoleManagerInitialized())
            {
                ConsoleManager.AddCustomGearName(displayName.ToLower(), prefabName.ToLower());
            }
            else
            {
                ConsoleWaitlist.AddToWaitlist(displayName, prefabName);
                //Logger.LogDebug("Console Manager not initialized. '{0}' , '{1}' added to waitlist.", displayName, prefabName);
            }
        }

        public static bool ContainsKey(MelonLoader.TinyJSON.ProxyObject dict, string key)
        {
            foreach (var pair in dict)
            {
                if (pair.Key == key)
                {
                    return true;
                }
            }
            return false;
        }

        public static T ParseEnum<T>(string text) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), text, true);
        }

        internal static T TranslateEnumValue<T, E>(E value)
        {
            return (T)Enum.Parse(typeof(T), Enum.GetName(typeof(E), value));
        }
    }

    public static class UIUtils
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

    public static class NameUtils
    {
        public static string AddCraftingIconPrefix(string name)
        {
            return "ico_CraftItem__" + name;
        }

        public static string RemoveCraftingIconPrefix(string iconFileName)
        {
            return iconFileName.Replace("ico_CraftItem__", "");
        }

        public static string AddGearPrefix(string name)
        {
            return "GEAR_" + name;
        }

        public static string RemoveGearPrefix(string gearName)
        {
            return gearName.Replace("GEAR_", "");
        }
    }
}