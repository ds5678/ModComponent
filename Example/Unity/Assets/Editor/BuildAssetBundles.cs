using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEditor;

public class BuildAssetBundles : MonoBehaviour
{
    private const string TARGET_DIRECTORY = "AssetBundles";

    [MenuItem("Assets/Build AssetBundles")]
    public static void ExportResource()
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

            if (!Directory.Exists(TARGET_DIRECTORY))
            {
                Directory.CreateDirectory(TARGET_DIRECTORY);
            }

            BuildPipeline.BuildAssetBundles(TARGET_DIRECTORY, new AssetBundleBuild[] { build }, BuildAssetBundleOptions.ForceRebuildAssetBundle, BuildTarget.StandaloneWindows);
        }
    }
}
