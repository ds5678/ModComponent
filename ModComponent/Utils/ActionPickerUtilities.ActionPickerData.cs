using Il2Cpp;
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

		public static implicit operator ActionPickerItemData(ActionPickerData data)
		{
			return new ActionPickerItemData(data.SpriteName, data.LocID, data.Callback);
		}
	}
}
