extern alias Hinterland;
using Hinterland;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.Utils;

internal static partial class ActionPickerUtilities
{
	public static void ShowCustomActionPicker(GameObject objectInteractedWith, List<ActionPickerData> actionList)
	{
		Panel_ActionPicker panel = InterfaceManager.m_Panel_ActionPicker;
		if (panel == null || InterfaceManager.IsOverlayActiveCached())
		{
			return;
		}

		if (panel.m_ActionPickerItemDataList == null)
		{
			panel.m_ActionPickerItemDataList = new Il2CppSystem.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData>();
		}
		else
		{
			panel.m_ActionPickerItemDataList.Clear();
		}

		foreach (ActionPickerData element in actionList)
		{
			panel.m_ActionPickerItemDataList.Add(element);
		}

		InterfaceManager.m_Panel_ActionPicker.m_ObjectInteractedWith = objectInteractedWith;
		InterfaceManager.m_Panel_ActionPicker.EnableWithCurrentList();
	}
}
