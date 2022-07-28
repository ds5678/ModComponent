extern alias Hinterland;
using Hinterland;
using UnityEngine;

namespace ModComponent.API.Modifications
{
	[MelonLoader.RegisterTypeInIl2Cpp]
	public class PlayAkSound : MonoBehaviour
	{
		/// <summary>
		/// The name of the sound (the wwise event loaded from a sound bank with AssetLoader) to be played when this item is enabled or triggered.
		/// </summary>
		public string SoundName;

		public bool PlayOnEnable;

		public void OnEnable()
		{
			if (PlayOnEnable) PlaySound();
		}

		public void PlaySound() => GameAudioManager.Play3DSound(this.SoundName, this.gameObject);

		public PlayAkSound(System.IntPtr intPtr) : base(intPtr) { }
	}
}
