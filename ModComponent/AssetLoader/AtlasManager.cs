using Il2Cpp;
using UnityEngine;

namespace ModComponent.AssetLoader;

internal static class AtlasManager
{
	private static Dictionary<string, UIAtlas> knownSpriteAtlases = new Dictionary<string, UIAtlas>();

	public static void LoadUiAtlas(UnityEngine.Object asset)
	{
		GameObject? gameObject = asset.TryCast<GameObject>();
		if (gameObject == null)
		{
			Logger.Log($"Asset called '{asset.name}' is not a GameObject as expected.");
			return;
		}

		UIAtlas? uiAtlas = gameObject.GetComponent<UIAtlas>();
		if (uiAtlas == null)
		{
			Logger.Log($"Asset called '{asset.name}' does not contain a UIAtlast as expected.");
			return;
		}

		Logger.Log($"Processing asset '{asset.name}' as UIAtlas.");

		string[] sprites = uiAtlas.GetListOfSprites().ToArray();
		foreach (string eachSprite in sprites)
		{
			if (knownSpriteAtlases.ContainsKey(eachSprite))
			{
				Logger.Log($"Replacing definition of sprite '{eachSprite}' from atlas '{knownSpriteAtlases[eachSprite].name}' to '{uiAtlas.name}'.");
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
		knownSpriteAtlases.TryGetValue(spriteName, out UIAtlas? result);
		return result;
	}
}
