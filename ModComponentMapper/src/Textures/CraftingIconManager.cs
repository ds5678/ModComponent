using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

namespace ModComponentMapper
{
    class CraftingIconManager
    {
        private static Dictionary<string, string> craftingIconDictionary = new Dictionary<string, string>(); //gear name : icon path
        internal static bool PathDictContains(string key)
        {
            return craftingIconDictionary.ContainsKey(key);
        }
        private static string GetIconPath(string gearName)
        {
            string key = gearName;
            if (PathDictContains(key))
            {
                return craftingIconDictionary[key];
            }
            else
            {
                return null;
            }
        }
        
        public static Texture2D GetTextureFromPath(string filepath, int width, int height)
        {
            if (File.Exists(filepath))
            {
                byte[] data = File.ReadAllBytes(filepath);
                Texture2D texture = new Texture2D(width,height);
                ImageConversion.LoadImage(texture, data);
                return texture;
            }
            else
            {
                return null;
            }
            
        }

        internal static Texture2D GetCraftingIconFromGearName(string gearName)
        {
            string iconPath = GetIconPath(gearName);
            return GetTextureFromPath(iconPath, 128, 128);
        }

        internal static void RegisterIcon(string gearName,string iconPath)
        {
            craftingIconDictionary.Add(gearName, iconPath);
            Logger.Log("Crafting Icon registered for '{0}'", gearName);
        }

        internal static string CraftingNameFromGearName(string gearName)
        {
            return NameUtils.AddCraftingIconPrefix(NameUtils.RemoveGearPrefix(gearName));
        }
        
        internal static string GearNameFromCraftingName(string craftingName)
        {
            return NameUtils.AddGearPrefix(NameUtils.RemoveCraftingIconPrefix(craftingName));
        }
    }
}
