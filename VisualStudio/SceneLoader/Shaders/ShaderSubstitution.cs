using UnityEngine;

namespace SceneLoader.Shaders
{
	internal static class ShaderSubstitution
	{
		/// <summary>
		/// Replaces dummy shaders on the object with in-game equivalents and missing shaders with Valve/vr_standard.
		/// </summary>
		public static void ReplaceDummyShaders(GameObject obj)
		{
			if (obj != null)
			{
				foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>(true))
				{
					try
					{
						foreach (Material material in renderer.sharedMaterials)
						{
							if (ShaderList.DummyShaderReplacements.ContainsKey(material.shader.name))
							{
								material.shader = Shader.Find(ShaderList.DummyShaderReplacements[material.shader.name]);
							}
							else if (Shader.Find(material.shader.name.Replace("SLZ", "SDK")) != null)
							{
								material.shader = Shader.Find(material.shader.name.Replace("SLZ", "SDK"));
							}
						}
					}
					catch (System.Exception e)
					{
						Logger.LogError(e.Message);
					}
				}
			}
		}

		/// <summary>
		/// Replaces all shaders on the object with the Unity Standard shader.
		/// </summary>
		public static void ReplaceWithStandard(GameObject obj)
		{
			if (obj != null)
			{
				Shader shader = Shader.Find("Standard");
				foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>())
				{
					try
					{
						foreach (Material material in renderer.sharedMaterials)
						{
							material.shader = shader;
						}
					}
					catch (System.Exception e)
					{
						Logger.LogError(e.Message);
					}
				}
			}
		}

		public static void AddDummyShaderReplacement(string dummyName, string inGameName)
		{
			ShaderList.DummyShaderReplacements.Add(dummyName, inGameName);
		}
	}
}
