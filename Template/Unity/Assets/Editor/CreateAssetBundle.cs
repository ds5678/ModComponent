using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEditor;

public class CreateAssetBundle : MonoBehaviour
{

    [MenuItem("Assets/Build Asset Bundles")]
    static void ExportResource()
    {
        string[] assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        foreach (string eachAssetBundleName in assetBundleNames)
        {
            AssetBundleBuild build = new AssetBundleBuild();
            build.assetBundleName = eachAssetBundleName;
            if (!build.assetBundleName.EndsWith(".unity3d"))
            {
                build.assetBundleName += ".unity3d";
            }

            build.assetNames = AssetDatabase.GetAssetPathsFromAssetBundle(eachAssetBundleName);

            Debug.Log("Building Asset Bundle '" + eachAssetBundleName + "' with assets");
            foreach (string eachAssetPath in build.assetNames)
            {
                Debug.Log("  " + eachAssetPath);
            }

            if (!Directory.Exists("./Assets/AssetBundles"))
            {
                Directory.CreateDirectory("./Assets/AssetBundles");
            }

            BuildPipeline.BuildAssetBundles("Assets/AssetBundles", new AssetBundleBuild[] { build }, BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.StandaloneWindows);
        }
    }
}
