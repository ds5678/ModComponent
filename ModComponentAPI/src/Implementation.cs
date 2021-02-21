using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace ModComponentAPI
{
    class Implementation : MelonMod
    {
        public override void OnApplicationStart()
        {
            Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<EquippableImplementation>();
            UnhollowerRuntimeLib.ClassInjector.RegisterTypeInIl2Cpp<RubberDuckTest>();
        }

        internal static void Log(string message)
        {
            MelonLogger.Log(message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            Log(preformattedMessage);
        }
    }
}
