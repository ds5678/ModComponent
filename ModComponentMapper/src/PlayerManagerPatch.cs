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
            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(gi);
            if (equippable != null)
            {
                equippable.OnControlModeChangedWhileEquipped?.Invoke();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "EquipItem")]
    class PlayerManagerEquipItemPatch
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
    class PlayerManagerOnEquipItemBeginPatch
    {
        public static void Postfix(PlayerManager __instance)
        {
            GearEquipper.Equip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UnequipItemInHandsInternal")]
    class PlayerManagerUnequipItemInHandsInternalPatch
    {
        public static void Postfix(PlayerManager __instance)
        {
            GearEquipper.Unequip(ModUtils.GetEquippableModComponent(__instance.m_ItemInHands));
        }
    }

    [HarmonyPatch(typeof(PlayerManager), "UseInventoryItem")]
    class PlayerManagerUseInventoryItemPatch
    {
        public static bool Prefix(PlayerManager __instance, GearItem gi)
        {
            EquippableModComponent equippable = ModUtils.GetEquippableModComponent(gi);
            if (equippable == null)
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
