using MelonLoader.TinyJSON;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Behaviours
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModAccelerantBehaviour : ModFireMakingBaseBehaviour
	{
		/// <summary>
		/// Is the item destroyed immediately after use?
		/// </summary>
		public bool DestroyedOnUse;

		public ModAccelerantBehaviour(System.IntPtr intPtr) : base(intPtr) { }

		[HideFromIl2Cpp]
		internal override void InitializeBehaviour(ProxyObject dict, string className = "ModAccelerantComponent")
		{
			base.InitializeBehaviour(dict, className);
			this.DestroyedOnUse = dict[className]["DestroyedOnUse"];
		}
	}
}