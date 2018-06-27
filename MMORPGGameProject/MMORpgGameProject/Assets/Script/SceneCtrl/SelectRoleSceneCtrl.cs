using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelectRoleSceneCtrl : MonoBehaviour
{
    private UISceneSelectRoleView m_UISceneSelectRoleView;
    /// <summary>
    /// ��Ϸְҵ�������
    /// </summary>
    private IDictionary<int, GameObject> m_JobObjectDic = new Dictionary<int, GameObject>();

    //ְҵ����
    public List<Transform> m_roleContainer;

    //��ק��Ŀ��
    public Transform m_dragTarget;

    //ÿ����ת�ĽǶ�
    private float m_rotateAngle = 90;

    //Ŀ��Ƕ�
    private float m_targetAngle = 0;

    //�Ƿ�����ת��
    private bool m_IsRotate = false;

    //��ת�ٶ�
    [SerializeField]
    private float m_rotateSpeed = 20;

    private void Awake()
    {
        m_UISceneSelectRoleView = UISceneCtr.Instance.LoadSceneUI(UISceneCtr.SceneUIType.SelectRole).GetComponent<UISceneSelectRoleView>();

        m_UISceneSelectRoleView.m_UISelectRoleDragView.m_OnSelectRoleDrag = OnSelectRoleDragCallback;
    }

    /// <summary>
    /// ��קѡ����ͼ
    /// </summary>
    /// <param name="obj">0=��ѡ�� 1=����ѡ��</param>
    /// <param name="m_IsRotating">��ʶ�Ƿ�����ק��</param>
    private void OnSelectRoleDragCallback(int obj,bool m_IsRotating)
    {
        m_IsRotate = m_IsRotating;
        m_rotateAngle = Mathf.Abs(m_rotateAngle) * (obj == 0 ? -1 : 1);
        m_targetAngle = m_dragTarget.eulerAngles.y + m_rotateAngle;
        Debug.Log(obj);
    }

    void Update()
    {
        if(m_IsRotate == true)
        {
            float toAnleY = Mathf.MoveTowardsAngle(m_dragTarget.eulerAngles.y, m_targetAngle, Time.deltaTime * m_rotateSpeed);
            m_dragTarget.eulerAngles = new Vector3(m_dragTarget.eulerAngles.x, toAnleY, m_dragTarget.eulerAngles.z);
        }
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
