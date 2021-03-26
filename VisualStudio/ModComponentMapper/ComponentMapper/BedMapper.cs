using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
    internal class BedMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            ModBedComponent modBedComponent = modComponent as ModBedComponent;
            if (modBedComponent == null)
            {
                return;
            }

            Bed bed = ModUtils.GetOrCreateComponent<Bed>(modBedComponent);

            bed.m_LocalizedDisplayName = Mapper.CreateLocalizedString(modComponent.DisplayNameLocalizationId);
            bed.m_ConditionPercentGainPerHour = modBedComponent.ConditionGainPerHour;
            bed.m_UinterruptedRestPercentGainPerHour = modBedComponent.AdditionalConditionGainPerHour;
            bed.m_WarmthBonusCelsius = modBedComponent.WarmthBonusCelsius;

            bed.m_PercentChanceReduceBearAttackWhenSleeping = modBedComponent.BearAttackModifier;
            bed.m_PercentChanceReduceWolfAttackWhenSleeping = modBedComponent.WolfAttackModifier;

            bed.m_OpenAudio = ModUtils.DefaultIfEmpty(modBedComponent.OpenAudio, "PLAY_SNDGENSLEEPINGBAGCLOSE");
            bed.m_CloseAudio = ModUtils.DefaultIfEmpty(modBedComponent.CloseAudio, "PLAY_SNDGENSLEEPINGBAGOPEN");

            bed.m_BedRollMesh = modBedComponent.PackedMesh ?? modBedComponent.gameObject;
            bed.m_BedRollMesh.layer = vp_Layer.Gear;
            bed.m_BedRollPlacedMesh = modBedComponent.UsableMesh ?? modBedComponent.gameObject;
            bed.m_BedRollPlacedMesh.layer = vp_Layer.Gear;
            bed.SetState(BedRollState.Rolled);

            DegradeOnUse degradeOnUse = ModUtils.GetOrCreateComponent<DegradeOnUse>(modBedComponent);
            degradeOnUse.m_DegradeHP = Mathf.Max(degradeOnUse.m_DegradeHP, modBedComponent.DegradePerHour);

            PlaceableItem placeableItem = ModUtils.GetOrCreateComponent<PlaceableItem>(modBedComponent);
            //placeableItem.m_Range = 4;
            //m_prefab_name ???
        }
    }
}