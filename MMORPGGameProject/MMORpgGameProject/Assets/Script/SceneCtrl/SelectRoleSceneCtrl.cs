using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelectRoleSceneCtrl : MonoBehaviour
{
    private UISceneSelectRoleView m_UISceneSelectRoleView;
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

    private void Awake()
    {
        m_UISceneSelectRoleView = UISceneCtr.Instance.LoadSceneUI(UISceneCtr.SceneUIType.SelectRole).GetComponent<UISceneSelectRoleView>();

        m_UISceneSelectRoleView.m_UISelectRoleDragView.m_OnSelectRoleDrag = OnSelectRoleDragCallback;
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

        //监听协议
        SocketDispatcher.Instance.AddEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, OnLogOnGameServerReturn);
        LogOnGameServer();
        //加载游戏职业角色
        LoadJobObject();
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
        }
        else
        {

        }
    }
}
