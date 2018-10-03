using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
    internal class SharpenableMapper
    {
        public static void Configure(ModComponent modComponent)
        {
            ModSharpenableComponent modSharpenableComponent = ModUtils.GetComponent<ModSharpenableComponent>(modComponent);
            if (modSharpenableComponent == null)
            {
                return;
            }

            Sharpenable sharpenable = ModUtils.GetOrCreateComponent<Sharpenable>(modSharpenableComponent);

            sharpenable.m_ConditionIncreaseMax = modSharpenableComponent.ConditionMax;
            sharpenable.m_ConditionIncreaseMin = modSharpenableComponent.ConditionMin;
            sharpenable.m_DurationMinutesMax = modSharpenableComponent.MinutesMax;
            sharpenable.m_DurationMinutesMin = modSharpenableComponent.MinutesMin;

            sharpenable.m_SharpenToolChoices = ModUtils.GetItems<ToolsItem>(modSharpenableComponent.Tools, modComponent.name + ": Tools");
            sharpenable.m_RequiresToolToSharpen = sharpenable.m_SharpenToolChoices.Count() > 0;
        }
    }
}
