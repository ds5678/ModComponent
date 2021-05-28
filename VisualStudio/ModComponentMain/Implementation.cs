using MelonLoader;
using System.IO;
using UnityEngine;

namespace ModComponentMain
{
	public static class BuildInfo
	{
		public const string Name = "ModComponent"; // Name of the Mod.  (MUST BE SET)
		public const string Description = "A utility mod for custom item creation."; // Description for the Mod.  (Set as null if none)
		public const string Author = "WulfMarius, ds5678"; // Author of the Mod.  (MUST BE SET)
		public const string Company = null; // Company that made the Mod.  (Set as null if none)
		public const string Version = "4.6.0"; // Version of the Mod.  (MUST BE SET)
		public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
	}
	public class Implementation : MelonMod
	{
		public override void OnApplicationStart()
		{
			//ModComponentMapper.TestFunctions.TestFunction();
			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");

			Logger.LogDebug("Debug Compilation");
			Logger.LogNotDebug("Release Compilation");

			Settings.OnLoad();
			Injections.API_Injections();
			//ModComponentMapper.TestFunctions.TestFunction();
			ModComponentMapper.MapperImplementation.OnApplicationStart();
		}

		public static string GetModsFolderPath()
		{
			return Path.GetFullPath(typeof(MelonMod).Assembly.Location + @"\..\..\Mods");
		}
	}
}
