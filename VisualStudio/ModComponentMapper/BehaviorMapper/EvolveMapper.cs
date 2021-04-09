using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
    internal static class EvolveMapper
    {
        internal static void Configure(ModComponent modComponent) => Configure(modComponent.gameObject);
        internal static void Configure(GameObject prefab)
        {
            ModEvolveComponent modEvolveComponent = ComponentUtils.GetComponent<ModEvolveComponent>(prefab);
            if (modEvolveComponent == null) return;

            EvolveItem evolveItem = ComponentUtils.GetOrCreateComponent<EvolveItem>(modEvolveComponent);
            evolveItem.m_ForceNoAutoEvolve = false;
            evolveItem.m_GearItemToBecome = GetTargetItem(modEvolveComponent.TargetItemName, modEvolveComponent.name);
            evolveItem.m_RequireIndoors = modEvolveComponent.IndoorsOnly;
            evolveItem.m_StartEvolvePercent = 0;
            evolveItem.m_TimeToEvolveGameDays = Mathf.Clamp(modEvolveComponent.EvolveHours / 24f, 0.01f, 1000);
        }

        private static GearItem GetTargetItem(string targetItemName, string reference)
        {
            GameObject targetItem = Resources.Load(targetItemName)?.Cast<GameObject>();
            if (ComponentUtils.GetModComponent(targetItem) != null)
            {
                // if this a modded item, map it now (no harm if it was already mapped earlier)
                Mapper.Map(targetItem);
            }

            return ModUtils.GetItem<GearItem>(targetItemName, reference);
        }
    }
}