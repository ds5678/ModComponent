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
			JsonUtils.TrySetFloat(ref this.LitersPurify, dict, className, "LitersPurify");
			JsonUtils.TrySetFloat(ref this.ProgressBarDurationSeconds, dict, className, "ProgressBarDurationSeconds");
			JsonUtils.TrySetString(ref this.ProgressBarLocalizationID, dict, className, "ProgressBarLocalizationID");
			JsonUtils.TrySetString(ref this.PurifyAudio, dict, className, "PurifyAudio");
		}
	}
}
