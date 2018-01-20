using Harmony;


namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Panel_Log), "EnableCraftingView")]
    internal class Panel_LogPatch
    {
        private static bool hasLoadedBlueprints = false;
        /// <summary>
        /// We have to wait until the game is loaded to add the item to the list of blueprints. So we are doing this when the crafting view comes to scene. But we can do it only once.
        /// </summary>
        public static void Prefix()
        {
            if (hasLoadedBlueprints)
                return;
            Mapper.MapBluePrints();
            hasLoadedBlueprints = true;
        }
    }
}
