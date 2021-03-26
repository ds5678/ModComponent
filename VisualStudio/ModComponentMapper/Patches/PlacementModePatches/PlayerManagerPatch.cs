using Harmony;
using UnityEngine;

//did a first pass through 
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
            if (__instance.m_ObjectToPlace)
            {
                __instance.m_ObjectToPlace.AddComponent<RestoreMaterialQueue>();
            }
        }
    }
    //End Replacement Patches



    //TRANSPILER!!!!!!! <===========================================================================================================
    //Seems to be removing the wolf intimidation buff calculation
    /*[HarmonyPatch(typeof(PlayerManager), "UpdateBuffDurations")]//Exists
    internal class PlayerManager_UpdateBuffDurations
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                MethodInfo methodInfo = codes[i].operand as MethodInfo;
                if (methodInfo != null && methodInfo.Name == "GetWornWolfIntimidationClothing" && methodInfo.DeclaringType == typeof(PlayerManager))
                {
                    for (int j = 0; j < 17; j++)
                    {
                        codes.RemoveAt(i - 1);
                    }
                }
            }

            return codes;
        }
    }
    */
}