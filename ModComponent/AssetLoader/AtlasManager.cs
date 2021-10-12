using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.AssetLoader
{
	internal static class AtlasManager
	{
		private static Dictionary<string, UIAtlas> knownSpriteAtlases = new Dictionary<string, UIAtlas>();

		public static void LoadUiAtlas(Object asset)
		{
			GameObject gameObject = asset.Cast<GameObject>();
			if (gameObject == null)
			{
				Logger.Log("Asset called '{0}' is not a GameObject as expected.", asset.name);
				return;
			}

			UIAtlas uiAtlas = gameObject.GetComponent<UIAtlas>();
			if (uiAtlas == null)
			{
				Logger.Log("Asset called '{0}' does not contain a UIAtlast as expected.", asset.name);
				return;
			}

			Logger.Log("Processing asset '{0}' as UIAtlas.", asset.name);

			string[] sprites = uiAtlas.GetListOfSprites().ToArray();
			foreach (var eachSprite in sprites)
			{
				if (knownSpriteAtlases.ContainsKey(eachSprite))
				{
					Logger.Log("Replacing definition of sprite '{0}' from atlas '{1}' to '{2}'.", eachSprite, knownSpriteAtlases[eachSprite].name, uiAtlas.name);
					knownSpriteAtlases[eachSprite] = uiAtlas;
				}
				else
				{
					knownSpriteAtlases.Add(eachSprite, uiAtlas);
				}
			}
		}

		internal static UIAtlas GetSpriteAtlas(string spriteName)
		{
			UIAtlas result = null;
			knownSpriteAtlases.TryGetValue(spriteName, out result);
			return result;
		}
	}
}
