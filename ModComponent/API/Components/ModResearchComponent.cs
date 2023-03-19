using Il2Cpp;
using Il2CppInterop.Runtime.Attributes;
using MelonLoader.TinyJSON;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModResearchComponent : ModBaseComponent
{
	public SkillType SkillType = SkillType.None;
	public int TimeRequirementHours = 5;
	public int SkillPoints = 10;
	public int NoBenefitAtSkillLevel = 4;
	public string ReadAudio = "Play_ResearchBook";

	public ModResearchComponent(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeComponent(ProxyObject dict, string className = "ModResearchComponent")
	{
		base.InitializeComponent(dict, className);
		this.SkillType = dict.GetEnum<SkillType>(className, "SkillType");
		this.TimeRequirementHours = dict.GetVariant(className, "TimeRequirementHours");
		this.SkillPoints = dict.GetVariant(className, "SkillPoints");
		this.NoBenefitAtSkillLevel = dict.GetVariant(className, "NoBenefitAtSkillLevel");
		this.ReadAudio = dict.GetVariant(className, "ReadAudio");
	}
}
