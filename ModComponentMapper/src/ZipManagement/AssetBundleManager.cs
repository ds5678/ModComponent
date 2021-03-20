using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModComponentMapper
{
    internal static class AssetBundleManager
    {
        private static List<string> pendingAssetBundles = new List<string>();

        internal static void Add(string relativePath)
        {
            if (pendingAssetBundles.Contains(relativePath))
            {
                Logger.LogWarning("Asset Bundle Manager already has '{0}' on the list of pending asset bundles.");
            }
            else pendingAssetBundles.Add(relativePath);
        }

        internal static void LoadPendingAssetBundles()
        {
            Logger.Log("Loading the pending asset bundles");
            foreach(string relativePath in pendingAssetBundles)
            {
                AutoMapper.LoadAssetBundle(relativePath);
            }
            pendingAssetBundles.Clear();
        }
    }
}
