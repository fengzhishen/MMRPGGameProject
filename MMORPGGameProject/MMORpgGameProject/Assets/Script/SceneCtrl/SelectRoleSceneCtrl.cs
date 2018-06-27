using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class SelectRoleSceneCtrl : MonoBehaviour
{
    /// <summary>
    /// 游戏职业镜像对象
    /// </summary>
    private IDictionary<int, GameObject> m_JobObjectDic = new Dictionary<int, GameObject>();

    //职业容器
    public List<Transform> m_roleContainer;

    //拖拽的目标
    public Transform m_dragTarget;

    //每次旋转的角度
    private float m_rotateAngle = 90;

    //目标角度
    private float m_targetAngle = 0;

    //是否在旋转中
    private bool m_IsRotate = false;

    //旋转速度
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
    /// 当点击选人场景中的职业UI回调处理
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
    /// 拖拽选人视图
    /// </summary>
    /// <param name="obj">0=左选择 1=向右选择</param>
    /// <param name="m_IsRotating">标识是否在拖拽中</param>
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
                }
            }
        }
        //监听服务器返回登录信息事件
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);

        //注册服务器返回创建角色事件
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturnEventHandler);
       
        //注册开始游戏按钮点击事件
        m_uiSceneSelectRoleView.OnBtnBeginGameClick = OnBtnBeginGameClickEventHanlder;

        LogOnGameServer();

        //加载游戏职业角色
        LoadJobObject();
    }

    public void Destory()
    {
        //移除对服务器返回登录信息事件的监听
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);

        //取消服务器返回创建角色事件监听
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturnEventHandler);

        //移除开始游戏按钮点击事件监听
        m_uiSceneSelectRoleView.OnBtnBeginGameClick -= OnBtnBeginGameClickEventHanlder;
    }
    /// <summary>
    /// 服务器对客户端创建角色的事件响应
    /// </summary>
    /// <param name="p"></param>
    private void OnCreateRoleReturnEventHandler(byte[] p)
    {
        RoleOperation_CreateRoleReturnProto proto = RoleOperation_CreateRoleReturnProto.GetProto(p);
        Debug.Log(proto.IsSuccess);
        if(proto.IsSuccess)
        {
            AppDebug.Log("创建成功");
        }
        else
        {
            UIMessageCtr.Instance.Show("提示", "创建角色失败");
        }
    }

    /// <summary>
    /// 点击开始游戏按钮事件处理
    /// </summary>
    private void OnBtnBeginGameClickEventHanlder()
    {
        RoleOperation_CreateRoleProto proto = new RoleOperation_CreateRoleProto();

        proto.JobId = (byte)m_currentJobId;
        proto.RoleNickName = m_uiSceneSelectRoleView.RoleNameInputField.text;

        //角色昵称合法性检查
        if(string.IsNullOrEmpty(proto.RoleNickName))
        {
            UIMessageCtr.Instance.Show("提示", "请输入你的昵称");
            return;
        }

        //把创建的角色信息发送到服务端
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
    }

    /// <summary>
    /// 登录服务器
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
    /// 从本地加载游戏职业镜像并且克隆
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
    /// 当我们登录游戏区服 服务器给我们返回的角色需要的数据信息
    /// </summary>
    /// <param name="p"></param>
    private void OnLogOnGameServerReturn(byte[] p)
    {
        RoleOperation_LogOnGameServerReturnProto proto = RoleOperation_LogOnGameServerReturnProto.GetProto(p);

        int roleCount = proto.RoleCount;
        AppDebug.Log("roleCount =" + roleCount);

        //玩家还没创建过角色
        if(roleCount == 0)
        {
            //要新建角色  弹新建角色界面

            //初始化的时候 职业id为1
            m_currentJobId = 1;

            //游戏一启动就随机一个名字
            m_uiSceneSelectRoleView.RandomRoleName();
        }
        else
        {

        }
    }
}
