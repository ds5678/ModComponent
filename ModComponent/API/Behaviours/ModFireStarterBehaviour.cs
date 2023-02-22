using Il2CppInterop.Runtime.Attributes;
using MelonLoader.TinyJSON;

namespace ModComponent.API.Behaviours;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModFireStarterBehaviour : ModFireMakingBaseBehaviour
{
	/// <summary>
	/// How many in-game seconds this item will take to ignite tinder.
	/// </summary>
	public float SecondsToIgniteTinder;

	/// <summary>
	/// How many in-game seconds this item will take to ignite a torch.
	/// </summary>
	public float SecondsToIgniteTorch;

	/// <summary>
	/// How many times can this item be used?
	/// </summary>
	public float NumberOfUses;

	/// <summary>
	/// Does the item require sunlight to work?
	/// </summary>
	public bool RequiresSunLight;

	/// <summary>
	/// What sound to play during usage. Not used for accelerants.
	/// </summary>
	public string OnUseSoundEvent = string.Empty;

	/// <summary>
	/// Set the condition to 0% after the fire starting finished (either successful or not).
	/// </summary>
	public bool RuinedAfterUse;

	/// <summary>
	/// Is the item destroyed immediately after use?
	/// </summary>
	public bool DestroyedOnUse;

	public ModFireStarterBehaviour(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeBehaviour(ProxyObject dict, string className = "ModFireStarterBehaviour")
	{
		base.InitializeBehaviour(dict, className);
		this.DestroyedOnUse = dict.GetVariant(className, "DestroyedOnUse");
		this.NumberOfUses = dict.GetVariant(className, "NumberOfUses");
		this.OnUseSoundEvent = dict.GetVariant(className, "OnUseSoundEvent");
		this.RequiresSunLight = dict.GetVariant(className, "RequiresSunLight");
		this.RuinedAfterUse = dict.GetVariant(className, "RuinedAfterUse");
		this.SecondsToIgniteTinder = dict.GetVariant(className, "SecondsToIgniteTinder");
		this.SecondsToIgniteTorch = dict.GetVariant(className, "SecondsToIgniteTorch");
	}
}
