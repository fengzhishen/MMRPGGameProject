using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
using Mmcoy.Framework;
/// <summary>
/// 负责控制登录注册面板视图
/// </summary>
public class AccountCtr : SystemBaseCtr<AccountCtr>
{
    private UILogOnView logOnView;
    private UIResiterView resiterView;

    //是否自动登录
    private bool m_isAutoLogOn = false;

    public AccountCtr()
    {
        this.AddEventListener(ConstDefine.UILogOnView_Logon, OnUILogOnViewLogonButtonClickCallback);
        this.AddEventListener(ConstDefine.UILogOnView_Register, OnUILogOnViewRegisterButtonClickCallback);
        this.AddEventListener(ConstDefine.UIResiterView_Logon, ReViewbtnReturnClick);
        this.AddEventListener(ConstDefine.UIResiterView_ReturnRegister, ReViewBtnClick);
    }

    /// <summary>
    /// 当注册面板返回注册button被点击后的回调
    /// </summary>
    /// <param name="param"></param>
    private void ReViewBtnClick(object[] param)
    {
        Debug.Log(param[0]);
        WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
    }

    /// <summary>
    /// 当点击注册面板的注册按键之后的回调处理
    /// </summary>
    /// <param name="param"></param>
    private void ReViewbtnReturnClick(object[] param)
    {
        Log("注册点击");
        if(string.IsNullOrEmpty(resiterView.txt_AccountInpField.text))
        {
            this.ShowMessage("注册提示", "用户名不能为空",okAction:()=> { AppDebug.Log("点击了注册按钮的回调"); });
            return;
        }

        if (string.IsNullOrEmpty(resiterView.txt_PwdInpField.text))
        {
            this.ShowMessage("注册提示", "注册密码不能为空");
            return;
        }

        // WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn);
        IDictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 0;
        dic["UserName"] = resiterView.txt_AccountInpField.text;
        dic["Pwd"] = resiterView.txt_PwdInpField.text;
        dic["ChannelId"] = 0;
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", true, dic, OnRegCallback);
    }

    /// <summary>
    /// 当注册完毕后的回调
    /// </summary>
    /// <param name="obj"></param>
    private void OnRegCallback(NetWorkHttp.CallBackArgs obj)
    {
        if(obj.HasError)
        {
            LogError(obj.ErrorMsg);
        }
        else
        {
            ReturnValue returnValue = LitJson.JsonMapper.ToObject<ReturnValue>(obj.Value);
            RetAccountEntity retAccountEntity = LitJson.JsonMapper.ToObject<RetAccountEntity>(returnValue.Value.ToString());
            GlobalInit.Instance.m_currentAccountEntity = retAccountEntity;
            Log("注册成功" + retAccountEntity.LastServerName);
            Stat.Reg(retAccountEntity.Id, resiterView.txt_AccountInpField.text);

            //本地缓存玩家的帐号和密码
            PlayerPrefs.SetInt(ConstDefine.LogOn_AccountID, retAccountEntity.Id);

            PlayerPrefs.SetString(ConstDefine.LogOn_AccountID, resiterView.txt_AccountInpField.text);

            PlayerPrefs.SetString(ConstDefine.LogOn_AccountID, resiterView.txt_PwdInpField.text);


            WindowUIMgr.Instance.CloseWindow(WindowUIType.Reg);

            GameServerCtrl.Instance.OpenView(WindowUIType.GameServerEnter);
        }
    }

    /// <summary>
    /// 点击登录面板的注册按钮的回调处理
    /// </summary>
    /// <param name="param"></param>
    private void OnUILogOnViewRegisterButtonClickCallback(object[] param)
    {
        Log("登录按钮点击");
        WindowUIMgr.Instance.CloseWindow(WindowUIType.LogOn);
        resiterView = WindowUIMgr.Instance.OpenWindow(WindowUIType.Reg).GetComponent<UIResiterView>();
    }

    /// <summary>
    /// 当点击登录面板的登录按钮的回调处理
    /// </summary>
    /// <param name="param"></param>
    private void OnUILogOnViewLogonButtonClickCallback(object[] param)
    {
        Log(param[0]);
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["Type"] = 1;   //0注册 1登录
        dic["UserName"] = logOnView.txt_AccountInpField.text;
        dic["Pwd"] = logOnView.txt_PwdInpField.text;
        NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", true, dic, OnLogOnCallback);

        m_isAutoLogOn = false;  //点击登录按钮后表示是手动登录      
    }

