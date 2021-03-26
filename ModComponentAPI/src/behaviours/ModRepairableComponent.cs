using UnityEngine;

namespace ModComponentAPI
{
    public class ModRepairableComponent : MonoBehaviour
    {
        /// <summary>
        /// The audio to play while repairing.
        /// </summary>
        public string Audio;

        /// <summary>
        /// How many in-game minutes does it take to repair this item?
        /// </summary>
        public int Minutes;

        /// <summary>
        /// How much condition does repairing restore?
        /// </summary>
        public int Condition;

        /// <summary>
        /// The name of the tools suitable for repair. At least one of those will be required for repairing.
        /// Leave empty, if this item should be repairable without tools.
        /// </summary>
        public string[] RequiredTools;

        /// <summary>
        /// The name of the materials required for repair.
        /// </summary>
        public string[] MaterialNames;

        /// <summary>
        /// The number of the materials required for repair.
        /// </summary>
        public int[] MaterialCounts;

        public ModRepairableComponent(System.IntPtr intPtr) : base(intPtr) { }
    }
}