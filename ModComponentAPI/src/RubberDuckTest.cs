using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModComponentAPI
{
    public class RubberDuckTest : Il2CppSystem.Object
    {
        private static bool registeredInI2Cpp = false;
        public ModComponent ModComponent = null;

        public RubberDuckTest(System.IntPtr intPtr) : base(intPtr) { }

        public static Il2CppSystem.Type GetIl2CppSystemType()
        {
            if (!registeredInI2Cpp)
            {
                //UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<RubberDuckTest>();
                registeredInI2Cpp = true;
            }
            return UnhollowerRuntimeLib.Il2CppType.Of<RubberDuckTest>();
        }

        public void OnEquipped()
        {
            ShowButtonPopups();
        }

        public void OnPrimaryAction()
        {
            //ModUtils.PlayAudio("Play_UseRubberDuck");
        }

        private static void ShowButtonPopups()
        {
            //EquipItemPopupUtils.ShowItemPopups(Localization.Get("GAMEPLAY_Squeeze"), string.Empty, false, false, true);
        }
    }
}
