using UnityEngine;

namespace ModComponentAPI
{
    public abstract class ModSaveBehaviour : MonoBehaviour
    {
        public abstract void Deserialize(string data);

        public abstract string Serialize();
    }
}