using Harmony;
using UnityEngine;

//did a first pass through; didn't find anything
//does not need to be declared

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), "Start")]//Exists
    internal class Panel_Inventory_Examine_Start
    {
        internal static void Postfix(Panel_Inventory_Examine __instance)
        {
            if (__instance.m_HarvestYields == null || __instance.m_HarvestYields.Length > 2)
            {
                return;
            }

            HarvestRepairMaterial[] array = new HarvestRepairMaterial[3];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Object.Instantiate(__instance.m_HarvestYields[0].gameObject, __instance.m_HarvestYields[0].transform.parent).GetComponent<HarvestRepairMaterial>();
                array[i].Hide();
            }

            for (int i = 0; i < __instance.m_HarvestYields.Length; i++)
            {
                Object.Destroy(__instance.m_HarvestYields[i].gameObject);
            }

            __instance.m_HarvestYields = array;
            __instance.m_HarvestYieldSpacing *= 0.8f;
        }
    }
}