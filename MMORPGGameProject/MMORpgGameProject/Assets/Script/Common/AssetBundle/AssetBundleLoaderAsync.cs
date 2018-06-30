using UnityEngine;
using System.Collections;

public class AssetBundleLoaderAsync:MonoBehaviour
{
    private string abPath = string.Empty;
    private string assetName = string.Empty;
    private AssetBundleCreateRequest bundleRequest;
    private AssetBundle bundle;
    public System.Action<GameObject> OnLoadABCompleted;
    public float asyncProgess;
    void Start()
    {
        StartCoroutine(Load());
    }

    public void InitPathAndAssetName(string path,string assetName)
    {
        this.abPath = path;
        this.assetName = assetName;
    }
	private IEnumerator Load()
    {
        byte[] abBtyes = LoadFileMgr.Instance.GetBuffer(LoadFileMgr.Instance.LoacalFilePath + "/" + abPath);
        bundleRequest = AssetBundle.LoadFromMemoryAsync(abBtyes);       
        asyncProgess = bundleRequest.progress;
        yield return bundleRequest;
        bundle = bundleRequest.assetBundle;
        if(OnLoadABCompleted != null)
        {
            OnLoadABCompleted(bundle.LoadAsset<GameObject>(assetName));
            Destroy(gameObject);
        }
    }

    private void OnDestory()
    {       
        if(bundle != null)
        {
            bundle.Unload(false);
        }
        abPath = null;
        assetName = null;
    }
}
