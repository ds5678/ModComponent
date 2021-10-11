using MelonLoader.ICSharpCode.SharpZipLib.Zip;
using ModComponentMapper.InformationMenu;
using ModComponent.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ModComponentMapper
{
	internal enum FileType
	{
		json,
		unity3d,
		txt,
		dll,
		bnk,
		image,
		other
	}

	internal static class ZipFileLoader
	{
		internal static readonly List<byte[]> hashes = new List<byte[]>();
		internal static void Initialize()
		{
			string zipFolderDirectory = ModComponent.Utils.FileUtils.GetModComponentZipsFolderPath();
			if (!Directory.Exists(zipFolderDirectory))
			{
				Logger.Log("Directory '{0}' does not exist. Creating ...", zipFolderDirectory);
				Directory.CreateDirectory(zipFolderDirectory);
				return;
			}
			LoadZipFilesInDirectory(zipFolderDirectory);
		}
		internal static void LoadZipFilesInDirectory(string directory)
		{
			string[] directories = Directory.GetDirectories(directory);
			foreach (string eachDirectory in directories)
			{
				LoadZipFilesInDirectory(eachDirectory);
			}

			string[] files = Directory.GetFiles(directory);
			foreach (string eachFile in files)
			{
#if DEBUG
				if (eachFile.ToLower().EndsWith(".modcomponent") || eachFile.ToLower().EndsWith(".zip"))
#else
				if (eachFile.ToLower().EndsWith(".modcomponent"))
#endif
				{
					//PageManager.AddToItemPacksPage(new ItemPackData(eachFile));
					LoadZipFile(eachFile);
				}
			}
		}
		internal static void LoadZipFile(string zipFilePath)
		{
			Logger.LogGreen("Reading zip file at: '{0}'", zipFilePath);
			var fileStream = File.OpenRead(zipFilePath);

			hashes.Add(SHA256.Create().ComputeHash(fileStream));
			fileStream.Position = 0;

			var zipInputStream = new ZipInputStream(fileStream);
			ZipEntry entry;
			while ((entry = zipInputStream.GetNextEntry()) != null)
			{
				string internalPath = entry.Name;
				string filename = Path.GetFileName(internalPath);
				FileType fileType = GetFileType(filename);
				if (fileType == FileType.other)
					continue;


				using (var unzippedFileStream = new MemoryStream())
				{
					int size = 0;
					byte[] buffer = new byte[4096];
					while (true)
					{
						size = zipInputStream.Read(buffer, 0, buffer.Length);
						if (size > 0)
							unzippedFileStream.Write(buffer, 0, size);
						else
							break;
					}
					string fullPath = Path.Combine(zipFilePath, internalPath);
					switch (fileType)
					{
						case FileType.json:
							HandleJson(internalPath, ReadToString(unzippedFileStream), fullPath);
							break;
						case FileType.unity3d:
							HandleUnity3d(internalPath, unzippedFileStream, fullPath);
							break;
						case FileType.txt:
							HandleTxt(internalPath, ReadToString(unzippedFileStream), fullPath);
							break;
						case FileType.dll:
							Logger.Log("Loading dll from zip at '{0}'", internalPath);
							Assembly.Load(unzippedFileStream.ToArray());
							break;
						case FileType.bnk:
							Logger.Log("Loading bnk from zip at '{0}'", internalPath);
							ModComponent.AssetLoader.ModSoundBankManager.RegisterSoundBank(unzippedFileStream.ToArray());
							break;
						case FileType.image:
							HandleImage(internalPath, unzippedFileStream.ToArray(), fullPath);
							break;
					}
				}
			}
		}
		internal static Encoding GetEncoding(MemoryStream memoryStream)
		{
			using (var reader = new StreamReader(memoryStream, true))
			{
				reader.Peek();
				return reader.CurrentEncoding;
			}
		}
		internal static string ReadToString(MemoryStream memoryStream)
		{
			Encoding encoding = GetEncoding(memoryStream);
			//Logger.Log(encoding.EncodingName);
			return encoding.GetString(memoryStream.ToArray());
		}
		internal static FileType GetFileType(string filename)
		{
			if (String.IsNullOrWhiteSpace(filename)) return FileType.other;
			if (filename.EndsWith(".unity3d")) return FileType.unity3d;
			if (filename.EndsWith(".json")) return FileType.json;
			if (filename.EndsWith(".txt")) return FileType.txt;
			if (filename.EndsWith(".dll")) return FileType.dll;
			if (filename.EndsWith(".bnk")) return FileType.bnk;
			if (filename.EndsWith(".png") || filename.EndsWith(".jpg")) return FileType.image;
			return FileType.other;
		}
		private static void HandleJson(string internalPath, string text, string fullPath)
		{
			string filenameNoExtension = Path.GetFileNameWithoutExtension(internalPath);
			if (internalPath.StartsWith(@"auto-mapped/"))
			{
				Logger.Log("Reading automapped json from zip at '{0}'", internalPath);
				JsonHandler.RegisterJsonText(filenameNoExtension, fullPath, text, JsonType.Automapped);
			}
			else if (internalPath.StartsWith(@"blueprints/"))
			{
				Logger.Log("Reading blueprint json from zip at '{0}'", internalPath);
				JsonHandler.RegisterJsonText(filenameNoExtension, fullPath, text, JsonType.Blueprint);
			}
			else if (internalPath.StartsWith(@"existing-json/"))
			{
				Logger.Log("Reading existing json from zip at '{0}'", internalPath);
				JsonHandler.RegisterJsonText(filenameNoExtension, fullPath, text, JsonType.Existing);
			}
			else if (internalPath.StartsWith(@"localizations/"))
			{
				Logger.Log("Reading json localization from zip at '{0}'", internalPath);
				LocalizationUtilities.LocalizationManager.LoadJSONLocalization(text);
			}
		}
		private static void HandleTxt(string internalPath, string text, string fullPath)
		{
			if (internalPath.StartsWith(@"gear-spawns/"))
			{
				Logger.Log("Reading txt from zip at '{0}'", internalPath);
				string[] lines = Regex.Split(text, "\r\n|\r|\n");
				GearSpawnReader.ProcessLines(lines, fullPath);
			}
		}
		private static void HandleUnity3d(string internalPath, MemoryStream memoryStream, string fullPath)
		{
			if (internalPath.StartsWith(@"auto-mapped/"))
			{
				Logger.Log("Loading asset bundle from zip at '{0}'", internalPath);
				try
				{
					AssetBundle assetBundle = AssetBundle.LoadFromMemory(memoryStream.ToArray());
					string relativePath = FileUtils.GetPathRelativeToModsFolder(fullPath);
					ModComponent.AssetLoader.ModAssetBundleManager.RegisterAssetBundle(relativePath, assetBundle);
					AssetBundleManager.Add(relativePath, fullPath);
				}
				catch (Exception e)
				{
					InformationMenu.PackManager.SetItemPackNotWorking(fullPath, $"Could not load asset bundle '{fullPath}'. {e.Message}");
				}
			}
		}

		private static void HandleImage(string internalPath, byte[] data, string fullPath)
		{
			string filenameNoExtension = Path.GetFileNameWithoutExtension(internalPath);
			if (internalPath.StartsWith(@"skins/"))
			{
				Logger.Log("Loading skin image from zip at '{0}'", internalPath);
				ModComponent.AssetLoader.SkinManager.AddToTextureList(filenameNoExtension, data);
			}
			else if (internalPath.StartsWith(@"icons/"))
			{
				Logger.Log("Loading icon image from zip at '{0}'", internalPath);
				ModComponent.AssetLoader.AlternateIconManager.AddToTextureList(filenameNoExtension, data);
			}
		}
	}
}
