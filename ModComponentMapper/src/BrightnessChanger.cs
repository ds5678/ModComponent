namespace ModComponentMapper
{
    public class BrightnessChanger
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
}