using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace ModComponentAPI
{
    public class ModRifleComponent : EquippableModComponent
    {
        //[Header("Rifle/General")]
        //[Tooltip("The maximum number of round that can be loaded at any given time.")]
        public int ClipSize;
        //[Tooltip("Raw damage per shot in HP. This is modified by the target's damage modifier. The Rifle skill will increase the damage by 20% at the highest level!")]
        public int DamagePerShot;
        //[Tooltip("Range in meters. Beyond this distance, hits will not inflict damage. The Rifle skill will increase the range by 40% at the highest level!")]
        public int Range;

        //[Header("Rifle/Timing")]
        //[Tooltip("Time in seconds before the gun can shoot again.")]
        //[Range(0.0f, 10)]
        public float FiringDelay = 2.8f;
        //[Tooltip("Time in seconds before the gun can be used after loading it.")]
        //[Range(0.0f, 10)]
        public float ReloadDelay = 2f;
        //[Tooltip("Time in seconds before the gun can shoot after it went into 'Aim' mode.")]
        //[Range(0.0f, 10)]
        public float AimDelay = 0f;
        //[Tooltip("Time in seconds between firing the gun and the muzzle flash appearing")]
        //[Range(0.0f, 1)]
        public float MuzzleFlashDelay = 0.1f;
        //[Tooltip("Time in seconds between firing the gun and the muzzle smoke appearing")]
        //[Range(0.0f, 1)]
        public float MuzzleSmokeDelay = 0.15f;

        //[Header("Rifle/Sway")]
        //[Tooltip("The minimum amount of sway experience with zero fatigue. (unknown unit, Hunting Rifle: 0)")]
        //[Range(0, 1)]
        public float MinSway = 0;
        //[Tooltip("The minimum amount of sway experience with maximum fatigue. (unknown unit, Hunting Rifle: 0.75)")]
        //[Range(0, 1)]
        public float MaxSway = 0.75f;
        //[Tooltip("The amount sway increases for every second in 'Aim' mode. (unknown unit, Hunting Rifle: 0.1)")]
        //[Range(0, 1)]
        public float SwayIncrement = 0.1f;

        public ModRifleComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}
