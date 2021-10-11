using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public abstract class ModBaseComponent : MonoBehaviour
	{
		/// <summary>
		/// How this item will be called in the DeveloperConsole. <br/>
		/// Leave empty for a sensible default.
		/// </summary>
		public string ConsoleName;

		/// <summary>
		/// Localization key to be used for the in-game name of the item.
		/// </summary>
		public string DisplayNameLocalizationId;

		/// <summary>
		/// Localization key to be used for the in-game description of the item.
		/// </summary>
		public string DescriptionLocalizatonId;


		/// <summary>
		/// The inventory category to be used for this item. <br/>
		/// Leave at 'Auto' for a sensible default.
		/// </summary>
		public ItemCategory InventoryCategory = ItemCategory.Auto;

		/// <summary>
		/// Localization key to be used for the 'Action' (e.g. 'Equip', 'Eat', ...) button in the inventory.<br/>
		/// The text is purely cosmetic and will not influcence the action the button triggers. <br/>
		/// Leave empty for a sensible default.
		/// </summary>
		public string InventoryActionLocalizationId;

		/// <summary>
		/// Sound to play when the item is picked up.
		/// </summary>
		public string PickUpAudio;

		/// <summary>
		/// Sound to play when the item is holstered.
		/// </summary>
		public string StowAudio = "Play_InventoryStow";

		/// <summary>
		/// Sound to play when the item is dropped.
		/// </summary>
		public string PutBackAudio;

		/// <summary>
		/// Sound to play when the item wore out during an action.
		/// </summary>
		public string WornOutAudio;


		/// <summary>
		/// The weight of the item in kilograms.
		/// </summary>
		public float WeightKG;

		/// <summary>
		/// The maximum hit points of the item.
		/// </summary>
		public float MaxHP;

		/// <summary>
		/// The number of days it takes for this item to decay - without use - from 100% to 0%. <br/>
		/// Leave at 0 if the item should not decay over time.
		/// </summary>
		public float DaysToDecay;

		/// <summary>
		/// The initial condition of the item when found or crafted.
		/// </summary>
		public StartingCondition InitialCondition;


		/// <summary>
		/// Will the item be inspected when picked up? <br/>
		/// If not enabled, the item will go straight to the inventory.
		/// </summary>
		public bool InspectOnPickup;

		/// <summary>
		/// Distance from the camera during inspect.
		/// </summary>
		public float InspectDistance = 0.4f;

		/// <summary>
		/// Scales the item during inspect.
		/// </summary>
		public Vector3 InspectScale = Vector3.one;

		/// <summary>
		/// Each vector component stands for a rotation by the given degrees around the corresponding axis.
		/// </summary>
		public Vector3 InspectAngles = Vector3.zero;

		/// <summary>
		/// Offset from the center during inspect.
		/// </summary>
		public Vector3 InspectOffset = Vector3.zero;


		/// <summary>
		/// Model to show during inspect mode. <br/>
		/// NOTE: You must either set BOTH models or NO models.
		/// </summary>
		public GameObject InspectModel;

		/// <summary>
		/// Model to show when not inspecting the item. <br/>
		/// NOTE: You must either set BOTH models or NO models.
		/// </summary>
		public GameObject NormalModel;

		[HideFromIl2Cpp]
		public string GetEffectiveConsoleName()
		{
			if (string.IsNullOrEmpty(this.ConsoleName))
			{
				return this.name.Replace("GEAR_", ""); ;
			}

			return this.ConsoleName;
		}

		public ModBaseComponent(IntPtr intPtr) : base(intPtr) { }

		public enum StartingCondition
		{
			Random,
			Perfect,
			High,
			Medium,
			Low
		}

		public enum ItemCategory
		{
			Auto,
			Clothing,
			FirstAid,
			Firestarting,
			Food,
			Material,
			Tool
		}
	}
}
