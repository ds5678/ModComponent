using System.Collections.Generic;
using UnityEngine;

namespace AssetLoader
{
	internal static class AlternateIconManager
	{
		private static readonly Dictionary<string, byte[]> textures = new Dictionary<string, byte[]>();

		internal static void AddToTextureList(string name, byte[] array)
		{
			if (string.IsNullOrWhiteSpace(name) || array == null || array.Length == 0) return;
			textures.Add(name, array);
		}

		internal static bool ContainsKey(string key)
		{
			if (key == null) return false;
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
	}
}
