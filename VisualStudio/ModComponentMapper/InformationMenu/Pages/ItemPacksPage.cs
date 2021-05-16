using System.Collections.Generic;

namespace ModComponentMapper.InformationMenu
{
	internal class ItemPacksPage : InfoPage
	{
		private List<ItemPackData> itemPackList = new List<ItemPackData>();

		internal ItemPacksPage(string name) : base(name) { }
		internal void AddToItemPackList(ItemPackData itemPackData) => itemPackList.Add(itemPackData);
		protected override void MakeGUIContents(GUIBuilder guiBuilder)
		{
			if (itemPackList.Count == 0)
			{
				guiBuilder.AddHeader("No item packs loaded", false);
			}
			else
			{
				List<ItemPackData> workingPacks = new List<ItemPackData>();
				List<ItemPackData> nonWorkingPacks = new List<ItemPackData>();
				foreach (ItemPackData itemPack in itemPackList)
				{
					if (itemPack.GetLoadedCorrectly()) workingPacks.Add(itemPack);
					else nonWorkingPacks.Add(itemPack);
				}
				if (nonWorkingPacks.Count > 0)
				{
					guiBuilder.AddHeader("Item Packs - Non-Working", false);
					foreach (ItemPackData itemPack in nonWorkingPacks)
					{
						guiBuilder.AddEmptySetting(itemPack.GetName(), itemPack.GetZipFileName());
					}
				}
				if (workingPacks.Count > 0)
				{
					guiBuilder.AddHeader("Item Packs - Working (Probably)", false);
					foreach (ItemPackData itemPack in workingPacks)
					{
						guiBuilder.AddEmptySetting(itemPack.GetName(), itemPack.GetZipFileName());
					}
				}
			}
		}
		internal void SetItemPackNotWorking(string pathToAsset)
		{
			foreach (var itemPack in itemPackList)
			{
				if (pathToAsset.StartsWith(itemPack.GetZipFilePath())) itemPack.SetLoadedIncorrectly();
			}
		}
	}
}
