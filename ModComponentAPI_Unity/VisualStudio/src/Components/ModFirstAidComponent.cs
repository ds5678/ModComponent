using UnityEngine;

namespace ModComponentAPI
{
    public class ModFirstAidComponent : ModComponent
    {
        [Header("FirstAid")]
        [Tooltip("Localization key to be used to show a description text while using the item. Should be something like 'Taking Antibiotics', 'Applying Bandage', etc.")]
        public string ProgressBarMessage;

        [Tooltip("Localization key to be used to indicate what action is possible with this item. E.g 'Take Antibiotics', 'Apply Bandage'. This is probably not used.")]
        public string RemedyText;

        [Tooltip("Amount of condition instantly restored after using this item.")]
        [Range(0, 100)]
        public int InstantHealing;

        [Tooltip("What type of treatment does this item provide?")]
        public FirstAidType FirstAidType = FirstAidType.Antibiotics;

        [Tooltip("Time in seconds to use this item. Most vanilla items use 2 or 3 seconds.")]
        [Range(1, 10)]
        public int TimeToUseSeconds = 3;

        [Tooltip("How many items are required for one dose or application?")]
        [Range(1,10)]
        public int UnitsPerUse = 1;

        [Tooltip("Sound to play when using the item.")]
        public string UseAudio;
    }

    public enum FirstAidType
    {
        Antibiotics,
        Bandage,
        Disinfectant,
        PainKiller,
    }
}