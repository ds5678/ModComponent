using ModComponentAPI;

//Might want to remove sutures and sprains from the API if they're there

namespace ModComponentMapper.ComponentMapper
{
	internal static class FirstAidMapper
	{
		internal static void Configure(ModComponent modComponent)
		{
			ModFirstAidComponent modFirstAidComponent = modComponent.TryCast<ModFirstAidComponent>();
			if (modFirstAidComponent is null) return;

			FirstAidItem firstAidItem = ModComponentUtils.ComponentUtils.GetOrCreateComponent<FirstAidItem>(modFirstAidComponent);

			// not used
			firstAidItem.m_AppliesSutures = false;
			firstAidItem.m_StabalizesSprains = false;

			switch (modFirstAidComponent.FirstAidType)
			{
				case FirstAidType.Antibiotics:
					firstAidItem.m_ProvidesAntibiotics = true;
					break;

				case FirstAidType.Bandage:
					firstAidItem.m_AppliesBandage = true;
					break;

				case FirstAidType.Disinfectant:
					firstAidItem.m_CleansWounds = true;
					break;

				case FirstAidType.PainKiller:
					firstAidItem.m_KillsPain = true;
					break;
			}

			firstAidItem.m_HPIncrease = modFirstAidComponent.InstantHealing;
			firstAidItem.m_TimeToUseSeconds = modFirstAidComponent.TimeToUseSeconds;
			firstAidItem.m_UnitsPerUse = modFirstAidComponent.UnitsPerUse;
			firstAidItem.m_UseAudio = modFirstAidComponent.UseAudio;
		}
	}
}