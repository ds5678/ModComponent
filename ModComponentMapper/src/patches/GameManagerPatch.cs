using Harmony;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(GameManager), "Awake")]
    internal class GameManager_Awake
    {
        public static void Postfix()
        {
            try
            {
                Mapper.MapBlueprints();
                Mapper.MapSkills();
            }
            catch (System.Exception e)
            {
                Debug.Log("Mapping failed: " + e);
            }
        }
    }
}