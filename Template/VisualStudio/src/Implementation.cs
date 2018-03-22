using ModComponentAPI;
using ModComponentMapper;

namespace RubberDuck
{
    public class Implementation
    {
        public ModComponent ModComponent;

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
            EquipItemPopupUtils.ShowItemPopups(Localization.Get("GAMEPLAY_Squeeze"), string.Empty, false, false, false, true);
        }
    }
}
