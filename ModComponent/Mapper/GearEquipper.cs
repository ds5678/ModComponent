using Il2Cpp;
using ModComponent.API.Components;
using ModComponent.Utils;
using UnityEngine;

namespace ModComponent.Mapper;

internal static class GearEquipper
{
	public static void Equip(ModBaseEquippableComponent equippable)
	{
		if (equippable == null)
		{
			return;
		}

		GameObject? equippedModelPrefab = AssetBundleUtils.LoadAsset<GameObject>(equippable.EquippedModelPrefabName);
		if (equippedModelPrefab != null)
		{
			equippable.EquippedModel = UnityEngine.Object.Instantiate(equippedModelPrefab, GameManager.GetWeaponCamera().transform);
			equippable.EquippedModel.layer = vp_Layer.Weapon;
		}
		else
		{
			Logger.Log($"The equippedModelPrefab for '{equippable.EquippedModelPrefabName}' was null.");
		}

		equippable.OnEquipped?.Invoke();

		InterfaceManager.QuitCurrentScreens();
		ModComponent.Utils.ModUtils.PlayAudio(equippable.EquippingAudio);
	}

	public static void Unequip(ModBaseEquippableComponent modComponent)
	{
		if (modComponent == null)
		{
			return;
		}
		else
		{
			GameManager.GetPlayerManagerComponent().UnequipItemInHandsSkipAnimation();
		}
	}

	internal static void OnUnequipped(ModBaseEquippableComponent modComponent)
	{
		if (modComponent == null)
		{
			return;
		}

		if (modComponent.EquippedModel != null)
		{
			UnityEngine.Object.Destroy(modComponent.EquippedModel);
			modComponent.EquippedModel = null;
		}

		modComponent.OnUnequipped?.Invoke();
		ModComponent.Utils.ModUtils.PlayAudio(modComponent.StowAudio);
	}
}
