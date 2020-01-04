using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Test_01_BuildAssetBundle : MonoBehaviour {

    [MenuItem("我的工具/打包安卓AssetBundle")]
	static void BuildAndrioAssetBundle()
    {
        string outPath = Path.Combine(Application.dataPath, "StreamingAssets"); //将两个路径联合成一个路径

        if (Directory.Exists(outPath))
            Directory.Delete(outPath,true);
        Directory.CreateDirectory(outPath);

        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
        AssetDatabase.Refresh();
    }

    [MenuItem("我的工具/打包IOS AssetBundle")]
    static void BuildIOSAssetBundle()
    {
        string outPath = Path.Combine(Application.dataPath, "StreamingAssets"); //将两个路径联合成一个路径

        if (Directory.Exists(outPath))
            Directory.Delete(outPath, true);
        Directory.CreateDirectory(outPath);

        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
        AssetDatabase.Refresh();
    }

    [MenuItem("我的工具/打包Mac AssetBundle")]
    static void BuildMacAssetBundle()
    {
        string outPath = Path.Combine(Application.dataPath, "StreamingAssets"); //将两个路径联合成一个路径

        if (Directory.Exists(outPath))
            Directory.Delete(outPath, true);
        Directory.CreateDirectory(outPath);

        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneOSX);
        AssetDatabase.Refresh();
    }

    [MenuItem("我的工具/打包指定 AssetBundle")]
    static void BuildOneAssetBundle()
    {
        string outPath = Path.Combine(Application.dataPath, "StreamingAssets"); //将两个路径联合成一个路径

        if (Directory.Exists(outPath))
            Directory.Delete(outPath, true);
        Directory.CreateDirectory(outPath);


        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        builds.Add(new AssetBundleBuild()
        {
            assetBundleName = "www.unity3d",
            assetNames = new string[]
        { "Assets/AssetBundle/fayt/GameObject.prefab" }
        });
        builds.Add(new AssetBundleBuild()
        {
            assetBundleName = "www2.unity3d",
            assetNames = new string[]
        { "Assets/Sucai/timg.png" }
        });
        BuildPipeline.BuildAssetBundles(outPath, builds.ToArray(),BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneOSX);
        AssetDatabase.Refresh();
    }
}
