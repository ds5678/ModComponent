using System;
using System.Collections.Generic;
using UnhollowerBaseLib.Attributes;
using UnityEngine;
using static PlayerAnimation;
using static PlayerAnimation.State;

namespace ModComponentMapper
{
	public delegate void OnStateChanged(State oldState, State newState);

	internal class OneTimeEvent
	{
		public ModAnimationStateMachine animation;
		public State state;
		public OnAnimationEvent onAnimationEvent;

		[HideFromIl2Cpp]
		public OneTimeEvent(ModAnimationStateMachine animation, State state, OnAnimationEvent onAnimationEvent)
		{
			this.animation = animation;
			this.state = state;
			this.onAnimationEvent = onAnimationEvent;

			this.animation.stateChanged += OnStateChanged;
		}

		[HideFromIl2Cpp]
		public void OnStateChanged(State oldState, State newState)
		{
			if (oldState == state)
			{
				onAnimationEvent?.Invoke();
			}

			animation.stateChanged -= OnStateChanged;
		}
	}

	public class ModAnimationStateMachine : MonoBehaviour
	{
		[method: HideFromIl2Cpp]
		public event OnStateChanged stateChanged;

		private State state = Hidden;
		private PlayerStateTransitions transitions;

		private State transitionState;
		private float transitionTime;

		private static Dictionary<State, float> transitionDelays = new Dictionary<State, float>();
		private static Dictionary<State, State> transitionStates = new Dictionary<State, State>();

		[HideFromIl2Cpp]
		static ModAnimationStateMachine()
		{
			UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<ModComponentMapper.ModAnimationStateMachine>();

			transitionStates.Add(Equipping, Showing);
			transitionDelays.Add(Equipping, 0.3f);

			transitionStates.Add(ToAiming, Aiming);
			transitionDelays.Add(ToAiming, 0.3f);

			transitionStates.Add(FromAiming, Showing);
			transitionDelays.Add(FromAiming, 0.3f);

			transitionStates.Add(Reloading, Reloading);
			transitionDelays.Add(Reloading, 1f);

			transitionStates.Add(Firing, Aiming);
			transitionDelays.Add(Firing, 0.2f);
		}

		public ModAnimationStateMachine(IntPtr intPtr) : base(intPtr) { }

		[HideFromIl2Cpp]
		public void RegisterOneTimeEvent(State state, OnAnimationEvent onAnimationEvent)
		{
			new OneTimeEvent(this, state, onAnimationEvent);
		}

		void Update()
		{
			if (transitionTime > 0 && transitionTime <= Time.time)
			{
				ChangeToState(transitionState);
			}
		}

		[HideFromIl2Cpp]
		public void SetTransitions(PlayerStateTransitions transitions)
		{
			this.transitions = transitions;
		}

		[HideFromIl2Cpp]
		public bool IsAllowedToFire()
		{
			return IsStateAny(Aiming);
		}

		[HideFromIl2Cpp]
		public bool IsAiming()
		{
			return IsStateAny(FromAiming, Aiming, ToAiming);
		}

		[HideFromIl2Cpp]
		public bool CanTransitionTo(State targetState)
		{
			return transitions != null && transitions.IsValidTransition(state, targetState);
		}

		[HideFromIl2Cpp]
		public void TransitionToAndFire(State targetState, OnAnimationEvent onAnimationEvent)
		{
			TransitionTo(targetState);
			RegisterOneTimeEvent(targetState, onAnimationEvent);
		}

		[HideFromIl2Cpp]
		public void TransitionTo(State targetState)
		{
			if (!CanTransitionTo(targetState))
			{
				return;
			}

			this.ChangeToState(targetState);
		}

		[HideFromIl2Cpp]
		public bool GetFirstPersonWeaponCanSwitch()
		{
			return IsStateAny(Showing, Stowed, Hidden, Aiming, Reloading);
		}

		[HideFromIl2Cpp]
		public State GetState()
		{
			return state;
		}

		[HideFromIl2Cpp]
		private void ChangeToState(State targetState)
		{
			State oldState = state;
			state = targetState;
			OnStateChanged(oldState, state);
		}

		[HideFromIl2Cpp]
		private bool IsStateAny(params State[] states)
		{
			foreach (State eachState in states)
			{
				if (state == eachState)
				{
					return true;
				}
			}

			return false;
		}

		[HideFromIl2Cpp]
		private void OnStateChanged(State oldState, State newState)
		{
			ScheduleAutomaticTransition();
			stateChanged?.Invoke(oldState, newState);
		}

		[HideFromIl2Cpp]
		private void ScheduleAutomaticTransition()
		{
			if (transitionStates.TryGetValue(state, out transitionState))
			{
				float delay;
				transitionDelays.TryGetValue(state, out delay);
				transitionTime = Time.time + delay;
			}
			else
			{
				transitionTime = -1;
			}
		}
	}
}
