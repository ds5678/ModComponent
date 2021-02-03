using UnityEngine;

namespace ModComponentAPI
{
    //[DisallowMultipleComponent]
    public class ModRepairableComponent : MonoBehaviour
    {
        //[Tooltip("The audio to play while repairing")]
        public string Audio;

        //[Tooltip("How many in-game minutes does it take to repair this item?")]
        //[Range(1, 120)]
        public int Minutes;

        //[Tooltip("How much condition does repairing restore?")]
        //[Range(1, 100)]
        public int Condition;

        //[Tooltip("The name of the tools suitable for repair. At least one of those will be required for repairing. Leave empty, if this item should be repairable without tools.")]
        public string[] RequiredTools;

        //[Tooltip("The name of the materials required for repair")]
        public string[] MaterialNames;

        //[Tooltip("The number of the materials required for repair")]
        public int[] MaterialCounts;

        public ModRepairableComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}