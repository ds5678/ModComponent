using Harmony;
using ModComponentUtils;
using System.Collections.Generic;
using UnityEngine;

namespace AssetLoader
{
	internal static class SkinManager
	{
		private static readonly Dictionary<string, byte[]> textures = new Dictionary<string, byte[]>();

		internal static void AddToTextureList(string name, byte[] array)
		{
			if (string.IsNullOrWhiteSpace(name) || array is null || array.Length == 0) return;
			textures.Add(name, array);
		}

		internal static bool ContainsKey(string key)
		{
			if (key is null) return false;
			else return textures.ContainsKey(key);
		}

		internal static Texture2D GetTexture(string textureName)
		{
			if (!textures.ContainsKey(textureName)) return null;
			byte[] array = textures[textureName];
			Texture2D output = new Texture2D(2, 2);
			if (ImageConversion.LoadImage(output, array) && output != null)
			{
				output.name = textureName;
				return output;
			}
			else
			{
				Logger.LogError($"Failed to load image: '{textureName}'");
				textures.Remove(textureName);
				return null;
			}
		}

		internal static void ReplaceTextures(GameObject gameObject)
		{
			if (gameObject is null) return;
			foreach (Renderer renderer in gameObject.GetComponentsInChildren<Renderer>(true))
			{
				foreach (Material mat in renderer.GetMaterialArray())
				{
					if (ContainsKey(mat?.mainTexture?.name)) mat.mainTexture = GetTexture(mat.mainTexture.name);
				}
			}
		}

		internal static void ReplaceTextures(string name) => ReplaceTextures(Resources.Load(name).TryCast<GameObject>());

		internal static void ReplacePlayerTextures() => ReplaceTextures(GetFirstPersonObject());

		internal static GameObject GetFirstPersonObject() => ModUtils.GetSibling(GameManager.GetPlayerObject(), "NEW_FPHand_Rig");

		[HarmonyPatch(typeof(SaveGameSystem), "LoadSceneData")]
		internal class SaveGameSystem_LoadSceneData
		{
			private static void Postfix()
			{
				if (textures.Count > 0)
				{
					System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
					stopwatch.Start();
					ReplacePlayerTextures();
					stopwatch.Stop();
					Logger.Log("Completed First Person Object Skin Replacement in {0} ms", stopwatch.ElapsedMilliseconds);
				}
			}
		}

		[HarmonyPatch(typeof(GearItem), "Awake")]
		internal class GearItem_Awake
		{
			private static void Postfix(GearItem __instance)
			{
				if (textures.Count > 0) ReplaceTextures(__instance.gameObject);
			}
		}
	}
}
