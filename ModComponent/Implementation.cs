using MelonLoader;
using UnityEngine;

namespace ModComponent
{
	internal class Implementation : MelonMod
	{
		public override void OnApplicationStart()
		{
			InitialLogStatements();
			Settings.instance.AddToModSettings("ModComponent");

			Mapper.MapperCore.InitializeAndMapAssets();
		}

		private void InitialLogStatements()
		{
			Debug.Log($"[{Info.Name}] Version {Info.Version} loaded!");
			Logger.LogDebug("Debug Compilation");
			Logger.LogNotDebug("Release Compilation");
		}

		public static byte[][] GetItemPackHashes() => Mapper.ZipFileLoader.hashes.ToArray();
	}
}
