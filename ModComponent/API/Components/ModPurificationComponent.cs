using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModPurificationComponent : ModBaseComponent
	{
		public float LitersPurify = 1f;
		public float ProgressBarDurationSeconds = 5f;
		public string ProgressBarLocalizationID = "GAMEPLAY_PurifyingWater";
		public string PurifyAudio = "Play_WaterPurification";

		public ModPurificationComponent(System.IntPtr intPtr) : base(intPtr) { }

		[HideFromIl2Cpp]
		internal override void InitializeComponent(ProxyObject dict, string className = "ModPurificationComponent")
		{
			base.InitializeComponent(dict, className);
			this.LitersPurify = dict.GetVariant(className,"LitersPurify");
			this.ProgressBarDurationSeconds = dict.GetVariant(className,"ProgressBarDurationSeconds");
			this.ProgressBarLocalizationID = dict.GetVariant(className,"ProgressBarLocalizationID");
			this.PurifyAudio = dict.GetVariant(className,"PurifyAudio");
		}
	}
}
