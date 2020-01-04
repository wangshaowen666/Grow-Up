using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class Test_02_LoadAssetbundle : MonoBehaviour {

    private void Start()
    {

        //AssetBundle ab = AssetBundle.LoadFromFile(DownLoadAssetBundle("www.unity3d"));
        //AssetBundle.LoadFromFile(DownLoadAssetBundle("www2.unity3d"));


        //GameObject go = ab.LoadAsset<GameObject>("GameObject");
        //var g=Instantiate(go);

        StartCoroutine(MyTask());
    }

    string DownLoadAssetBundle(string abName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, abName);
        WWW www = new WWW("file://" + path);

        string name2 = Path.GetFileName(path);
        string filePath = Path.Combine(Application.persistentDataPath, name2);
        if (File.Exists(filePath))
            File.Delete(filePath);

        File.WriteAllBytes(filePath, www.bytes);
        return filePath;
    }

    IEnumerator MyTask()
    {
        yield return new WaitForSeconds(2);
        AssetBundle asset = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "StreamingAssets"));
        AssetBundleManifest manifest = asset.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        foreach (var item in manifest.GetAllDependencies("scene.unity3d"))
            AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, item));

        AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "scene.unity3d"));
        SceneManager.LoadScene("BundleScene");
    }
}
