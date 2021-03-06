﻿using UnityEngine;
using UnhollowerBaseLib;
using System;

namespace ModComponentAPI
{
    public abstract class ModSaveBehaviour : MonoBehaviour
    {
        public abstract void Deserialize(string data);

        public abstract string Serialize();

        public ModSaveBehaviour(IntPtr intPtr) : base(intPtr) { }
    }
}