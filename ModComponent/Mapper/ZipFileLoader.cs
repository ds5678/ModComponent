using MelonLoader.ICSharpCode.SharpZipLib.Zip;
using MelonLoader.TinyJSON;
using ModComponent.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace ModComponent.Mapper;

internal static class ZipFileLoader
{
	internal static readonly List<byte[]> hashes = new();

	internal static void Initialize()
	{
		LoadZipFilesInDirectory(FileUtils.GetModsFolderPath(), false);
	}

	private static void LoadZipFilesInDirectory(string directory, bool recursive)
	{
		if (recursive)
		{
			string[] directories = Directory.GetDirectories(directory);
			foreach (string eachDirectory in directories)
			{
				LoadZipFilesInDirectory(eachDirectory, true);
			}
		}

		string[] files = Directory.GetFiles(directory);
		foreach (string eachFile in files)
		{
			if (eachFile.ToLower().EndsWith(".modcomponent"))
			{
				//PageManager.AddToItemPacksPage(new ItemPackData(eachFile));
				LoadZipFile(eachFile);
			}
		}
	}

	private static void LoadZipFile(string zipFilePath)
	{
		Logger.LogGreen($"Reading zip file at: '{zipFilePath}'");
		FileStream fileStream = File.OpenRead(zipFilePath);

		hashes.Add(SHA256.Create().ComputeHash(fileStream));
		fileStream.Position = 0;

		ZipInputStream zipInputStream = new ZipInputStream(fileStream);
		ZipEntry entry;
		while ((entry = zipInputStream.GetNextEntry()) != null)
		{
			string internalPath = entry.Name;
			string filename = Path.GetFileName(internalPath);
			FileType fileType = GetFileType(filename);
			if (fileType == FileType.other)
			{
				continue;
			}

			using MemoryStream unzippedFileStream = new MemoryStream();
			int size = 0;
			byte[] buffer = new byte[4096];
			while (true)
			{
				size = zipInputStream.Read(buffer, 0, buffer.Length);
				if (size > 0)
				{
					unzippedFileStream.Write(buffer, 0, size);
				}
				else
				{
					break;
				}
			}
			if (!TryHandleFile(zipFilePath, internalPath, fileType, unzippedFileStream))
			{
				return;
			}
		}
	}

	private static Encoding GetEncoding(MemoryStream memoryStream)
	{
		using StreamReader reader = new StreamReader(memoryStream, true);
		reader.Peek();
		return reader.CurrentEncoding;
	}

	private static string ReadToString(MemoryStream memoryStream)
	{
		Encoding encoding = GetEncoding(memoryStream);
		return encoding.GetString(memoryStream.ToArray());
	}

	private static FileType GetFileType(string filename)
	{
		if (string.IsNullOrWhiteSpace(filename))
		{
			return FileType.other;
		}

		if (filename.EndsWith(".unity3d", StringComparison.Ordinal))
		{
			return FileType.unity3d;
		}

		if (filename.EndsWith(".json", StringComparison.Ordinal))
		{
			return FileType.json;
		}

		if (filename.EndsWith(".txt", StringComparison.Ordinal))
		{
			return FileType.txt;
		}

		if (filename.EndsWith(".dll", StringComparison.Ordinal))
		{
			return FileType.dll;
		}

		if (filename.EndsWith(".bnk", StringComparison.Ordinal))
		{
			return FileType.bnk;
		}

		return FileType.other;
	}

	private static bool TryHandleFile(string zipFilePath, string internalPath, FileType fileType, MemoryStream unzippedFileStream)
	{
		switch (fileType)
		{
			case FileType.json:
				return TryHandleJson(zipFilePath, internalPath, ReadToString(unzippedFileStream));
			case FileType.unity3d:
				return TryHandleUnity3d(zipFilePath, internalPath, unzippedFileStream.ToArray());
			case FileType.txt:
				return TryHandleTxt(zipFilePath, internalPath, ReadToString(unzippedFileStream));
			case FileType.dll:
				return TryLoadAssembly(zipFilePath, internalPath, unzippedFileStream.ToArray());
			case FileType.bnk:
				return TryRegisterSoundBank(zipFilePath, internalPath, unzippedFileStream.ToArray());
			default:
				string fullPath = Path.Combine(zipFilePath, internalPath);
				PackManager.SetItemPackNotWorking(zipFilePath, $"Could not handle asset '{fullPath}'");
				return false;
		}
	}

