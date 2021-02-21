using ModComponentAPI;
using ModComponentMapper;
using MelonLoader;
using UnityEngine;

namespace RubberDuck
{
    /*class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<RubberDuckImplementation>();
        }

        internal static void Log(string message)
        {
            MelonLogger.Log(message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            Log(preformattedMessage);
        }
    }

    public class TestClass
    {
        public static int GetNumber()
        {
            return 42;
        }
    }*/

    public class RubberDuckImplementation : Il2CppSystem.Object
    {
        private static bool registeredInI2Cpp = false;
        public ModComponent ModComponent = null;

        public RubberDuckImplementation(System.IntPtr intPtr) : base(intPtr) { }

        public static Il2CppSystem.Type GetIl2CppSystemType()
        {
            if (!registeredInI2Cpp)
            {
                UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<RubberDuckImplementation>();
                registeredInI2Cpp = true;
            }
            return UnhollowerRuntimeLib.Il2CppType.Of<RubberDuckImplementation>();
        }

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
