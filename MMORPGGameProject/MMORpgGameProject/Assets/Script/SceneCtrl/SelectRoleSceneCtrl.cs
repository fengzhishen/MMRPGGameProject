using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelectRoleSceneCtrl : MonoBehaviour
{
    /// <summary>
    /// ��Ϸְҵ�������
    /// </summary>
    private IDictionary<int, GameObject> m_JobObjectDic = new Dictionary<int, GameObject>();

    //ְҵ����
    public List<Transform> m_roleContainer;

    private void Awake()
    {
        UISceneCtr.Instance.LoadSceneUI(UISceneCtr.SceneUIType.SelectRole);
    }

	void Start ()
    {
	   if(DelegateDefine.Instance.OnSceneLoadOk != null)
        {
            DelegateDefine.Instance.OnSceneLoadOk();
        }

        //����Э��
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);
        LogOnGameServer();
        //������Ϸְҵ��ɫ
        LoadJobObject();
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
    /// �ӱ��ؼ�����Ϸְҵ�����ҿ�¡
    /// </summary>
    private void LoadJobObject()
    {
        List<JobEntity> jobs = JobDBModel.GetInstance.GetDataList;

        for (int i = 0; i < jobs.Count; i++)
        {
            GameObject @object = AssetBundleMgr.Instance.LoadClone(string.Format("/Role/{0}.assetbundle",jobs[i].PrefabName), jobs[i].PrefabName);

            @object.transform.SetParent(m_roleContainer[i]);
            @object.transform.localScale = Vector3.one;
            @object.transform.localPosition = Vector3.zero;
            @object.transform.localRotation = Quaternion.identity;

            if (@object != null)
            {
                m_JobObjectDic.Add(jobs[i].Id, @object);
            }
        }
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
