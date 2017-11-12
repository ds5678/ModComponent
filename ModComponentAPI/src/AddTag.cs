using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
