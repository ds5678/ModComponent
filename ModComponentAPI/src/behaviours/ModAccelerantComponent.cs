namespace ModComponentAPI
{
    public class ModAccelerantComponent : ModFireStartingComponent
    {
        /// <summary>
        /// In-game seconds offset for fire starting duration from this accelerant.
        /// NOT scaled by fire starting skill. Positive values mean 'slower', negative values mean 'faster'.
        /// </summary>
        public float DurationOffset;

        public ModAccelerantComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}