//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-15 23:07:03
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RoleMgr:Singleton<RoleMgr> 
{
    /// <summary>
    /// 主角是否已经初始化
    /// </summary>
    private bool m_IsMainPlayerInit = false;

    /// <summary>
    /// 初始化主角
    /// </summary>
    public void InitMainPlayer(Transform m_PlayerBornPos)
    {
        if (m_IsMainPlayerInit == true) return;

        if (GlobalInit.Instance.MainPlayerInfo == null) return;

        GameObject MainPlayerObj = Object.Instantiate<GameObject>(GlobalInit.Instance.m_JobObjectDic[GlobalInit.Instance.MainPlayerInfo.JobId]);

        MainPlayerObj.transform.SetParent(m_PlayerBornPos);

        GlobalInit.Instance.CurrPlayer = MainPlayerObj.GetComponent<RoleCtrl>();

        GlobalInit.Instance.CurrPlayer.Init(RoleType.MainPlayer, GlobalInit.Instance.MainPlayerInfo, new RoleMainPlayerCityAI(GlobalInit.Instance.CurrPlayer));

        m_IsMainPlayerInit = true;
    }

    /// <summary>
    /// 从安装包中加载角色头像
    /// </summary>
    /// <param name="headPic"></param>
    /// <returns></returns>
    public Sprite LoadRoleHead(string headPic)
    {
        return Resources.Load<Sprite>(string.Format("UI/HeadImg/{0}", headPic));
    }

    /// <summary>
    /// 根据职业id加载角色
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    public GameObject LoadPlayer(int jobId)
    {
       return Object.Instantiate(GlobalInit.Instance.m_JobObjectDic[jobId]);
    }

    #region LoadRole 根据角色预设名称 加载角色
    /// <summary>
    /// 根据角色预设名称 加载角色
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadRole(string name, RoleType type)
    {
        string path = string.Empty;

        switch (type)
        {
            case RoleType.MainPlayer:
                path = "Player";
                break;
            case RoleType.Monster:
                path = "Monster";
                break;
        }

        return ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Role, string.Format("{0}/{1}", path, name), cache: true);
    }
    #endregion

    /// <summary>
    /// load npc to ram
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public  GameObject LoadNPC(string prefabName)
    {
        GameObject @object = AssetBundleMgr.Instance.LoadAsset(string.Format("/Role/{0}.assetbundle", prefabName),prefabName);

        @object = UnityEngine.Object.Instantiate<GameObject>(@object);

        return @object;
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}