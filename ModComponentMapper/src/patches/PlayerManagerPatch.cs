using Harmony;
using ModComponentAPI;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//did a first pass through 
//HAS A TRANSPILER PATCH, which I think I fixed
//Inline patch probably fixed
//does not need to be declared
//might have some additional inlined methods

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(PlayerManager), "InteractiveObjectsProcessAltFire")]//Exists
    internal class PlayerManager_InteractiveObjectsProcessAltFire
    {
        internal static bool Prefix(PlayerManager __instance)
        {
            AlternativeAction alternativeAction = ModUtils.GetComponent<AlternativeAction>(__instance.m_InteractiveObjectUnderCrosshair);
            if (alternativeAction == null)
            {
                return true;
            }

            alternativeAction.Execute();
            return false;
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]//Exists
    internal class PlayerManager_PutOnClothingItem
    {
        internal static void Postfix(GearItem gi)
        {
            ModClothingComponent modClothingComponent = ModUtils.GetComponent<ModClothingComponent>(gi);
            modClothingComponent?.OnPutOn?.Invoke();

            Implementation.UpdateWolfIntimidationBuff();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "RestoreOriginalTint")]//Exists
    internal class PlayerManager_RestoreOriginalTint
    {
        internal static void Postfix(PlayerManager __instance, GameObject go)
        {
            Object.Destroy(go.GetComponent<RestoreMaterialQueue>());
        }
    }

    //[HarmonyPatch(typeof(PlayerManager), "StoreOriginalTint")]//DOES NOT EXIST !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    //internal class PlayerManager_StoreOriginalTint
    //{
    //    internal static void Prefix(PlayerManager __instance, GameObject go)
    //    {
    //        go.AddComponent<RestoreMaterialQueue>();
    //    }
    //}

    //Replacement Patches
    [HarmonyPatch(typeof(PlayerManager), "PrepareGhostedObject")]//inlined?
    internal class PlayerManager_PrepareGhostedObject
    {
        internal static void Prefix(PlayerManager __instance)
        {
            if (__instance.m_ObjectToPlace)
            {
                __instance.m_ObjectToPlace.AddComponent<RestoreMaterialQueue>();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "OnPlaceMeshAnimationEvent")]//inlined
    internal class PlayerManager_OnPlaceMeshAnimationEvent
    {
        internal static void Prefix(PlayerManager __instance)
        {
            if (__instance.m_ObjectToPlace)
            {
                __instance.m_ObjectToPlace.AddComponent<RestoreMaterialQueue>();
            }
        }
    }
    //End Replacement Patches

    [HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]//Exists
    internal class PlayerManager_TakeOffClothingItem
    {
        internal static void Postfix(GearItem gi)
        {
            ModClothingComponent modClothingComponent = ModUtils.GetComponent<ModClothingComponent>(gi);
            modClothingComponent?.OnTakeOff?.Invoke();

            Implementation.UpdateWolfIntimidationBuff();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsInternal")]//Exists
    internal class PlayerManager_UnequipItemInHandsInternalPatch
    {
        internal static void Postfix(PlayerManager __instance)
        {
            GearEquipper.Unequip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsSkipAnimation")]//Exists
    internal class PlayerManager_UnequipItemInHandsSkipAnimation
    {
        internal static void Prefix(PlayerManager __instance)
        {
            GearEquipper.OnUnequipped(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    //TRANSPILER!!!!!!! <===========================================================================================================
    //Seems to be removing the wolf intimidation buff calculation
    /*[HarmonyPatch(typeof(PlayerManager), "UpdateBuffDurations")]//Exists
    internal class PlayerManager_UpdateBuffDurations
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                MethodInfo methodInfo = codes[i].operand as MethodInfo;
                if (methodInfo != null && methodInfo.Name == "GetWornWolfIntimidationClothing" && methodInfo.DeclaringType == typeof(PlayerManager))
                {
                    for (int j = 0; j < 17; j++)
                    {
                        codes.RemoveAt(i - 1);
                    }
                }
            }

            return codes;
        }
    }
    */
    [HarmonyPatch(typeof(PlayerManager),"UpdateBuffDurations")]
    internal static class PlayerManager_UpdateBuffDurations
    {
        internal static void Postfix(PlayerManager __instance)
        {
            __instance.RemoveWolfIntimidationBuff();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "EquipItem")]//Exists
    internal class PlayerManagerEquipItemPatch
    {
        internal static void Prefix(PlayerManager __instance, GearItem gi)
        {
            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(__instance.m_ItemInHands);
            if (equippable != null)
            {
                __instance.UnequipItemInHands();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "OnEquipItemBegin")]//inlined?
    internal class PlayerManagerOnEquipItemBeginPatch
    {
        internal static void Postfix(PlayerManager __instance)
        {
            GearEquipper.Equip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "SetControlMode")]//Exists
    internal class PlayerManagerSetControlModePatch
    {
        private static PlayerControlMode lastMode;

        internal static void Postfix(PlayerManager __instance, PlayerControlMode mode)
        {
            if (mode == lastMode)
            {
                return;
            }

            lastMode = mode;

            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(__instance.m_ItemInHands);
            if (equippable != null)
            {
                equippable.OnControlModeChangedWhileEquipped?.Invoke();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UseInventoryItem")]//Exists
    internal class PlayerManagerUseInventoryItemPatch
    {
        internal static bool Prefix(PlayerManager __instance, GearItem gi)
        {
            if (ModUtils.GetComponent<FirstPersonItem>(gi) != null)
            {
                return true;
            }

            if (ModUtils.GetEquippableModComponent(gi) == null)
            {
                return true;
            }

            var currentGi = __instance.m_ItemInHands;
            if (currentGi != null)
            {
                __instance.UnequipItemInHands();
            }

            if (gi != currentGi)
            {
                __instance.EquipItem(gi, false);
            }

            return false;
        }
    }
}