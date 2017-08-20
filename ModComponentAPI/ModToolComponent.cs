using UnityEngine;

namespace ModComponentAPI
{
    public class ModToolComponent : ModComponent
    {
        [Header("Implementation")]
        [Tooltip("The name of the type implementing the specific game logic of this item. Use 'Namespace.TypeName,AssemblyName', e.g. 'Binoculars.Implementation,Binoculars'. Leave empty if this item needs no special game logic.")]
        public string ImplementationType;
    }
}
