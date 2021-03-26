using ModComponentAPI;
using System;

namespace ModComponentMapper.ComponentMapper
{
    internal class HarvestableMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            ModHarvestableComponent modHarvestableComponent = ModUtils.GetComponent<ModHarvestableComponent>(modComponent);
            if (modHarvestableComponent == null)
            {
                return;
            }

            Harvest harvest = ModUtils.GetOrCreateComponent<Harvest>(modHarvestableComponent);
            harvest.m_Audio = modHarvestableComponent.Audio;
            harvest.m_DurationMinutes = modHarvestableComponent.Minutes;

            if (modHarvestableComponent.YieldNames.Length != modHarvestableComponent.YieldCounts.Length)
            {
                throw new ArgumentException("YieldNames and YieldCounts do not have the same length on gear item '" + modHarvestableComponent.name + "'.");
            }

            harvest.m_YieldGear = ModUtils.GetItems<GearItem>(modHarvestableComponent.YieldNames, modHarvestableComponent.name);
            harvest.m_YieldGearUnits = modHarvestableComponent.YieldCounts;

            harvest.m_AppliedSkillType = SkillType.None;
            harvest.m_RequiredTools = ModUtils.GetItems<ToolsItem>(modHarvestableComponent.RequiredToolNames, modHarvestableComponent.name);
            harvest.m_GunpowderYield = 0f;
        }
    }
}