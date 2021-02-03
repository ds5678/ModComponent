using ModComponentAPI;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper.ComponentMapper
{
    internal class EvolveMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            ModEvolveComponent modEvolveComponent = ModUtils.GetComponent<ModEvolveComponent>(modComponent);
            if (modEvolveComponent == null)
            {
                return;
            }

            EvolveItem evolveItem = ModUtils.GetOrCreateComponent<EvolveItem>(modEvolveComponent);
            evolveItem.m_ForceNoAutoEvolve = false;
            evolveItem.m_GearItemToBecome = GetTargetItem(modEvolveComponent.TargetItemName, modEvolveComponent.name);
            evolveItem.m_RequireIndoors = modEvolveComponent.IndoorsOnly;
            evolveItem.m_StartEvolvePercent = 0;
            evolveItem.m_TimeToEvolveGameDays = Mathf.Clamp(modEvolveComponent.EvolveHours / 24f, 0.01f, 1000);
        }

        private static GearItem GetTargetItem(string targetItemName, string reference)
        {
            GameObject targetItem = Resources.Load(targetItemName) as GameObject;
            if (ModUtils.GetModComponent(targetItem) != null)
            {
                // if this a modded item, map it now (no harm if it was already mapped earlier)
                Mapper.Map(targetItem);
            }

            return ModUtils.GetItem<GearItem>(targetItemName, reference);
        }
    }
}