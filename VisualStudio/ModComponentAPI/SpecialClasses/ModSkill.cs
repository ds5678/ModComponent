using UnityEngine;

namespace ModComponentAPI
{
	public class ModSkill : MonoBehaviour
	{
		/// <summary>
		/// Localization key to be used for the in-game name of the skill.
		/// </summary>
		public string DisplayName;

		/// <summary>
		/// Name of the icon to be used for the skill.<br/>
		/// (e.g. for the popup notification for receiving skill points)<br/>
		/// Important: This must be an icon from a UIAtlas!
		/// </summary>
		public string Icon;

		/// <summary>
		/// Name of the background image to be used for the skill.<br/>
		/// (e.g. in the skill overview)
		/// </summary>
		public string Image;


		/// <summary>
		/// Number of points the user must collect to advance to level 2 in this skill.
		/// </summary>
		public int PointsLevel2;

		/// <summary>
		/// Number of points the user must collect to advance to level 3 in this skill.
		/// </summary>
		public int PointsLevel3;

		/// <summary>
		/// Number of points the user must collect to advance to level 4 in this skill.
		/// </summary>
		public int PointsLevel4;

		/// <summary>
		/// Number of points the user must collect to advance to level 5 in this skill.
		/// </summary>
		public int PointsLevel5;


		/// <summary>
		/// Localization key to be used for the description of level 1 of the skill.
		/// </summary>
		public string DescriptionLevel1;

		/// <summary>
		/// Localization key to be used for the description of level 2 of the skill.
		/// </summary>
		public string DescriptionLevel2;

		/// <summary>
		/// Localization key to be used for the description of level 3 of the skill.
		/// </summary>
		public string DescriptionLevel3;

		/// <summary>
		/// Localization key to be used for the description of level 4 of the skill.
		/// </summary>
		public string DescriptionLevel4;

		/// <summary>
		/// Localization key to be used for the description of level 5 of the skill.
		/// </summary>
		public string DescriptionLevel5;


		/// <summary>
		/// Localization key to be used for the effects of level 1 of the skill.
		/// </summary>
		public string EffectsLevel1;

		/// <summary>
		/// Localization key to be used for the effects of level 2 of the skill.
		/// </summary>
		public string EffectsLevel2;

		/// <summary>
		/// Localization key to be used for the effects of level 3 of the skill.
		/// </summary>
		public string EffectsLevel3;

		/// <summary>
		/// Localization key to be used for the effects of level 4 of the skill.
		/// </summary>
		public string EffectsLevel4;

		/// <summary>
		/// Localization key to be used for the effects of level 5 of the skill.
		/// </summary>
		public string EffectsLevel5;

		public ModSkill(System.IntPtr intPtr) : base(intPtr) { }
	}
}
