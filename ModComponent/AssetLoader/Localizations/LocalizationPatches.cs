using HarmonyLib;

namespace ModComponent.AssetLoader
{
	internal static class Localization_Patches
	{
		[HarmonyPatch(typeof(Localization), "Get")]
		[HarmonyPriority(Priority.First)]
		internal static class Localization_Get
		{
			private static void Postfix(ref string __result, string key)
			{
				if (LocalizationManager.Exists(key)) __result = LocalizationManager.Get(key);
			}
		}

		[HarmonyPatch(typeof(Localization), "GetForFallbackLanguage")]
		[HarmonyPriority(Priority.First)]
		internal static class Localization_GetForFallbackLanguage
		{
			private static void Postfix(ref string __result, string key)
			{
				if (LocalizationManager.Exists(key, "English")) __result = LocalizationManager.GetForLang(key, "English");
			}
		}

		[HarmonyPatch(typeof(Localization), "GetForLang")]
		[HarmonyPriority(Priority.First)]
		internal static class Localization_GetForLang
		{
			private static void Postfix(ref string __result, string key, string lang)
			{
				if (LocalizationManager.Exists(key, lang)) __result = LocalizationManager.GetForLang(key, lang);
			}
		}

		[HarmonyPatch(typeof(Localization), "Exists")]
		internal static class Localization_Exists
		{
			private static void Postfix(ref bool __result, string key)
			{
				if (!__result) __result = LocalizationManager.Exists(key);
			}
		}

		[HarmonyPatch(typeof(Localization), "LoadStringTableForLanguage")]
		internal static class Localization_test2
		{
			private static void Postfix(string language)
			{
				LocalizationManager.MaybeLoadPendingAssets();
			}
		}
	}
}
