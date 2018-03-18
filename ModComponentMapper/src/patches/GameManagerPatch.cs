using Harmony;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal class GameManager_Awake
    {
        public static void Postfix()
        {
            Mapper.MapBlueprints();
            Mapper.MapSkills();
        }
    }
}