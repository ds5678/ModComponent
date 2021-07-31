using ModComponentAPI;
using UnityEngine;

namespace ModComponentMapper.ComponentMapper
{
	internal static class RifleMapper
	{
		internal static void Configure(ModComponent modComponent)
		{
			ModRifleComponent modRifleComponent = modComponent?.TryCast<ModRifleComponent>();
			if (modRifleComponent == null) return;

			GunItem gunItem = ModComponentUtils.ComponentUtils.GetOrCreateComponent<GunItem>(modRifleComponent);

			gunItem.m_GunType = GunType.Rifle;
			gunItem.m_AmmoPrefab = Resources.Load("GEAR_RifleAmmoSingle")?.TryCast<GameObject>();
			gunItem.m_AmmoSpriteName = "ico_units_ammo";

			gunItem.m_AccuracyRange = modRifleComponent.Range;
			gunItem.m_ClipSize = modRifleComponent.ClipSize;
			gunItem.m_DamageHP = modRifleComponent.DamagePerShot;
			gunItem.m_FiringRateSeconds = modRifleComponent.FiringDelay;
			gunItem.m_MuzzleFlash_FlashDelay = modRifleComponent.MuzzleFlashDelay;
			gunItem.m_MuzzleFlash_SmokeDelay = modRifleComponent.MuzzleSmokeDelay;
			gunItem.m_ReloadCoolDownSeconds = modRifleComponent.ReloadDelay;

			gunItem.m_DryFireAudio = "Play_RifleDryFire";
			gunItem.m_ImpactAudio = "Play_BulletImpacts";

			gunItem.m_SwayIncreasePerSecond = modRifleComponent.SwayIncrement;
			gunItem.m_SwayValueZeroFatigue = modRifleComponent.MinSway;
			gunItem.m_SwayValueMaxFatigue = modRifleComponent.MaxSway;

			Cleanable cleanable = ModComponentUtils.ComponentUtils.GetOrCreateComponent<Cleanable>(modRifleComponent);
			cleanable.m_ConditionIncreaseMin = 2;
			cleanable.m_ConditionIncreaseMin = 5;
			cleanable.m_DurationMinutesMin = 15;
			cleanable.m_DurationMinutesMax = 5;
			cleanable.m_CleanAudio = "Play_RifleCleaning";
			cleanable.m_RequiresToolToClean = true;
			cleanable.m_CleanToolChoices = new ToolsItem[] { Resources.Load<GameObject>("GEAR_RifleCleaningKit").GetComponent<ToolsItem>() };

			FirstPersonItem firstPersonItem = ConfiguredRifleFirstPersonItem(modRifleComponent);

			ModAnimationStateMachine animation = ModComponentUtils.ComponentUtils.GetOrCreateComponent<ModAnimationStateMachine>(modRifleComponent);
			animation.SetTransitions(firstPersonItem.m_PlayerStateTransitions);
		}

		private static FirstPersonItem ConfiguredRifleFirstPersonItem(ModRifleComponent modRifleComponent)
		{
			FirstPersonItem result = ModComponentUtils.ComponentUtils.GetOrCreateComponent<FirstPersonItem>(modRifleComponent);

			GameObject revolver = Resources.Load<GameObject>("GEAR_Revolver");
			if (revolver == null) Logger.LogWarning("Could not load revolver gameobject!");
			else Logger.LogWarning("Loaded revolver gameobject!");

			FirstPersonItem template = revolver?.GetComponent<FirstPersonItem>();
			if (template == null) Logger.LogWarning("Could not load first person template from revolver!");
			else Logger.LogWarning("Loaded first person template from revolver!");

			result.m_FirstPersonObjectName = ModComponentUtils.NameUtils.NormalizeName(modRifleComponent.name);
			result.m_UnWieldAudio = template.m_UnWieldAudio;
			result.m_WieldAudio = template.m_WieldAudio;
			Logger.LogWarning("made it this far");
			result.m_PlayerStateTransitions = Object.Instantiate(template.m_PlayerStateTransitions);
			Logger.LogWarning("farther");
			//result.Awake();
			Logger.LogWarning("the end");
			return result;
		}
	}
}
