using UnityEngine;

namespace ModComponentAPI
{
    public class PlayAkSound : MonoBehaviour
    {
        public string soundName;

        public void OnEnable()
        {
        }

        public PlayAkSound(System.IntPtr intPtr) : base(intPtr) { }
    }
}