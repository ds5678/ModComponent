using ModComponentAPI;
using System.Linq;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class SharpenableMapper
	{
		internal static void Configure(ModComponent modComponent) => Configure(modComponent.gameObject);
		public static void Configure(GameObject prefab)
		{
			ModSharpenableComponent modSharpenableComponent = ModComponentUtils.ComponentUtils.GetComponent<ModSharpenableComponent>(prefab);
			if (modSharpenableComponent is null) return;

			Sharpenable sharpenable = ModComponentUtils.ComponentUtils.GetOrCreateComponent<Sharpenable>(modSharpenableComponent);

			sharpenable.m_ConditionIncreaseMax = modSharpenableComponent.ConditionMax;
			sharpenable.m_ConditionIncreaseMin = modSharpenableComponent.ConditionMin;
			sharpenable.m_DurationMinutesMax = modSharpenableComponent.MinutesMax;
			sharpenable.m_DurationMinutesMin = modSharpenableComponent.MinutesMin;

			sharpenable.m_SharpenToolChoices = ModComponentUtils.ModUtils.GetItems<ToolsItem>(modSharpenableComponent.Tools, prefab.name + ": Tools");
			sharpenable.m_RequiresToolToSharpen = sharpenable.m_SharpenToolChoices.Count() > 0;
		}
	}
}
