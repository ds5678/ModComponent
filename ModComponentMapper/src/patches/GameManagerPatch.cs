using Harmony;


namespace ModComponentMapper
{
    [HarmonyPatch(typeof(GameManager), "InstantiateSystems")]
    internal class GameManagerInstantiateSystemsPatch
    {
        public static void Postfix()
        {
            Mapper.MapBlueprints();
        }
    }
}
