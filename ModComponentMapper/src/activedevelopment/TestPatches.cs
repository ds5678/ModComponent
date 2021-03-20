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

        /*[HarmonyPatch(typeof(TextureInjector), "Start")]
        internal class MaterialTestPatch
        {
            private static bool Prefix(TextureInjector __instance)
            {
                if (ModUtils.GetComponent<ModComponent>(__instance))
                {
                    Logger.Log("Saved '{0}' from start", __instance.name);
                    return false;
                }
                Logger.Log("Start: '{0}'", __instance.name); 
                return true;
            }
        }

        [HarmonyPatch(typeof(TextureInjector), "Initialize")]
        internal class MaterialTestPatch2
        {
            private static bool Prefix(TextureInjector __instance)
            {
                if (ModUtils.GetComponent<ModComponent>(__instance))
                {
                    Logger.Log("Saved '{0}' from initialization", __instance.name);
                    return false;
                }
                Logger.Log("Initialization '{0}'", __instance.name);
                return true;
            }
        }*/

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
