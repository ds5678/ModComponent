extern alias Hinterland;
using Hinterland;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace ModComponent.AssetLoader;

internal static class ModSoundBankManager
{
	private const int MEMORY_ALIGNMENT = 16;

	private static bool DelayLoadingSoundBanks = true;
	private static List<string> pendingPaths = new List<string>();
	private static List<byte[]> pendingBytes = new List<byte[]>();

	public static void RegisterSoundBank(string relativePath)
	{
		string modDirectory = ModComponent.Utils.FileUtils.GetModsFolderPath();
		string soundBankPath = Path.Combine(modDirectory, relativePath);
		if (!File.Exists(soundBankPath))
		{
			throw new FileNotFoundException("Sound bank '" + relativePath + "' could not be found at '" + soundBankPath + "'.");
		}

		if (DelayLoadingSoundBanks)
		{
			Logger.Log($"Adding sound bank '{relativePath}' to the list of pending sound banks.");
			pendingPaths.Add(soundBankPath);
		}
		else
		{
			LoadSoundBank(soundBankPath);
		}
	}
	public static void RegisterSoundBank(byte[] data)
	{
		if (data == null)
		{
			throw new ArgumentNullException("Data for sound bank was null");
		}

		if (DelayLoadingSoundBanks)
		{
			Logger.Log("Adding sound bank to the list of pending sound banks.");
			pendingBytes.Add(data);
		}
		else
		{
			LoadSoundBank(data);
		}
	}

	internal static void RegisterPendingSoundBanks()
	{
		Logger.Log("Registering pending sound banks.");
		DelayLoadingSoundBanks = false;

		foreach (string eachPendingPath in pendingPaths)
		{
			LoadSoundBank(eachPendingPath);
		}
		pendingPaths.Clear();

		foreach (byte[] eachPendingBytes in pendingBytes)
		{
			LoadSoundBank(eachPendingBytes);
		}
		pendingBytes.Clear();
	}

	private static void LoadSoundBank(string soundBankPath)
	{
		//Logger.Log("Loading mod sound bank from '{0}'.", soundBankPath);
		//Logger.Log(soundBankPath);
		byte[] data = File.ReadAllBytes(soundBankPath);
		LoadSoundBank(data, soundBankPath);
	}
	private static void LoadSoundBank(byte[] data, string? soundBankPath = null)
	{
		// allocate memory and copy file contents to aligned address
		IntPtr allocated = Marshal.AllocHGlobal(data.Length + MEMORY_ALIGNMENT - 1);
		IntPtr aligned = new IntPtr((allocated.ToInt64() + MEMORY_ALIGNMENT - 1) / MEMORY_ALIGNMENT * MEMORY_ALIGNMENT);
		Marshal.Copy(data, 0, aligned, data.Length);

		AKRESULT result = AkSoundEngine.LoadBank(aligned, (uint)data.Length, out uint bankID);
		if (result != AKRESULT.AK_Success)
		{
			if (string.IsNullOrEmpty(soundBankPath))
			{
				Logger.Log("Failed to load sound bank.");
			}
			else
			{
				Logger.Log($"Failed to load sound bank from: '{soundBankPath}'");
			}

			Logger.Log($"Result was {result}.");
			Marshal.FreeHGlobal(allocated);
		}
	}
}