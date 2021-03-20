namespace ModComponentAPI
{
    public enum LiquidType
    {
        Water,
        Kerosene
    }

    public class ModLiquidComponent : ModComponent
    {
        //[Header("Liquid Item")]
        public LiquidType LiquidType = LiquidType.Water;
        public float LiquidCapacityLiters;
        public bool RandomizeQuantity = false;
        public float LiquidLiters = 0f;

        void Awake()
        {
            CopyFieldHandler.UpdateFieldValues<ModLiquidComponent>(this);
        }

        public ModLiquidComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}
