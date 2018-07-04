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
    /// 当前世界地图id
    /// </summary>
    private int m_currWorldMapId = -1;

    /// <summary>
    /// 当前所在的世界地图编号
    /// </summary>
    public int CurrWorldMapId
    {
        get { return m_currWorldMapId; }
    }
    /// <summary>
    /// 去世界地图场景(主城 + 野外场景)
    /// </summary>
    public void LoadToWorldMap(int worldMapId)
    {
        this.m_currWorldMapId = worldMapId;

        CurrentSceneType = SceneType.WorldMap;

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