using UnityEngine;

namespace ModComponentAPI
{
    public class ModEvolveComponent : MonoBehaviour
    {
        //[Tooltip("Name of the item into which this item will. E.g. 'GEAR_GutDried'")]
        public string TargetItemName;

        //[Tooltip("Does this item only evolve when it is stored indoors?")]
        public bool IndoorsOnly;

        //[Tooltip("How many in-game hours does this item take to evolve from 0% to 100%?")]
        //[Range(1, 1000)]
        public int EvolveHours = 1;

        public ModEvolveComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}