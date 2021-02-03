using UnityEngine;

namespace ModComponentAPI
{
    public class ModSkill : MonoBehaviour
    {
        //[Tooltip("Localization key to be used for the in-game name of the skill.")]
        public string DisplayName;
        //[Tooltip("Name of the icon to be used for the skill (e.g. for the popup notification for receiving skill points)\nImportant: This must be an icon from a UIAtlas!")]
        public string Icon;
        //[Tooltip("Name of the background image to be used for the skill (e.g. in the skill overview)")]
        public string Image;

        //[Tooltip("Number of points the user must collect to advance to level 2 in this skill.")]
        public int PointsLevel2;
        //[Tooltip("Number of points the user must collect to advance to level 3 in this skill.")]
        public int PointsLevel3;
        //[Tooltip("Number of points the user must collect to advance to level 4 in this skill.")]
        public int PointsLevel4;
        //[Tooltip("Number of points the user must collect to advance to level 5 in this skill.")]
        public int PointsLevel5;

        //[Tooltip("Localization key to be used for the description of level 1 of the skill.")]
        public string DescriptionLevel1;
        //[Tooltip("Localization key to be used for the description of level 2 of the skill.")]
        public string DescriptionLevel2;
        //[Tooltip("Localization key to be used for the description of level 3 of the skill.")]
        public string DescriptionLevel3;
        //[Tooltip("Localization key to be used for the description of level 4 of the skill.")]
        public string DescriptionLevel4;
        //[Tooltip("Localization key to be used for the description of level 5 of the skill.")]
        public string DescriptionLevel5;

        //[Tooltip("Localization key to be used for the effects of level 1 of the skill.")]
        public string EffectsLevel1;
        //[Tooltip("Localization key to be used for the effects of level 2 of the skill.")]
        public string EffectsLevel2;
        //[Tooltip("Localization key to be used for the effects of level 3 of the skill.")]
        public string EffectsLevel3;
        //[Tooltip("Localization key to be used for the effects of level 4 of the skill.")]
        public string EffectsLevel4;
        //[Tooltip("Localization key to be used for the effects of level 5 of the skill.")]
        public string EffectsLevel5;

        public ModSkill(System.IntPtr intPtr) : base(intPtr) { }
    }
}
