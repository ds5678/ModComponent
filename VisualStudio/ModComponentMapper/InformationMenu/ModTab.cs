using UnityEngine;
using Il2Cpp = Il2CppSystem.Collections.Generic;

namespace ModComponentMapper
{
	internal class ModTab
	{

		internal readonly UIGrid uiGrid;
		internal readonly Il2Cpp.List<GameObject> menuItems;

		internal float scrollBarHeight;
		internal bool requiresConfirmation;

		internal ModTab(UIGrid uiGrid, Il2Cpp.List<GameObject> menuItems)
		{
			this.uiGrid = uiGrid;
			this.menuItems = menuItems;
		}
	}
}
