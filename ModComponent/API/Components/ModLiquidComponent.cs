using MelonLoader.TinyJSON;
using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModLiquidComponent : ModBaseComponent
	{
		/// <summary>
		/// The type of liquid this item contains.
		/// </summary>
		public LiquidKind LiquidType = LiquidKind.Water;

		/// <summary>
		/// The capacity of this container in liters
		/// </summary>
		public float LiquidCapacityLiters;

		/// <summary>
		/// If true, this container will have a random initial quantity.
		/// </summary>
		public bool RandomizeQuantity = false;

		/// <summary>
		/// If initial quantity not randomized, it will have this amount initially.
		/// </summary>
		public float LiquidLiters = 0f;

		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModLiquidComponent>(this);
		}

		public ModLiquidComponent(System.IntPtr intPtr) : base(intPtr) { }

		public enum LiquidKind
		{
			Water,
			Kerosene
		}

		[HideFromIl2Cpp]
		internal override void InitializeComponent(ProxyObject dict, string className = "ModLiquidComponent")
		{
			base.InitializeComponent(dict, className);
			this.LiquidType = EnumUtils.ParseEnum<ModLiquidComponent.LiquidKind>(dict[className]["LiquidType"]);
			this.LiquidCapacityLiters = dict[className]["LiquidCapacityLiters"];
			this.RandomizeQuantity = dict[className]["RandomizedQuantity"];
			this.LiquidLiters = Mathf.Clamp(dict[className]["LiquidLiters"], 0f, this.LiquidCapacityLiters); //overridden if Randomized
		}
	}
}
