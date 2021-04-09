namespace ModComponentAPI
{
    public class ModExplosiveComponent : EquippableModComponent
    {
        public float killRange = 5;

        public float explosionDelay;

        public string explosionAudio;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModExplosiveComponent>(this);
        }

        public ModExplosiveComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}
