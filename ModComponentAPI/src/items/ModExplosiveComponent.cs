using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModComponentAPI
{
    public class ModExplosiveComponent : EquippableModComponent
    {
        public float killRange = 5;

        public float explosionDelay;

        public string explosionAudio;

        public ModExplosiveComponent(IntPtr intPtr) : base(intPtr) { }
    }
}
