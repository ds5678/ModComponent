using Harmony;
using ModComponentAPI;

using static PlayerAnimation;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(PlayerManager), "PutOnClothingItem")]
    internal class PlayerManager_PutOnClothingItem
    {
        public static void Postfix(GearItem gi)
        {
            ModClothingComponent modClothingComponent = ModUtils.GetComponent<ModClothingComponent>(gi);
            modClothingComponent?.OnPutOn?.Invoke();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "TakeOffClothingItem")]
    internal class PlayerManager_TakeOffClothingItem
    {
        public static void Postfix(GearItem gi)
        {
            ModClothingComponent modClothingComponent = ModUtils.GetComponent<ModClothingComponent>(gi);
            modClothingComponent?.OnTakeOff?.Invoke();
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "EquipItem")]
    internal class PlayerManagerEquipItemPatch
    {
        public static void Prefix(PlayerManager __instance, GearItem gi)
        {
            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(__instance.m_ItemInHands);
            if (equippable != null)
            {
                __instance.UnequipItemInHands();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "OnEquipItemBegin")]
    internal class PlayerManagerOnEquipItemBeginPatch
    {
        public static void Postfix(PlayerManager __instance)
        {
            ModAnimationStateMachine animation = ModUtils.GetComponent<ModAnimationStateMachine>(__instance.m_ItemInHands);
            if (animation != null)
            {
                animation.TransitionTo(State.Equipping);
                GameManager.GetPlayerAnimationComponent().m_EnableAnimationDrivenZoom = false;
                return;
            }

            GearEquipper.Equip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "SetControlMode")]
    internal class PlayerManagerSetControlModePatch
    {
        private static PlayerControlMode lastMode;

        public static void Postfix(PlayerManager __instance, PlayerControlMode mode)
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

    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsInternal")]
    internal class PlayerManagerUnequipItemInHandsInternalPatch
    {
        public static void Postfix(PlayerManager __instance)
        {
            ModAnimationStateMachine animation = ModUtils.GetComponent<ModAnimationStateMachine>(__instance.m_ItemInHands);
            if (animation != null)
            {
                animation.TransitionTo(State.Dequipping);
                GameManager.GetPlayerAnimationComponent().m_EnableAnimationDrivenZoom = true;
                return;
            }

            GearEquipper.Unequip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsSkipAnimation")]
    internal class PlayerManager_UnequipItemInHandsSkipAnimation
    {
        public static void Prefix(PlayerManager __instance)
        {
            GearEquipper.OnUnequipped(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UseInventoryItem")]
    internal class PlayerManagerUseInventoryItemPatch
    {
        public static bool Prefix(PlayerManager __instance, GearItem gi)
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