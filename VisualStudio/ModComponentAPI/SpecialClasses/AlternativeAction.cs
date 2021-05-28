using UnityEngine;

namespace ModComponentAPI
{
	public abstract class AlternativeAction : MonoBehaviour
	{
		/// <summary>
		/// Executes when the primary button is pressed.<br/>
		/// Runs before default behavior.
		/// </summary>
		/// <remarks>
		/// The primary button is typically the left mouse button.
		/// </remarks>
		public virtual void ExecutePrimary() { }

		/// <summary>
		/// Executes when the secondary button is pressed.<br/>
		/// Runs before default behavior.
		/// </summary>
		/// <remarks>
		/// The secondary button is typically the right mouse button.
		/// </remarks>
		public virtual void ExecuteSecondary() { }

		/// <summary>
		/// Executes when the tertiary button is pressed.
		/// </summary>
		/// <remarks>
		/// The tertiary button is set in the mod settings.
		/// </remarks>
		public virtual void ExecuteTertiary() { }

		public AlternativeAction(System.IntPtr intPtr) : base(intPtr) { }
	}
}