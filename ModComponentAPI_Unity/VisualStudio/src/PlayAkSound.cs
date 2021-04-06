using UnityEngine;

namespace ModComponentAPI
{
    public class PlayAkSound : MonoBehaviour
    {
        [Tooltip("The name of the sound (the wwise event loaded from a sound bank with AssetLoader) to be played when this item is enabled.")]
        public string soundName;

        /*public void OnEnable()
        {
        }*/
    }
}