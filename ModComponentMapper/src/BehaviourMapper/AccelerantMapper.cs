using ModComponentAPI;

namespace ModComponentMapper.ComponentMapper
{
    internal class AccelerantMapper
    {
        public static void Configure(ModComponent modComponent)
        {
            ModAccelerantComponent modAccelerantComponent = ModUtils.GetComponent<ModAccelerantComponent>(modComponent);
            if (modAccelerantComponent == null)
            {
                return;
            }

            FireStarterItem fireStarterItem = ModUtils.GetOrCreateComponent<FireStarterItem>(modAccelerantComponent);

            fireStarterItem.m_IsAccelerant = true;
            fireStarterItem.m_FireStartDurationModifier = modAccelerantComponent.DurationOffset;
            fireStarterItem.m_FireStartSkillModifier = modAccelerantComponent.SuccessModifier;
            fireStarterItem.m_ConsumeOnUse = modAccelerantComponent.DestroyedOnUse;
        }
    }
}
