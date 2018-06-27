using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class SelectRoleSceneCtrl : MonoBehaviour
{
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

    [SerializeField]
    private UISceneSelectRoleView m_uiSceneSelectRoleView;

    private List<JobEntity> m_jobEntityList;

    [SerializeField]
    private float m_doMoveX = 20;
    [SerializeField]
    private float m_duration = 0.5f;

    private int m_currentJobId = int.MinValue;

    private void Awake()
    {
        m_uiSceneSelectRoleView = UISceneCtr.Instance.LoadSceneUI(UISceneCtr.SceneUIType.SelectRole).GetComponent<UISceneSelectRoleView>();    
    }

    /// <summary>
    /// �����ѡ�˳����е�ְҵUI�ص�����
    /// </summary>
    /// <param name="jobId"></param>
    /// <param name="rotateAngle"></param>
    private void OnSelectJobCallback(int jobId, int rotateAngle, UISelectRoleJobItemView jobItemView)
    {
        m_currentJobId = jobId;
        m_IsRotate = true;
        m_targetAngle = rotateAngle;

        JobEntity job = m_jobEntityList.Find((JobEntity jobEntity) => { return jobEntity.Id == jobId; });
        m_uiSceneSelectRoleView.m_uISelectRoleJobDescView.SetUI(job.Name, job.Desc);

        Tweener tweener = jobItemView.transform.DOMoveX(jobItemView.transform.position.x + m_doMoveX, m_duration).SetAutoKill<Tweener>(false).SetDelay<Tweener>(0)
            .Pause<Tweener>().SetEase<Tweener>(Ease.InOutBack);
        jobItemView.transform.DOPlayForward();
        tweener.OnComplete<Tweener>(() => { jobItemView.transform.DOPlayBackwards()/*jobItemView.transform.DOMoveX(jobItemView.transform.position.x - m_doMoveX, m_duration)*/; });
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
    }

    void Update()
    {
        if(Mathf.Abs(m_dragTarget.eulerAngles.y % 360 - m_targetAngle) < 0.1)
        {
            m_IsRotate = false;
        }
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
        m_uiSceneSelectRoleView.m_UISelectRoleDragView.m_OnSelectRoleDrag = OnSelectRoleDragCallback;

        if (m_uiSceneSelectRoleView.m_uiSelectRoleJobItemViewList != null)
        {
            if (m_uiSceneSelectRoleView.m_uiSelectRoleJobItemViewList.Length > 0)
            {
                for (int i = 0; i < m_uiSceneSelectRoleView.m_uiSelectRoleJobItemViewList.Length; i++)
                {
                    m_uiSceneSelectRoleView.m_uiSelectRoleJobItemViewList[i].OnSelectJob = OnSelectJobCallback;
                    Debug.Log("i=" + i);
                }
            }
        }
        //�������������ص�¼��Ϣ�¼�
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);

        //ע����������ش�����ɫ�¼�
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturnEventHandler);
       
        //ע�Ὺʼ��Ϸ��ť����¼�
        m_uiSceneSelectRoleView.OnBtnBeginGameClick = OnBtnBeginGameClickEventHanlder;

        LogOnGameServer();
        //������Ϸְҵ��ɫ
        LoadJobObject();
    }

    public void Destory()
    {
        //�Ƴ��Է��������ص�¼��Ϣ�¼��ļ���
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);

        //ȡ�����������ش�����ɫ�¼�����
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturnEventHandler);

        //�Ƴ���ʼ��Ϸ��ť����¼�����
        m_uiSceneSelectRoleView.OnBtnBeginGameClick -= OnBtnBeginGameClickEventHanlder;
    }
    /// <summary>
    /// �������Կͻ��˴�����ɫ���¼���Ӧ
    /// </summary>
    /// <param name="p"></param>
    private void OnCreateRoleReturnEventHandler(byte[] p)
    {
        RoleOperation_CreateRoleReturnProto proto = RoleOperation_CreateRoleReturnProto.GetProto(p);

        if(proto.IsSuccess)
        {
            AppDebug.Log("�����ɹ�");
        }
        else
        {
            UIMessageCtr.Instance.Show("��ʾ", "������ɫʧ��");
        }
    }

    /// <summary>
    /// �����ʼ��Ϸ��ť�¼�����
    /// </summary>
    private void OnBtnBeginGameClickEventHanlder()
    {
        RoleOperation_CreateRoleProto proto = new RoleOperation_CreateRoleProto();

        proto.JobId = (byte)m_currentJobId;
        proto.RoleNickName = m_uiSceneSelectRoleView.RoleNameInputField.text;

        //��ɫ�ǳƺϷ��Լ��
        if(string.IsNullOrEmpty(proto.RoleNickName))
        {
            UIMessageCtr.Instance.Show("��ʾ", "����������ǳ�");
            return;
        }

        //�Ѵ����Ľ�ɫ��Ϣ���͵������
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
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
        m_jobEntityList = JobDBModel.GetInstance.GetDataList;

        for (int i = 0; i < m_jobEntityList.Count; i++)
        {
            GameObject @object = AssetBundleMgr.Instance.LoadClone(string.Format("/Role/{0}.assetbundle", m_jobEntityList[i].PrefabName), m_jobEntityList[i].PrefabName);

            @object.transform.SetParent(m_roleContainer[i]);
            @object.transform.localScale = Vector3.one;
            @object.transform.localPosition = Vector3.zero;
            @object.transform.localRotation = Quaternion.identity;

            if (@object != null)
            {
                m_JobObjectDic.Add(m_jobEntityList[i].Id, @object);
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

            //��ʼ����ʱ�� ְҵidΪ1
            m_currentJobId = 1;

            //��Ϸһ���������һ������
            m_uiSceneSelectRoleView.RandomRoleName();
        }
        else
        {

        }
    }
}
