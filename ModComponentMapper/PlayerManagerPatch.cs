using Harmony;

using ModComponentAPI;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(PlayerManager), "SetControlMode")]
    class PlayerManagerSetControlModePatch
    {
        private static PlayerControlMode lastMode;

        public static void Postfix(PlayerManager __instance, PlayerControlMode mode)
        {
            if (mode == lastMode)
            {
                return;
            }

            lastMode = mode;

            GearItem gi = __instance.m_ItemInHands;
            ModComponent modComponent = ModUtils.GetModComponent(gi);
            if (modComponent != null)
            {
                modComponent.OnControlModeChangedWhileEquipped?.Invoke();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "EquipItem")]
    class PlayerManagerEquipItemPatch
    {
        public static void Prefix(PlayerManager __instance, GearItem gi)
        {
            ModComponent modComponent = ModUtils.GetModComponent(__instance.m_ItemInHands);
            if (modComponent != null)
            {
                __instance.UnequipItemInHands();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "OnEquipItemBegin")]
    class PlayerManagerOnEquipItemBeginPatch
    {
        public static void Postfix(PlayerManager __instance)
        {
            ModComponent modComponent = ModUtils.GetModComponent(__instance.m_ItemInHands);
            if (modComponent != null)
            {
                modComponent.OnEquipped?.Invoke();
                ModUtils.PlayAudio(modComponent.PickUpAudio);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsInternal")]
    class PlayerManagerUnequipItemInHandsInternalPatch
    {
        public static void Postfix(PlayerManager __instance)
        {
            ModComponent modComponent = ModUtils.GetModComponent(__instance.m_ItemInHands);
            if (modComponent != null)
            {
                __instance.UnequipItemInHandsSkipAnimation();
                modComponent.OnUnequipped?.Invoke();
                ModUtils.PlayAudio(modComponent.StowAudio);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UseInventoryItem")]
    class PlayerManagerUseInventoryItemPatch
    {
        public static bool Prefix(PlayerManager __instance, GearItem gi)
        {
            ModToolComponent modToolComponent = ModUtils.GetModComponent<ModToolComponent>(gi);
            if (modToolComponent == null)
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
