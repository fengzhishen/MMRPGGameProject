using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class UISceneSelectRoleView : UISceneViewBase
{
    //��׺����ͼ
    public UISelectRoleDragView m_UISelectRoleDragView;

    [HideInInspector]
    public UISelectRoleJobItemView[] m_uiSelectRoleJobItemViewList;

    public UISelectRoleJobDescView m_uISelectRoleJobDescView;

    /// <summary>
    /// ��ɫ�ǳ�
    /// </summary>
    [SerializeField]
    private InputField m_RoleNameInputField;

    /// <summary>
    /// �õ���ɫ�ǳ�
    /// </summary>
    public InputField RoleNameInputField
    {
        get
        {
            return m_RoleNameInputField;
        }
    }

    /// <summary>
    /// ��ʼ��Ϸ��ť���
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
    /// �������ɫ����
    /// </summary>
    public void RandomRoleName()
    {
        m_RoleNameInputField.text = GameUtil.RandomName();
    }
}
