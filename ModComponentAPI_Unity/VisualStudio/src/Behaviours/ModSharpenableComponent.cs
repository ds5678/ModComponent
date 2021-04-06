using UnityEngine;

namespace ModComponentAPI
{
    public class ModSharpenableComponent : MonoBehaviour
    {
        [Header("Sharpenable")]

        [Tooltip("Which tools can be used to sharpen this item, e.g. 'GEAR_SharpeningStone'. Leave empty to make this sharpenable without tools.")]
        public string[] Tools;

        [Tooltip("How many in-game minutes does it take to sharpen this item at minimum skill.")]
        [Range(1, 100)]
        public int MinutesMin;
        [Tooltip("How many in-game minutes does it take to sharpen this item at maximum skill.")]
        [Range(1, 100)]
        public int MinutesMax;

        [Tooltip("How much condition is restored to this item at minimum skill.")]
        [Range(0.1f, 100f)]
        public float ConditionMin;
        [Tooltip("How much condition is restored to this item at maximum skill.")]
        [Range(0.1f, 100f)]
        public float ConditionMax;

        [Header("The sound to play while sharpening. Leave empty for a sensible default.")]
        public string Audio;
    }
}
