using MelonLoader.TinyJSON;
using Il2CppInterop.Runtime.Attributes;

namespace ModComponent.API.Behaviours;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModTinderBehaviour : ModFireMakingBaseBehaviour
{
	public ModTinderBehaviour(System.IntPtr intPtr) : base(intPtr) { }

	[HideFromIl2Cpp]
	internal override void InitializeBehaviour(ProxyObject dict, string className = "ModTinderBehaviour")
	{
		base.InitializeBehaviour(dict, className);
	}
}
