using Harmony;
using System;
using UnityEngine;

namespace ModComponentMapper.InformationMenu
{

	internal static class ModComponentMenuPatches
	{

		internal const int MODCOMPONENT_ID = 0x4d43; // "MC" in hex

		[HarmonyPatch(typeof(Panel_OptionsMenu), "InitializeAutosaveMenuItems", new Type[0])]
		internal static class BuildModSettingsGUIPatch
		{
			internal static void Postfix()
			{
				DateTime tStart = DateTime.UtcNow;

				try
				{
					Logger.Log("Building ModComponent Menu GUI");
					ModComponentMenu.BuildGUI();
				}
				catch (Exception e)
				{
					Logger.LogError("Exception while building ModComponent Menu GUI\n" + e.ToString());
					return;
				}

				long timeMillis = (long)(DateTime.UtcNow - tStart).TotalMilliseconds;
				Logger.Log("Done! Took " + timeMillis + " ms");
			}
		}

		[HarmonyPatch(typeof(Panel_OptionsMenu), "ConfigureMenu", new Type[0])]
		internal static class AddModSettingsButton
		{
			internal static void Postfix(Panel_OptionsMenu __instance)
			{
				if (!ModComponentMenu.HasVisiblePages())
					return;

				BasicMenu basicMenu = __instance.m_BasicMenu;
				if (basicMenu == null)
					return;

				//AddAnotherMenuItem(basicMenu); // We need one more than they have...
				BasicMenu.BasicMenuItemModel firstItem = basicMenu.m_ItemModelList[0];
				int itemIndex = basicMenu.GetItemCount();
				basicMenu.AddItem("ModComponent", MODCOMPONENT_ID, itemIndex, "ModComponent", "View information about ModComponent and your installed item packs.", null,
						new Action(() => ShowModComponentMenu(__instance)), firstItem.m_NormalTint, firstItem.m_HighlightTint);
			}

			internal static void ShowModComponentMenu(Panel_OptionsMenu __instance)
			{
				GUI settings = GetModComponentMenuGUI(__instance);
				settings.Enable(__instance);
			}

			//
			//Since ModSettings already runs this, it is unnecessary for some reason to run it again.
			//In fact, running it twice causes an index out of bounds error if there is any patch on BasicMenu.InternalClickAction
			//That patch doesn't have to contain any code for the error to occur. It just has to exist.
			//Relentless Night 4.30 has one such patch.
			//
			internal static void AddAnotherMenuItem(BasicMenu basicMenu)
			{
				GameObject gameObject = NGUITools.AddChild(basicMenu.m_MenuGrid.gameObject, basicMenu.m_BasicMenuItemPrefab);
				gameObject.name = "ModComponent MenuItem";
				BasicMenuItem item = gameObject.GetComponent<BasicMenuItem>();
				BasicMenu.BasicMenuItemView view = item.m_View;
				int itemIndex = basicMenu.m_MenuItems.Count;
				EventDelegate onClick = new EventDelegate(new Action(() => basicMenu.OnItemClicked(itemIndex)));
				view.m_Button.onClick.Add(onClick);
				EventDelegate onDoubleClick = new EventDelegate(new Action(() => basicMenu.OnItemDoubleClicked(itemIndex)));
				view.m_DoubleClickButton.m_OnDoubleClick.Add(onDoubleClick);
				basicMenu.m_MenuItems.Add(view);
			}
		}

		[HarmonyPatch(typeof(Panel_OptionsMenu), "MainMenuTabOnEnable", new Type[0])]
		internal static class DisableModSettingsWhenBackPressed
		{
			internal static void Prefix(Panel_OptionsMenu __instance)
			{
				GameObject modComponentTab = GetModComponentTab(__instance);
				modComponentTab.SetActive(false);
			}
		}

		internal static GUI GetModComponentMenuGUI(Panel_OptionsMenu panel)
		{
			Transform panelTransform = panel.transform.Find("Pages/ModComponent");
			return panelTransform.GetComponent<GUI>();
		}

		internal static GameObject GetModComponentTab(Panel_OptionsMenu panel)
		{
			return panel.transform.Find("Pages/ModComponent").gameObject;
		}
	}
}
