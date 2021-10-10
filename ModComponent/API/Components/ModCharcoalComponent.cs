namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModCharcoalComponent : ModBaseComponent
	{
		public float SurveyGameMinutes = 15;
		public float SurveyRealSeconds = 3;
		public float SurveySkillExtendedHours = 1;
		public string SurveyLoopAudio = "Play_MapCharcoalWriting";
		public ModCharcoalComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}
