using Harmony;
using UnityEngine;

//Appears to handle an issue occuring when something harvests into more than two item types
//I think it's fixing a visual issue

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Panel_Inventory_Examine), "Start")]//runs
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