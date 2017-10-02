
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponentAPI
{
    public enum LootTableName
    {
        LootTableHumanCorpse,
        LootTableBackPack,
        LootTableBathroomCabinet,
        LootTableCabinet,
        LootTableCashRegister,
        LootTableDresser,
        LootTableFileCabinet,
        LootTableFirstAidKit,
        LootTableFishingDrawer,
        LootTableFreezer,
        LootTableFridge,
        LootTableKitchenCupboard,
        LootTableLaundry,
        LootTableLocker,
        LootTableLockerLocked,
        LootTableMetalBox,
        LootTableOven,
        LootTablePlasticBox,
        LootTableSafe,
        LootTableToolChest_sml,
        LootTableToolChest,
        LootTableToolChestDrawer,
        LootTableWardrobe_regular,
        LootTableWorkbench,
    }

    public enum SceneName
    {
        FarmhouseA,
        SafeHouseA,
        QuonsetGasStation,
        CoastalHouseD,
        CoastalHouseE,
        CoastalRegion,
        RadioControlHut,
    }

    [DisallowMultipleComponent]
    public class AutoMapperComponent : MonoBehaviour
    {
        [Tooltip("How this item will be called in the DeveloperConsole. Leave empty for a sensible default.")]
        public string ConsoleName;

        public LootTableEntry[] Entries;

    }

    [Serializable]
    public class LootTableEntry
    {
        public LootTableName LootTable;

        [Tooltip("'1' is very unlikely (e.g. MRE), '20' is very likely (e.g. Cotton Socks).")]
        [Range(1, 50)]
        public int Chance;
    }
}
