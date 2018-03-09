using UnityEngine;

namespace ModComponentAPI
{
    public class AddTag : MonoBehaviour
    {
        public string Tag;

        public void Awake()
        {
            gameObject.tag = Tag;
        }
    }
}