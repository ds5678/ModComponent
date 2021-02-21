using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModComponentAPI
{
    public class EquippableImplementation : Il2CppSystem.Object
    {
        public ModComponent ModComponent = null;

        public EquippableImplementation(System.IntPtr intPtr) : base(intPtr) { }
    }
}
