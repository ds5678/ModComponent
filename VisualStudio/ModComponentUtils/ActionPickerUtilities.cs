using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ModComponentUtils
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
				return new Panel_ActionPicker.ActionPickerItemData(data.SpriteName,data.LocID,data.Callback);
			}
		}
		public static void ShowCustomActionPicker(GameObject objectInteractedWith,List<ActionPickerData> actionList)
		{
			if (InterfaceManager.m_Panel_ActionPicker is null || InterfaceManager.IsOverlayActiveCached()) return;

			if(ReplaceList(InterfaceManager.m_Panel_ActionPicker.m_ActionPickerItemDataList, actionList))
			{
				InterfaceManager.m_Panel_ActionPicker.m_ObjectInteractedWith = objectInteractedWith;
				InterfaceManager.m_Panel_ActionPicker.EnableWithCurrentList();
			}
		}
		internal static bool ReplaceList(Il2CppSystem.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData> original, List<ActionPickerData> actionList)
		{
			if (actionList is null) return false;
			if (original is null) original = new Il2CppSystem.Collections.Generic.List<Panel_ActionPicker.ActionPickerItemData>();
			else original.Clear();
			foreach (var element in actionList) original.Add(element);
			return true;
		}
	}
}
