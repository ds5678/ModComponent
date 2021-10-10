using MelonLoader;
using UnityEngine;

namespace ModComponent.Main
{
	internal class Implementation : MelonMod
	{
		public override void OnApplicationStart()
		{
			InitialLogStatements();
			Settings.instance.AddToModSettings("ModComponent");

			ModComponentMapper.MapperCore.InitializeAndMapAssets();
		}

		private void InitialLogStatements()
		{
			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
			Logger.LogDebug("Debug Compilation");
			Logger.LogNotDebug("Release Compilation");
		}

		public static byte[][] GetItemPackHashes() => ModComponentMapper.ZipFileLoader.hashes.ToArray();
	}
}
