using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModComponentMapper
{
    class JsonHandler
    {
        private static Dictionary<string, string> itemJsons = new Dictionary<string, string>();

        public static void RegisterJson(string itemName,string path)
        {
            if (itemJsons.ContainsKey(itemName))
            {
                Implementation.Log("Overwriting path for {0}", itemName);
                itemJsons[itemName] = path;
            }
            else
            {
                itemJsons.Add(itemName, path);
            }
        }
        public static string GetPath(string itemName)
        {
            if (itemJsons.ContainsKey(itemName.ToLower()))
            {
                return itemJsons[itemName.ToLower()];
            }
            else
            {
                Implementation.Log("Could not find {0} in json dictionary", itemName);
                return null;
            }
        }
        public static void RegisterDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                return;
            }
            string[] directories = Directory.GetDirectories(directory);
            foreach (string eachDirectory in directories)
            {
                RegisterDirectory(eachDirectory);
            }

            string[] files = Directory.GetFiles(directory);
            foreach (string eachFile in files)
            {
                string name = AssetLoader.ModAssetBundleManager.GetAssetName(eachFile);
                if (eachFile.ToLower().EndsWith(".json"))
                {
                    Implementation.Log("Found '{0}'", eachFile);
                    RegisterJson(name, eachFile);
                    continue;
                }

            }
        }
        public static void LogDirectoryContents()
        {
            foreach(string key in itemJsons.Keys)
            {
                Implementation.Log("Key: '{0}', Entry: '{1}'", key, itemJsons[key]);
            }
        }
    }
}
