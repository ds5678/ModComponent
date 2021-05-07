using System.Collections.Generic;

namespace Preloader
{
	internal static class SceneObjects
	{
		internal readonly static Dictionary<string, List<string>> loadList = new Dictionary<string, List<string>>();
		internal readonly static Dictionary<string, List<string>> defaultLoadList = new Dictionary<string, List<string>>()
		{
			["AshCanyonRegion_SANDBOX"] = new List<string>()
			{
				"Root/Design/Interactive/INTERACTIVE_WorkBenchB",//dynamic
				"Root/Design/Interactive/INTERACTIVE_FireBarrel"//dynamic
			},
			["CampOffice"] = new List<string>()
			{
				//"Root/Design/Interactive/INTERACTIVE_WorkBench",//static
				//"Root/Design/Interactive/INTERACTIVE_BunkBed",//static
				"Root/Design/Interactive/INTERACTIVE_PotBellyStove",//dynamic
				"Root/Design/Interactive/INTERACTIVE_StoveWoodC",//dynamic
				"Root/Design/Interactive/INTERACTIVE_RadioA"//dynamic
				
			},
			//["FarmhouseB"] = new List<string>()
			//{
			//"Root/Design/Interactive/INTERACTIVE_StoveMetalA",//static
			//"Root/Design/Interactive/INTERACTIVE_FirePlace"//dynamic
			//},
			//["CoastalHouseC"] = new List<string>()
			//{
			//	"Root/Design/Interactive/INTERACTIVE_KingBedA"//static
			//},
			["PrepperCacheA"] = new List<string>()
			{
				//"Root/Art/Geo/OBJ_MetalShelfA_Prefab",//static
				//"Root/Art/Geo/OBJ_MetalShelfB_Prefab",//static
				//"Root/Art/Geo/OBJ_MetalShelfC_Prefab",//static
				//"Root/Art/Geo/OBJ_MetalShelfD_Prefab",//static
				"Root/Design/Interactive/INTERACTIVE_BedMattressB",//dynamic
				"Root/Art/Geo/OBJ_BoxCrateA_Prefab",//dynamic
				"Root/Art/Geo/OBJ_BoxCrateB_Prefab"//dynamic
			},
			["WoodCabinC"] = new List<string>()
			{
				//"Root/Design/Interactive/INTERACTIVE_TwinBedB",//static
				"Root/Design/Interactive/INTERACTIVE_RimGrill"//dynamic
			},
			["WhalingShipA"] = new List<string>()
			{
				"Root/Art/INTERACTIVE_Forge"//dynamic
				//"Root/Design/Scripting/SCRIPT_GameManager"
			},
			["MaintenanceShedB_SANDBOX"] = new List<string>()
			{
				"Design/Gear/Interactive/INTERACTIVE_AmmoWorkBench",//dynamic
				"Design/Gear/Interactive/INTERACTIVE_IndustrialMillingMachine"//dynamic
			}
		};

		internal static void AddToList(string scene, string path)
		{
			if (!loadList.ContainsKey(scene)) loadList.Add(scene, new List<string>());

			if (!loadList[scene].Contains(path)) loadList[scene].Add(path);
#if DEBUG
			else Logger.LogWarning("Object Preloading List already contains a request in '{0}' for '{1}'", scene, path);
#endif
		}
		internal static void AddToList(Dictionary<string, List<string>> otherList)
		{
			foreach (var scenePair in otherList)
			{
				foreach (string path in scenePair.Value)
				{
					AddToList(scenePair.Key, path);
				}
			}
		}
	}
}
