using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// 区服控制器
/// </summary>
public class GameServerCtrl : SystemBaseCtr<GameServerCtrl>
{
    private UIGameServerEnterView m_gameServerEnterView;
    private UIGameServerSelectView m_uIGameServerSelectView;

    public GameServerCtrl()
    {
        //添加button点击事件
        AddEventListener(ConstDefine.UIGameServerEnterView_btnEnterGame, GameServerEnterViewBtnEnterGameClick);
        AddEventListener(ConstDefine.UIGameServerEnterView_btnSelectGameServer, GameServerSelectViewBtnSelectGameClick);

    }

    /// <summary>
    /// 选择区服方法
    /// </summary>
    /// <param name="p"></param>
    private void GameServerSelectViewBtnSelectGameClick(object[] p)
    {
        //1.打开选择区服的视图
        m_uIGameServerSelectView = WindowUIMgr.Instance.OpenWindow(WindowUIType.GameServerSelect).GetComponent<UIGameServerSelectView>();

        m_uIGameServerSelectView.m_OnGameServerPageClickCallback = GetGameServer;

        //2.打开选服视图的时候 从服务器拉去页签和区服数据    
        GetGameServerPage();
    }

    /// <summary>
    /// 获取页签
    /// </summary>
    private void GetGameServerPage()
    {
        //获取页签
        Dictionary<string, object> dic = new Dictionary<string, object>();

        dic["Type"] = 0;   //type = 0 获取页签   1 是服务器列表  

        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", true, dic, OnGetGameServerPageCallback);
    }

    /// <summary>
    /// 获取区服
    /// </summary>
    private void GetGameServer(int pageIndex)
    {
        Debug.Log(11);
        //获取页签
        Dictionary<string, object> dic = new Dictionary<string, object>();

        dic["Type"] = 1;   //type = 0 获取页签   1 是服务器列表  
        dic["PageIndex"] = pageIndex;
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/GameServer", true, dic, OnGetGameServerCallback);
    }

    private void OnGetGameServerCallback(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            LogError(obj.ErrorMsg);
        }
        else
        {
            List<RetGameServerEntity> retGameServerPageEntities = LitJson.JsonMapper.ToObject<List<RetGameServerEntity>>(obj.Value);

            if (retGameServerPageEntities != null)
            {
                m_uIGameServerSelectView.SetGameServerUI(retGameServerPageEntities, OnClickGameServerItemCallback);
            }
        }
    }

    /// <summary>
    /// 当点击选择服务器视图中的服务器item回调处理
    /// </summary>
    /// <param name="serverName"></param>
    private void OnClickGameServerItemCallback(RetGameServerEntity gameServerEntity)
    {
        m_gameServerEnterView.SetUI(gameServerEntity);

        //保存当前选择的区服信息
        GlobalInit.Instance.m_currentSelectGameServer = gameServerEntity;
     
        //关闭当前选择服务器视图
        WindowUIMgr.Instance.CloseWindow(WindowUIType.GameServerSelect);
    }

    /// <summary>
    /// 进入游戏
    /// </summary>
    /// <param name="p"></param>
    private void GameServerEnterViewBtnEnterGameClick(object[] p)
    {
       
    }

    /// <summary>
    /// 回调获取区服页签
    /// </summary>
    /// <param name="obj"></param>
    private void OnGetGameServerPageCallback(NetWorkHttp.CallBackArgs obj)
    {
        if (obj.HasError)
        {
            LogError(obj.ErrorMsg);
        }
        else
        {
            List<RetGameServerPageEntity> retGameServerPageEntities = LitJson.JsonMapper.ToObject<List<RetGameServerPageEntity>>(obj.Value);
            retGameServerPageEntities.Insert(0, new RetGameServerPageEntity { Name = "推荐服务器",PageIndex = 0});

            if (retGameServerPageEntities != null)
            {          
                m_uIGameServerSelectView.SetGameServerPageUI(retGameServerPageEntities);
            }

            GetGameServer(0);
        }
     }

    public new void OpenView(WindowUIType type)
    {
        switch (type)
        {
            case WindowUIType.GameServerEnter:
                OpenGameServerEnterView();
                break;
            case WindowUIType.GameServerSelect:
                OpenGameServerSelectView();
                    break;
            default:
                break;
        }
    }

    /// <summary>
    /// 打开进去游戏服的窗体
    /// </summary>
    private  void OpenGameServerEnterView()
    {
        // base.OpenView(WindowUIType.GameServerEnter);
        m_gameServerEnterView = WindowUIMgr.Instance.OpenWindow(WindowUIType.GameServerEnter).GetComponent<UIGameServerEnterView>();
    }

    /// <summary>
    /// 打开游戏服选择视图
    /// </summary>
    private void OpenGameServerSelectView()
    {

    }

    /// <summary>
    /// 释放
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();

        //移除button点击事件
        RemoveEventListener(ConstDefine.UIGameServerEnterView_btnSelectGameServer, GameServerEnterViewBtnEnterGameClick);
        RemoveEventListener(ConstDefine.UIGameServerEnterView_btnSelectGameServer, GameServerSelectViewBtnSelectGameClick);
    }
}
