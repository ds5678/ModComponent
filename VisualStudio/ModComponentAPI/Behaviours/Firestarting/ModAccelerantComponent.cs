namespace ModComponentAPI
{
	public class ModAccelerantComponent : ModFireMakingComponent
	{
		/// <summary>
		/// Is the item destroyed immediately after use?
		/// </summary>
		public bool DestroyedOnUse;

		public ModAccelerantComponent(System.IntPtr intPtr) : base(intPtr) { }
	}
}