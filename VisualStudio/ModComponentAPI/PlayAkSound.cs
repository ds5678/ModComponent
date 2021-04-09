using UnityEngine;

namespace ModComponentAPI
{
    public class PlayAkSound : MonoBehaviour
    {
        /// <summary>
        /// The name of the sound (the wwise event loaded from a sound bank with AssetLoader) to be played when this item is enabled.
        /// </summary>
        public string soundName;

        public void OnEnable()
        {
            GameAudioManager.Play3DSound(this.soundName, this.gameObject);
        }

        public PlayAkSound(System.IntPtr intPtr) : base(intPtr) { }
    }
}
