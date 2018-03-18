using Harmony;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Panel_ActionsRadial), "Start")]
    internal class Panel_ActionsRadial_Start
    {
        public static void Postfix(Panel_ActionsRadial __instance)
        {
            RadialConfigurator.InsertAllGears();
        }
    }
}