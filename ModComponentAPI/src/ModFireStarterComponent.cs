using UnityEngine;

namespace ModComponentAPI
{
    public class ModFireStarterComponent : ModComponent
    {
        [Header("Fire Starter Item")]
        [Tooltip("How long will this item take in seconds to ligth the tinder")]
        public float SecondsToIgniteTinder;
        [Tooltip("How long will this item take in seconds to ligth the Torch")]
        public float SecondsToIgniteTorch;
        [Tooltip("Not really sure what this does yet")]
        public float FireStartSkillModifier;
        [Tooltip("Also not sure what this does")]
        public float FireStartDurationModifier;
        [Tooltip("How much does the item degrade on each use")]
        public float ConditionDegradeOnUse;
        [Tooltip("Does the item get consumed on use?")]
        public bool ConsumeOnUse;
        [Tooltip("Does the item require sunlight to work?")]
        public bool RequiresSunLight;
        [Tooltip("Is this istem an Accelerant")]
        public bool IsAccelerant;
        [Tooltip("What sound to play during usage")]
        public string OnUseSoundEvent;
    }
}
