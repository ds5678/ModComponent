extern alias Hinterland;
using Hinterland;
using ModComponent.API.Behaviours;
using ModComponent.API.Components;

namespace ModComponent.Mapper.BehaviourMappers;

internal static class FireStarterMapper
{
	internal static void Configure(ModBaseComponent modComponent)
	{
		ModFireStarterBehaviour modFireStarterComponent = ModComponent.Utils.ComponentUtils.GetComponentSafe<ModFireStarterBehaviour>(modComponent);
		if (modFireStarterComponent == null)
		{
			return;
		}

		FireStarterItem fireStarterItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FireStarterItem>(modFireStarterComponent);

		fireStarterItem.m_SecondsToIgniteTinder = modFireStarterComponent.SecondsToIgniteTinder;
		fireStarterItem.m_SecondsToIgniteTorch = modFireStarterComponent.SecondsToIgniteTorch;

		fireStarterItem.m_FireStartSkillModifier = modFireStarterComponent.SuccessModifier;

		fireStarterItem.m_ConditionDegradeOnUse = ItemMapper.GetDecayPerStep(modFireStarterComponent.NumberOfUses, modComponent.MaxHP);
		fireStarterItem.m_ConsumeOnUse = modFireStarterComponent.DestroyedOnUse;
		fireStarterItem.m_RequiresSunLight = modFireStarterComponent.RequiresSunLight;
		fireStarterItem.m_OnUseSoundEvent = modFireStarterComponent.OnUseSoundEvent;
	}
}