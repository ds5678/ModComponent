using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneLoader.Shaders
{
	internal static class MaterialManager
	{
		internal static List<Material> fixedMaterials = new List<Material>();

		internal static void FixObjectShader(GameObject obj)
		{
			if (obj is null)
			{
				Logger.LogError("Skipping shader fix on " + obj.name + " due to NullReferenceException");
			}
			else
			{
				foreach (Renderer renderer in obj.GetComponentsInChildren<Renderer>(true))
				{
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
							}
						}
					}
					catch (Exception ex)
					{
						Logger.LogError("Skipping shader fix on " + obj.name + " due to " + ex.GetType().Name);
					}
				}
			}
		}
	}
}
