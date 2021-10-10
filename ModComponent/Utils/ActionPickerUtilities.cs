using System;
using System.Collections.Generic;
using UnityEngine;

namespace ModComponent.Utils
{
	internal static class ActionPickerUtilities
	{
		public struct ActionPickerData
		{
			string SpriteName;
			string LocID;
			Action Callback;
			public ActionPickerData(string spriteName, string locId, Action callback)
			{
				SpriteName = spriteName;
				LocID = locId;
				Callback = callback;
			}
			public static implicit operator Panel_ActionPicker.ActionPickerItemData(ActionPickerData data)
			{
				return new Panel_ActionPicker.ActionPickerItemData(data.SpriteName, data.LocID, data.Callback);
			}
		}
		public static void ShowCustomActionPicker(GameObject objectInteractedWith, List<ActionPickerData> actionList)
		{
			Panel_ActionPicker panel = InterfaceManager.m_Panel_ActionPicker;
			if (panel == null || InterfaceManager.IsOverlayActiveCached()) return;

			if (panel.m_ActionPickerItemDataList == null)
				panel.m_ActionPickerItemDataList = new Il2CppSystem.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData>();
			else panel.m_ActionPickerItemDataList.Clear();
			foreach (var element in actionList)
				panel.m_ActionPickerItemDataList.Add(element);

			InterfaceManager.m_Panel_ActionPicker.m_ObjectInteractedWith = objectInteractedWith;
			InterfaceManager.m_Panel_ActionPicker.EnableWithCurrentList();
		}
	}
}
