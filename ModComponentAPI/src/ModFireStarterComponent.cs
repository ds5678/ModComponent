using UnityEngine;

namespace ModComponentAPI
{
    public class ModFireStarterComponent : ModFireStartingComponent
    {
        [Tooltip("How many in-game seconds this item will take to ignite tinder.")]
        public float SecondsToIgniteTinder;
        [Tooltip("How many in-game seconds this item will take to ignite a torch.")]
        public float SecondsToIgniteTorch;

        [Tooltip("How many times can this item be used?")]
        public float NumberOfUses;
        [Tooltip("Does the item require sunlight to work?")]
        public bool RequiresSunLight;
        [Tooltip("What sound to play during usage. Not used for accelerants.")]
        public string OnUseSoundEvent;
    }
}
