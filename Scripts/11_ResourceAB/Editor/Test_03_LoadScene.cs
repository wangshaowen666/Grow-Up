using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class Test_03_LoadScene : MonoBehaviour {

    [MenuItem("我的工具/hha")]
    static void TTtart()
    {
        //1.选中写入路径并清空
        //2.写入
        //3.读取(检测依赖)
        //4.加载

        string outPath = Application.streamingAssetsPath;
        if (Directory.Exists(outPath))
            Directory.Delete(outPath, true);
        Directory.CreateDirectory(outPath);

        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();
        builds.Add(new AssetBundleBuild()
        {
            assetBundleName = "scene.unity3d",
            assetNames = new string[] { "Assets/Scenes/BundleScene.unity" }
        });
        BuildPipeline.BuildAssetBundles(outPath, builds.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneOSX);
        AssetDatabase.Refresh();

        //AssetBundle asset = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "StreamingAssets"));
        //AssetBundleManifest manifest = asset.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //foreach (var item in manifest.GetAllDependencies("scene.unity3d"))
        //    AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, item));

        //AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "scene,unity3d"));
        //SceneManager.LoadScene("BundleScene");
    }
}
