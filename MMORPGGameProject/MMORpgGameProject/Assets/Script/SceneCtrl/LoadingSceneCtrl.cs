//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-05 11:13:45
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LoadingSceneCtrl : MonoBehaviour 
{
    /// <summary>
    /// UI场景控制器
    /// </summary>
    [SerializeField]
    private UISceneLoadingCtrl m_UILoadingCtrl;

    private AsyncOperation m_Async = null;

    /// <summary>
    /// 当前的进度
    /// </summary>
    private int m_CurrProgress = 0;

	void Start ()
	{
        LayerUIMgr.Instance.Reset();
        StartCoroutine(LoadingScene());
        DelegateDefine.Instance.OnSceneLoadOk = OnSecneLoadOkCallback;
    }

    private void OnSecneLoadOkCallback()
    {
        if(m_UILoadingCtrl != null)
        {
            Destroy(m_UILoadingCtrl.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void OnDestory()
    {
        DelegateDefine.Instance.OnSceneLoadOk -= OnSecneLoadOkCallback;
    }
    private IEnumerator LoadingScene()
    {
        string strSceneName = string.Empty;
        switch (SceneMgr.Instance.CurrentSceneType)
        {
            case SceneType.LogOn:
                strSceneName = "Scene_LogOn";
                break;
            case SceneType.SelectRole:
                strSceneName = "Scene_SelectRole";
                break;
            case SceneType.WorldMap:

                //现在需要根据当前服务器返回的最后登录的世界地图id拿到地图相关实体数据
                WorldMapEntity worldMapEntity = WorldMapDBModel.GetInstance.GetEntityById(SceneMgr.Instance.CurrWorldMapId);

                if(worldMapEntity != null)
                {
                    //现在我们从地图实体类中找到我们地图编号对应的场景名字
                    strSceneName = worldMapEntity.SceneName;
                }
                else
                {
                    AppDebug.Log(GetType() + "/LoadingScene()/当前世界地图id没有得到对应的实体数据,请检查。id = " + SceneMgr.Instance.CurrWorldMapId);
                    strSceneName = string.Empty;
                }
                break;
        }

        //说明有错误需要调整
        if(strSceneName == string.Empty)
        {
            //直接退出携程
            yield break;
        }

        if(SceneMgr.Instance.CurrentSceneType == SceneType.SelectRole || SceneMgr.Instance.CurrentSceneType == SceneType.WorldMap)
        {
            AssetBundleMgr.Instance.LoadAssetAsync(string.Format("Scene/{0}.unity3d", strSceneName), strSceneName, OnLoadABCompleted: (UnityEngine.GameObject obj) =>
                {
                    m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
                    m_Async.allowSceneActivation = false;
                });
        }
        else
        {
            m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
            m_Async.allowSceneActivation = false;
            yield return m_Async;
        }
    
    }

	void Update ()
	{
        if (m_Async == null) return;
        int toProgress = 0;

        if (m_Async.progress < 0.9f)
        {
            toProgress = Mathf.Clamp((int)m_Async.progress * 100, 1, 100);
        }
        else
        {
            toProgress = 100;
        }

        if (m_CurrProgress < toProgress)
        {
            m_CurrProgress++;
        }
        else
        {          
            m_Async.allowSceneActivation = true;

        }

        m_UILoadingCtrl.SetProgressValue(m_CurrProgress * 0.01f);
       
    }
}