	private static bool TryLoadAssembly(string zipFilePath, string internalPath, byte[] data)
	{
		try
		{
			Logger.Log($"Loading dll from zip at '{internalPath}'");
			Assembly.Load(data);
			return true;
		}
		catch (Exception e)
		{
			string fullPath = Path.Combine(zipFilePath, internalPath);
			PackManager.SetItemPackNotWorking(zipFilePath, $"Could not load assembly '{fullPath}'. {e.Message}");
			return false;
		}
	}

	private static bool TryRegisterSoundBank(string zipFilePath, string internalPath, byte[] data)
	{
		try
		{
			Logger.Log($"Loading bnk from zip at '{internalPath}'");
			ModComponent.AssetLoader.ModSoundBankManager.RegisterSoundBank(data);
			return true;
		}
		catch (Exception e)
		{
			string fullPath = Path.Combine(zipFilePath, internalPath);
			PackManager.SetItemPackNotWorking(zipFilePath, $"Could not register sound bank '{fullPath}'. {e.Message}");
			return false;
		}
	}

	private static bool TryHandleJson(string zipFilePath, string internalPath, string text)
	{
		try
		{
			string filenameNoExtension = Path.GetFileNameWithoutExtension(internalPath);
			if (internalPath.StartsWith(@"auto-mapped/"))
			{
				Logger.Log($"Reading automapped json from zip at '{internalPath}'");
				JsonHandler.RegisterJsonText(filenameNoExtension, text);
			}
			else if (internalPath.StartsWith(@"blueprints/"))
			{
				Logger.Log($"Reading blueprint json from zip at '{internalPath}'");
				CraftingRevisions.BlueprintManager.AddBlueprintFromJson(text, false);
			}
			else if (internalPath.StartsWith(@"localizations/"))
			{
				Logger.Log($"Reading json localization from zip at '{internalPath}'");
				LocalizationUtilities.LocalizationManager.LoadJSONLocalization(text);
			}
			else if (internalPath.ToLowerInvariant() == "buildinfo.json")
			{
				LogItemPackInformation(text);
			}
			else
			{
				throw new NotSupportedException($"Json file does not have a valid internal path: {internalPath}");
			}
			return true;
		}
		catch (Exception e)
		{
			string fullPath = Path.Combine(zipFilePath, internalPath);
			PackManager.SetItemPackNotWorking(zipFilePath, $"Could not load json '{fullPath}'. {e.Message}");
			return false;
		}
	}

	private static void LogItemPackInformation(string jsonText)
	{
		ProxyObject? dict = (ProxyObject)JSON.Load(jsonText);
		string modName = dict["Name"];
		string version = dict["Version"];
		Logger.Log($"Found: {modName} {version}");
	}

	private static bool TryHandleTxt(string zipFilePath, string internalPath, string text)
	{
		if (internalPath.StartsWith(@"gear-spawns/"))
		{
			try
			{
				Logger.Log($"Reading txt from zip at '{internalPath}'");
				GearSpawner.SpawnManager.ParseSpawnInformation(text);
				return true;
			}
			catch (Exception e)
			{
				string fullPath = Path.Combine(zipFilePath, internalPath);
				PackManager.SetItemPackNotWorking(zipFilePath, $"Could not load gear spawn '{fullPath}'. {e.Message}");
				return false;
			}
		}
		else
		{
			string fullPath = Path.Combine(zipFilePath, internalPath);
			PackManager.SetItemPackNotWorking(zipFilePath, $"Txt file not in the gear-spawns folder: '{fullPath}'");
			return false;
		}
	}

	private static bool TryHandleUnity3d(string zipFilePath, string internalPath, byte[] data)
	{
		string fullPath = Path.Combine(zipFilePath, internalPath);
		if (internalPath.StartsWith(@"auto-mapped/"))
		{
			Logger.Log($"Loading asset bundle from zip at '{internalPath}'");
			try
			{
				AssetBundle assetBundle = AssetBundle.LoadFromMemory(data);
				string relativePath = FileUtils.GetPathRelativeToModsFolder(fullPath);
				ModComponent.AssetLoader.ModAssetBundleManager.RegisterAssetBundle(relativePath, assetBundle);
				AutoMapper.AddAssetBundle(relativePath, zipFilePath);
				return true;
			}
			catch (Exception e)
			{
				PackManager.SetItemPackNotWorking(zipFilePath, $"Could not load asset bundle '{fullPath}'. {e.Message}");
				return false;
			}
		}
		else
		{
			PackManager.SetItemPackNotWorking(zipFilePath, $"Asset bundle not in the auto-mapped folder: '{fullPath}'");
			return false;
		}
	}
}
