using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UISelectRoleRoleItemView : MonoBehaviour
{
    /// <summary>
    /// 角色编号
    /// </summary>
    private int m_RoleId;

    /// <summary>
    /// 角色职业
    /// </summary>
    [SerializeField]
    private Text m_RoleJob;

    /// <summary>
    /// 角色名字
    /// </summary>
    [SerializeField]
    private Text m_RoleName;

    /// <summary>
    /// 角色头像
    /// </summary>
    [SerializeField]
    private Image m_RoleHeadImage;

    /// <summary>
    /// 角色等级
    /// </summary>
    [SerializeField]
    private Text m_RoleLevel;

    /// <summary>
    /// 选择已有角色委托
    /// </summary>
    public Action<int> OnSelectRoleCallback;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnBtnClickCallback);
    }

    /// <summary>
    /// 点击button回调处理
    /// </summary>
    private void OnBtnClickCallback()
    {
        if(OnSelectRoleCallback != null)
        {
            OnSelectRoleCallback(m_RoleId);
        }
    }

    /// <summary>
    /// 设置职业信息
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
