using MelonLoader.TinyJSON;
using UnhollowerBaseLib.Attributes;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModAmmoComponent : ModBaseComponent
	{
		public enum GunType
		{
			Rifle,
			Revolver
		}

		/// <summary>
		/// Determines what weapon uses this bullet type.
		/// </summary>
		public GunType AmmoForGunType = GunType.Rifle;

        public ModAmmoComponent(System.IntPtr intPtr) : base(intPtr) { }

        [HideFromIl2Cpp]
		internal override void InitializeComponent(ProxyObject dict, string className = "ModAmmoComponent")
		{
			base.InitializeComponent(dict, className);
			this.AmmoForGunType = dict.GetEnum<ModAmmoComponent.GunType>(className, "AmmoForGunType");
		}
	}
}