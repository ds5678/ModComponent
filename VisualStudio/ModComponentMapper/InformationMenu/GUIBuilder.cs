using System.Collections.Generic;
using UnityEngine;
using Il2Cpp = Il2CppSystem.Collections.Generic;

namespace ModComponentMapper
{
	internal class GUIBuilder
	{

		internal const int gridCellHeight = 33;

		internal static readonly GameObject headerLabelPrefab;
		internal static readonly GameObject comboBoxPrefab;
		internal readonly GUI settingsGUI;
		internal readonly MenuGroup menuGroup;

		static GUIBuilder()
		{
			var xpPanel = Resources.Load("Panel_CustomXPSetup").TryCast<GameObject>()?.GetComponent<Panel_CustomXPSetup>();
			Transform firstSection = xpPanel?.m_ScrollPanelOffsetTransform.GetChild(0);

			headerLabelPrefab = UnityEngine.Object.Instantiate(firstSection.Find("Header").gameObject);
			headerLabelPrefab.SetActive(false);

			comboBoxPrefab = UnityEngine.Object.Instantiate(xpPanel.m_AllowInteriorSpawnPopupList.gameObject);
			comboBoxPrefab.SetActive(false);
		}

		protected readonly UIGrid uiGrid;
		protected readonly Il2Cpp.List<GameObject> menuItems;

		protected Header lastHeader;

		protected GUIBuilder(UIGrid uiGrid, Il2Cpp.List<GameObject> menuItems)
		{
			this.uiGrid = uiGrid;
			this.menuItems = menuItems;
		}

		internal GUIBuilder(string modName, GUI settingsGUI) : this(modName, settingsGUI, settingsGUI.CreateModTab(modName)) { }

		internal GUIBuilder(string modName, GUI settingsGUI, ModTab modTab) : this(modTab.uiGrid, modTab.menuItems)
		{
			this.settingsGUI = settingsGUI;
			menuGroup = new MenuGroup(modName, settingsGUI);
		}

		internal static GameObject CreateModComponentTab()
		{
			Panel_OptionsMenu panel = InterfaceManager.m_Panel_OptionsMenu;
			Transform pages = panel.transform.Find("Pages");
			GameObject tab = UnityEngine.Object.Instantiate(panel.m_QualityTab, pages);
			tab.name = "ModComponent";

			Transform titleLabel = tab.transform.Find("TitleDisplay/Label");
			UnityEngine.Object.Destroy(titleLabel.GetComponent<UILocalize>());
			titleLabel.GetComponent<UILabel>().text = "ModComponent";

			panel.m_MainMenuItemTabs.Add(tab);
			panel.m_Tabs.Add(tab);

			return tab;
		}

		internal void AddPage(InfoPage infoPage)
		{
			menuGroup.NotifyChildAdded(infoPage.IsVisible());
			infoPage.AddVisibilityListener((visible) =>
			{
				menuGroup.NotifyChildVisible(visible);
			});
		}

		internal void AddHeader(string title, bool localize)
		{
			GameObject padding = NGUITools.AddChild(uiGrid.gameObject);
			GameObject header = NGUITools.AddChild(uiGrid.gameObject);
			GameObject label = NGUITools.AddChild(header, headerLabelPrefab);

			label.SetActive(true);
			label.transform.localPosition = new Vector2(-70, 0);
			label.name = "Custom Header (" + title + ")";
			SetLabelText(label.transform, title, localize);

			lastHeader = new Header(header, padding);
		}

		internal void AddPaddingHeader()
		{
			GameObject padding = NGUITools.AddChild(uiGrid.gameObject);
			lastHeader = new Header(padding);
		}
		internal void AddEmptySetting(string name, string description)
		{
			GameObject setting = CreateSetting(name, false, description, false, comboBoxPrefab, "Label");
			ConsoleComboBox comboBox = setting.GetComponent<ConsoleComboBox>();
			comboBox.items.Clear();
			comboBox.m_Localize = false;

			setting.transform.FindChild("Button_Decrease")?.gameObject?.SetActive(false);
			setting.transform.FindChild("Button_Increase")?.gameObject?.SetActive(false);
			setting.transform.FindChild("Label_Value")?.gameObject?.SetActive(false);

			bool startVisible = true;
			if (setting.activeSelf != startVisible)
			{
				setting.SetActive(startVisible);
			}
			lastHeader?.NotifyChildAdded(startVisible);
		}


		internal GameObject CreateSetting(string nameText, bool nameLocalize, string descriptionText, bool descriptionLocalize, GameObject prefab, string labelName)
		{
			GameObject setting = NGUITools.AddChild(uiGrid.gameObject, prefab);
			setting.name = "Custom Setting (" + nameText + ")";

			Transform labelTransform = setting.transform.Find(labelName);
			SetLabelText(labelTransform, nameText, nameLocalize);

			DescriptionHolder descriptionHolder = setting.AddComponent<DescriptionHolder>();
			descriptionHolder.SetDescription(descriptionText, descriptionLocalize);

			menuItems.Add(setting);
			return setting;
		}

		internal static void SetLabelText(Transform transform, string text, bool localize)
		{
			if (localize)
			{
				UILocalize uiLocalize = transform.GetComponent<UILocalize>();
				uiLocalize.key = text;
			}
			else
			{
				UnityEngine.Object.Destroy(transform.GetComponent<UILocalize>());
				UILabel uiLabel = transform.GetComponent<UILabel>();
				uiLabel.text = text;
			}
		}

		protected class Header : Group
		{

			internal readonly List<GameObject> guiObjects;

			internal Header(params GameObject[] guiObjects)
			{
				this.guiObjects = new List<GameObject>(guiObjects);
			}

			protected override void SetVisible(bool visible)
			{
				foreach (GameObject guiObject in guiObjects)
				{
					NGUITools.SetActiveSelf(guiObject, visible);
				}
			}
		}

		internal class MenuGroup : Group
		{

			internal readonly string modName;
			internal readonly GUI modSettings;

			internal MenuGroup(string modName, GUI modSettings)
			{
				this.modName = modName;
				this.modSettings = modSettings;
			}

			protected override void SetVisible(bool visible)
			{
				if (visible)
				{
					modSettings.AddModSelector(modName);
				}
				else
				{
					modSettings.RemoveModSelector(modName);
				}
			}
		}
	}
}