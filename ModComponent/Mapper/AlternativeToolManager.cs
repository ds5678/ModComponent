using Il2Cpp;

using ModComponent.API.Components;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.Mapper;

internal static class AlternativeToolManager
{
	private static List<ModToolComponent> toolList = new();
	private static List<string> templateNameList = new();

	internal static void AddToList(ModToolComponent alternateTool, string templateName)
	{
		toolList.Add(alternateTool);
		templateNameList.Add(templateName);
	}

	private static void Clear()
	{
		toolList = new();
		templateNameList = new();
	}

	internal static void ProcessList()
	{
		for (int i = 0; i < toolList.Count; i++)
		{
			AddAlternativeTool(toolList[i], templateNameList[i]);
		}
		Clear();
	}

	private static void AddAlternativeTool(ModToolComponent modToolComponent, string templateName)
	{
		GameObject original = Resources.Load(templateName).Cast<GameObject>();
		if (original == null)
		{
			return;
		}

		AlternateTools alternateTools = ModComponent.Utils.ComponentUtils.GetOrCreateComponent<AlternateTools>(original);
		List<GameObject> list = new();
		if (alternateTools.m_AlternateToolsList != null)
		{
			list.AddRange(alternateTools.m_AlternateToolsList);
		}
		list.Add(modToolComponent.gameObject);
		alternateTools.m_AlternateToolsList = list.ToArray();
	}
}
