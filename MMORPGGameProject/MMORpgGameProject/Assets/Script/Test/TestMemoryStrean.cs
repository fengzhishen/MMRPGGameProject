using UnityEngine;
using System.Collections;
using System;
using LitJson;

public class TestMemoryStrean : MonoBehaviour
{

    void Start()
    {
        
       // NetWorkHttp.Instance.SendData(GlobalInit.WebAccountUrl + "api/Account", true, json.ToJson(), OnPostDataCallback);
      
        NetWorkSocket.Instance.Connect("192.168.1.102", 1011);

        //using (MMO_MemoryStream ms = new MMO_MemoryStream())
        //{
        //    ms.WriteUTF8String("我爱你");
        //    NetWorkSocket.Instance.SendMsg(ms.ToArray());

        //}     
    }

    private void OnGetDataCallback(NetWorkHttp.CallBackArgs args)
    {
        if(args.HasError)
        {
            Debug.Log(args.ErrorMsg);
            return;
        }
        Debug.Log(args.Value);

  
    }

    private void OnPostDataCallback(NetWorkHttp.CallBackArgs args)
    {
        if (args.HasError)
        {
            Debug.Log(args.ErrorMsg);
            return;
        }

        ReturnValue value = JsonMapper.ToObject<ReturnValue>(args.Value);
        Debug.Log(value.Value);
    }
}	

