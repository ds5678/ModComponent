using AssetLoader;
using ModComponentMapper;
using UnityEngine;

namespace RubberDuck
{
    internal class Initializer
    {
        private const string PREFAB_RUBBER_DUCK = "GEAR_RubberDuck";
        private const string DISPLAY_NAME_RUBBER_DUCK = "RubberDuck";

        public static void OnLoad()
        {
            ModAssetBundleManager.RegisterAssetBundle("Rubber-Duck/rubber_duck.unity3d");
            ModSoundBankManager.RegisterSoundBank("Rubber-Duck/rubber_duck.bnk");

            ModUtils.RegisterConsoleGearName(DISPLAY_NAME_RUBBER_DUCK, PREFAB_RUBBER_DUCK);
            ModUtils.InsertIntoLootTable(LootTableName.LootTableBathroomCabinet, (GameObject)Resources.Load(PREFAB_RUBBER_DUCK), 10);
        }
    }
}
