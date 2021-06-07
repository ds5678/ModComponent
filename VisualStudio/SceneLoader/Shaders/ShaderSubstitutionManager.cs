using System.Collections.Generic;
using UnityEngine;

namespace SceneLoader.Shaders
{
	internal static class ShaderSubstitutionManager
	{
		internal static List<Material> fixedMaterials = new List<Material>();

		/// <summary>
		/// Replaces dummy shaders on the object with in-game equivalents.
		/// </summary>
		public static void ReplaceDummyShaders(GameObject obj, bool recursive)
		{
			if (obj is null) return;

			if (recursive)
			{
				foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>(true))
				{
					ReplaceDummyShaders(renderer);
				}
			}
			else
			{
				foreach (Renderer renderer in obj.GetComponents<Renderer>())
				{
					ReplaceDummyShaders(renderer);
				}
			}
		}

		public static void ReplaceDummyShaders(Renderer renderer)
		{
			if (renderer is null) return;

			try
			{
				foreach (Material material in renderer.sharedMaterials)
				{
					if (!fixedMaterials.Contains(material))
					{
						fixedMaterials.Add(material);
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
			}
			catch (System.Exception e)
			{
				Logger.LogError(e.Message);
			}
		}

		public static void AddDummyShaderReplacement(string dummyName, string inGameName)
		{
			ShaderList.DummyShaderReplacements.Add(dummyName, inGameName);
		}
	}
}
