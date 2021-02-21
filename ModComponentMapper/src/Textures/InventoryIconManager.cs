using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ModComponentMapper
{
    internal class InventoryIconManager
    {
        private static List<string> inventoryIconList = new List<string>();

        internal static void AddToIconList(string name)
        {
            if (Contains(name))
            {
                Logger.Log("'{0}' already in inventory icon list", name);
            }
            else
            {
                inventoryIconList.Add(name);
            }
        }
        internal static bool Contains(string name)
        {
            return inventoryIconList.Contains(name);
        }
        internal static Texture2D GetInventoryIcon(string name)
        {
            if (Contains(name))
            {
                return Resources.Load(name).Cast<Texture2D>();
            }
            else
            {
                return null;
            }
        }
        public static string GetIconNameFromGearItem(GearItem gi)
        {
            return GetIconNameFromGearItemName(gi.name);
        }

        public static string GetIconNameFromGearItemName(string gearItemName)
        {
            return "ico_GearItem__" + NameUtils.RemoveGearPrefix(gearItemName);
        }
    }

    internal class Patches
    {
        [HarmonyPatch(typeof(Utils), "GetInventoryGridIconTexture")]
        internal class InsertExternalGridIcons1
        {
            private static bool Prefix(string name, ref Texture2D __result)
            {
                //Logger.Log("GetInventoryGridIconTexture: '{0}'",name);
                if (InventoryIconManager.Contains(name.ToLower()))
                {
                    __result = InventoryIconManager.GetInventoryIcon(name.ToLower());
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [HarmonyPatch(typeof(Utils), "GetInventoryIconTexture")]
        internal class InsertExternalGridIcons2
        {
            private static bool Prefix(GearItem gi, ref Texture2D __result)
            {
                //Logger.Log("GetInventoryIconTexture: '{0}' , '{1}'", gi.name, gi.gameObject.name);
                string name = InventoryIconManager.GetIconNameFromGearItem(gi);
                if (InventoryIconManager.Contains(name.ToLower()))
                {
                    __result = InventoryIconManager.GetInventoryIcon(name.ToLower());
                    //Logger.Log("'{0}' has width '{1}' and height '{2}'", name, __result.width.ToString(), __result.height.ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        [HarmonyPatch(typeof(Utils), "GetInventoryIconTextureFromPrefabName")]
        internal class InsertExternalGridIcons3
        {
            private static bool Prefix(string prefabName, ref Texture2D __result)
            {
                //Logger.Log("GetInventoryIconTextureFromPrefabName: '{0}'", prefabName);
                string name = InventoryIconManager.GetIconNameFromGearItemName(prefabName);
                if (InventoryIconManager.Contains(name.ToLower()))
                {
                    __result = InventoryIconManager.GetInventoryIcon(name.ToLower());
                    //Logger.Log("'{0}' has width '{1}' and height '{2}'", name, __result.width.ToString(), __result.height.ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
