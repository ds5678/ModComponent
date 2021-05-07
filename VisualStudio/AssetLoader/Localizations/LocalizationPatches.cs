using Harmony;

namespace AssetLoader
{
	internal static class Localization_Patches
	{
		[HarmonyPatch(typeof(GameManager), "Update")]
		internal class LoadWaitingLocalizations
		{
			private static void Postfix()
			{
				LocalizationManager.MaybeLoadPendingAssets();
			}
		}

		[HarmonyPatch(typeof(Localization), "Get")]
		[HarmonyPriority(Priority.First)]
		internal class Localization_Get
		{
			private static void Postfix(ref string __result, string key)
			{
				//Implementation.Log("Get '{0}'", key);
				if (LocalizationManager.Exists(key)) __result = LocalizationManager.Get(key);
			}
		}

		[HarmonyPatch(typeof(Localization), "GetForFallbackLanguage")]
		[HarmonyPriority(Priority.First)]
		internal class Localization_GetForFallbackLanguage
		{
			private static void Postfix(ref string __result, string key)
			{
				//Implementation.Log("Get '{0}'", key);
				if (LocalizationManager.Exists(key, "English")) __result = LocalizationManager.GetForLang(key, "English");
			}
		}

		[HarmonyPatch(typeof(Localization), "GetForLang")]
		[HarmonyPriority(Priority.First)]
		internal class Localization_GetForLang
		{
			private static void Postfix(ref string __result, string key, string lang)
			{
				//Implementation.Log("Get '{0}' for '{1}'", key, lang);
				if (LocalizationManager.Exists(key, lang)) __result = LocalizationManager.GetForLang(key, lang);
			}
		}

		[HarmonyPatch(typeof(Localization), "Exists")]
		internal class Localization_Exists
		{
			private static void Postfix(ref bool __result, string key)
			{
				if (!__result) __result = LocalizationManager.Exists(key);
			}
		}

		/*[HarmonyPatch(typeof(Localization), "LoadAndSelectLanguage")]
        internal class Localization_test1
        {
            private static void Prefix(string language)
            {
                Implementation.Log("LoadAndSelectLanguage : Prefix : '{0}'", language);
            }
            private static void Postfix(string language)
            {
                Implementation.Log("LoadAndSelectLanguage : Postfix : '{0}'", language);
            }
        }*/

		[HarmonyPatch(typeof(Localization), "LoadStringTableForLanguage")]
		internal class Localization_test2
		{
			private static void Prefix(string language)
			{
				//Implementation.Log("LoadStringTableForLanguage : Prefix : '{0}'", language);
			}
			private static void Postfix(string language)
			{
				//Implementation.Log("LoadStringTableForLanguage : Postfix : '{0}'", language);
				LocalizationManager.MaybeLoadPendingAssets();
				//Logger.Log(MelonLoader.TinyJSON.JSON.Dump(LocalizationManager.localizationDictionary, MelonLoader.TinyJSON.EncodeOptions.PrettyPrint));
			}
		}

		/*[HarmonyPatch(typeof(Localization), "MaybeLoadStringTable")]
        internal class Localization_test3
        {
            private static void Prefix(string language)
            {
                Implementation.Log("MaybeLoadStringTable : Prefix : '{0}'", language);
            }
            private static void Postfix(string language)
            {
                Implementation.Log("MaybeLoadStringTable : Postfix : '{0}'", language);
            }
        }*/
	}
}
