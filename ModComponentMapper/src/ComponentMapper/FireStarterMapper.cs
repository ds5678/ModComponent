using ModComponentAPI;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper.ComponentMapper
{
    internal class FireStarterMapper
    {
        internal static void Configure(ModComponent modComponent)
        {
            ModFireStarterComponent modFireStarterComponent = ModUtils.GetComponent<ModFireStarterComponent>(modComponent);
            if (modFireStarterComponent == null)
            {
                return;
            }

            FireStarterItem fireStarterItem = ModUtils.GetOrCreateComponent<FireStarterItem>(modFireStarterComponent);

            fireStarterItem.m_SecondsToIgniteTinder = modFireStarterComponent.SecondsToIgniteTinder;
            fireStarterItem.m_SecondsToIgniteTorch = modFireStarterComponent.SecondsToIgniteTorch;

            fireStarterItem.m_FireStartSkillModifier = modFireStarterComponent.SuccessModifier;

            fireStarterItem.m_ConditionDegradeOnUse = Mapper.GetDecayPerStep(modFireStarterComponent.NumberOfUses, modComponent.MaxHP);
            fireStarterItem.m_ConsumeOnUse = modFireStarterComponent.DestroyedOnUse;
            fireStarterItem.m_RequiresSunLight = modFireStarterComponent.RequiresSunLight;
            fireStarterItem.m_OnUseSoundEvent = modFireStarterComponent.OnUseSoundEvent;
        }
    }
}