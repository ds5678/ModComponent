using Harmony;
using ModComponentAPI;
using System;
using System.Collections.Generic;
using UnityEngine;

//did a first pass through; didn't find anything
//ONE needs to be declared

namespace ModComponentMapper
{
	public static class ModWeaponManager //does not need to be declared
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
			string name = ModComponentUtils.NameUtils.NormalizeName(modRifleComponent.name);
			if (weaponViewTransform.Find(name) != null)
			{
				// already registered
				return;
			}

			Logger.Log("Registering '{0}'", name);

			GameObject template = weaponViewTransform.Find("Rifle").gameObject;
			GameObject weapon = UnityEngine.Object.Instantiate(template, weaponViewTransform);
			weapon.name = name;
			weapon.AddComponent<FixMuzzleFlashTransformParent>();
			weapon.GetComponent<vp_FPSWeapon>().m_UseFirstPersonHands = false;

			Transform rifle_rig = weapon.transform.Find("rifle_rig");
			if (rifle_rig != null)
			{
				rifle_rig.transform.parent = null;
				UnityEngine.Object.Destroy(rifle_rig.gameObject);
			}

			GameObject equippedModelPrefab = Resources.Load(modRifleComponent.EquippedModelPrefabName)?.Cast<GameObject>();
			GameObject equippedModel = UnityEngine.GameObject.Instantiate(equippedModelPrefab, weapon.transform);
			equippedModel.name = (name + "_rig").ToLower();

		}
	}

	[HarmonyPatch(typeof(GameManager), "InstantiatePlayerObject")]//Exists
	class GameManagerInstantiatePlayerObjectPatch
	{
		public static void Prefix(GameManager __instance)
		{
			ModWeaponManager.RegisterRifles(__instance);
		}
	}

	class FixMuzzleFlashTransformParent : MonoBehaviour //NEEDS TO BE DECLARED
	{
		public void Start()
		{
			vp_FPSShooter shooter = this.GetComponent<vp_FPSShooter>();
			if (shooter != null && shooter.MuzzleFlash != null)
			{
				shooter.MuzzleFlash.transform.parent = shooter.BulletEmissionLocator;
			}
		}

		public FixMuzzleFlashTransformParent(IntPtr intPtr) : base(intPtr) { }

		static FixMuzzleFlashTransformParent() => UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentMapper.FixMuzzleFlashTransformParent>();
	}
}
