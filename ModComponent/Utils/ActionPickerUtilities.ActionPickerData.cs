extern alias Hinterland;
using Hinterland;
using System;

namespace ModComponent.Utils;

internal static partial class ActionPickerUtilities
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
}
