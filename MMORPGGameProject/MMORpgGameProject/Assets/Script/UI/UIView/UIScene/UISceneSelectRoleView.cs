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

    private void Awake()
    {
        m_uiSelectRoleJobItemViewList = GameObject.Find("Canvas/Container_LeftTop/JobItem").GetComponentsInChildren<UISelectRoleJobItemView>();
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
