using System.IO;
using UnityEngine;

namespace ModComponentMapper
{
	internal static class SkinManager
	{
		internal static void ConvertImage(string path, byte[] array)
		{
			if (string.IsNullOrWhiteSpace(path) || array is null || array.Length == 0) return;
			string name = Path.GetFileNameWithoutExtension(path);
			Texture2D output = new Texture2D(1000,1000);
			if (ImageConversion.LoadImage(output, array) && output != null)
			{
				Object.DontDestroyOnLoad(output);
				output.name = name;
				AssetLoader.AlternateAssetManager.AddAlternateAsset(name, output.Cast<Object>());
			}
			else Logger.LogError($"Failed to load image: '{path}'");
		}
	}
}
