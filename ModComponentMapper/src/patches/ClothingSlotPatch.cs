using Harmony;
using ModComponentAPI;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace ModComponentMapper
{
    [HarmonyPatch(typeof(ClothingSlot), "CheckForChangeLayer")]
    internal class ClothingSlot_CheckForChangeLayer
    {
        public static bool Prefix(ClothingSlot __instance)
        {
            ModClothingComponent clothingComponent = ModUtils.GetComponent<ModClothingComponent>(__instance.m_GearItem);
            if (clothingComponent == null)
            {
                return true;
            }

            int actualDrawLayer = Mathf.Max(40, clothingComponent.DrawLayer);
            AccessTools.Method(__instance.GetType(), "UpdatePaperDollTextureLayer").Invoke(__instance, new object[] { actualDrawLayer });

            return false;
        }
    }

    [HarmonyPatch(typeof(ClothingSlot), "SetPaperDollTexture")]
    internal class ClothingSlot_SetPaperDollTexture
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                if (IsUnpatchableResourceLoading(codes[i]))
                {
                    MakeResourceLoadingPatchable(codes, i);
                }
            }

            return instructions;
        }

        private static void MakeResourceLoadingPatchable(List<CodeInstruction> instructions, int offset)
        {
            instructions[offset - 2].opcode = OpCodes.Nop;
            instructions[offset - 1].opcode = OpCodes.Nop;

            var methodCall = instructions[offset];
            MethodInfo originalMethodInfo = (MethodInfo)methodCall.operand;
            MethodInfo newMethodInfo = AccessTools.Method(originalMethodInfo.DeclaringType, "Load", new System.Type[] { typeof(string) });

            instructions[offset].operand = newMethodInfo;
        }

        private static bool IsUnpatchableResourceLoading(CodeInstruction codeInstruction)
        {
            if (codeInstruction.opcode != OpCodes.Call)
            {
                return false;
            }

            MethodInfo methodInfo = codeInstruction.operand as MethodInfo;
            if (methodInfo == null)
            {
                return false;
            }

            return methodInfo.Name == "Load" && methodInfo.DeclaringType == typeof(Resources) && methodInfo.GetParameters().Length > 1;
        }
    }
}