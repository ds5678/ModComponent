using ModComponentAPI;
using System;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class MillableMapper
	{
		internal static void Configure(ModComponent modComponent) => Configure(modComponent.gameObject);
		internal static void Configure(GameObject prefab)
		{
			ModMillableComponent modMillable = ComponentUtils.GetComponent<ModMillableComponent>(prefab);
			if (modMillable == null) return;

			Millable millable = ComponentUtils.GetOrCreateComponent<Millable>(modMillable);
			millable.m_CanRestoreFromWornOut = modMillable.CanRestoreFromWornOut;
			millable.m_RecoveryDurationMinutes = modMillable.RecoveryDurationMinutes;
			millable.m_RepairDurationMinutes = modMillable.RepairDurationMinutes;
			millable.m_Skill = EnumUtils.TranslateEnumValue<SkillType, ModComponentAPI.SkillType>(modMillable.skill);
			if (modMillable.RepairRequiredGear.Length != modMillable.RepairRequiredGearUnits.Length)
			{
				throw new ArgumentException("RepairRequiredGear and RepairRequiredGearUnits do not have the same length on gear item '" + modMillable.name + "'.");
			}
			millable.m_RepairRequiredGear = ModUtils.GetItems<GearItem>(modMillable.RepairRequiredGear, modMillable.name);
			millable.m_RepairRequiredGearUnits = modMillable.RepairRequiredGearUnits;
			if (modMillable.RestoreRequiredGear.Length != modMillable.RestoreRequiredGearUnits.Length)
			{
				throw new ArgumentException("RestoreRequiredGear and RestoreRequiredGearUnits do not have the same length on gear item '" + modMillable.name + "'.");
			}
			millable.m_RestoreRequiredGear = ModUtils.GetItems<GearItem>(modMillable.RestoreRequiredGear, modMillable.name);
			millable.m_RestoreRequiredGearUnits = modMillable.RestoreRequiredGearUnits;
		}
	}
}
