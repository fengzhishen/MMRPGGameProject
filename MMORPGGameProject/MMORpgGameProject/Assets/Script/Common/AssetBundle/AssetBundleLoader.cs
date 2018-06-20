using UnityEngine;
using System.Collections;
using System;

public class AssetBundleLoader: IDisposable
{
    public bool bUnloadLoadAsset = false;
    private AssetBundle bundle;

    public AssetBundleLoader(string assetbundlePath)
    {
        if(!string.IsNullOrEmpty(assetbundlePath))
        {
            string abPath = LoadFileMgr.Instance.LoacalFilePath + assetbundlePath;
            byte[] abBytes = LoadFileMgr.Instance.GetBuffer(abPath);
            bundle = AssetBundle.LoadFromMemory(abBytes);

        }
    }

    public void Dispose()
    {
        Dispose(bUnloadLoadAsset);
    }
    private void Dispose(bool bUnloadLoadAssets)
    {       
        if (bUnloadLoadAssets)
        {
            if (bundle != null)
            {
                bundle.Unload(true);
            }          
        }
        else
        {
            bundle.Unload(false);
        }

        bundle = null;
    }
    public T LoadAsset<T>(string name) where T:UnityEngine.Object
    {
        if(bundle != null)
        {
           return bundle.LoadAsset<GameObject>(name) as T;
        }
        Debug.LogError(GetType() + "/AB包没有加载。");
        return null;
    }

    public T LoadAssetClone<T>(string name) where T : UnityEngine.Object
    {
        if (bundle != null)
        {
            return GameObject.Instantiate<T>(bundle.LoadAsset<GameObject>(name) as T);
        }
        Debug.LogError(GetType() + "/AB包没有加载。");
        return null;
    }   
}
