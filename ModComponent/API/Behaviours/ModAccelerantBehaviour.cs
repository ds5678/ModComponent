namespace ModComponentAPI
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class ModAccelerantBehaviour : ModFireMakingBaseBehaviour
	{
		/// <summary>
		/// Is the item destroyed immediately after use?
		/// </summary>
		public bool DestroyedOnUse;

		public ModAccelerantBehaviour(System.IntPtr intPtr) : base(intPtr) { }
	}
}