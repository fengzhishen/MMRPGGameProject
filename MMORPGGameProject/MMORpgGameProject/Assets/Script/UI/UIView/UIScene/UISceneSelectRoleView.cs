using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UISceneSelectRoleView : UISceneViewBase
{
    //拖缀的视图
    public UISelectRoleDragView m_UISelectRoleDragView;

    [HideInInspector]
    public UISelectRoleJobItemView[] m_uiSelectRoleJobItemViewList;

    public UISelectRoleJobDescView m_uISelectRoleJobDescView;

    /// <summary>
    /// 角色昵称
    /// </summary>
    [SerializeField]
    private InputField m_RoleNameInputField;

    /// <summary>
    /// 得到角色昵称
    /// </summary>
    public InputField RoleNameInputField
    {
        get
        {
            return m_RoleNameInputField;
        }
    }

    /// <summary>
    /// 开始游戏按钮点击
    /// </summary>
    public Action OnBtnBeginGameClick;

    /// <summary>
    /// 创建角色相关UI
    /// </summary>
    public  List<Transform> m_uiCreateRole;

    /// <summary>
    /// 删除角色物体
    /// </summary>
    [SerializeField]
    private GameObject m_DeleteRoleObj;
    /// <summary>
    /// 新建角色物体
    /// </summary>
    [SerializeField]
    private GameObject m_CreateRoleObj;

    /// <summary>
    /// 删除角色button
    /// </summary>
    public Button m_DeleteButton;

    /// <summary>
    /// 已有角色职业信息item预设
    /// </summary>
    [SerializeField]
    private Transform m_RoleItemPrefab;

    /// <summary>
    /// 已有角色职业信息item预设容器
    /// </summary>
    [SerializeField]
    private Transform m_RoleListContainer;

    /// <summary>
    /// 角色头像精力
    /// </summary>
    [SerializeField]
    private Sprite[] m_RoleHeadSprite;

    public UISelectRoleDeleteRoleView deleteRoleView;

    /// <summary>
    /// 返回按钮被点击的委托
    /// </summary>
    public Action OnBtnReturnClick;

    /// <summary>
    /// 新建角色按钮点击后的委托
    /// </summary>
    public Action OnBtnCreateRoleClick;

    private void Awake()
    {
        m_uiSelectRoleJobItemViewList = GameObject.Find("Canvas/Container_LeftTop/JobItem").GetComponentsInChildren<UISelectRoleJobItemView>();
    }

    /// <summary>
    /// 点击删除button回调处理
    /// </summary>
    /// <param name="nickName"></param>
    /// <param name="OnClickBtnCallback"></param>
    public void OnClickDeleteButton(string nickName,Action OnClickBtnCallback)
    {
        deleteRoleView.Show(nickName, OnClickBtnCallback);
    }
    /// <summary>
    /// 设置创建角色相关UI的显示和隐藏
    /// </summary>
    /// <param name="iShow"></param>
    public void SetUICreateRoleShow(bool iShow = true)
    {
        if (m_uiCreateRole != null && m_uiCreateRole.Count > 0)
        {
            foreach (Transform item in m_uiCreateRole)
            {
                m_CreateRoleObj.SetActive(!iShow);
                m_DeleteRoleObj.SetActive(!iShow);
                item.gameObject.SetActive(iShow);           
            }
        }
    }

    private List<GameObject> RoleItemObj = new List<GameObject>();

     /// <summary>
    //销毁已有角色界面职业信息预设
    /// </summary>
    public void DestorySelectRoleItemInfo()
    {
        if (RoleItemObj.Count <= 0) return;

        for (int i = 0; i < RoleItemObj.Count; i++)
        {
            Destroy(RoleItemObj[i]);
        }

        RoleItemObj.Clear();
    }
    /// <summary>
    /// 设置已有角色职业信息列表
    /// </summary>
    /// <param name="roleItems"></param>
    public void SetRoleList(List<RoleOperation_LogOnGameServerReturnProto.RoleItem> roleItems,Action<int> selectRoleCallback)
    {
        if(RoleItemObj != null && RoleItemObj.Count > 0)
        {
            for (int i = 0; i < RoleItemObj.Count; i++)
            {
                DestroyImmediate(RoleItemObj[i]);
            }
            RoleItemObj.Clear();
        }

        if (roleItems != null && roleItems.Count > 0)
        {
            for(int i = 0; i < roleItems.Count; i++)
            {
                //开始克隆预设
                GameObject obj = Instantiate<GameObject>(m_RoleItemPrefab.gameObject);
                obj.transform.SetParent(m_RoleListContainer);
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localPosition -= new Vector3(0,80*i + 5,0);

                RoleItemObj.Add(obj);

                UISelectRoleRoleItemView roleItemView = obj.GetComponent<UISelectRoleRoleItemView>();
                roleItemView.SetUI(roleItems[i].RoleNickName, roleItems[i].RoleLevel, roleItems[i].RoleId, roleItems[i].RoleJob, m_RoleHeadSprite[roleItems[i].RoleJob - 1], selectRoleCallback);
            }
        }
    }
    protected override void OnBtnClick(GameObject go)
    {
        switch (go.name)
        {
            case "btnRandomName":
                RandomRoleName();
                break;
            case "btnBeginGame":
               if((OnBtnBeginGameClick == null ? 0 : 1) == 1)
                {
                    OnBtnBeginGameClick.Invoke();
                }
                break;
            case "btnReturn":
                if(OnBtnReturnClick != null)
                {
                    OnBtnReturnClick();
                }
                break;
            case "btnCreateRole":
                if(OnBtnCreateRoleClick != null)
                {
                    OnBtnCreateRoleClick();
                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 随机出角色名字
    /// </summary>
    public void RandomRoleName()
    {
        m_RoleNameInputField.text = GameUtil.RandomName();
    }
}
