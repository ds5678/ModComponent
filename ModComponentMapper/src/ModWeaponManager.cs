using Harmony;
using System.Collections.Generic;
using UnityEngine;

using ModComponentAPI;

namespace ModComponentMapper
{
    public class ModWeaponManager
    {
        private static List<ModRifleComponent> pendingRifles = new List<ModRifleComponent>();

        public static void RegisterRifle(ModRifleComponent modRifleComponent)
        {
            pendingRifles.Add(modRifleComponent);
        }

        internal static void RegisterRifles(GameManager __instance)
        {
            Transform weaponViewTransform = __instance.m_PlayerObjectPrefab.transform.Find("WeaponView");

            foreach (ModRifleComponent eachPendingRifle in pendingRifles)
            {
                RegisterRifle(eachPendingRifle, weaponViewTransform);
            }
        }

        private static void RegisterRifle(ModRifleComponent modRifleComponent, Transform weaponViewTransform)
        {
            string name = ModUtils.NormalizeName(modRifleComponent.name);
            if (weaponViewTransform.Find(name) != null)
            {
                // already registered
                return;
            }

            LogUtils.Log("Registering '{0}'", name);

            GameObject template = weaponViewTransform.Find("Rifle").gameObject;
            GameObject weapon = Object.Instantiate(template, weaponViewTransform);
            weapon.name = name;
            weapon.AddComponent<FixMuzzleFlashTransformParent>();
            weapon.GetComponent<vp_FPSWeapon>().m_UseFirstPersonHands = false;

            Transform rifle_rig = weapon.transform.Find("rifle_rig");
            if (rifle_rig != null)
            {
                rifle_rig.transform.parent = null;
                Object.Destroy(rifle_rig.gameObject);
            }

            GameObject equippedModel = Object.Instantiate(modRifleComponent.EquippedModelPrefab, weapon.transform);
            equippedModel.name = (name + "_rig").ToLower();

        }
    }

    [HarmonyPatch(typeof(GameManager), "InstantiatePlayerObject")]
    class GameManagerInstantiatePlayerObjectPatch
    {
        public static void Prefix(GameManager __instance)
        {
            ModWeaponManager.RegisterRifles(__instance);
        }
    }

    class FixMuzzleFlashTransformParent : MonoBehaviour
    {
        public void Start()
        {
            vp_FPSShooter shooter = this.GetComponent<vp_FPSShooter>();
            if (shooter != null && shooter.MuzzleFlash != null)
            {
                shooter.MuzzleFlash.transform.parent = shooter.BulletEmissionLocator;
            }
        }
    }
}
