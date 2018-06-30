using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UISelectRoleRoleItemView : MonoBehaviour
{
    /// <summary>
    /// ��ɫ���
    /// </summary>
    private int m_RoleId;

    /// <summary>
    /// ��ɫְҵ
    /// </summary>
    [SerializeField]
    private Text m_RoleJob;

    /// <summary>
    /// ��ɫ����
    /// </summary>
    [SerializeField]
    private Text m_RoleName;

    /// <summary>
    /// ��ɫͷ��
    /// </summary>
    [SerializeField]
    private Image m_RoleHeadImage;

    /// <summary>
    /// ��ɫ�ȼ�
    /// </summary>
    [SerializeField]
    private Text m_RoleLevel;

    /// <summary>
    /// ѡ�����н�ɫί��
    /// </summary>
    public Action<int> OnSelectRoleCallback;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnBtnClickCallback);
    }

    /// <summary>
    /// ���button�ص�����
    /// </summary>
    private void OnBtnClickCallback()
    {
        if(OnSelectRoleCallback != null)
        {
            OnSelectRoleCallback(m_RoleId);
        }
    }

    /// <summary>
    /// ����ְҵ��Ϣ
    /// </summary>
    /// <param name="roleName"></param>
    /// <param name="roleLevel"></param>
    /// <param name="roleId"></param>
    /// <param name="roleJobId"></param>
    /// <param name="headSprite"></param>
    public void SetUI(string roleName, int roleLevel, int roleId, int roleJobId, Sprite headSprite, Action<int> SelectRoleCallback)
    {
        this.OnSelectRoleCallback = SelectRoleCallback;
        this.m_RoleId = roleId;
        this.m_RoleJob.text = JobDBModel.GetInstance.GetEntityById(roleJobId).Name;
        this.m_RoleLevel.text = string.Format("LV {0}",roleLevel);
        this.m_RoleName.text = roleName;
        this.m_RoleHeadImage.sprite = headSprite;
    }
}
