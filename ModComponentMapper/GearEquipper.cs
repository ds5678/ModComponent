using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper
{
    internal class GearEquipper
    {
        public static void Equip(EquippableModComponent modComponent)
        {
            if (modComponent == null)
            {
                return;
            }

            if (modComponent.EquippedModelPrefab != null)
            {
                modComponent.EquippedModel = UnityEngine.Object.Instantiate(modComponent.EquippedModelPrefab, GameManager.GetWeaponCamera().transform);
                modComponent.EquippedModel.layer = vp_Layer.Weapon;

                Collider[] colliders = modComponent.EquippedModel.GetComponents<Collider>();
                foreach (Collider eachCollider in colliders)
                {
                    Object.Destroy(eachCollider);
                }
            }

            modComponent.OnEquipped?.Invoke();

            InterfaceManager.QuitCurrentScreens();
            ModUtils.PlayAudio(modComponent.PickUpAudio);
        }

        public static void Unequip(EquippableModComponent modComponent)
        {
            if (modComponent == null)
            {
                return;
            }

            if (modComponent.EquippedModel != null)
            {
                Object.Destroy(modComponent.EquippedModel);
                modComponent.EquippedModel = null;
            }

            GameManager.GetPlayerManagerComponent().UnequipItemInHandsSkipAnimation();
            modComponent.OnUnequipped?.Invoke();
            ModUtils.PlayAudio(modComponent.StowAudio);
        }
    }
}
