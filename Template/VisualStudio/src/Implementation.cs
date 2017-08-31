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
            GameAudioManager.PlaySound("Play_UseRubberDuck", InterfaceManager.GetSoundEmitter());
        }

        private static void ShowButtonPopups()
        {
            EquipItemPopupUtils.ShowItemPopups(Localization.Get("GAMEPLAY_Squeeze"), string.Empty, false, false, false, true);
        }
    }
}
