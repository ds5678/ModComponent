using ModComponent.API;
using ModComponent.API.Components;

//Might want to remove sutures and sprains from the API if they're there

namespace ModComponent.Mapper.ComponentMapper
{
	internal static class FirstAidMapper
	{
		internal static void Configure(ModBaseComponent modComponent)
		{
			ModFirstAidComponent modFirstAidComponent = modComponent.TryCast<ModFirstAidComponent>();
			if (modFirstAidComponent == null) return;

			FirstAidItem firstAidItem = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<FirstAidItem>(modFirstAidComponent);

			// not used
			firstAidItem.m_AppliesSutures = false;
			firstAidItem.m_StabalizesSprains = false;

			switch (modFirstAidComponent.FirstAidType)
			{
				case ModFirstAidComponent.FirstAidKind.Antibiotics:
					firstAidItem.m_ProvidesAntibiotics = true;
					break;

				case ModFirstAidComponent.FirstAidKind.Bandage:
					firstAidItem.m_AppliesBandage = true;
					break;

				case ModFirstAidComponent.FirstAidKind.Disinfectant:
					firstAidItem.m_CleansWounds = true;
					break;

				case ModFirstAidComponent.FirstAidKind.PainKiller:
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