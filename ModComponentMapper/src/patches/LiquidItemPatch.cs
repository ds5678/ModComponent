using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using UnityEngine;

namespace ModComponentMapper
{
    internal class LiquidItemPatch
    {
        //make water containers able to have randomized initial quantities
        [HarmonyPatch(typeof(LiquidItem),"Awake")]
        internal class LiquidItem_Awake
        {
            private static void Postfix(LiquidItem __instance)
            {
                if(__instance.m_RandomizeQuantity && __instance.m_LiquidType == GearLiquidTypeEnum.Water)
                {
                    __instance.m_LiquidLiters = UnityEngine.Random.Range(__instance.m_LiquidCapacityLiters / 8f, __instance.m_LiquidCapacityLiters);
                }
            }
        }
    }
}
