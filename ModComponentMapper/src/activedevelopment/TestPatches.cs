using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harmony;
using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper
{
    internal class TestPatches
    {
        //[HarmonyPatch(typeof(InventoryGridItem),"Update")]
        //internal class changeIcons
        //{
        //    private static void Postfix(InventoryGridItem __instance)
        //    {

        //        __instance.m_GearSprite = new UITexture();
        //        __instance.m_GearSprite.mainTexture = (Utils.GetCachedTexture("ico_gearitem__skigoggles"));
        //    }
        //}
        [HarmonyPatch(typeof(GameManager),"Update")]
        internal class GameUpdateTestPatch
        {
            private static void Postfix()
            {
                
            }
        }

        [HarmonyPatch(typeof(GameManager),"Awake")]
        internal class GameAwakeTestPatch
        {
            private static void Postfix()
            {
                
            }
        }

        [HarmonyPatch(typeof(BlueprintDisplayItem), "Setup")]
        private static class FixRecipeIcons
        {
            internal static void Postfix(BlueprintDisplayItem __instance, BlueprintItem bpi)
            {
                string craftedGearName = bpi?.m_CraftedResult?.name;
                if ( craftedGearName != null && CraftingIconManager.PathDictContains(craftedGearName) )
                {
                    Texture2D texture = CraftingIconManager.GetCraftingIconFromGearName(craftedGearName);
                    if (texture != null)
                    {
                        __instance.m_Icon.mTexture = texture;
                    }
                }
            }
        }

        public static void CheckForModComponent()
        {
            GameObject gameObject = Resources.Load("GEAR_ScrapMetal").Cast<GameObject>();
            if(gameObject == null)
            {
                Logger.Log("Scrap metal was null");
            }
            ModComponent modComponent = ModUtils.GetComponent<ModComponent>(gameObject);
            if(modComponent == null)
            {
                Logger.Log("Scrap metal was unaffected.");
            }
            else
            {
                Logger.Log("Scrap metal has a mod component.");
            }
        }
        public static void CheckIfTheSame()
        {
            GameObject go1 = Resources.Load("GEAR_ScrapMetal").Cast<GameObject>();
            GameObject go2 = Resources.Load("GEAR_ScrapMetal").Cast<GameObject>();
            if(go1 == go2)
            {
                Logger.Log("The scrap metal variables reference the same object.");
            }
            else
            {
                Logger.Log("The scrap metal object are different from each other.");
            }
        }
    }
}
