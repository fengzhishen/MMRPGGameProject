using UnityEngine;
using System.Collections;
using System;
using LitJson;

public class TestMemoryStrean : MonoBehaviour
{

    void Start()
    {
        TestProto testProto = new TestProto()
        {
            Id = 1,
            Name = "我",
            Type = 10,
            Price = 20.1
        };

        byte[] buffer = testProto.ToArray();

        TestProto testProto1 = new TestProto();
        JsonData json = new JsonData();
        json["Type"] = 0; //0注册 1登录
        json["UserName"] = "";
        json["Pwd"] = "";

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

        AccountEntity entity = new AccountEntity();

        entity = LitJson.JsonMapper.ToObject<AccountEntity>(args.Value);

        string format = "id:{0}, UserName:{1}, YuanBao:{2}";
        Debug.LogFormat(format, entity.Id, entity.UserName, entity.YuanBao);
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

