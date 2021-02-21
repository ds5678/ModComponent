using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Harmony;

namespace ModComponentMapper
{
    class ConsoleWaitlist
    {
        private static List<string> commandWaitlistDisplay = new List<string>(0);
        private static List<string> commandWaitlistPrefab = new List<string>(0);

        public static void AddToWaitlist(string displayName,string prefabName)
        {
            commandWaitlistDisplay.Add(displayName);
            commandWaitlistPrefab.Add(prefabName);
        }
        public static bool IsConsoleManagerInitialized()
        {
            return ConsoleManager.m_Initialized;
        }
        public static void TryUpdateWaitlist()
        {
            if(IsConsoleManagerInitialized() && commandWaitlistDisplay.Count > 0)
            {
                for(int i = 0; i < commandWaitlistDisplay.Count; i++)
                {
                    string displayName = commandWaitlistDisplay[i];
                    string prefabName = commandWaitlistPrefab[i];
                    ModUtils.RegisterConsoleGearName(displayName, prefabName);
                }
                commandWaitlistDisplay = new List<string>(0);
                commandWaitlistPrefab = new List<string>(0);
                Logger.Log("Console Commands added. The waitlist is empty.");
            }
        }

        [HarmonyPatch(typeof(GameManager),"Update")]
        internal static class UpdateConsoleCommands
        {
            private static void Postfix()
            {
                TryUpdateWaitlist();
            }
        }
    }
}
