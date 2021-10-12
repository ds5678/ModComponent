using MelonLoader.TinyJSON;
using ModComponent.Utils;
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
			JsonUtils.TrySetFloat(ref this.SurveyGameMinutes, dict, className, "SurveyGameMinutes");
			JsonUtils.TrySetFloat(ref this.SurveyRealSeconds, dict, className, "SurveyRealSeconds");
			JsonUtils.TrySetFloat(ref this.SurveySkillExtendedHours, dict, className, "SurveySkillExtendedHours");
			JsonUtils.TrySetString(ref this.SurveyLoopAudio, dict, className, "SurveyLoopAudio");
		}
	}
}
