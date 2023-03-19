using Il2CppInterop.Runtime.Attributes;
using MelonLoader.TinyJSON;
using ModComponent.Utils;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public partial class ModFirstAidComponent : ModBaseComponent
{
	/// <summary>
	/// Localization key to be used to show a description text while using the item. <br/>
	/// Should be something like 'Taking Antibiotics', 'Applying Bandage', etc.
	/// </summary>
	public string ProgressBarMessage = "";

	/// <summary>
	/// Localization key to be used to indicate what action is possible with this item. <br/>
	/// E.g 'Take Antibiotics', 'Apply Bandage'. This is probably not used.
	/// </summary>
	public string RemedyText = "";

	/// <summary>
	/// Amount of condition instantly restored after using this item.
	/// </summary>
	public int InstantHealing;

	/// <summary>
	/// What type of treatment does this item provide?
	/// </summary>
	public FirstAidKind FirstAidType = FirstAidKind.Antibiotics;

	/// <summary>
	/// Time in seconds to use this item. <br/>
	/// Most vanilla items use 2 or 3 seconds.
	/// </summary>
	public int TimeToUseSeconds = 3;

	/// <summary>
	/// How many items are required for one dose or application?
	/// </summary>
	public int UnitsPerUse = 1;

	/// <summary>
	/// Sound to play when using the item.
	/// </summary>
	public string UseAudio = "";

	void Awake()
	{
		CopyFieldHandler.UpdateFieldValues(this);
	}

	public ModFirstAidComponent(IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeComponent(ProxyObject dict, string className = "ModFirstAidComponent")
	{
		base.InitializeComponent(dict, className);
		this.ProgressBarMessage = dict.GetVariant(className, "ProgressBarMessage");
		this.RemedyText = dict.GetVariant(className, "RemedyText");
		this.InstantHealing = dict.GetVariant(className, "InstantHealing");
		this.FirstAidType = dict.GetEnum<FirstAidKind>(className, "FirstAidType");
		this.TimeToUseSeconds = dict.GetVariant(className, "TimeToUseSeconds");
		this.UnitsPerUse = dict.GetVariant(className, "UnitsPerUse");
		this.UseAudio = dict.GetVariant(className, "UseAudio");
	}
}