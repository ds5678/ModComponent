using Harmony;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

//did a first pass through; HAS A TRANSPILER!!!
//does not need to be declared
/*
namespace ModComponentMapper
{
    [HarmonyPatch(typeof(CameraStatusEffects), "UpdateImage")]//Exists
    internal class CameraStatusEffects_UpdateImage
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode != OpCodes.Callvirt)
                {
                    continue;
                }

                MethodInfo methodInfo = codes[i].operand as MethodInfo;
                if (methodInfo == null || methodInfo.Name != "GetDefaultGamma" || methodInfo.DeclaringType != typeof(ColorGrading)) //no calls to GetDefaultGamma in 1.56
                {
                    continue;
                }

                codes[i - 2].opcode = OpCodes.Nop;
                codes[i - 1].opcode = OpCodes.Nop;
                codes[i].opcode = OpCodes.Call;
                codes[i].operand = typeof(BrightnessChanger).GetMethod("GetGamma");
            }

            return codes;
        }
    }
}
*/