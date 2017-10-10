using UnityEngine;

namespace ModComponentAPI
{
    public enum LootTableName
    {
        BackPack,
        BathroomCabinet,
        Cabinet,
        CargoClothingA,
        CargoClothingB,
        CargoClothingC,
        CargoClothingRareA,
        CargoClothingRareB,
        CargoClothingRareC,
        CargoDrinkA,
        CargoDrinkB,
        CargoDrinkC,
        CargoFire,
        CargoFoodA,
        CargoFoodB,
        CargoFoodC,
        CargoFoodRareA,
        CargoFoodRareB,
        CargoFoodRareC,
        CargoMaterialsA,
        CargoMaterialsB,
        CargoMaterialsRareA,
        CargoMaterialsRareB,
        CargoMaterialsRareC,
        CargoMedical,
        CargoMedicalRare,
        CargoMiscA,
        CargoMiscB,
        CargoMiscC,
        CargoMiscRareA,
        CargoMiscRareB,
        CargoMiscRareC,
        CargoRifle,
        CargoShoes,
        CargoTools,
        CashRegister,
        Dresser,
        EndTable_bedroom,
        FileCabinet,
        FirstAidKit,
        FishingDrawer,
        FishingSaltWater,
        FishingFreshWater,
        Freezer,
        Fridge,
        GenericCabinet,
        HumanCorpse,
        HumanCorpseRare,
        KitchenCupboard,
        Laundry,
        Locker,
        LockerLocked,
        MetalBox,
        Oven,
        PlasticBox,
        RifleSupplies,
        Safe,
        Stones,
        TideLine,
        ToolChest_sml,
        ToolChest,
        ToolChestDrawer,
        VehicleGloveBox,
        VehicleTrunk,
        VehicleTrunkLocked,
        Wardrobe_regular,
        Workbench,
    }

    public class ModLootTableEntryComponent : MonoBehaviour
    {
        public LootTableName LootTable;

        [Tooltip("'1' is very unlikely (e.g. MRE), '20' is very likely (e.g. Cotton Socks).")]
        [Range(1, 50)]
        public int Weight = 1;
    }
}