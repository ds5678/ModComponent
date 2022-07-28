using System.Collections.Generic;

namespace ModComponent.Mapper;

internal class ItemPackData
{
	private string name;
	private string zipFileName;
	private string zipFilePath;
	private bool loadedCorrectly = true;

	public ItemPackData(string zipFilePath)
	{
		this.name = ParseNameFromZipFilePath(zipFilePath);
		this.zipFileName = ModComponent.AssetLoader.ModAssetBundleManager.GetAssetName(zipFilePath, false);
		this.zipFilePath = zipFilePath;
	}
	public ItemPackData(string zipFilePath, string zipFileName)
	{
		this.name = ParseNameFromZipFilePath(zipFileName);
		this.zipFileName = zipFileName;
		this.zipFilePath = zipFilePath;
	}

	public string GetName() => string.Copy(name);
	public string GetZipFileName() => string.Copy(zipFileName);
	public string GetZipFilePath() => string.Copy(zipFilePath);
	public bool GetLoadedCorrectly() => loadedCorrectly;
	public void SetLoadedIncorrectly()
	{
		if (loadedCorrectly) loadedCorrectly = false;
	}

	public static string ParseNameFromZipFilePath(string zipFilePath)
	{
		string trimmed = zipFilePath.Trim();
		//Logger.Log(trimmed);
		string noPathNoExtensions = ModComponent.AssetLoader.ModAssetBundleManager.GetAssetName(trimmed);
		//Logger.Log(noPathNoExtensions);
		string[] splitBySeparators = noPathNoExtensions.Split(new char[] { '-', '_', ' ' });
		//Logger.Log(JSON.Dump(splitBySeparators));
		string[] splitByCamelCase = SplitCamelCase(splitBySeparators);
		//Logger.Log(JSON.Dump(splitByCamelCase));
		return MergeWithSpaces(splitByCamelCase);
	}
	public static string[] SplitCamelCase(string line)
	{
		if (string.IsNullOrEmpty(line)) return new string[0];
		List<string> result = new List<string>();
		string temp = "";
		for (int i = 0; i < line.Length; i++)
		{
			if (char.IsUpper(line[i]) && i != 0)
			{
				result.Add(string.Copy(temp));
				temp = "";
			}
			temp += line[i];
		}
		result.Add(string.Copy(temp));
		return result.ToArray();
	}
	public static string[] SplitCamelCase(string[] lines)
	{
		string[] result = new string[0];
		foreach (string line in lines)
		{
			result = Combine<string>(result, SplitCamelCase(line));
		}
		return result;
	}
	public static T[] Combine<T>(T[] array1, T[] array2)
	{
		T[] result = new T[array1.Length + array2.Length];
		for (int i = 0; i < array1.Length; i++)
		{
			result[i] = array1[i];
		}
		for (int j = 0; j < array2.Length; j++)
		{
			result[array1.Length + j] = array2[j];
		}
		return result;
	}
	public static string MergeWithSpaces(string[] words)
	{
		string result = "";
		bool firstWord = true;
		for (int i = 0; i < words.Length; i++)
		{
			if (string.IsNullOrEmpty(words[i])) continue;
			if (firstWord)
			{
				result = result + words[i];
				firstWord = false;
			}
			else result = result + " " + words[i];
		}
		return result;
	}
}
