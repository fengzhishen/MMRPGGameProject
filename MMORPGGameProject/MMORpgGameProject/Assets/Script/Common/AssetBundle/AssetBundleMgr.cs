using UnityEngine;
using System.Collections;

public class AssetBundleMgr : Singleton<AssetBundleMgr>
{
    /// <summary>
    /// 加载AB包中的资源
    /// </summary>
    /// <param name="abPath"></param>
    /// <param name="assetName"></param>
    public GameObject LoadAsset(string abPath,string assetName)
    {
        if(string.IsNullOrEmpty(abPath) || string.IsNullOrEmpty(assetName))
        {
            Debug.LogError(GetType() + "/参数非法");
            return null;
        }
        using (AssetBundleLoader loader = new AssetBundleLoader(abPath))
        {
           return loader.LoadAsset<GameObject>(assetName);
        }
    }

    /// <summary>
    /// 加载AB包中的资源并且克隆
    /// </summary>
    /// <param name="abPath"></param>
    /// <param name="assetName"></param>
    public GameObject LoadClone(string abPath, string assetName)
    {
        if (string.IsNullOrEmpty(abPath) || string.IsNullOrEmpty(assetName))
        {
            Debug.LogError(GetType() + "/参数非法");
            return null;
        }
        using (AssetBundleLoader loader = new AssetBundleLoader(abPath))
        {
           return loader.LoadAssetClone<GameObject>(assetName);
        }
    }

    public AssetBundleLoaderAsync LoadAssetAsync(string abPath,string assetName,System.Action<GameObject> OnLoadABCompleted)
    {
        GameObject @object = new GameObject();
        AssetBundleLoaderAsync loaderAsync = @object.AddComponent<AssetBundleLoaderAsync>();
        loaderAsync.OnLoadABCompleted += OnLoadABCompleted;
        loaderAsync.InitPathAndAssetName(abPath, assetName);
        return loaderAsync;
    }

    public override void Dispose()
    {
        base.Dispose();
        Resources.UnloadUnusedAssets();
    }
}
