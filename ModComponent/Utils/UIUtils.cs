﻿using Il2Cpp;
using UnityEngine;

namespace ModComponent.Utils;

public static class UIUtils
{
	public static UITexture CreateOverlay(Texture2D texture)
	{
		UIRoot root = UIRoot.list[0];
		UIPanel panel = NGUITools.AddChild<UIPanel>(root.gameObject);

		UITexture result = NGUITools.AddChild<UITexture>(panel.gameObject);
		result.mainTexture = texture;

		Vector2 windowSize = panel.GetWindowSize();
		result.width = (int)windowSize.x;
		result.height = (int)windowSize.y;

		return result;
	}
}
