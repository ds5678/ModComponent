using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Harmony;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(SandboxManager), "SceneLoadCompleted")]
    internal class SandboxManagerSceneLoadCompletedPatch
    {
        public static void Postfix()
        {
            if (!GameManager.m_SceneWasRestored)
            {
                GearSpawner.SpawnGearForScene(GameManager.m_ActiveScene);
            }
        }
    }
}
