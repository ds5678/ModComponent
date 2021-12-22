using MelonLoader.TinyJSON;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModCharcoalComponent : ModBaseComponent
	{
		public float SurveyGameMinutes = 15;
		public float SurveyRealSeconds = 3;
		public float SurveySkillExtendedHours = 1;
		public string SurveyLoopAudio = "Play_MapCharcoalWriting";
		public ModCharcoalComponent(System.IntPtr intPtr) : base(intPtr) { }

		[HideFromIl2Cpp]
		internal override void InitializeComponent(ProxyObject dict, string className = "ModCharcoalComponent")
		{
			base.InitializeComponent(dict, className);
			this.SurveyGameMinutes = dict.GetVariant(className, "SurveyGameMinutes");
			this.SurveyRealSeconds = dict.GetVariant(className, "SurveyRealSeconds");
			this.SurveySkillExtendedHours = dict.GetVariant(className, "SurveySkillExtendedHours");
			this.SurveyLoopAudio = dict.GetVariant(className, "SurveyLoopAudio");
		}
	}
}