    /// <summary>
    /// 设置已经选择的服务器信息
    /// </summary>
    /// <param name="entity"></param>
    private void SetCurrentSelectGameServer(RetAccountEntity entity)
    {
        RetGameServerEntity serverEntity = new RetGameServerEntity
        {
            Id = entity.Id,
            Name = entity.LastServerName,
            Ip = entity.LastServerIP,
            Port = entity.LastServerPort
        };

        GlobalInit.Instance.m_currentSelectGameServer = serverEntity;
    }

    /// <summary>
    /// 点击登录之后服务器对客户端的回调处理
    /// </summary>
    /// <param name="obj"></param>
    private void OnLogOnCallback(NetWorkHttp.CallBackArgs obj)
    {
        if(obj.HasError)
        {
            Debug.Log(obj.ErrorMsg);
        }
        else
        {
            ReturnValue @return = LitJson.JsonMapper.ToObject<ReturnValue>(obj.Value);
            if(@return.HasError)
            {
                ShowMessage("温馨提示", "你输入的帐号不存在");
                Debug.Log(@return.ErroMsg);
            }
            else
            {
                RetAccountEntity retAccountEntity = LitJson.JsonMapper.ToObject<RetAccountEntity>(@return.Value.ToString());

                GlobalInit.Instance.m_currentAccountEntity = retAccountEntity;

                SetCurrentSelectGameServer(retAccountEntity);

                string userName = "";
                if(m_isAutoLogOn)
                {
                    userName = PlayerPrefs.GetString(ConstDefine.LogOn_AccountUserName);
                    Stat.LogOn(PlayerPrefs.GetInt(ConstDefine.LogOn_AccountID),PlayerPrefs.GetString(ConstDefine.LogOn_AccountUserName));
                }
                else
                {
                    //保存一下帐号密码在本地
                    PlayerPrefs.SetInt(ConstDefine.LogOn_AccountID, retAccountEntity.Id);
                    PlayerPrefs.SetString(ConstDefine.LogOn_AccountUserName, logOnView.txt_AccountInpField.text);
                    PlayerPrefs.SetString(ConstDefine.LogOn_AccountPwd, logOnView.txt_PwdInpField.text);
                    Stat.LogOn(retAccountEntity.Id, logOnView.txt_AccountInpField.text);
                }

                 GameServerCtrl.Instance.OpenView(WindowUIType.GameServerEnter);
                 GameServerCtrl.Instance.GameServerEnterView.SetUI(GlobalInit.Instance.m_currentSelectGameServer);
            }
        }
    }

    public void OpenLogOnView()
    {
        logOnView = WindowUIMgr.Instance.OpenWindow(WindowUIType.LogOn).GetComponent<UILogOnView>();
    }

    /// <summary>
    /// 快速登录
    /// </summary>
    public void QuickLogOn()
    {
        //1.先判断本地帐号
        //2.本地没有帐号就进入注册view
        //3.如果有就自动登录   登录成功进入游戏view

        if(!PlayerPrefs.HasKey(ConstDefine.LogOn_AccountID))
        {
            //进入注册视图 进行注册操作
            this.OpenView(WindowUIType.Reg);
        }
        else //自动登录
        {
            m_isAutoLogOn = true; //设置自动登录标识

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic["Type"] = 1; //登录
            dic["UserName"] = PlayerPrefs.GetString(ConstDefine.LogOn_AccountUserName);
            dic["Pwd"] = PlayerPrefs.GetString(ConstDefine.LogOn_AccountPwd);

            //开始发送请求
            NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/account", true, dic, OnLogOnCallback);
        }
    }

    public override void Dispose()
    {
        base.Dispose();

        RemoveEventListener(ConstDefine.UILogOnView_Logon, OnUILogOnViewLogonButtonClickCallback);
        RemoveEventListener(ConstDefine.UILogOnView_Register, OnUILogOnViewRegisterButtonClickCallback);
        RemoveEventListener(ConstDefine.UIResiterView_Logon, ReViewbtnReturnClick);
        RemoveEventListener(ConstDefine.UIResiterView_ReturnRegister, ReViewBtnClick);
    }
}
