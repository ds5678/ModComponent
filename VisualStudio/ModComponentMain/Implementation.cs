using MelonLoader;
using System.IO;
using UnityEngine;

namespace ModComponentMain
{
	public class Implementation : MelonMod
	{
		public override void OnApplicationStart()
		{
			//ModComponentMapper.TestFunctions.TestFunction();
			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
#if DEBUG
            Logger.Log("Debug Compilation");
#else
			Logger.Log("Release Compilation");
#endif
			Settings.OnLoad();
			Injections.AssetLoader_Injections();
			Injections.API_Injections();
			Injections.Mapper_Injections();
			Preloader.PreloadingManager.Initialize();
			ModComponentMapper.MapperImplementation.OnApplicationStart();
		}

		public override void OnGUI()
		{
			Preloader.PreloadingManager.OnGUI();
		}

		public static string GetModsFolderPath()
		{
			return Path.GetFullPath(typeof(MelonMod).Assembly.Location + @"\..\..\Mods");
		}
	}
}
