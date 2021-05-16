using System;
using System.Reflection;

namespace ModComponentMapper.InformationMenu
{
	internal class VersionInfoPage : InfoPage
	{
		internal VersionInfoPage(string name) : base(name) { }
		protected override void MakeGUIContents(GUIBuilder guiBuilder)
		{
			guiBuilder.AddHeader("ModComponent", false);
			guiBuilder.AddEmptySetting("ModComponent", GetVersionString(typeof(ModComponentMain.Implementation)));
			guiBuilder.AddHeader("Dependencies", false);
			guiBuilder.AddEmptySetting("MelonLoader", GetVersionString(typeof(MelonLoader.MelonBase), 4));
			guiBuilder.AddEmptySetting("ModSettings", GetVersionString(typeof(ModSettings.ModSettingsBase)));
		}
		public string GetVersionString(Type type) => GetVersionString(type, 3);
		public string GetVersionString(Type type, int depth)
		{
			Version version = Assembly.GetAssembly(type).GetName().Version;
			switch (depth)
			{
				case 1:
					return string.Format("{0}", version.Major);
				case 2:
					return string.Format("{0}.{1}", version.Major, version.Minor);
				case 3:
					return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
				case 4:
					return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
				default:
					return string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Build);
			}
		}
	}
}
