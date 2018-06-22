using UnityEngine;
using System.Collections;
using System;

public class SelectRoleSceneCtrl : MonoBehaviour
{

	void Start ()
    {
	   if(DelegateDefine.Instance.OnSceneLoadOk != null)
        {
            DelegateDefine.Instance.OnSceneLoadOk();
        }

        //����Э��
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);
        LogOnGameServer();
    }

    /// <summary>
    /// ��¼������
    /// </summary>
    private void LogOnGameServer()
    {
        RoleOperation_LogOnGameServerProto proto = new RoleOperation_LogOnGameServerProto();
        {
            proto.AccountId = GlobalInit.Instance.m_currentAccountEntity.Id;
        }

        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    /// <summary>
    /// �����ǵ�¼��Ϸ���� �����������Ƿ��صĽ�ɫ��Ҫ��������Ϣ
    /// </summary>
    /// <param name="p"></param>
    private void OnLogOnGameServerReturn(byte[] p)
    {
        RoleOperation_LogOnGameServerReturnProto proto = RoleOperation_LogOnGameServerReturnProto.GetProto(p);

        int roleCount = proto.RoleCount;
        AppDebug.Log("roleCount =" + roleCount);

        //��һ�û��������ɫ
        if(roleCount == 0)
        {
            //Ҫ�½���ɫ  ���½���ɫ����
        }
        else
        {

        }
    }
}
