using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(GearItem), "Awake")]
    public class GearItem_Awake
    {
        public static void Postfix(GearItem __instance)
        {
            ModToolComponent modTool = ModUtils.GetComponent<ModToolComponent>(__instance);
            if (modTool == null)
            {
                return;
            }
            else if(__instance.name == "Gear_SledgeHammer")
            {
                Logger.Log("Could not find tool component for sledge hammer");
            }

            Logger.Log(__instance.name.Replace("(Clone)",""));
            GameObject prefab = Resources.Load(__instance.name.Replace("(Clone)", ""))?.Cast<GameObject>();
            if(prefab != null)
            {
                ModToolComponent prefabTool = ModUtils.GetComponent<ModToolComponent>(prefab);
                if(prefabTool != null)
                {
                    modTool.ConsoleName = prefabTool.ConsoleName;
                    Logger.Log("Successfully reassigned");
                }
                else
                {
                    Logger.Log("couldn't get tool component from prefab");
                }
            }
            else
            {
                Logger.Log("Sledgehammer prefab was null.");
            }
        }
    }
}
