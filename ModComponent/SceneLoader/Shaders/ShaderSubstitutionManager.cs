using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.SceneLoader.Shaders;

internal static class ShaderSubstitutionManager
{
	internal static List<Material> fixedMaterials = new List<Material>();

	/// <summary>
	/// Replaces dummy shaders on the object with in-game equivalents.
	/// </summary>
	public static void ReplaceDummyShaders(GameObject obj, bool recursive)
	{
		if (obj == null) 
			return;

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
		if (renderer == null) 
			return;

		try
		{
			foreach (Material material in renderer.sharedMaterials)
			{
				FixMaterial(material);
			}
		}
		catch (System.Exception e)
		{
			Logger.LogError(e.ToString());
		}
	}

	public static void ReplaceDummyShaders(Terrain terrain)
	{
		if (terrain == null) 
			return;

		try
		{
			//Disabled for now because of weird transparency issue
			//FixMaterial(terrain.materialTemplate);

			TerrainData terrainData = terrain.terrainData;

			if (terrainData == null)
				return;

			foreach(TreePrototype treePrototype in terrainData.treePrototypes)
			{
				if(treePrototype != null)
					ReplaceDummyShaders(treePrototype.m_Prefab, true);
			}

			foreach(DetailPrototype detailPrototype in terrainData.detailPrototypes)
			{
				if (detailPrototype != null)
					ReplaceDummyShaders(detailPrototype.m_Prototype, true);
			}

			terrainData.RefreshPrototypes();
		}
		catch (System.Exception e)
		{
			Logger.LogError(e.ToString());
		}
	}

	private static void FixMaterial(Material material)
	{
		if (material == null)
			return;

		if (!fixedMaterials.Contains(material))
		{
			fixedMaterials.Add(material);
			if (material.shader.name.StartsWith("Dummy"))
			{
				material.shader = Shader.Find(material.shader.name.Replace("Dummy", "")) ?? material.shader;
			}
			else if (ShaderList.DummyShaderReplacements.ContainsKey(material.shader.name))
			{
				material.shader = Shader.Find(ShaderList.DummyShaderReplacements[material.shader.name]);
			}
		}
	}

	public static void AddDummyShaderReplacement(string dummyName, string inGameName)
	{
		ShaderList.DummyShaderReplacements.Add(dummyName, inGameName);
	}
}
