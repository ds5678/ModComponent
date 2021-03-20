using ModComponentMapper;

namespace RubberDuck
{
    public class RubberDuckImplementation
    {
        public void OnEquipped()
        {
            ShowButtonPopups();
        }

        public void OnPrimaryAction()
        {
            ModUtils.PlayAudio("Play_UseRubberDuck");
        }

        private static void ShowButtonPopups()
        {
            EquipItemPopupUtils.ShowItemPopups(Localization.Get("GAMEPLAY_Squeeze"), string.Empty, false, false, true);
        }
    }
}
