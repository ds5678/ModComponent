using Harmony;
using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.patches
{
    [HarmonyPatch(typeof(GearItem), "Awake")]
    internal class GearItem_Awake
    {
        public static void Prefix(GearItem __instance)
        {
            //ModComponentFields.UpdateAllComponentFieldValues(__instance);
        }
        public static void Postfix(GearItem __instance)
        {
            StackableRandomization.MaybeNotFullStack(__instance);
        }
    }

    internal class StackableRandomization
    {
        public static void MaybeNotFullStack(GearItem gearItem)
        {
            StackableItem stackable = gearItem?.GetComponent<StackableItem>();
            ModStackableComponent modStackable = gearItem?.GetComponent<ModStackableComponent>();
            if (stackable && modStackable && !gearItem.m_BeenInspected)
            {
                if (UnityEngine.Random.Range(0f, 1f) > modStackable.ChanceFull && stackable.m_UnitsPerItem > 1)
                {
                    stackable.m_Units = UnityEngine.Random.Range(1, stackable.m_UnitsPerItem - 1);
                }
                else
                {
                    stackable.m_Units = stackable.m_UnitsPerItem;
                }
            }
        }
    }

    internal class ModComponentFields
    {
        public static void UpdateFieldValues<T>(GearItem gearItem) where T : UnityEngine.Component
        {
            T modComponent = ModUtils.GetComponent<T>(gearItem);
            if (modComponent == null)
            {
                return;
            }

            string gearName = ModUtils.NormalizeName(gearItem.name);
            //Logger.Log(gearName);
            GameObject prefab = Resources.Load(gearName)?.Cast<GameObject>();
            if (prefab == null)
            {
                Logger.Log("While copying fields for '{0}', the prefab was null.");
            }
            else
            {
                T prefabComponent = ModUtils.GetComponent<T>(prefab);
                if (prefabComponent != null)
                {
                    ModUtils.CopyFields<T>(modComponent, prefabComponent);
                    //Logger.Log("Successfully reassigned {0}", typeof(T).ToString());
                }
            }
        }

        public static void UpdateAllComponentFieldValues(GearItem gearItem)
        {
            UpdateFieldValues<ModBedComponent>(gearItem);
            UpdateFieldValues<ModBodyHarvestComponent>(gearItem);
            UpdateFieldValues<ModClothingComponent>(gearItem);
            UpdateFieldValues<ModCookableComponent>(gearItem);
            UpdateFieldValues<ModCookingPotComponent>(gearItem);
            UpdateFieldValues<ModExplosiveComponent>(gearItem);
            UpdateFieldValues<ModFirstAidComponent>(gearItem);
            UpdateFieldValues<ModFoodComponent>(gearItem);
            UpdateFieldValues<ModGenericComponent>(gearItem);
            UpdateFieldValues<ModLiquidComponent>(gearItem);
            UpdateFieldValues<ModRifleComponent>(gearItem);
            UpdateFieldValues<ModToolComponent>(gearItem);

            UpdateFieldValues<ModStackableComponent>(gearItem);
        }
    }
}
