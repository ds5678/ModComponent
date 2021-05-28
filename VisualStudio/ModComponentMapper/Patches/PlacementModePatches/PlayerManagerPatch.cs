/*using Harmony;
using UnityEngine;

//I think this does nothing

//HAS A TRANSPILER PATCH, which I think I fixed
//Inline patch probably fixed
//does not need to be declared
//might have some additional inlined methods

namespace ModComponentMapper
{
	[HarmonyPatch(typeof(PlayerManager), "RestoreOriginalTint")]//Exists
	internal class PlayerManager_RestoreOriginalTint
	{
		internal static void Postfix(PlayerManager __instance, GameObject go)
		{
			Logger.Log("RestoreOriginalTint");
			Object.Destroy(go.GetComponent<RestoreMaterialQueue>());
		}
	}

	//[HarmonyPatch(typeof(PlayerManager), "StoreOriginalTint")]//DOES NOT EXIST !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	//internal class PlayerManager_StoreOriginalTint
	//{
	//    internal static void Prefix(PlayerManager __instance, GameObject go)
	//    {
	//        go.AddComponent<RestoreMaterialQueue>();
	//    }
	//}

	//Replacement Patches
	[HarmonyPatch(typeof(PlayerManager), "PrepareGhostedObject")]//inlined?
	internal class PlayerManager_PrepareGhostedObject
	{
		internal static void Prefix(PlayerManager __instance)
		{
			Logger.Log("PrepareGhostedObject");
			if (__instance.m_ObjectToPlace)
			{
				__instance.m_ObjectToPlace.AddComponent<RestoreMaterialQueue>();
			}
		}
	}

	[HarmonyPatch(typeof(PlayerManager), "OnPlaceMeshAnimationEvent")]//inlined
	internal class PlayerManager_OnPlaceMeshAnimationEvent
	{
		internal static void Prefix(PlayerManager __instance)
		{
			Logger.Log("OnPlaceMeshAnimationEvent");
			if (__instance.m_ObjectToPlace)
			{
				__instance.m_ObjectToPlace.AddComponent<RestoreMaterialQueue>();
			}
		}
	}
	//End Replacement Patches
}*/