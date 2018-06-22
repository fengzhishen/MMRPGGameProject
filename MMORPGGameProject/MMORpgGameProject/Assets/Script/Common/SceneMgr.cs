using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理器
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 当前场景类型
    /// </summary>
    public SceneType CurrentSceneType
    {
        get;
        private set;
    }

    public void LoadToLogOn()
    {
        CurrentSceneType = SceneType.LogOn;
        SceneManager.LoadScene("Scene_Loading");
    }

    /// <summary>
    /// 去城镇场景
    /// </summary>
    public void LoadToCity()
    {
        CurrentSceneType = SceneType.City;
        SceneManager.LoadScene("Scene/SceneMap/Scene_Loading");
    }

    /// <summary>
    /// 去角色选择场景
    /// </summary>
    public void LoadToSelectRole()
    {
        CurrentSceneType = SceneType.SelectRole;

        SceneManager.LoadScene("Scene/SceneMap/Scene_Loading");
    }
}