using Harmony;
using static PlayerAnimation;
using static PlayerAnimation.State;

//has some inlined methods

namespace ModComponentMapper
{
	class PlayerAnimationPatch
	{
		[HarmonyPatch(typeof(PlayerAnimation), "IsDequipping")]//zero caller count
		class PlayerAnimation_IsDequipping
		{
			public static bool Prefix(ref bool __result)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				if (ComponentUtils.GetModComponent(playerManager.m_ItemInHands) == null)
				{
					return true;
				}

				__result = false;
				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "IsAiming")]//zero caller count
		class PlayerAnimationIsAimingPatch
		{
			public static bool Prefix(ref bool __result)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				__result = animation.IsAiming();
				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "IsAllowedToFire")]//positive caller count
		class PlayerAnimationIsAllowedToFirePatch
		{
			public static bool Prefix(ref bool __result)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				__result = animation.IsAllowedToFire();
				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "CanTransitionToState")]//positive caller count
		class PlayerAnimationCanTransitionToStatePatch
		{
			public static bool Prefix(PlayerAnimation __instance, PlayerAnimation.State state, ref bool __result)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				__result = animation.CanTransitionTo(state);
				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "GetFirstPersonWeaponCanSwitch")]//zero caller count
		class PlayerAnimationGetFirstPersonWeaponCanSwitchPatch
		{
			public static bool Prefix(ref bool __result)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				__result = animation.GetFirstPersonWeaponCanSwitch();
				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "UpdateReloadCount")]//positive caller count
		class PlayerAnimationUpdateReloadCountPatch
		{
			public static bool Prefix()
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "Trigger_Generic_Reload")]//positive caller count
		class PlayerAnimationTrigger_Generic_ReloadPatch
		{
			public static bool Prefix(int bulletsToReload, OnAnimationEvent roundLoadedEventCallback, OnAnimationEvent clipLoadedEventCallback, OnAnimationEvent reloadCompleteEventCallback)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				ReloadProcess process = new ReloadProcess();
				process.remainingBullets = bulletsToReload;
				process.reloadCompleteEventCallback = reloadCompleteEventCallback;
				process.roundLoadedEventCallback = roundLoadedEventCallback;
				process.clipLoadedEventCallback = clipLoadedEventCallback;
				process.animation = animation;
				process.Start();

				return false;
			}
		}

		private enum ReloadStep
		{
			OpenBolt, LoadClip, LoadRound, CloseBolt
		}

		private class ReloadProcess
		{
			internal int remainingBullets;

			internal OnAnimationEvent roundLoadedEventCallback;
			internal OnAnimationEvent clipLoadedEventCallback;
			internal OnAnimationEvent reloadCompleteEventCallback;
			internal ModAnimationStateMachine animation;

			private ReloadStep step;

			public void Start()
			{
				animation.stateChanged += OnCycleComplete;
				animation.TransitionTo(Reloading);
				GameAudioManager.PlaySound(AK.EVENTS.PLAY_FPH_HUNTINGRIFLE_RELOAD_OPENBOLT, GameManager.GetPlayerObject());
				step = ReloadStep.OpenBolt;
			}

			public void OnCycleComplete(State oldState, State newState)
			{
				if (oldState != Reloading || newState != Reloading)
				{
					return;
				}

				switch (step)
				{
					case ReloadStep.CloseBolt:
						animation.stateChanged -= OnCycleComplete;
						reloadCompleteEventCallback?.Invoke();
						animation.TransitionTo(Showing);
						return;

					case ReloadStep.OpenBolt:
						break;

					case ReloadStep.LoadClip:
						remainingBullets -= 5;
						clipLoadedEventCallback?.Invoke();
						break;

					case ReloadStep.LoadRound:
						remainingBullets--;
						roundLoadedEventCallback?.Invoke();
						break;
				}

				if (remainingBullets >= 5)
				{
					GameAudioManager.PlaySound(AK.EVENTS.PLAY_FPH_HUNTINGRIFLE_RELOAD_CARTRIDGE_LOOP_, GameManager.GetPlayerObject());
					step = ReloadStep.LoadClip;
				}
				else if (remainingBullets > 0)
				{
					GameAudioManager.PlaySound(AK.EVENTS.PLAY_FPH_HUNTINGRIFLE_RELOAD_SINGLE_LOOP, GameManager.GetPlayerObject());
					step = ReloadStep.LoadRound;
				}
				else
				{
					GameAudioManager.PlaySound(AK.EVENTS.PLAY_FPH_HUNTINGRIFLE_RELOAD_SINGLE_CLOSEBOLT, GameManager.GetPlayerObject());
					step = ReloadStep.CloseBolt;
				}
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "Trigger_Generic_Aim")]//positive caller count
		class PlayerAnimationTrigger_Generic_AimPatch
		{
			public static bool Prefix(PlayerAnimation.OnAnimationEvent onAnimationComplete)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				animation.TransitionToAndFire(ToAiming, onAnimationComplete);
				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "Update_Aiming")]//zero caller count
		class PlayerAnimationUpdate_AimingPatch
		{
			public static bool Prefix()
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "Trigger_Generic_Aim_Cancel")]//positive caller count
		class PlayerAnimationTrigger_Generic_Aim_CancelPatch
		{
			public static bool Prefix(OnAnimationEvent onAnimationComplete)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				animation.TransitionToAndFire(FromAiming, onAnimationComplete);
				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "Trigger_Generic_Fire")]//positive caller count
		class PlayerAnimationTrigger_Generic_FirePatch
		{
			public static bool Prefix()
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				animation.TransitionTo(Firing);
				GameManager.GetVpFPSCamera().AddForce2(0f, 0.25f, 0.25f);

				return false;
			}
		}

		[HarmonyPatch(typeof(PlayerAnimation), "Update_WeaponFatigue")]//positive caller count
		class PlayerAnimationUpdate_WeaponFatiguePatch
		{
			private static UnityEngine.Vector3 MAX_SHAKE_AMPLITUDE = new UnityEngine.Vector3(0.2f, 0f, 0.1f);

			public static bool Prefix(float fatigue)
			{
				PlayerManager playerManager = GameManager.GetPlayerManagerComponent();
				ModAnimationStateMachine animation = ComponentUtils.GetComponent<ModAnimationStateMachine>(playerManager.m_ItemInHands);
				if (animation == null)
				{
					return true;
				}

				var camera = GameManager.GetVpFPSCamera();
				vp_FPSWeapon currentWeapon = camera.CurrentWeapon;
				if (currentWeapon != null)
				{
					currentWeapon.ShakeSpeed = 0.5f;
					currentWeapon.ShakeAmplitude = MAX_SHAKE_AMPLITUDE * fatigue;
				}

				return false;
			}
		}
	}
}
