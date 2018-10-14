using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Harmony;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(Panel_Log), "RefreshBlueprintsSlider")]
    class Panel_LogPatch
    {
        public static void Postfix(Panel_Log __instance)
        {
            if (__instance.m_ScrollbarBlueprints.activeSelf)
            {
                UISlider componentsInChild = __instance.m_ScrollbarBlueprints.GetComponentsInChildren<UISlider>(true)[0];
                if (componentsInChild == null)
                {
                    return;
                }

                List<BlueprintItem> m_FilteredBlueprintItemList = (List<BlueprintItem>)AccessTools.Field(__instance.GetType(), "m_FilteredBlueprintItemList").GetValue(__instance);
                int v = Mathf.CeilToInt(m_FilteredBlueprintItemList.Count / 4f) - 3;

                componentsInChild.numberOfSteps = v;
            }
        }
    }
}
