using ModComponentAPI;
using UnityEngine;

namespace ModComponent.Mapper
{
	internal static class GearEquipper
	{
		public static void Equip(EquippableModComponent equippable)
		{
			if (equippable == null) return;

			GameObject equippedModelPrefab = Resources.Load(equippable.EquippedModelPrefabName)?.Cast<GameObject>();
			if (equippedModelPrefab != null)
			{
				equippable.EquippedModel = Object.Instantiate(equippedModelPrefab, GameManager.GetWeaponCamera().transform);
				equippable.EquippedModel.layer = vp_Layer.Weapon;
			}
			else Logger.Log("The equippedModelPrefab for '{0}' was null.", equippable.EquippedModelPrefabName);

			equippable.OnEquipped?.Invoke();

			InterfaceManager.QuitCurrentScreens();
			ModComponent.Utils.ModUtils.PlayAudio(equippable.EquippingAudio);
		}

		public static void Unequip(EquippableModComponent modComponent)
		{
			if (modComponent == null) return;
			else GameManager.GetPlayerManagerComponent().UnequipItemInHandsSkipAnimation();
		}

		internal static void OnUnequipped(EquippableModComponent modComponent)
		{
			if (modComponent == null) return;

			if (modComponent.EquippedModel != null)
			{
				Object.Destroy(modComponent.EquippedModel);
				modComponent.EquippedModel = null;
			}

			modComponent.OnUnequipped?.Invoke();
			ModComponent.Utils.ModUtils.PlayAudio(modComponent.StowAudio);
		}
	}
}
