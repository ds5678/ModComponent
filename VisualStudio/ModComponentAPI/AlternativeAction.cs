using UnityEngine;

namespace ModComponentAPI
{
    public abstract class AlternativeAction : MonoBehaviour
    {
        public abstract void Execute();

        public AlternativeAction(System.IntPtr intPtr) : base(intPtr) { }
    }
}