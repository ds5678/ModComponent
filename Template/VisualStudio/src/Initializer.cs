using AssetLoader;
using ModComponentMapper;

using static ModComponentMapper.LootTableName;

namespace RubberDuck
{
    internal class Initializer
    {
        public static void OnLoad()
        {
            ModAssetBundleManager.RegisterAssetBundle("Rubber-Duck/rubber_duck.unity3d");
            ModSoundBankManager.RegisterSoundBank("Rubber-Duck/rubber_duck.bnk");

            InitRubberDuck();
        }

        private static void InitRubberDuck()
        {
            Mapper.Map("GEAR_RubberDuck")
                .RegisterInConsole("RubberDuck")
                .AddToLootTable(LootTableBathroomCabinet, 10);
        }
    }
}
