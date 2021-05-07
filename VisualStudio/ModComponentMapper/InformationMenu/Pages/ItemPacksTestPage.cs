namespace ModComponentMapper
{
	internal class ItemPacksTestPage : InfoPage
	{
		internal string[] workingPacks;
		internal string[] nonWorkingPacks;

		internal ItemPacksTestPage(string name, string[] working, string[] nonworking) : base(name)
		{
			this.workingPacks = working;
			this.nonWorkingPacks = nonworking;
		}
		protected override void MakeGUIContents(GUIBuilder guiBuilder)
		{
			guiBuilder.AddHeader("Working Item Packs", false);
			foreach (string itemPack in workingPacks)
			{
				guiBuilder.AddEmptySetting(itemPack, "filename.zip");
			}
			guiBuilder.AddHeader("Non-Working Item Packs", false);
			foreach (string itemPack in nonWorkingPacks)
			{
				guiBuilder.AddEmptySetting(itemPack, "filename.zip");
			}
		}
	}
}
