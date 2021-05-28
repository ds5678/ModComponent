using System.Collections.Generic;

namespace SceneLoader.Shaders
{
	internal static class ShaderList
	{
		/// <summary>
		/// Dummy : Actual
		/// </summary>
		public static Dictionary<string, string> DummyShaderReplacements { get; private set; } = new Dictionary<string, string>
		{
			//example from boneworks
			{
				"Hidden/InternalErrorShader",
				"Valve/vr_standard"
			},
			{
				"Valve/vr_standard2",
				"Valve/vr_standard"
			},
			{
				"Valve/vr_standard",
				"Valve/vr_standard"
			},
			{
				"Standard",
				"Valve/vr_standard"
			},
			{
				"SDK/AdditiveHDR",
				"SLZ/Additive HDR"
			},
			{
				"SDK/CubeReflection",
				"SLZ/Cube Reflection"
			}
		};
	}
}
