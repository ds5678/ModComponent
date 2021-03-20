using System;
using UnityEngine;

namespace ModComponentAPI
{
    public class ModBedComponent : ModComponent
    {
        //[Header("Bed")]
        //[Tooltip("How many condition points are restored per hour by sleeping in this bed? This is the base rate and applied for the first hour. The second and following hours will benefit from 'UninterruptedRestPercentGainPerHour'.")]
        //[Range(0, 100)]
        public float ConditionGainPerHour;
        //[Tooltip("Additionally restored condition points restored per hour. The n-th hour of sleeping gives (n - 1) * UninterruptedRestGainPerHour additional health points.")]
        //[Range(0, 100)]
        public float AdditionalConditionGainPerHour;
        //[Tooltip("Warmth bonus of the bed in °C.")]
        //[Range(0, 30)]
        public float WarmthBonusCelsius;
        //[Tooltip("How much condition does this bed item lose per hour of use?")]
        //[Range(0f, 100f)]
        public float DegradePerHour;
        //[Tooltip("Modifier for the chance of bear encounters during sleep. Positive values decrease the chance; negative values increase the chance.")]
        //[Range(-100, 100)]
        public float BearAttackModifier;
        //[Tooltip("Modifier for the chance of wolf encounters during sleep. Positive values decrease the chance; negative values increase the chance.")]
        //[Range(-100, 100)]
        public float WolfAttackModifier;

        //[Tooltip("Sound to be played when beginning to sleep in this bed. Leave empty for a sensible default.")]
        public string OpenAudio;
        //[Tooltip("Sound to be played when ending to sleep in this bed. Leave empty for a sensible default.")]
        public string CloseAudio;

        //[Tooltip("Optional game object to be used for representing the bed in a 'packed' state.")]
        public GameObject PackedMesh;
        //[Tooltip("Optional game object to be used for representing the bed in a 'usable' state.")]
        public GameObject UsableMesh;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModBedComponent>(this);
        }

        public ModBedComponent(IntPtr intPtr) : base(intPtr) { }
    }
}