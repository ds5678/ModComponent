namespace ModComponentMapper
{
    public class GammaChanger
    {
        private static float gamma = 1;

        public static float GetGamma()
        {
            return gamma;
        }

        public static void SetGamma(float gamma)
        {
            GammaChanger.gamma = gamma;
            GameManager.GetCameraEffects().m_ColorGrading.m_Gamma = gamma;
        }

        public static void ResetGamma()
        {
            SetGamma(GameManager.GetCameraEffects().m_ColorGrading.GetDefaultGamma());
        }
    }
}