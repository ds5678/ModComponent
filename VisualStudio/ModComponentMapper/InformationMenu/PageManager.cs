namespace ModComponentMapper.InformationMenu
{
	internal static class PageManager
	{
		private static readonly ItemPacksPage itemPacksPage = new ItemPacksPage("Item Packs");
		private static readonly VersionInfoPage versionInfoPage = new VersionInfoPage("Version Information");
		internal static void Initialize()
		{
			ModComponentMenu.RegisterPage(versionInfoPage);
			ModComponentMenu.RegisterPage(itemPacksPage);
			//ModComponentMenu.RegisterPage(new ItemPacksTestPage("Item Packs Test", new string[] { "Binoculars", "Better Water Management", "Cannery Manufacturing" }, new string[] { "Food Pack", "Clothing Pack", "Fire Pack" }));
			//ModComponentMenu.RegisterPage(new ItemPacksTestPage("Number Test", new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" }, new string[] { "21" }));
		}
		internal static void AddToItemPacksPage(ItemPackData itemPackData) => itemPacksPage.AddToItemPackList(itemPackData);
		internal static void SetItemPackNotWorking(string pathToAsset) => itemPacksPage.SetItemPackNotWorking(pathToAsset);
	}
}
