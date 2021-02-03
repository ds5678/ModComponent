using Harmony;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Panel_ActionsRadial), "Start")]//Exists
    internal class Panel_ActionsRadial_Start
    {
        public static void Postfix(Panel_ActionsRadial __instance)
        {
            RadialConfigurator.InsertAllGears();
        }
    }
}