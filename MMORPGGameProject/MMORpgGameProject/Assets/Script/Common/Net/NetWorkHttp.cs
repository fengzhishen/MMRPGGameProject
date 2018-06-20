using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class NetWorkHttp : SingletonMMO<NetWorkHttp>
{
    public class CallBackArgs:EventArgs
    {
        public bool HasError = false;

        public string ErrorMsg = null;

        public string Value = null;
    }

    private Action<CallBackArgs> m_getCallBack;

    private Action<CallBackArgs> m_postCallBack;

    private CallBackArgs m_callbackArgs;

    private bool  m_IsBusy = false;

    public bool IsBusy
    {
        get
        {
            return m_IsBusy;
        }
        set { m_IsBusy = value; }
    }  

    public void SendData(string url,bool isPost,IDictionary<string,object> dic, Action<CallBackArgs> callback)
    {
        if (IsBusy) return;
        IsBusy = true;
        string json = string.Empty;

        if (isPost)
        {
            m_postCallBack = callback;
        }
        else
        {
            m_getCallBack = callback;
        }

        if (isPost)
        {
            //web加密
            if(dic != null)
            {
                //客户端id 
                dic["cdId"] = DeviceUtity.ClientDeviceID;

                //签名
                dic["sign"] = EncryptUtil.MD5(string.Format("{0}:{1}", DeviceUtity.DeviceDentifier,GlobalInit.Instance.CurrServerTime));

                //时间戳
                dic["t"] = GlobalInit.Instance.CurrServerTime;

                dic["deviceIdentifier"] = DeviceUtity.DeviceDentifier;

                dic["deviceModel"] = DeviceUtity.DeviceModel;

                json = dic == null ? "" : LitJson.JsonMapper.ToJson(dic);
            }
            PostUrl(url, json);
        }
        else
        {
            GetUrl(url);
        }
    }
    #region GetUrl Get请求

    private void GetUrl(string url)
    {
        WWW www = new WWW(url);
        StartCoroutine(Get(www));
    }

    //函数定义
    private IEnumerator Get(WWW data)
    {
        yield return data; //在当前帧停止执行当前代码转去执行调用他的主线程
        IsBusy = false;
        if (!string.IsNullOrEmpty(data.error))
        {
            Debug.Log(data.error);
        }
        else
        {
            if(data.text == "null")
            {
                m_callbackArgs = new CallBackArgs
                {
                    ErrorMsg = "没有得到id对应用户信息",
                    HasError = true,
                };

                if (m_getCallBack != null)
                {
                    m_getCallBack(m_callbackArgs);
                }
            }
            else if(m_getCallBack != null)
            {
                m_callbackArgs = new CallBackArgs
                {
                    ErrorMsg = "",
                    HasError = false,
                    Value = data.text
                };

                m_getCallBack(m_callbackArgs);
            }
        }
    }

    #endregion

    #region PostUrl Post请求

    private void PostUrl(string url,string json)
    {
        WWWForm form = new WWWForm();

        form.AddField("", json);

        WWW data = new WWW(url, form);

        StartCoroutine(Request(data));
    }

    #endregion

    //函数定义
    private IEnumerator Request(WWW data)
    {
        yield return data; //在当前帧停止执行当前代码转去执行调用他的主线程
        IsBusy = false;
        if (!string.IsNullOrEmpty(data.error))
        {
            if (m_postCallBack != null)
            {
                CallBackArgs args = new CallBackArgs
                {
                    HasError = true,
                    Value = null,
                    ErrorMsg = data.error
                };            
                m_postCallBack(args);
            }
        }
        else
        {
      
           if(m_postCallBack != null)
            {
                CallBackArgs args = new CallBackArgs
                {
                    HasError = false,
                    ErrorMsg = null,
                    Value = data.text,
                };
                m_postCallBack(args);
            }
        }
    }
}
