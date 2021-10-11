using ModComponent.Utils;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace ModComponent.API.Components
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModRandomWeightedItemComponent : ModBaseComponent
	{
		/// <summary>
		/// The names of the gear items that this could spawn. Must be the same length as "ItemWeights"
		/// </summary>
		public string[] ItemNames = new string[0];

		/// <summary>
		/// The integer weights of the gear items that this could spawn. Must be the same length as "ItemNames"
		/// </summary>
		public int[] ItemWeights = new int[0];

		public ModRandomWeightedItemComponent(System.IntPtr intPtr) : base(intPtr) { }

		void Awake()
		{
			CopyFieldHandler.UpdateFieldValues<ModRandomWeightedItemComponent>(this);
		}
		void Update()
		{
			if (ModComponent.Main.Settings.instance.disableRandomItemSpawns) return;
			if (this.ItemNames == null || this.ItemNames.Length == 0)
			{
				Logger.LogWarning("'{0}' had an invalid list of potential spawn items.", this.name);
				Destroy(this.gameObject);
				return;
			}
			if (this.ItemWeights == null || this.ItemWeights.Length == 0)
			{
				Logger.LogWarning("'{0}' had an invalid list of item spawn weights.", this.name);
				Destroy(this.gameObject);
				return;
			}
			if (this.ItemWeights.Length != this.ItemNames.Length)
			{
				Logger.LogWarning("The lists of item names and spawn weights for '{0}' had unequal length.", this.name);
				Destroy(this.gameObject);
				return;
			}

			int index = this.GetIndex();
			GameObject prefab = Resources.Load(this.ItemNames[index])?.Cast<GameObject>();
			if (prefab == null)
			{
				Logger.LogWarning("Could not use '{0}' to spawn random item '{1}'", this.name, this.ItemNames[index]);
				Destroy(this.gameObject);
				return;
			}

			GameObject gear = Instantiate(prefab, this.transform.position, this.transform.rotation);
			gear.name = prefab.name;
			DisableObjectForXPMode xpmode = gear?.GetComponent<DisableObjectForXPMode>();
			if (xpmode != null) Destroy(xpmode);
			Destroy(this.gameObject);
		}

		[HideFromIl2Cpp]
		private int GetIndex()
		{
			if (this.ItemNames.Length == 1) return 0;

			int randomValue = RandomUtils.Range(0, GetTotalWeight());
			int runningTotal = 0;
			int count = 0;
			foreach (int weight in this.ItemWeights)
			{
				runningTotal += weight;
				if (runningTotal > randomValue) return count;
				else count++;
			}
			Logger.LogError("Bug found while running 'GetIndex' for '{0}'. For loop did not return a value.", this.name);
			return ItemNames.Length - 1; //should never happen
		}

		[HideFromIl2Cpp]
		private int GetTotalWeight()
		{
			if (this.ItemWeights == null) return 0;
			int result = 0;
			foreach (int weight in this.ItemWeights)
			{
				result += weight;
			}
			return result;
		}
	}
}
