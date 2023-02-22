using Il2Cpp;

using MelonLoader.TinyJSON;
using ModComponent.Utils;
using Il2CppInterop.Runtime.Attributes;
using UnityEngine;

namespace ModComponent.API.Components;

[MelonLoader.RegisterTypeInIl2Cpp]
public class ModRandomItemComponent : ModBaseComponent
{
	/// <summary>
	/// The names of the gear items that this could spawn.
	/// </summary>
	public string[] ItemNames = System.Array.Empty<string>();

	public ModRandomItemComponent(System.IntPtr intPtr) : base(intPtr) { }

	void Awake()
	{
		CopyFieldHandler.UpdateFieldValues(this);
	}
	void Update()
	{
		if (Settings.instance.disableRandomItemSpawns)
		{
			return;
		}

		if (this.ItemNames == null || this.ItemNames.Length == 0)
		{
			Logger.LogWarning($"'{this.name}' had an invalid list of potential spawn items.");
			Destroy(this.gameObject);
			return;
		}

		int index = RandomUtils.Range(0, this.ItemNames.Length);
		GameObject? prefab = Resources.Load(this.ItemNames[index])?.Cast<GameObject>();
		if (prefab == null)
		{
			Logger.LogWarning($"Could not use '{this.name}' to spawn random item '{this.ItemNames[index]}'");
			Destroy(this.gameObject);
			return;
		}

		GameObject gear = Instantiate(prefab, this.transform.position, this.transform.rotation);
		gear.name = prefab.name;
		DisableObjectForXPMode? xpmode = gear?.GetComponent<DisableObjectForXPMode>();
		if (xpmode != null)
		{
			Destroy(xpmode);
		}

		Destroy(this.gameObject);
	}

	[HideFromIl2Cpp]
	internal override void InitializeComponent(ProxyObject dict, string className = "ModRandomItemComponent")
	{
		base.InitializeComponent(dict, className);
		this.ItemNames = dict.GetStringArray(className, "ItemNames");
	}
}
