using Harmony;

namespace ModComponentMapper
{
    internal class TestPatches
    {
        /*[HarmonyPatch(typeof(GameManager), "Update")]
        internal class GameUpdateTestPatch
        {
            private static void Postfix()
            {

            }
        }*/

        [HarmonyPatch(typeof(GameManager), "Awake")]
        internal class GameAwakeTestPatch
        {
            private static void Postfix()
            {

            }
        }

        /*[HarmonyPatch(typeof(Container), "Awake")]
        internal class RandomizeLoot
        {
            private static void Postfix(Container __instance)
            {
                Logger.Log("Awake");
                if (!__instance.IsInspected())
                {
                    Logger.Log("Reseting Gear");
                    __instance.m_GearToInstantiate.Clear();
                    __instance.PopulateWithRandomGear();
                }
            }
        }

        [HarmonyPatch(typeof(Container), "Start")]
        internal class RandomizeLoot2
        {
            private static void Postfix(Container __instance)
            {
                Logger.Log("Start");
                if (!__instance.IsInspected())
                {
                    Logger.Log("Reseting Gear");
                    __instance.m_GearToInstantiate.Clear();
                    __instance.PopulateWithRandomGear();
                }
            }
        }*/


    }
}
