using UnityEngine;

namespace ModComponentAPI
{
    public enum SceneName
    {
        BankA,
        BarnHouseA,
        CampOffice,
        CaveB,
        CaveC,
        CaveD,
        ChurchB,
        CoastalHouseA,
        CoastalHouseB,
        CoastalHouseC,
        CoastalHouseD,
        CoastalHouseE,
        CoastalHouseF,
        CoastalHouseg,
        CoastalHouseh,
        CoastalRegion,
        ConvenienceStoreA,
        CrashMountainRegion,
        Dam,
        DamCaveTransitionZone,
        DamRiverTransitionZoneB,
        DamTrailerB,
        DamTransitionZone,
        FarmHouseA,
        FarmHouseABasement,
        FarmHouseB,
        FishingCabinA,
        FishingCabinB,
        FishingCabinC,
        FishingCabinD,
        FoodBox,
        GreyMothersHouseA,
        HighwayMineTransitionZone,
        HighwayTransitionZone,
        HouseBasementC,
        HouseBasementE,
        HouseBasementF,
        HouseBasementPV,
        HuntingLodgeA,
        LakeCabinA,
        LakeCabinB,
        LakeCabinC,
        LakeCabinD,
        LakeCabinE,
        LakeCabinF,
        LakeRegion,
        LightHouseA,
        LoneLakeCabinA,
        MaintenanceShedA,
        MarshRegion,
        MiltonHouseA,
        MiltonHouseC,
        MiltonHouseD,
        MiltonHouseF1,
        MiltonHouseF2,
        MiltonHouseF3,
        MiltonHouseG,
        MiltonHouseH1,
        MiltonHouseH2,
        MiltonHouseH3,
        MiltonTrailerB,
        MineTransitionZone,
        MountainCaveA,
        MountainCaveB,
        MountaintownCaveA,
        MountaintownCaveB,
        MountaintownRegion,
        NorthernHighwayRegion,
        ObservatoryRegion,
        PostOfficeA,
        PrepperCacheA,
        PrepperCacheAurora,
        PrepperCacheB,
        PrepperCacheC,
        PrepperCacheD,
        PrepperCacheE,
        PrepperCacheEmpty,
        PrepperCacheF,
        QuonsetGasStation,
        RadioControlHut,
        RavineTransitionZone,
        RuralRegion,
        RuralStoreA,
        SafeHouseA,
        TracksRegion,
        TrailerA,
        TrailerB,
        TrailerC,
        TrailerD,
        TrailerSShape,
        WhalingMine,
        WhalingShipA,
        WhalingStationRegion,
        WhalingWarehouseA,
        WoodCabinA,
        WoodCabinB,
        WoodCabinC,
    }

    public class ModSpawnLocationComponent : MonoBehaviour
    {
        [Tooltip("Name of the scene in which to spawn this item.")]
        public SceneName Scene;

        [Tooltip("Position in the scene.")]
        public Vector3 Position;

        [Tooltip("Rotation of the item.")]
        public Vector3 Rotation;

        [Tooltip("How likely is this item to spawn at this location? 100 means 'Always', 0 means 'Never'.")]
        [Range(1, 100)]
        public int SpawnChance = 100;
    }
}