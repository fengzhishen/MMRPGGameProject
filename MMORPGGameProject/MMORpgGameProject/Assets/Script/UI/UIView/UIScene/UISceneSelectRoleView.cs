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

    /// <summary>
    /// ������ɫ���UI
    /// </summary>
    public  List<Transform> m_uiCreateRole;

    /// <summary>
    /// ɾ����ɫ����
    /// </summary>
    [SerializeField]
    private GameObject m_DeleteRoleObj;
    /// <summary>
    /// �½���ɫ����
    /// </summary>
    [SerializeField]
    private GameObject m_CreateRoleObj;

    /// <summary>
    /// ɾ����ɫbutton
    /// </summary>
    public Button m_DeleteButton;

    /// <summary>
    /// ���н�ɫְҵ��ϢitemԤ��
    /// </summary>
    [SerializeField]
    private Transform m_RoleItemPrefab;

    /// <summary>
    /// ���н�ɫְҵ��ϢitemԤ������
    /// </summary>
    [SerializeField]
    private Transform m_RoleListContainer;

    /// <summary>
    /// ��ɫͷ����
    /// </summary>
    [SerializeField]
    private Sprite[] m_RoleHeadSprite;

    public UISelectRoleDeleteRoleView deleteRoleView;

    /// <summary>
    /// ���ذ�ť�������ί��
    /// </summary>
    public Action OnBtnReturnClick;

    /// <summary>
    /// �½���ɫ��ť������ί��
    /// </summary>
    public Action OnBtnCreateRoleClick;

    private void Awake()
    {
        m_uiSelectRoleJobItemViewList = GameObject.Find("Canvas/Container_LeftTop/JobItem").GetComponentsInChildren<UISelectRoleJobItemView>();
    }

    /// <summary>
    /// ���ɾ��button�ص�����
    /// </summary>
    /// <param name="nickName"></param>
    /// <param name="OnClickBtnCallback"></param>
    public void OnClickDeleteButton(string nickName,Action OnClickBtnCallback)
    {
        deleteRoleView.Show(nickName, OnClickBtnCallback);
    }
    /// <summary>
    /// ���ô�����ɫ���UI����ʾ������
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
    //�������н�ɫ����ְҵ��ϢԤ��
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
    /// �������н�ɫְҵ��Ϣ�б�
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
                //��ʼ��¡Ԥ��
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
    /// �������ɫ����
    /// </summary>
    public void RandomRoleName()
    {
        m_RoleNameInputField.text = GameUtil.RandomName();
    }
}
