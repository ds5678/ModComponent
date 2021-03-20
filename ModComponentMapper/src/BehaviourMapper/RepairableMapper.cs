using ModComponentAPI;
using System;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper.ComponentMapper
{
    internal class RepairableMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            ModRepairableComponent modRepairableComponent = modComponent.GetComponent<ModRepairableComponent>();
            if (modRepairableComponent == null)
            {
                return;
            }

            Repairable repairable = ModUtils.GetOrCreateComponent<Repairable>(modRepairableComponent);
            repairable.m_RepairAudio = modRepairableComponent.Audio;
            repairable.m_DurationMinutes = modRepairableComponent.Minutes;
            repairable.m_ConditionIncrease = modRepairableComponent.Condition;

            if (modRepairableComponent.MaterialNames.Length != modRepairableComponent.MaterialCounts.Length)
            {
                throw new ArgumentException("MaterialNames and MaterialCounts do not have the same length on gear item '" + modRepairableComponent.name + "'.");
            }

            repairable.m_RequiredGear = ModUtils.GetItems<GearItem>(modRepairableComponent.MaterialNames, modRepairableComponent.name);
            repairable.m_RequiredGearUnits = modRepairableComponent.MaterialCounts;

            repairable.m_RepairToolChoices = ModUtils.GetItems<ToolsItem>(modRepairableComponent.RequiredTools, modRepairableComponent.name);
            repairable.m_RequiresToolToRepair = repairable.m_RepairToolChoices.Length > 0;
        }
    }
}