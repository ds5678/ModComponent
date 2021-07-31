using HarmonyLib;
using System;
using UnityEngine;

namespace AssetLoader
{
	public class SaveAtlas : MonoBehaviour
	{
		public UIAtlas original;

		public SaveAtlas(IntPtr intPtr) : base(intPtr) { }

		static SaveAtlas() => UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<AssetLoader.SaveAtlas>(false);
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
