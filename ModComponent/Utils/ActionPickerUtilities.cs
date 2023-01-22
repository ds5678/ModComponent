using Il2Cpp;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.Utils;

internal static partial class ActionPickerUtilities
{
	public static void ShowCustomActionPicker(GameObject objectInteractedWith, List<ActionPickerData> actionList)
	{
		Panel_ActionPicker panel = UnityEngine.Object.FindObjectOfType<Panel_ActionPicker>(true);
		if (panel == null || InterfaceManager.IsOverlayActiveCached())
		{
			return;
		}

		if (panel.m_ActionPickerItemDataList == null)
		{
			panel.m_ActionPickerItemDataList = new Il2CppSystem.Collections.Generic.List<ActionPickerItemData>();
		}
		else
		{
			panel.m_ActionPickerItemDataList.Clear();
		}

		foreach (ActionPickerData element in actionList)
		{
			panel.m_ActionPickerItemDataList.Add(element);
		}

		// Zombie was here
		panel.m_ObjectInteractedWith = objectInteractedWith;
		panel.EnableWithCurrentList();
	}
}
