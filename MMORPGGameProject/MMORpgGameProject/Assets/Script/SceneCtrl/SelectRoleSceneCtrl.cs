using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class SelectRoleSceneCtrl : MonoBehaviour
{  
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
    private float m_rotateSpeed = 50;

    [SerializeField]
    private UISceneSelectRoleView m_uiSceneSelectRoleView;

    private List<JobEntity> m_jobEntityList;

    [SerializeField]
    private float m_doMoveX = 20;
    [SerializeField]
    private float m_duration = 0.5f;

    private int m_currentJobId = int.MinValue;

    private Dictionary<int, RoleCtrl> m_jobRoleCtr = new Dictionary<int, RoleCtrl>();

    /// <summary>
    /// 选择角色预设挂在位置
    /// </summary>
    [SerializeField]
    private Transform m_SelectRoleContainer;

    /// <summary>
    /// 创建角色相关场景模型
    /// </summary>
    [SerializeField]
    private List<Transform> m_UICreateSceneMode;

    private void Awake()
    {
        m_uiSceneSelectRoleView = UISceneCtr.Instance.LoadSceneUI(UISceneCtr.SceneUIType.SelectRole).GetComponent<UISceneSelectRoleView>();

        m_uiSceneSelectRoleView.m_DeleteButton.onClick.AddListener(OnClickBtnCallback);

        m_uiSceneSelectRoleView.OnBtnReturnClick = OnBtnReturnClick;

        m_uiSceneSelectRoleView.OnBtnCreateRoleClick = OnBtnCreateRoleCallback;
    }

    /// <summary>
    /// 对新建角色按钮点击的响应
    /// </summary>
    private void OnBtnCreateRoleCallback()
    {
        m_bIsCreateRole = true;

        m_uiSceneSelectRoleView.SetUICreateRoleShow(true);
        DeleteSelectRole();

        //克隆角色
        CloneCreateRole();

        //显示创建角色场景模型
        this.SetCreateRoleSceneModelShow(true);

        //销毁已有角色界面职业信息预设
        m_uiSceneSelectRoleView.DestorySelectRoleItemInfo();
    }
  
    /// <summary>
    /// 点击返回按钮的响应
    /// </summary>
    private void OnBtnReturnClick()
    {
        //如果是新建角色界面 并且没有角色的话 我们就返回选区场景
        //是新建角色界面 有角色 就返回有角色界面
        //有角色界面 就返回选区场景
        if(m_bIsCreateRole)
        {
            if (m_roleList == null || m_roleList.Count == 0 )
            {
                NetWorkSocket.Instance.DisConnect();
                SceneMgr.Instance.LoadToLogOn();
            }
            else
            {
                //切换到已有角色界面
                m_bIsCreateRole = false;
                this.SetCreateRoleSceneModelShow(false);
                m_uiSceneSelectRoleView.SetUICreateRoleShow(false);
                DeleteCreateRoleClone();
                m_targetAngle = 0;
                //选择已有角色
                m_uiSceneSelectRoleView.SetRoleList(m_roleList, OnSelectRoleCallback);
                SetSelectRole(m_roleList[0].RoleId);
            }
        }
        else
        {

            SceneMgr.Instance.LoadToLogOn();
        }
    }

    /// <summary>
    /// 设置创建角色场景模型显示和隐藏
    /// </summary>
    /// <param name="iShow"></param>
    private void SetCreateRoleSceneModelShow(bool iShow = true)
    {
        if(m_UICreateSceneMode != null && m_UICreateSceneMode.Count > 0)
        {
            foreach (Transform item in m_UICreateSceneMode)
            {
                item.gameObject.SetActive(iShow);
            }
        }
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

        //服务器返回进入游戏消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_EnterGameReturn, OnEnterGameReturnCallback);

        //服务器返回删除角色消息
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_DeleteRoleReturn, OnDeleteRoleReturn);

        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_SelectRoleInfoReturn, OnSelectRoleInfoReturn);
        //注册开始游戏按钮点击事件
        m_uiSceneSelectRoleView.OnBtnBeginGameClick = OnBtnBeginGameClickEventHanlder;

        LogOnGameServer();

        //加载游戏职业角色
        LoadJobObject();
    }

    /// <summary>
    /// 服务器返回角色信息
    /// </summary>
    /// <param name="p"></param>
    private void OnSelectRoleInfoReturn(byte[] p)
    {
        RoleOperation_SelectRoleInfoReturnProto proto = RoleOperation_SelectRoleInfoReturnProto.GetProto(p);

        if(proto.IsSucess)
        {
            GlobalInit.Instance.MainPlayerInfo = new RoleInfoMainPlayer(proto);
            //需要跳转场景
            SceneMgr.Instance.LoadToCity();
        }
        else
        {

        }
    }

    /// <summary>
    /// 服务器返回删除角色的消息
    /// </summary>
    /// <param name="p"></param>
    private void OnDeleteRoleReturn(byte[] p)
    {
        RoleOperation_DeleteRoleReturnProto proto = RoleOperation_DeleteRoleReturnProto.GetProto(p);
        
        if(proto.IsSuccess)
        {
            //删除角色成功
            DeleteRole(m_currentRoleId);
            Debug.Log("删除角色成功");
        }
        else
        {
            UIMessageCtr.Instance.Show("提示", "删除角色失败");
        }
    }

    /// <summary>
    /// 删除当前选择的角色模型
    /// </summary>
    private void DeleteSelectRole()
    {
        if (m_SelectRoleContainer.childCount > 0)
        {
            DestroyImmediate(m_SelectRoleContainer.GetChild(0).gameObject);
        }
    }
    /// <summary>
    /// 删除本地角色
    /// </summary>
    /// <param name="roleId"></param>
    private void DeleteRole(int roleId)
    {
        DeleteSelectRole();
        for (int i = 0; i < m_roleList.Count; i++)
        {
            if(m_roleList[i].RoleId == roleId)
            {
                m_roleList.RemoveAt(i);
                break;
            }
        }

        m_uiSceneSelectRoleView.SetRoleList(m_roleList, OnSelectRoleCallback);

        //角色没有了需要切换到新建角色界面
        if (m_roleList.Count == 0)
        {
            m_bIsCreateRole = true;
            m_uiSceneSelectRoleView.SetUICreateRoleShow(true);
            CloneCreateRole();
            m_currentJobId = 1;
            m_uiSceneSelectRoleView.RandomRoleName();
        }
        else
        {
            SetSelectRole(m_roleList[0].RoleId);
        }
    }
    /// <summary>
    /// 已有角色 进入游戏  服务器返回信息
    /// </summary>
    /// <param name="p"></param>
    private void OnEnterGameReturnCallback(byte[] p)
    {
        RoleOperation_EnterGameReturnProto proto = RoleOperation_EnterGameReturnProto.GetProto(p);

        if (proto.IsSuccess)
        {
            //进入游戏成功
            AppDebug.Log("进入游戏");
        }
        else
        {
            UIMessageCtr.Instance.Show("提示", "创建角色失败?");
        }
    }

    public void OnDestroy()
    {
        //移除对服务器返回登录信息事件的监听
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);

        //取消服务器返回创建角色事件监听
        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, OnCreateRoleReturnEventHandler);

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_EnterGameReturn, OnEnterGameReturnCallback);

        //移除开始游戏按钮点击事件监听
        m_uiSceneSelectRoleView.OnBtnBeginGameClick -= OnBtnBeginGameClickEventHanlder;

        SocketDispatcher.Instance.RemoveEventListener(ProtoCodeDef.RoleOperation_DeleteRoleReturn, OnDeleteRoleReturn);

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

    //*****************开始游戏*******************************

    /// <summary>
    /// 标识是创建角色还是已有角色
    /// </summary>
    private bool m_bIsCreateRole = true;

    /// <summary>
    /// 点击开始游戏按钮事件处理
    /// </summary>
    private void OnBtnBeginGameClickEventHanlder()
    {
        //创建角色界面
        if (m_bIsCreateRole)
        {
            RoleOperation_CreateRoleProto proto = new RoleOperation_CreateRoleProto();

            proto.JobId = (byte)m_currentJobId;
            proto.RoleNickName = m_uiSceneSelectRoleView.RoleNameInputField.text;

            //角色昵称合法性检查
            if (string.IsNullOrEmpty(proto.RoleNickName))
            {
                UIMessageCtr.Instance.Show("提示", "请输入你的昵称");
                return;
            }

            //把创建的角色信息发送到服务端
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
        //选人界面
        else
        {
            //开始已有角色逻辑
            RoleOperation_EnterGameProto proto = new RoleOperation_EnterGameProto();
            proto.RoleId = this.m_currentRoleId;

            //把角色编号发送给服务器
            NetWorkSocket.Instance.SendMsg(proto.ToArray());
        }
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
    /// 从本地加载游戏职业镜像
    /// </summary>
    private void LoadJobObject()
    {
        m_jobEntityList = JobDBModel.GetInstance.GetDataList;

        for (int i = 0; i < m_jobEntityList.Count; i++)
        {
            GameObject @object = AssetBundleMgr.Instance.LoadAsset(string.Format("/Role/{0}.assetbundle", m_jobEntityList[i].PrefabName), m_jobEntityList[i].PrefabName);

            if (@object != null)
            {
                GlobalInit.Instance.m_JobObjectDic.Add(m_jobEntityList[i].Id, @object);
            }
        }
    }

    /*********************克隆角色相关操作************************/
    private List<GameObject> m_CloneRolePrefabList = new List<GameObject>();
        
    /// <summary>
    /// 克隆新建角色
    /// </summary>
    private void CloneCreateRole()
    {
        for (int i = 0; i < m_jobEntityList.Count; i++)
        {
            GameObject @object = Instantiate<GameObject>(GlobalInit.Instance.m_JobObjectDic[m_jobEntityList[i].Id]);

            if(m_CloneRolePrefabList.Contains(@object))
            {
                continue;
            }

            m_CloneRolePrefabList.Add(@object);

            @object.transform.SetParent(m_roleContainer[i]);
            @object.transform.localScale = Vector3.one;
            @object.transform.localPosition = Vector3.zero;
            @object.transform.localRotation = Quaternion.identity;

            RoleCtrl roleCtrl = @object.GetComponent<RoleCtrl>();

            if(roleCtrl != null && !m_jobRoleCtr.ContainsKey(m_jobEntityList[i].Id))
            {             
                m_jobRoleCtr.Add(m_jobEntityList[i].Id, roleCtrl);
            }
        }
    }

    /// <summary>
    /// 删除创建角色界面的职业克隆
    /// </summary>
    private void DeleteCreateRoleClone()
    {
        if(m_CloneRolePrefabList != null && m_CloneRolePrefabList.Count > 0)
        {
            foreach (GameObject item in m_CloneRolePrefabList)
            {
                Destroy(item);
            }
        }

        m_CloneRolePrefabList.Clear();
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
        if(proto.RoleList != null && proto.RoleList.Count > 0)
        {
            m_roleList = proto.RoleList;
        }
        //玩家还没创建过角色
        if(roleCount == 0)
        {
            m_bIsCreateRole = true;
            //新建角色  
            CloneCreateRole();
            //初始化的时候 职业id为1
            m_currentJobId = 1;

            //游戏一启动就随机一个名字
            m_uiSceneSelectRoleView.RandomRoleName();
        }
        else
        {
            m_bIsCreateRole = false;
            //玩家有角色了
            //隐藏创建角色相关UI
            m_uiSceneSelectRoleView.SetUICreateRoleShow(false);
            SetCreateRoleSceneModelShow(false);

            if (proto.RoleList != null && proto.RoleList.Count > 0)
            {
                m_uiSceneSelectRoleView.SetRoleList(proto.RoleList, OnSelectRoleCallback);
            }

            if(proto.RoleList.Count >= 1)
            {
                //默认第一次显示第一个角色
                SetSelectRole(m_roleList[0].RoleId);
            }
        }
    }

    /// <summary>
    /// 当选择已有角色回调处理
    /// </summary>
    /// <param name="obj"></param>
    private void OnSelectRoleCallback(int obj)
    {
        if(m_SelectRoleObj != null)
        {
            DestroyImmediate(m_SelectRoleObj);
            m_SelectRoleObj = null;
        }
        SetSelectRole(obj);
    }

    //---------------------------已有角色相关功能---------------

    /// <summary>
    /// 保存服务器返回的角色职业信息列表
    /// </summary>
    private List<RoleOperation_LogOnGameServerReturnProto.RoleItem> m_roleList;

    /// <summary>
    /// 已经选择的角色实例
    /// </summary>
    private GameObject m_SelectRoleObj = null;

    /// <summary>
    /// 选择的角色id
    /// </summary>
    private int m_currentRoleId = -1;


    /// <summary>
    /// 根据已有职业实例化对应预设
    /// </summary>
    /// <param name="roleId"></param>
    private void SetSelectRole(int roleId)
    {
        Debug.Log(roleId);
        this.m_currentRoleId = roleId;
        //职业id
        int jobId = -1;
        //通过roleId得到对应角色实体
        for (int i = 0; i < m_roleList.Count; i++)
        {
            if(m_roleList[i].RoleId == roleId)
            {
                jobId = m_roleList[i].RoleJob;
                break;
            }
        }
        if (jobId == -1) return;
        if (GlobalInit.Instance.m_JobObjectDic != null && GlobalInit.Instance.m_JobObjectDic.ContainsKey(jobId))
        {
            m_SelectRoleObj = Instantiate<GameObject>(GlobalInit.Instance.m_JobObjectDic[jobId]);
            m_SelectRoleObj.transform.SetParent(m_SelectRoleContainer);
            m_SelectRoleObj.transform.localPosition = Vector3.zero;
            m_SelectRoleObj.transform.localScale = Vector3.one;
            m_SelectRoleObj.transform.localRotation = Quaternion.identity;
        }
    }

    /*************删除角色***************************/  
    /// <summary>
    /// 当点击delete按钮的回调处理
    /// </summary>
    private void OnClickBtnCallback()
    {
        string currentSelectRoleNickName = string.Empty;

        for (int i = 0; i < m_roleList.Count; i++)
        {
            if(m_roleList[i].RoleId == m_currentRoleId)
            {
                currentSelectRoleNickName = m_roleList[i].RoleNickName;
                break;
            }
        }

        m_uiSceneSelectRoleView.OnClickDeleteButton(currentSelectRoleNickName, OnDeleteRoleComfirmButtonClickCallback);
        Debug.Log("删除角色开始");
    }

    /// <summary>
    /// 对删除角色确认按钮点击的回调
    /// </summary>
    private void OnDeleteRoleComfirmButtonClickCallback()
    {
        RoleOperation_DeleteRoleProto proto = new RoleOperation_DeleteRoleProto();
        proto.RoleId = m_currentRoleId;
        NetWorkSocket.Instance.SendMsg(proto.ToArray());
        Debug.Log("确定删除角色");
    }
}
