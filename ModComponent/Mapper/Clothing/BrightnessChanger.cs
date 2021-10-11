namespace ModComponent.Mapper
{
	public static class BrightnessChanger
	{
		public static float Brightness
		{
			get => UnityEngine.Rendering.PostProcessing.ColorGradingRenderer.s_Brightness;
			set => UnityEngine.Rendering.PostProcessing.ColorGradingRenderer.s_Brightness = value;
		}

		public static float GetDefault()
		{
			return InterfaceManager.m_Panel_OptionsMenu.m_State.m_Brightness;
		}

		public static void Reset()
		{
			Brightness = InterfaceManager.m_Panel_OptionsMenu.m_State.m_Brightness;
		}
	}

	/*[HarmonyPatch(typeof(UnityEngine.Rendering.PostProcessing.ColorGrading), "GetBlendGammaForBrightness")]
    internal static class ColorGrading_GetBlendGammaForBrightness
    {
        private static void Postfix(ref float __result)
        {
            //__result *= 4;//makes it darker
        }
    }*/
}