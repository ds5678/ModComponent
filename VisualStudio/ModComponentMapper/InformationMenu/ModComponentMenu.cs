using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponentMapper
{
	internal static class ModComponentMenu
	{

		internal static readonly HashSet<InfoPage> modComponentPages = new HashSet<InfoPage>();

		internal static GUI modComponentGUI = null;

		internal static void RegisterPage(InfoPage page)
		{
			if (string.IsNullOrEmpty(page.pageName))
			{
				throw new ArgumentException("Mod name must be a non-empty string", "page.pageName");
			}
			else if (modComponentPages.Contains(page))
			{
				throw new ArgumentException("Cannot add the same settings object multiple times", "page");
			}
			else if (modComponentGUI != null)
			{
				throw new InvalidOperationException("RegisterPage called after the GUI has been built.\n"
						+ "Call this method before Panel_CustomXPSetup::Awake, preferably from your mod's OnLoad method");
			}
			else
			{
				modComponentPages.Add(page);
			}
		}

		internal static void BuildGUI()
		{
			GameObject modSettingsTab = GUIBuilder.CreateModComponentTab();
			modComponentGUI = modSettingsTab.AddComponent<GUI>();
			modComponentGUI.Build();

			foreach (var itemPackList in modComponentPages)
			{
				itemPackList.AddToGUI(modComponentGUI);
			}
		}

		internal static void SetPagesVisible(bool visible)
		{
			foreach (var itemPackList in modComponentPages)
			{
				itemPackList.SetMenuVisible(visible);
			}
		}

		/// <summary>
		/// returns true if any mod settings are currently displayed
		/// </summary>
		/// <param name="isMainMenu"></param>
		/// <returns></returns>
		internal static bool HasVisiblePages()
		{
			foreach (var itemPackList in modComponentPages)
			{
				//if (itemPackList.GetVisible()) return true;
				if (itemPackList.IsUserVisible()) return true;
			}
			return false;
		}
	}
}
