using HarmonyLib;
using System;
using UnityEngine;

namespace ModComponent.AssetLoader
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class SaveAtlas : MonoBehaviour
	{
		public UIAtlas original;

		public SaveAtlas(IntPtr intPtr) : base(intPtr) { }
	}

	[HarmonyPatch(typeof(UISprite), "SetAtlasSprite")]
	internal static class UISprite_set_spriteName
	{
		internal static void Postfix(UISprite __instance)
		{
			UIAtlas atlas = AtlasUtils.GetRequiredAtlas(__instance, __instance.mSpriteName);
			if (__instance.atlas == atlas) return;

			AtlasUtils.SaveOriginalAtlas(__instance);
			__instance.atlas = atlas;
		}
	}
}
