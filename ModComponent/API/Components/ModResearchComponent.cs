using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModResearchComponent : ModBaseComponent
	{
		public ModSkillType SkillType = ModSkillType.None;
		public int TimeRequirementHours = 5;
		public int SkillPoints = 10;
		public int NoBenefitAtSkillLevel = 4;
		public string ReadAudio = "Play_ResearchBook";

		public ModResearchComponent(System.IntPtr intPtr) : base(intPtr) { }

		[HideFromIl2Cpp]
		internal override void InitializeComponent(ProxyObject dict, string className = "ModResearchComponent")
		{
			base.InitializeComponent(dict, className);
			JsonUtils.TrySetEnum<ModComponent.API.ModSkillType>(ref this.SkillType, dict, className, "SkillType");
			JsonUtils.TrySetInt(ref this.TimeRequirementHours, dict, className, "TimeRequirementHours");
			JsonUtils.TrySetInt(ref this.SkillPoints, dict, className, "SkillPoints");
			JsonUtils.TrySetInt(ref this.NoBenefitAtSkillLevel, dict, className, "NoBenefitAtSkillLevel");
			JsonUtils.TrySetString(ref this.ReadAudio, dict, className, "ReadAudio");
		}
	}
}
