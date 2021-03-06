//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-11-29 16:24:29
//备    注：场景UI管理器
//===================================================
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 场景UI管理器
/// </summary>
public class UISceneCtr : Singleton<UISceneCtr>
{

    /// <summary>
    /// 场景UI类型
    /// </summary>
    public enum SceneUIType
    {
        /// <summary>
        /// 登录
        /// </summary>
        LogOn,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        /// <summary>
        /// 主城
        /// </summary>
        MainCity,

        /// <summary>
        /// 选人
        /// </summary>
        SelectRole
    }

    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneViewBase CurrentUIScene;

    #region LoadSceneUI 加载场景UI
    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject LoadSceneUI(SceneUIType type,Action OnLoadComplete = null)
    {
        GameObject obj = null;
        switch (type)
        {
            case SceneUIType.LogOn:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UI_Root_Logon");
                CurrentUIScene = obj.GetComponent<UISceneViewBase>();
                break;
            case SceneUIType.Loading:
                break;
            case SceneUIType.SelectRole:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UI_Root_SelectRole");
                CurrentUIScene = obj.GetComponent<UISceneViewBase>();
                break;
            case SceneUIType.MainCity:
                obj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIScene, "UI_Root_MainCity");
                CurrentUIScene = obj.GetComponent<UISceneViewBase>();
                break;
        }

        CurrentUIScene.OnLoadComplete = OnLoadComplete;
        return obj;
    }
    #endregion
}