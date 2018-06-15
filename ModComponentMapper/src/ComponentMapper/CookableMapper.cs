using ModComponentAPI;
using System;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
    internal class CookableMapper
    {
        private static string GetDefaultCookAudio(ModCookableComponent modCookableComponent)
        {
            if (CookableType.Grub == modCookableComponent.type)
            {
                return "Play_BoilingLiquidThickHeavy";
            }

            if (CookableType.Meat == modCookableComponent.type)
            {
                return "Play_FryingHeavy";
            }

            return "Play_BoilingLiquidLight";
        }

        private static string GetDefaultStartCookingAudio(ModCookableComponent modCookableComponent)
        {
            if (CookableType.Grub == modCookableComponent.type)
            {
                return "Play_AddSlopToPot";
            }

            if (CookableType.Meat == modCookableComponent.type)
            {
                return "Play_AddMeatPan";
            }

            return "Play_AddWaterToPot";
        }

        internal static void ConfigureCookable(ModComponent modComponent)
        {
            ModCookableComponent modCookableComponent = modComponent as ModCookableComponent;
            if (modCookableComponent == null || !modCookableComponent.Cooking)
            {
                return;
            }

            Cookable cookable = ModUtils.GetOrCreateComponent<Cookable>(modCookableComponent);

            cookable.m_CookableType = ModUtils.TranslateEnumValue<Cookable.CookableType, CookableType>(modCookableComponent.type);
            cookable.m_CookTimeMinutes = modCookableComponent.CookingMinutes;
            cookable.m_ReadyTimeMinutes = modCookableComponent.BurntMinutes;
            cookable.m_NumUnitsRequired = modCookableComponent.CookingUnitsRequired;
            cookable.m_PotableWaterRequiredLiters = modCookableComponent.CookingWaterRequired;

            cookable.m_CookAudio = ModUtils.DefaultIfEmpty(modCookableComponent.CookingAudio, GetDefaultCookAudio(modCookableComponent));
            cookable.m_PutInPotAudio = ModUtils.DefaultIfEmpty(modCookableComponent.StartCookingAudio, GetDefaultStartCookingAudio(modCookableComponent));

            Cookable template = ModUtils.GetComponent<Cookable>(Resources.Load<GameObject>("GEAR_PinnacleCanPeaches"));
            cookable.m_MeshPotStyle = template?.m_MeshPotStyle;
            cookable.m_MeshCanStyle = template?.m_MeshCanStyle;
            cookable.m_LiquidMeshRenderer = template?.m_LiquidMeshRenderer;

            // either just heat or convert, but not both
            if (modCookableComponent.CookingResult == null)
            {
                // no conversion, just heating
                FoodItem foodItem = ModUtils.GetComponent<FoodItem>(modCookableComponent);
                if (foodItem != null)
                {
                    foodItem.m_HeatedWhenCooked = true;
                }
            }
            else
            {
                // no heating, but instead convert the item when cooking completes
                GearItem cookedGearItem = modCookableComponent.CookingResult.GetComponent<GearItem>();
                if (cookedGearItem == null)
                {
                    // not mapped yet, do it now
                    Mapper.Map(modCookableComponent.CookingResult);
                    cookedGearItem = modCookableComponent.CookingResult.GetComponent<GearItem>();
                }

                cookable.m_CookedPrefab = cookedGearItem ?? throw new ArgumentException("CookingResult does not map to GearItem for prefab " + modCookableComponent.name);
            }
        }
    }
}