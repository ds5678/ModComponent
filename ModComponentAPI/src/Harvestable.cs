using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace ModComponentAPI
{
    [DisallowMultipleComponent]
    public class ModHarvestable : MonoBehaviour
    {
        [Tooltip("How man in-game minutes does it take to harvest this item?")]
        [Range(1, 120)]
        public int Minutes;

        [Tooltip("The audio to play while harvesting")]
        public string Audio;

        [Tooltip("The name of the GearItems havesting will yield")]
        public string[] Yield;

    }
}
