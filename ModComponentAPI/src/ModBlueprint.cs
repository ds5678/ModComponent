using UnityEngine;

namespace ModComponentAPI
{ 
    public class ModBlueprint : MonoBehaviour
    {

 
        public string[] RequiredGear;
 
        public int[] RequiredGearUnits;
    
        public string RequiredTool;

        public string[] OptionalTools;
        public string CraftedResult;
        public int CraftedResultCount;
        public int DurationMinutes;
        public string CraftingAudio;
        public float KeroseneLitersRequired;
        public bool RequiresForge;
        public bool RequiresWorkbench;
        public bool RequiresLight;
        public bool Locked;


    }
}
