using Il2Cpp;
using HarmonyLib;
using ModComponent.AssetLoader;

namespace ModComponent.Patches;

[HarmonyPatch(typeof(UISprite), nameof(UISprite.SetAtlasSprite))]
internal static class UISprite_set_spriteName
{
	internal static void Postfix(UISprite __instance)
	{
		UIAtlas? atlas = AtlasUtils.GetRequiredAtlas(__instance, __instance.mSpriteName);
		if (__instance.atlas == atlas)
		{
			return;
		}

		AtlasUtils.SaveOriginalAtlas(__instance);
		__instance.atlas = atlas;
	}
}
